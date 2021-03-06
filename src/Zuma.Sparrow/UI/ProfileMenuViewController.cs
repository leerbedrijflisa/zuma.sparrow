﻿using System;
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
			btnRecSndLeft.TouchUpInside += OnRecSndLeft;
			btnRecSndRight.TouchUpInside += OnRecSndRight;
			btnChoiceProfile.TouchUpInside += OnChoiceProfile;
			btnCreateProfile.TouchUpInside += OnCreateProfile;
			rotationHelper.ScreenRotated += OnScreenRotated;

			inputProfileName.EditingDidEnd += OnInputProfileName;
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
			var navigationController = (NavigationController) NavigationController;
			sound.Play(navigationController.CurrentProfile.FirstOption.AudioUrl);
		}

		private void OnSndRight(object sender, EventArgs e)
		{
			var navigationController = (NavigationController) NavigationController;
			sound.Play(navigationController.CurrentProfile.SecondOption.AudioUrl);
		}

		private void OnRecSndLeft(object sender, EventArgs e)
		{
			if (isRecording)
			{
				btnRecSndLeft.SetTitle("stop", UIControlState.Normal);
				rec.StartRecording(true);
				isRecording = false;
				Console.WriteLine(isRecording);
			}
			else
			{
				btnRecSndLeft.SetTitle("Record", UIControlState.Normal);
				rec.StopRecording(true);
				isRecording = false;
				Console.WriteLine(isRecording);
			}
		}

		private void OnRecSndRight(object sender, EventArgs e)
		{
			throw new NotImplementedException();
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

			btnImageLeft.Hidden = false;
			btnImageRight.Hidden = false;
			btnRecSndLeft.Hidden = false;
			btnRecSndRight.Hidden = false;
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
			newProfile.CurrentProfileType = ProfileType.Custom;

			newProfile.Id = catalog.Create(newProfile);
			TableViewInitializer();
		}

		void OnInputProfileName(object sender, EventArgs e)
		{
			newProfile.Name = inputProfileName.Text;
			lblProfileName.Text = newProfile.Name;
			inputProfileName.ResignFirstResponder();
			inputProfileName.Hidden = true;
			lblProfileName.Hidden = false;
			catalog.Update(newProfile);
			TableViewInitializer();
		}

		private void OnImageLeft(object sender, EventArgs e)
		{
			if (imagePickerLeft == null) {
				imagePickerLeft = new UIImagePickerController ();
			}
			inputProfileName.ResignFirstResponder();
			imagePickerLeft.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

			imagePickerLeft.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);

			imagePickerLeft.FinishedPickingMedia += HandleFinnishedPickingMediaLeft;
			imagePickerLeft.Canceled += HandleCanceled;

			imagePickerLeft.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width);
			View.AddSubview (imagePickerLeft.View);
		}

		private void OnImageRight(object sender, EventArgs e)
		{
			if (imagePickerRight == null) {
				imagePickerRight = new UIImagePickerController ();
			}
			inputProfileName.ResignFirstResponder();
			imagePickerRight.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

			imagePickerRight.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);

			imagePickerRight.FinishedPickingMedia += Handle_FinnishedPickingMediaRight;
			imagePickerRight.Canceled += HandleCanceled;

			imagePickerRight.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width);
			View.AddSubview (imagePickerRight.View);
		}

		private void HandleFinnishedPickingMediaLeft(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
			imvLeft.Image = originalImage;
			imagePickerLeft.View.RemoveFromSuperview ();

			library.WriteImageToSavedPhotosAlbum (originalImage.CGImage,meta, (assetUrl, error) =>
			{
				Console.WriteLine ("assetUrl:"+assetUrl);
				newProfile.FirstOption.ImageUrl = assetUrl.ToString();
				catalog.Update(newProfile);
			});
		}

		private void Handle_FinnishedPickingMediaRight(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
			imvRight.Image = originalImage;
			imagePickerRight.View.RemoveFromSuperview ();

			library.WriteImageToSavedPhotosAlbum (originalImage.CGImage,meta, (assetUrl, error) =>
			{
				Console.WriteLine ("assetUrl:"+assetUrl);
				newProfile.SecondOption.ImageUrl = assetUrl.ToString();
				catalog.Update(newProfile);
			});
		}

		private void HandleCanceled(object sender, EventArgs e)
		{
			if(imagePickerLeft != null){
				imagePickerLeft.DismissViewController(true, () => {});
				imagePickerLeft.View.RemoveFromSuperview ();
			}
			if (imagePickerRight != null) {
				imagePickerRight.DismissViewController (true, () => {});
				imagePickerRight.View.RemoveFromSuperview ();
			}
		}

		private void UIInitializer()
		{
			var navigationController = (NavigationController) NavigationController;

			lblProfileName.Text = navigationController.CurrentProfile.Name;
			if (navigationController.CurrentProfile.CurrentProfileType == ProfileType.Default)
			{
				imvLeft.Image = UIImage.FromFile(navigationController.CurrentProfile.FirstOption.ImageUrl);
				imvRight.Image = UIImage.FromFile(navigationController.CurrentProfile.SecondOption.ImageUrl);
			}
			else if(navigationController.CurrentProfile.CurrentProfileType == ProfileType.Custom)
			{
			Console.WriteLine("Custom");
				if (navigationController.CurrentProfile.FirstOption.ImageUrl != "empty.png")
				{
					library.AssetForUrl(NSUrl.FromString(navigationController.CurrentProfile.FirstOption.ImageUrl), (asset) =>
					{
						imvLeft.Image = new UIImage(asset.DefaultRepresentation.GetImage());
					}, (failure) =>
					{
					});
				}
				else
				{
					imvLeft.Image = UIImage.FromFile("empty.png");
				}

				if (navigationController.CurrentProfile.SecondOption.ImageUrl != "empty.png")
				{
					library.AssetForUrl(NSUrl.FromString(navigationController.CurrentProfile.SecondOption.ImageUrl), (asset) =>{ imvRight.Image = new UIImage(asset.DefaultRepresentation.GetImage());}, (failure) =>{});
				}
				else
				{
					imvRight.Image = UIImage.FromFile("empty.png");
				}
			}

			btnImageLeft.Hidden = true;
			btnImageRight.Hidden = true;
			btnRecSndLeft.Hidden = true;
			btnRecSndRight.Hidden = true;
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
		private ChoiceProfile newProfile = new ChoiceProfile();
		private RotationHelper rotationHelper = new RotationHelper();
		private ChoiceProfileCatalog catalog = new ChoiceProfileCatalog();
		private ProfileTableSource tableSource = new ProfileTableSource();
		private ALAssetsLibrary library = new ALAssetsLibrary();
		private RecordSound rec = new RecordSound();
		private MainMenuViewController mainMenu;
		private UIImagePickerController imagePickerLeft;
		private UIImagePickerController imagePickerRight;
		private UIImage originalImage;
		private NSDictionary meta = new NSDictionary();
		private bool pushed, isRecording;

	}
}