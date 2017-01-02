using System;
using UIKit;

namespace iOS
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {

        }

        partial void OnAdd(UIButton sender)
        {
            if (!string.IsNullOrEmpty(label.Text))
            {
                label.Text += "+";
            }
        }

        partial void OnNumber(UIButton sender)
        {
            if (string.IsNullOrEmpty(label.Text) || label.Text == "0")
            {
                label.Text = sender.CurrentTitle;
            }
            else
            {
                label.Text += sender.CurrentTitle;
            }
        }

        partial void OnEquals(UIButton sender)
        {
            string[] split = label.Text.Split('+');
            int sum = 0;

            foreach (string text in split)
            {
                int x;
                if (int.TryParse(text, out x))
                    sum += x;
            }

            label.Text = sum.ToString();
        }
    }
}

