using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Zuma.Sparrow
{
	public partial class MainMenuViewController : UIViewController
	{
		public MainMenuViewController() : base("MainMenuViewController", null)
		{
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.LandscapeLeft | UIInterfaceOrientationMask.LandscapeRight;
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

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			btnStart.TouchUpInside += OnStartTouch;
			btnProfileMenu.TouchUpInside += OnProfileMenu;
		}

		private void OnStartTouch(object sender, EventArgs e)
		{
			// NOTE: we're setting the profile here for the moment, but later, this will be done by
			// the user in a seperate view.
			var navigationController = (NavigationController) NavigationController;
			var catalog = new ChoiceProfileCatalog();

			if (navigationController.CurrentProfile == null)
			{
				navigationController.CurrentProfile = catalog.Find("Ja/Nee");
			}

			var choiceViewController = new OneButtonViewController();
			NavigationController.PushViewController(choiceViewController, true);
		}

		private void OnProfileMenu(object sender, EventArgs e)
		{
			var profileMenu = new ProfileMenuViewController();
			NavigationController.PushViewController(profileMenu, true);
		}
	}
}