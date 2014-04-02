using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Mono.Data.Sqlite;
using System.IO;
using System.Text;
using System.Data;

namespace ZumaKeuzesContrast2
{
	public partial class MainMenu : UIViewController
	{
		public MainMenu (QueryProfile queryProfile) : base ()
		{
			this.queryProfile = queryProfile;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.LandscapeLeft | UIInterfaceOrientationMask.LandscapeRight;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//Create's a db if there isn't one already with a table to handle the Menu segement button options
			DatabaseRequests.CreateDatabase ();
			DatabaseRequests.CreateDefaultProfiles ();

			btnChoiceProfile.TouchUpInside += PushProfileMenu;

			InitializeTimerbtn ();

			btnClickTimer.TouchUpInside += ClickTimer;

			btnDarkTimer.TouchUpInside += DarkTimer;

			btnGo.TouchUpInside += PushMainMenu;

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

		private void InitializeTimerbtn()
		{
			btnClickTimer.MinimumValue = 1;
			btnDarkTimer.MinimumValue = 1;
			btnClickTimer.Value = 5;
			btnDarkTimer.Value = 5;
		}

		private void PushProfileMenu(object sender, EventArgs args) 
		{
			if (profileMenu == null) {
				profileMenu = new ProfileMenu ();
			}

			NavigationController.PushViewController (profileMenu, false);
		}

		private void PushMainMenu(object sender, EventArgs args)
		{
			Console.WriteLine (clickTimer.ToString () + " click");
			Console.WriteLine (darkTimer.ToString () + " dark");

			int segmetDifficultyLevel = scChoice.SelectedSegment;
			int segmetType = scSingleChoiceOptions.SelectedSegment;

			string temp = "temp";

			DatabaseRequests.StoreMenuSettings(segmetDifficultyLevel, segmetType, clickTimer, darkTimer, temp);
			if(viewController == null)
			{
				viewController = new MainViewController4();
			}

			NavigationController.PushViewController(viewController, false);
		}

		private void ClickTimer (object sender, EventArgs args)
		{
			LblTimer.Text = btnClickTimer.Value.ToString();
			int clickTimerValue = Convert.ToInt32 (btnClickTimer.Value);
			clickTimer = clickTimerValue;

		}

		private void DarkTimer (object sender, EventArgs args)
		{
			lblDarkTimer.Text = btnDarkTimer.Value.ToString();
			int darkTimerValue = Convert.ToInt32 (btnDarkTimer.Value);
			darkTimer = darkTimerValue;

		}

		private QueryProfile queryProfile;
		private MainViewController4 viewController;
		private ProfileMenu profileMenu;
		private int clickTimer = 5, darkTimer = 5;
	}
}