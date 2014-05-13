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
	[Register ("ProfileMenu")]
	partial class ProfileMenu
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnPushMainMenu { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnRefresh { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView lisProfiles { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView vwDetail { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView vwDetial { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView vwMaster { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnPushMainMenu != null) {
				btnPushMainMenu.Dispose ();
				btnPushMainMenu = null;
			}

			if (lisProfiles != null) {
				lisProfiles.Dispose ();
				lisProfiles = null;
			}

			if (vwDetail != null) {
				vwDetail.Dispose ();
				vwDetail = null;
			}

			if (vwDetial != null) {
				vwDetial.Dispose ();
				vwDetial = null;
			}

			if (vwMaster != null) {
				vwMaster.Dispose ();
				vwMaster = null;
			}

			if (btnRefresh != null) {
				btnRefresh.Dispose ();
				btnRefresh = null;
			}
		}
	}
}
