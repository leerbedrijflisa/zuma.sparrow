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
	[Register ("TestViewController")]
	partial class TestViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIView vwView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (vwView != null) {
				vwView.Dispose ();
				vwView = null;
			}
		}
	}
}
