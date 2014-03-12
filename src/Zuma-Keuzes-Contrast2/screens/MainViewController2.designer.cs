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
	[Register ("MainViewController2")]
	partial class MainViewController2
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnTrigger { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imvDisplayChoice { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnTrigger != null) {
				btnTrigger.Dispose ();
				btnTrigger = null;
			}

			if (imvDisplayChoice != null) {
				imvDisplayChoice.Dispose ();
				imvDisplayChoice = null;
			}
		}
	}
}
