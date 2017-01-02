using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Media;

namespace XamSnap.iOS
{
    public partial class MessagesController : UITableViewController
    {
        readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
        UIToolbar toolbar;
        UITextField message;
        UIBarButtonItem send, photo;
        MediaPicker picker;

        public MessagesController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Media Picker
            picker = new MediaPicker();

            //Text Field
            message = new UITextField(new CGRect(0, 0, TableView.Frame.Width - 120, 32))
            {
                BorderStyle = UITextBorderStyle.RoundedRect,
                ReturnKeyType = UIReturnKeyType.Send,
                ShouldReturn = _ =>
                {
                    Send();
                    return false;
                },
            };

            //Send button
            send = new UIBarButtonItem("Send", UIBarButtonItemStyle.Plain, (sender, e) => Send());

            //Photo button
            photo = new UIBarButtonItem(UIBarButtonSystemItem.Camera, (sender, e) =>
            {
                //In case the keyboard is up
                message.ResignFirstResponder();

                var actionSheet = new UIActionSheet("Choose photo?");
                actionSheet.AddButton("Take Photo");
                actionSheet.AddButton("Photo Library");
                actionSheet.AddButton("Cancel");
                actionSheet.Clicked += OnActionSheetClicked;
                actionSheet.CancelButtonIndex = 2;
                actionSheet.ShowFrom(photo, true);
            });

            //Toolbar
            toolbar = new UIToolbar(new CGRect(0, 0, TableView.Frame.Width, 44));
            toolbar.Items = new[]
            {
                photo,
                new UIBarButtonItem(message),
                send
            };

            TableView.Source = new TableSource();
            TableView.TableFooterView = toolbar;
        }

        public async override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            Title = messageViewModel.Conversation.UserName;

            //IsBusy
            messageViewModel.IsBusyChanged += OnIsBusyChanged;

            try
            {
                //This means we were choosing a photo
                if (PresentedViewController != null)
                    return;

                await messageViewModel.GetMessages();
                TableView.ReloadData();
                message.BecomeFirstResponder();
            }
            catch (Exception exc)
            {
                new UIAlertView("Oops!", exc.Message, null, "Ok").Show();
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            //IsBusy
            messageViewModel.IsBusyChanged -= OnIsBusyChanged;
        }

        async void OnActionSheetClicked(object sender, UIButtonEventArgs e)
        {
            MediaPickerController controller = null;
            try
            {
                if (e.ButtonIndex == 0)
                {
                    if (!picker.IsCameraAvailable)
                    {
                        new UIAlertView("Oops!", "Sorry, camera not supported on this device!", null, "Ok").Show();
                        return;
                    }

                    controller = picker.GetTakePhotoUI(new StoreCameraMediaOptions());
                    PresentViewController(controller, true, null);

                    var file = await controller.GetResultAsync();
                    messageViewModel.Image = file.Path;

                    Send();
                }
                else if (e.ButtonIndex == 1)
                {
                    controller = picker.GetPickPhotoUI();
                    PresentViewController(controller, true, null);

                    var file = await controller.GetResultAsync();
                    messageViewModel.Image = file.Path;

                    Send();
                }
            }
            catch (TaskCanceledException)
            {
                //Means the user just cancelled
            }
            finally
            {
                controller?.DismissViewController(true, null);
            }
        }

        void OnIsBusyChanged(object sender, EventArgs e)
        {
            message.Enabled =
              send.Enabled = !messageViewModel.IsBusy;
        }

        async void Send()
        {
            //Just hide the keyboard if they didn't type anything
            if (string.IsNullOrEmpty(message.Text) && string.IsNullOrEmpty(messageViewModel.Image))
            {
                message.ResignFirstResponder();
                return;
            }

            //Set the text, send the message
            messageViewModel.Text = message.Text;
            await messageViewModel.SendMessage();

            //Clear the text field & view model
            message.Text =
                messageViewModel.Text = string.Empty;

            //Reload the table
            TableView.InsertRows(new[] { NSIndexPath.FromRowSection(messageViewModel.Messages.Length - 1, 0) }, 
                UITableViewRowAnimation.Automatic);
        }

        class TableSource : UITableViewSource
        {
            const string MyCellName = "MyCell";
            const string TheirCellName = "TheirCell";

            readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
            readonly ISettings settings = ServiceContainer.Resolve<ISettings>();

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return messageViewModel.Messages == null ? 0 : messageViewModel.Messages.Length;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var message = messageViewModel.Messages[indexPath.Row];
                bool isMyMessage = message.UserName == settings.User.Name;
                var cell = tableView.DequeueReusableCell(isMyMessage ? MyCellName : TheirCellName);
                cell.TextLabel.Text = message.Text ?? string.Empty;
                cell.ImageView.Image = string.IsNullOrEmpty(message.Image) ? 
                    null : UIImage.FromFile(message.Image);
                return cell;
            }
        }
    }
}