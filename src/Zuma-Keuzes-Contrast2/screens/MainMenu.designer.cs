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
	[Register ("MainMenu")]
	partial class MainMenu
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnAdd { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnChoiceProfile { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIStepper btnClickTimer { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIStepper btnDarkTimer { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnGo { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSubtract { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblDarkTimer { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblProfile { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LblTimer { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISegmentedControl scChoice { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISegmentedControl scSingleChoiceOptions { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel txtLblDarkTimer { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAdd != null) {
				btnAdd.Dispose ();
				btnAdd = null;
			}

			if (btnChoiceProfile != null) {
				btnChoiceProfile.Dispose ();
				btnChoiceProfile = null;
			}

			if (btnClickTimer != null) {
				btnClickTimer.Dispose ();
				btnClickTimer = null;
			}

			if (btnDarkTimer != null) {
				btnDarkTimer.Dispose ();
				btnDarkTimer = null;
			}

			if (btnGo != null) {
				btnGo.Dispose ();
				btnGo = null;
			}

			if (btnSubtract != null) {
				btnSubtract.Dispose ();
				btnSubtract = null;
			}

			if (lblDarkTimer != null) {
				lblDarkTimer.Dispose ();
				lblDarkTimer = null;
			}

			if (lblProfile != null) {
				lblProfile.Dispose ();
				lblProfile = null;
			}

			if (LblTimer != null) {
				LblTimer.Dispose ();
				LblTimer = null;
			}

			if (scChoice != null) {
				scChoice.Dispose ();
				scChoice = null;
			}

			if (scSingleChoiceOptions != null) {
				scSingleChoiceOptions.Dispose ();
				scSingleChoiceOptions = null;
			}

			if (txtLblDarkTimer != null) {
				txtLblDarkTimer.Dispose ();
				txtLblDarkTimer = null;
			}
		}
	}
}
