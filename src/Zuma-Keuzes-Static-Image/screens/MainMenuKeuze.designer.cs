// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace ZumaKeuzesStaticImage
{
	[Register ("MainMenuKeuze")]
	partial class MainMenuKeuze
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnStart { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISegmentedControl scChoice { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISegmentedControl scSingleChoiceOptions { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (scChoice != null) {
				scChoice.Dispose ();
				scChoice = null;
			}

			if (scSingleChoiceOptions != null) {
				scSingleChoiceOptions.Dispose ();
				scSingleChoiceOptions = null;
			}

			if (btnStart != null) {
				btnStart.Dispose ();
				btnStart = null;
			}
		}
	}
}
