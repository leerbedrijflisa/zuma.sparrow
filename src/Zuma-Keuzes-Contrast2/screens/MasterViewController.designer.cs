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
	[Register ("MasterViewController")]
	partial class MasterViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView tblProfileList { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tblProfileList != null) {
				tblProfileList.Dispose ();
				tblProfileList = null;
			}
		}
	}
}
