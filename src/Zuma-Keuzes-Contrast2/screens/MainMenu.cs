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

			SetProfileSettings();
			InitializeUI ();

			btnClickTimer.TouchUpInside += ClickTimer;
			btnDarkTimer.TouchUpInside += DarkTimer;
			btnGo.TouchUpInside += PushMainView;
			btnChoiceProfile.TouchUpInside += PushProfileMenu;
			scChoice.ValueChanged += HideDarkTimer;
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
			selectedProfile = queryProfile.returnProfileRow(_selectedProfileRow);
		}

		private void InitializeUI()
		{
			imvBackgroundImage.Image = UIImage.FromFile ("images/achtergrond.png");
			imvLogoImage.Image = UIImage.FromFile ("images/wanna-logo.png");
			btnClickTimer.MinimumValue = 1;
			btnDarkTimer.MinimumValue = 1;
			btnClickTimer.MaximumValue = 99;
			btnDarkTimer.MaximumValue = 99;
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
			int segmetDifficultyLevel = scChoice.SelectedSegment;

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

		private void HideDarkTimer(object sender, EventArgs args)
		{
			if (scChoice.SelectedSegment == 1) {
				lblDarkTimer.Hidden = true;
				btnDarkTimer.Hidden = true;
				txtLblDarkTimer.Hidden = true;
			} else {
				lblDarkTimer.Hidden = false;
				btnDarkTimer.Hidden = false;
				txtLblDarkTimer.Hidden = false;
			}
		}

		QueryProfile queryProfile = new QueryProfile();
		private MainViewController4 viewController;
		private ProfileMenu profileMenu;
		private int clickTimer = 5, darkTimer = 5, _selectedProfileRow;
		private string[] menuSetting = new string[4], selectedProfile = new string[6];
		private string selectedProfileRow;
	}
}