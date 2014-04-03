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

			SetProfileSettings();

			InitializeUI ();

			btnClickTimer.TouchUpInside += ClickTimer;

			btnDarkTimer.TouchUpInside += DarkTimer;

			btnGo.TouchUpInside += PushMainView;

			btnChoiceProfile.TouchUpInside += PushProfileMenu;

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

		private void SetProfileSettings()
		{
			menuSetting = queryProfile.ReadMenuSettings();
			selectedProfileRow = menuSetting [3];
			_selectedProfileRow = Convert.ToInt32 (selectedProfileRow);

			Console.WriteLine ("selectedrow int " + _selectedProfileRow);
			selectedProfile = queryProfile.returnProfileRow(_selectedProfileRow);
		}

		private void InitializeUI()
		{
			btnClickTimer.MinimumValue = 1;
			btnDarkTimer.MinimumValue = 1;
			btnClickTimer.Value = 5;
			btnDarkTimer.Value = 5;

			lblProfile.Text = selectedProfile [0];
		}

		private void PushProfileMenu(object sender, EventArgs args) 
		{
			if (profileMenu == null) {
				profileMenu = new ProfileMenu ();
			}

			NavigationController.PushViewController (profileMenu, false);
		}

		private void PushMainView(object sender, EventArgs args)
		{
			Console.WriteLine (clickTimer.ToString () + " click");
			Console.WriteLine (darkTimer.ToString () + " dark");

			int segmetDifficultyLevel = scChoice.SelectedSegment;
//			int segmetType = scSingleChoiceOptions.SelectedSegment;

			DatabaseRequests.StoreMenuSettings(segmetDifficultyLevel, clickTimer, darkTimer, selectedProfileRow);
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
		private int clickTimer = 5, darkTimer = 5, _selectedProfileRow;
		private string[] menuSetting = new string[4], selectedProfile = new string[6];
		private string selectedProfileRow;
	}
}