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
		public MainMenu () : base ()
		{
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

			InitializeUI ();

			LblTimer.Text = timer.ToString();

			btnChoiceProfile.TouchUpInside += PushProfileMenu;

			btnAdd.TouchUpInside += AddButton;

			btnSubtract.TouchUpInside += SubtractButton;

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

		private void InitializeUI()
		{
			btnAdd.SetImage (UIImage.FromFile ("images/AddBTN.png"), UIControlState.Normal);
			btnSubtract.SetImage (UIImage.FromFile ("images/SubtractBTN.png"), UIControlState.Normal);
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
			int segmetDifficultyLevel = scChoice.SelectedSegment;
			int segmetType = scSingleChoiceOptions.SelectedSegment;

			DatabaseRequests.StoreMenuSettings(segmetDifficultyLevel, segmetType, timer);
			if(viewController == null)
			{
				viewController = new MainViewController4();
			}

			NavigationController.PushViewController(viewController, false);
		}

		private void AddButton (object sender, EventArgs args)
		{
			timer ++;
			LblTimer.Text = timer.ToString();
		}

		private void SubtractButton (object sender, EventArgs args)
		{
			timer ++;
			LblTimer.Text = timer.ToString();
		}

		private MainViewController4 viewController;
		private ProfileMenu profileMenu;
		private int timer = 5;
	}
}