using System;
using Android.App;
using Android.Widget;
using Android.OS;

namespace Droid
{
    [Activity(Label = "Calculator", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        TextView text; 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            text = FindViewById<TextView>(Resource.Id.text);

            var button = FindViewById<Button>(Resource.Id.number1);
            button.Click += OnNumber;

            button = FindViewById<Button>(Resource.Id.number2);
            button.Click += OnNumber;

            button = FindViewById<Button>(Resource.Id.number3);
            button.Click += OnNumber;

            button = FindViewById<Button>(Resource.Id.number4);
            button.Click += OnNumber;

            button = FindViewById<Button>(Resource.Id.number5);
            button.Click += OnNumber;

            button = FindViewById<Button>(Resource.Id.number6);
            button.Click += OnNumber;

            button = FindViewById<Button>(Resource.Id.number7);
            button.Click += OnNumber;

            button = FindViewById<Button>(Resource.Id.number8);
            button.Click += OnNumber;

            button = FindViewById<Button>(Resource.Id.number9);
            button.Click += OnNumber;

            button = FindViewById<Button>(Resource.Id.number0);
            button.Click += OnNumber;

            var add = FindViewById<Button>(Resource.Id.add);
            add.Click += OnAdd;

            var equals = FindViewById<Button>(Resource.Id.equals);
            equals.Click += OnEquals;
        }

        private void OnNumber(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (string.IsNullOrEmpty(text.Text) || text.Text == "0")
            {
                text.Text = button.Text;
            }
            else
            {
                text.Text += button.Text;
            }
        }

        private void OnAdd(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(text.Text))
            {
                text.Text += "+";
            }
        }

        private void OnEquals(object sender, EventArgs e)
        {
            string[] split = text.Text.Split('+');
            int sum = 0;

            foreach (string text in split)
            {
                int x;
                if (int.TryParse(text, out x))
                    sum += x;
            }

            text.Text = sum.ToString();
        }
    }
}


