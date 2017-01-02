using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Xamarin.Media;

namespace XamSnap.Droid
{
    [Activity(Label = "Messages")]
    public class MessagesActivity : BaseActivity<MessageViewModel>
    {
        ListView listView;
        EditText messageText;
        Button sendButton;
        ImageButton photoButton;
        Adapter adapter;
        MediaPicker picker;
        bool choosingPhoto;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Title = viewModel.Conversation.UserName;
            SetContentView(Resource.Layout.Messages);
            listView = FindViewById<ListView>(Resource.Id.messageList);
            messageText = FindViewById<EditText>(Resource.Id.messageText);
            sendButton = FindViewById<Button>(Resource.Id.sendButton);
            photoButton = FindViewById<ImageButton>(Resource.Id.photoButton);

            picker = new MediaPicker(this);

            listView.Adapter =
              adapter = new Adapter(this);

            sendButton.Click += (sender, e) => Send();

            photoButton.Click += (sender, e) =>
            {
                var dialog = new AlertDialog.Builder(this)
                    .SetTitle("Choose photo?")
                    .SetPositiveButton("Take Photo", OnTakePhoto)
                    .SetNegativeButton("Photo Library", OnChoosePhoto)
                    .SetNeutralButton("Cancel", delegate { })
                    .Create();
                dialog.Show();
            };
        }

        async void Send()
        {
            viewModel.Text = messageText.Text;
            try
            {
                await viewModel.SendMessage();
                messageText.Text = string.Empty;
                adapter.NotifyDataSetInvalidated();
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }

        void OnTakePhoto(object sender, EventArgs e)
        {
            var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions());
            choosingPhoto = true;
            StartActivityForResult(intent, 1);
        }

        void OnChoosePhoto(object sender, EventArgs e)
        {
            var intent = picker.GetPickPhotoUI();
            choosingPhoto = true;
            StartActivityForResult(intent, 1);
        }

        protected async override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok)
            {
                var file = await data.GetMediaFileExtraAsync(this);
                viewModel.Image = file.Path;
                Send();
            }
        }

        protected async override void OnResume()
        {
            base.OnResume();

            if (choosingPhoto)
            {
                choosingPhoto = false;
                return;
            }

            try
            {
                await viewModel.GetMessages();
                adapter.NotifyDataSetInvalidated();
                listView.SetSelection(adapter.Count);
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }

        class Adapter : BaseAdapter<Message>
        {
            readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
            readonly ISettings settings = ServiceContainer.Resolve<ISettings>();
            readonly LayoutInflater inflater;
            const int MyMessageType = 0, TheirMessageType = 1;

            public Adapter(Context context)
            {
                inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override int Count
            {
                get
                {
                    return messageViewModel.Messages == null ? 0 : messageViewModel.Messages.Length;
                }
            }

            public override Message this[int position]
            {
                get { return messageViewModel.Messages[position]; }
            }

            public override int ViewTypeCount
            {
                get { return 2; }
            }

            public override int GetItemViewType(int position)
            {
                var message = this[position];
                return message.UserName == settings.User.Name ? MyMessageType : TheirMessageType;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var message = this[position];
                int type = GetItemViewType(position);
                if (convertView == null)
                {
                    if (type == MyMessageType)
                    {
                        convertView = inflater.Inflate(Resource.Layout.MyMessageListItem, null);
                    }
                    else
                    {
                        convertView = inflater.Inflate(Resource.Layout.TheirMessageListItem, null);
                    }
                }
                TextView messageText;
                ImageView image;
                if (type == MyMessageType)
                {
                    messageText = convertView.FindViewById<TextView>(Resource.Id.myMessageText);
                    image = convertView.FindViewById<ImageView>(Resource.Id.myMessageImage);
                }
                else
                {
                    messageText = convertView.FindViewById<TextView>(Resource.Id.theirMessageText);
                    image = convertView.FindViewById<ImageView>(Resource.Id.theirMessageImage);
                }
                messageText.Text = message.Text ?? string.Empty;
                image.SetImageBitmap(string.IsNullOrEmpty(message.Image) ?
                    null : BitmapFactory.DecodeFile(message.Image));
                return convertView;
            }

        }
    }
}

