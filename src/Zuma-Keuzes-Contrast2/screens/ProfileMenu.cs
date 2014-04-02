using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Mono.Data.Sqlite;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace ZumaKeuzesContrast2
{
	public partial class ProfileMenu : UIViewController
	{
		UIViewController masterProfileMenu;
		DetailViewController detailProfileMenu;
//		QueryProfile queryProfile;

		public ProfileMenu () : base ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

//			queryProfile = new QueryProfile ();
			detailProfileMenu = new DetailViewController ();
			masterProfileMenu = new MasterViewController (detailProfileMenu);

			vwDetail.Add (detailProfileMenu.View);
			vwMaster.Add (masterProfileMenu.View);

		}

		public override void ViewWillAppear (bool animated) {
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, animated);
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			this.NavigationController.SetNavigationBarHidden (false, animated);
		}

		public override bool PrefersStatusBarHidden ()
		{
			return true;
		}

	}
}
