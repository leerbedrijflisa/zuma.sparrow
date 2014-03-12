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
	[Register ("MainViewController3")]
	partial class MainViewController3
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnChoice { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imvDisplayChoice { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnChoice != null) {
				btnChoice.Dispose ();
				btnChoice = null;
			}

			if (imvDisplayChoice != null) {
				imvDisplayChoice.Dispose ();
				imvDisplayChoice = null;
			}
		}
	}
}
