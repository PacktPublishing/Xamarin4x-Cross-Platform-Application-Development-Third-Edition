// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton add { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel label { get; set; }

        [Action ("OnNumber:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnNumber (UIKit.UIButton sender);

        [Action ("OnAdd:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnAdd (UIKit.UIButton sender);

        [Action ("OnEquals:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnEquals (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (add != null) {
                add.Dispose ();
                add = null;
            }

            if (label != null) {
                label.Dispose ();
                label = null;
            }
        }
    }
}