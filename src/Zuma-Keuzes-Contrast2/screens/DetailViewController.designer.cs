// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace ZumaKeuzesContrast2
{
	[Register ("DetailViewController")]
	partial class DetailViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView btnRight { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSaveProfile { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSetLeftImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSetLeftSnd { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSetRightImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSetRightSnd { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imvHidEverything { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imvLeft { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imvRight { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView vwHidden { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnRight != null) {
				btnRight.Dispose ();
				btnRight = null;
			}

			if (btnSaveProfile != null) {
				btnSaveProfile.Dispose ();
				btnSaveProfile = null;
			}

			if (btnSetLeftImage != null) {
				btnSetLeftImage.Dispose ();
				btnSetLeftImage = null;
			}

			if (btnSetLeftSnd != null) {
				btnSetLeftSnd.Dispose ();
				btnSetLeftSnd = null;
			}

			if (btnSetRightImage != null) {
				btnSetRightImage.Dispose ();
				btnSetRightImage = null;
			}

			if (btnSetRightSnd != null) {
				btnSetRightSnd.Dispose ();
				btnSetRightSnd = null;
			}

			if (imvHidEverything != null) {
				imvHidEverything.Dispose ();
				imvHidEverything = null;
			}

			if (imvLeft != null) {
				imvLeft.Dispose ();
				imvLeft = null;
			}

			if (imvRight != null) {
				imvRight.Dispose ();
				imvRight = null;
			}

			if (vwHidden != null) {
				vwHidden.Dispose ();
				vwHidden = null;
			}
		}
	}
}
