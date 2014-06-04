// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Zuma.Sparrow
{
	[Register ("OneButtonViewController")]
	partial class OneButtonViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView imgLandscapeLeft { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imgLandscapeRight { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imgPortraitDown { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imgPortraitUp { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView viewLandscape { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView viewPortrait { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgPortraitDown != null) {
				imgPortraitDown.Dispose ();
				imgPortraitDown = null;
			}

			if (imgPortraitUp != null) {
				imgPortraitUp.Dispose ();
				imgPortraitUp = null;
			}

			if (viewPortrait != null) {
				viewPortrait.Dispose ();
				viewPortrait = null;
			}

			if (imgLandscapeLeft != null) {
				imgLandscapeLeft.Dispose ();
				imgLandscapeLeft = null;
			}

			if (imgLandscapeRight != null) {
				imgLandscapeRight.Dispose ();
				imgLandscapeRight = null;
			}

			if (viewLandscape != null) {
				viewLandscape.Dispose ();
				viewLandscape = null;
			}
		}
	}
}
