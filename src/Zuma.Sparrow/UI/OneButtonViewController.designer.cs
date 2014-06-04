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
		MonoTouch.UIKit.UIImageView imgLeft { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imgRight { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgLeft != null) {
				imgLeft.Dispose ();
				imgLeft = null;
			}

			if (imgRight != null) {
				imgRight.Dispose ();
				imgRight = null;
			}
		}
	}
}
