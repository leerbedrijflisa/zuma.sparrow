
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Zuma.Sparrow
{
	public partial class ProfileMenuViewController : UIViewController
	{
		public ProfileMenuViewController() : base("ProfileMenuViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			pushed = false;

			var tableSource = new ProfileTableSource();
			tblProfiles.Source = tableSource;

			UIInitializer();

			tableSource.ProfileSelected += OnProfileSelected;

			btnPlaySndLeft.TouchUpInside += OnSndLeft;
			btnPlaySndRight.TouchUpInside += OnSndRight;
			btnChoiceProfile.TouchUpInside += OnChoiceProfile;
			rotationHelper.ScreenRotated += OnScreenRotated;

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

		private void OnProfileSelected(object sender, ProfileEventArgs e)
		{
			var profileCatalog = new ChoiceProfileCatalog();
			var navigationController = (NavigationController) NavigationController;

			navigationController.CurrentProfile = profileCatalog.Find(e.Profile);
			UIInitializer();
		}

		private void OnSndLeft(object sender, EventArgs e)
		{
			sound.Play(currentProfile.FirstOption.AudioUrl);
		}

		private void OnSndRight(object sender, EventArgs e)
		{
			sound.Play(currentProfile.SecondOption.AudioUrl);
		}

		private void OnScreenRotated(object sender, RotationEventArgs e)
		{
			if (mainMenu == null)
			{
				mainMenu = new MainMenuViewController();
			}

			if (!pushed)
			{
				pushed = true;
				NavigationController.PushViewController(mainMenu, false);
			}
		}

		void OnChoiceProfile(object sender, EventArgs e)
		{
			if (mainMenu == null)
			{
				mainMenu = new MainMenuViewController();
			}

			if (!pushed)
			{
				pushed = true;
				NavigationController.PushViewController(mainMenu, false);
			}
		}

		private void UIInitializer()
		{
			var navigationController = (NavigationController) NavigationController;

			lblProfileName.Text = navigationController.CurrentProfile.Name;
			imvLeft.Image = UIImage.FromFile(navigationController.CurrentProfile.FirstOption.ImageUrl);
			imvRight.Image = UIImage.FromFile(navigationController.CurrentProfile.SecondOption.ImageUrl);
		}
		
		private Sound sound = new Sound();
		private ChoiceProfile currentProfile = new ChoiceProfile();
		private RotationHelper rotationHelper = new RotationHelper();
		private MainMenuViewController mainMenu;
		private bool pushed;
	}
}