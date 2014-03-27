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
		UIViewController masterProfileMenu, detailProfileMenu;

		public ProfileMenu () : base ()
		{
		}
			
		string name;
		object returnFirst;
		List<string> ProfileNames = new List<string> ();
		string[] items;
		TableSource itemstable;
		MainMenu mainMenu;

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			masterProfileMenu = new MasterViewController ();
			detailProfileMenu = new DetailViewController ();

			vwDetail.Add (detailProfileMenu.View);
			vwMaster.Add (masterProfileMenu.View);

			vwDetail.Hidden = true;

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
