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
		MainMenu mainMenu;
	
		public ProfileMenu () : base ()
		{
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.LandscapeLeft | UIInterfaceOrientationMask.LandscapeRight;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			detailProfileMenu = new DetailViewController (this.NavigationController);
			masterProfileMenu = new MasterViewController (detailProfileMenu);

			vwDetail.Add (detailProfileMenu.View);
			vwMaster.Add (masterProfileMenu.View);

			btnPushMainMenu.TouchUpInside += PushMainMenu;

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

		private void PushMainMenu(object sender, EventArgs e)
		{
			if (mainMenu == null) 
			{
				mainMenu = new MainMenu ();
			}

			NavigationController.PushViewController (mainMenu, false);
		}

	}
}
