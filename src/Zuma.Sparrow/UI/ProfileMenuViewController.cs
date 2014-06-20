using System;
using System.Drawing;

using MonoTouch.AssetsLibrary;
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

			inputProfileName.EditingDidEndOnExit += OnInputProfileName;
			btnImageLeft.TouchUpInside += OnImageLeft;
			btnImageRight.TouchUpInside += OnImageRight;

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

			navigationController.CurrentProfile = catalog.Find(e.Profile.Id);
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
			inputProfileName.Hidden = false;
			inputProfileName.BecomeFirstResponder();

			lblProfileName.Text = untitled;
			imvLeft.Image = UIImage.FromFile("empty.png");
			imvRight.Image = UIImage.FromFile("empty.png");


			newProfile.Name = untitled;
			newProfile.FirstOption.ImageUrl = "empty.png";
			newProfile.FirstOption.AudioUrl = "";
			newProfile.SecondOption.ImageUrl = "empty.png";
			newProfile.SecondOption.AudioUrl = "";

			newProfile.Id = catalog.Create(newProfile);
			TableViewInitializer();
		}

		void OnInputProfileName(object sender, EventArgs e)
		{
			newProfile.Name = inputProfileName.Text;
			lblProfileName.Text = newProfile.Name;
			inputProfileName.Hidden = true;
			lblProfileName.Hidden = false;
			catalog.Update(newProfile);
			TableViewInitializer();
		}

		private void OnImageLeft(object sender, EventArgs e)
		{
			imagePicker = new UIImagePickerController ();

			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);

			imagePicker.FinishedPickingMedia += HandleFinnishedPickingMediaLeft;
			imagePicker.Canceled += HandleCanceled;

			View.AddSubview (imagePicker.View);
		}

		private void OnImageRight(object sender, EventArgs e)
		{
			imagePicker = new UIImagePickerController ();

			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);

			imagePicker.FinishedPickingMedia += Handle_FinnishedPickingMediaRight;
			imagePicker.Canceled += HandleCanceled;

			View.AddSubview (imagePicker.View);
		}

		private void HandleFinnishedPickingMediaLeft(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
			imvLeft.Image = originalImage;
			imagePicker.View.RemoveFromSuperview ();

			library.WriteImageToSavedPhotosAlbum (originalImage.CGImage,meta, (assetUrl, error) =>
			{
				Console.WriteLine ("assetUrl:"+assetUrl);
			});
		}

		private void Handle_FinnishedPickingMediaRight(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
			imvRight.Image = originalImage;
			imagePicker.View.RemoveFromSuperview ();

			library.WriteImageToSavedPhotosAlbum (originalImage.CGImage,meta, (assetUrl, error) =>
			{
				Console.WriteLine ("assetUrl:"+assetUrl);
			});
		}

		private void HandleCanceled(object sender, EventArgs e)
		{
			Console.WriteLine("dismiss");
			imagePicker.DismissViewController(true, () => {});
			imagePicker.View.RemoveFromSuperview ();
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
			inputProfileName.Hidden = true;
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
		private ChoiceProfile newProfile = new ChoiceProfile();
		private RotationHelper rotationHelper = new RotationHelper();
		private ChoiceProfileCatalog catalog = new ChoiceProfileCatalog();
		private ProfileTableSource tableSource = new ProfileTableSource();
		private ALAssetsLibrary library = new ALAssetsLibrary();
		private MainMenuViewController mainMenu;
		private UIImagePickerController imagePicker;
		private UIImage originalImage;
		private NSDictionary meta = new NSDictionary();
		private bool pushed;

	}
}