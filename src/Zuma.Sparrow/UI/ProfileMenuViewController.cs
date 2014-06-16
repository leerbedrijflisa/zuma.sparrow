
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

			var tableSource = new ProfileTableSource();
			tblProfiles.Source = tableSource;
			tableSource.ProfileSelected += OnProfileSelected;

			btnPlaySndLeft.TouchUpInside += OnSndLeft;
			btnPlaySndRight.TouchUpInside += OnSndRight;

		}

		private void OnProfileSelected(object sender, ProfileEventArgs e)
		{

			var profileCatalog = new ChoiceProfileCatalog();
			var navigationController = (NavigationController) NavigationController;

			navigationController.CurrentProfile = profileCatalog.Find(e.Profile);
			currentProfile = navigationController.CurrentProfile;

			lblProfileName.Text = currentProfile.Name;
			imvLeft.Image = UIImage.FromFile(currentProfile.FirstOption.ImageUrl);
			imvRight.Image = UIImage.FromFile(currentProfile.SecondOption.ImageUrl);

		}

		private void OnSndLeft(object sender, EventArgs e)
		{
			sound.Play(currentProfile.FirstOption.AudioUrl);
		}

		void OnSndRight(object sender, EventArgs e)
		{
			sound.Play(currentProfile.SecondOption.AudioUrl);
		}

		private Sound sound = new Sound();
		private ChoiceProfile currentProfile = new ChoiceProfile();
	}
}