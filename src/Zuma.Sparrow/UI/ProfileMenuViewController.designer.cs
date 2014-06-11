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
	[Register ("ProfileMenuViewController")]
	partial class ProfileMenuViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel lblProfileName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView tblProfiles { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView viewDetail { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tblProfiles != null) {
				tblProfiles.Dispose ();
				tblProfiles = null;
			}

			if (viewDetail != null) {
				viewDetail.Dispose ();
				viewDetail = null;
			}

			if (lblProfileName != null) {
				lblProfileName.Dispose ();
				lblProfileName = null;
			}
		}
	}
}
