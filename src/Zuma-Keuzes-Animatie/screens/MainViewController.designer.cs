// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace ZumaKeuzesAnimatie
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnTrigger { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imvDrinkingCup { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imvDrinkingCup != null) {
				imvDrinkingCup.Dispose ();
				imvDrinkingCup = null;
			}

			if (btnTrigger != null) {
				btnTrigger.Dispose ();
				btnTrigger = null;
			}
		}
	}
}
