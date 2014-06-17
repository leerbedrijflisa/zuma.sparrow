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
		MonoTouch.UIKit.UIButton btnChoiceProfile { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnCreateProfile { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnPlaySndLeft { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnPlaySndRight { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imvLeft { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imvRight { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblProfileName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView tblProfiles { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView viewDetail { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnChoiceProfile != null) {
				btnChoiceProfile.Dispose ();
				btnChoiceProfile = null;
			}

			if (btnPlaySndLeft != null) {
				btnPlaySndLeft.Dispose ();
				btnPlaySndLeft = null;
			}

			if (btnPlaySndRight != null) {
				btnPlaySndRight.Dispose ();
				btnPlaySndRight = null;
			}

			if (imvLeft != null) {
				imvLeft.Dispose ();
				imvLeft = null;
			}

			if (imvRight != null) {
				imvRight.Dispose ();
				imvRight = null;
			}

			if (lblProfileName != null) {
				lblProfileName.Dispose ();
				lblProfileName = null;
			}

			if (tblProfiles != null) {
				tblProfiles.Dispose ();
				tblProfiles = null;
			}

			if (viewDetail != null) {
				viewDetail.Dispose ();
				viewDetail = null;
			}

			if (btnCreateProfile != null) {
				btnCreateProfile.Dispose ();
				btnCreateProfile = null;
			}
		}
	}
}
