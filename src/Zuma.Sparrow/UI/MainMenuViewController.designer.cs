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
	[Register ("MainMenuViewController")]
	partial class MainMenuViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnProfileMenu { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnStart { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnStart != null) {
				btnStart.Dispose ();
				btnStart = null;
			}

			if (btnProfileMenu != null) {
				btnProfileMenu.Dispose ();
				btnProfileMenu = null;
			}
		}
	}
}
