
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

			UIInitializer();
			TableViewInitializer();


			btnPlaySndLeft.TouchUpInside += OnSndLeft;
			btnPlaySndRight.TouchUpInside += OnSndRight;
			btnChoiceProfile.TouchUpInside += OnChoiceProfile;
			btnCreateProfile.TouchUpInside += OnCreateProfile;
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

		private void OnChoiceProfile(object sender, EventArgs e)
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

		private void OnCreateProfile(object sender, EventArgs e)
		{
			var untitled = "Untitled Profile";

			btnPlaySndLeft.Hidden = true;
			btnPlaySndRight.Hidden = true;
			btnChoiceProfile.Hidden = true;
			btnCreateProfile.Hidden = true;

			lblProfileName.Text = untitled;
			imvLeft.Image = UIImage.FromFile("empty.png");
			imvRight.Image = UIImage.FromFile("empty.png");

			var newChoiceProfile = new ChoiceProfile();
			newChoiceProfile.Name = untitled;
			newChoiceProfile.FirstOption.ImageUrl = "empty.png";
			newChoiceProfile.FirstOption.AudioUrl = "";
			newChoiceProfile.SecondOption.ImageUrl = "empty.png";
			newChoiceProfile.SecondOption.AudioUrl = "";

			profileCatalog.Create(newChoiceProfile);
			TableViewInitializer();
		}

		private void UIInitializer()
		{
			var navigationController = (NavigationController) NavigationController;

			lblProfileName.Text = navigationController.CurrentProfile.Name;
			imvLeft.Image = UIImage.FromFile(navigationController.CurrentProfile.FirstOption.ImageUrl);
			imvRight.Image = UIImage.FromFile(navigationController.CurrentProfile.SecondOption.ImageUrl);

			btnPlaySndLeft.Hidden = false;
			btnPlaySndRight.Hidden = false;
			btnChoiceProfile.Hidden = false;
			btnCreateProfile.Hidden = false;

		}

		private void TableViewInitializer()
		{
			tableSource = new ProfileTableSource();
			tblProfiles.Source = tableSource;
			tblProfiles.ReloadData();

			tableSource.ProfileSelected += OnProfileSelected;
		}

		private Sound sound = new Sound();
		private ChoiceProfile currentProfile = new ChoiceProfile();
		private RotationHelper rotationHelper = new RotationHelper();
		private ChoiceProfileCatalog profileCatalog = new ChoiceProfileCatalog();
		private ProfileTableSource tableSource = new ProfileTableSource();
		private MainMenuViewController mainMenu;
		private bool pushed;
	}
}