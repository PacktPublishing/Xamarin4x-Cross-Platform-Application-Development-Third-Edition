using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace XamSnap.iOS
{
    public partial class MessagesController : UITableViewController
    {
        readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
        UIToolbar toolbar;
        UITextField message;
        UIBarButtonItem send;

        public MessagesController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Text Field
            message = new UITextField(new CGRect(0, 0, TableView.Frame.Width - 88, 32))
            {
                BorderStyle = UITextBorderStyle.RoundedRect,
                ReturnKeyType = UIReturnKeyType.Send,
                ShouldReturn = _ =>
                {
                    Send();
                    return false;
                },
            };

            //Bar button item
            send = new UIBarButtonItem("Send", UIBarButtonItemStyle.Plain, (sender, e) => Send());

            //Toolbar
            toolbar = new UIToolbar(new CGRect(0, TableView.Frame.Height - 44, TableView.Frame.Width, 44));
            toolbar.Items = new[]
            {
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

        void OnIsBusyChanged(object sender, EventArgs e)
        {
            message.Enabled =
              send.Enabled = !messageViewModel.IsBusy;
        }

        async void Send()
        {
            //Just hide the keyboard if they didn't type anything
            if (string.IsNullOrEmpty(message.Text))
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
                cell.TextLabel.Text = message.Text;
                return cell;
            }
        }
    }
}