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
		MonoTouch.UIKit.UITableView lisProfiles { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lisProfiles != null) {
				lisProfiles.Dispose ();
				lisProfiles = null;
			}
		}
	}
}
