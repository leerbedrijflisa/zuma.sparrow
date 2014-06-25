using System;
using System.Drawing;
using System.Collections.Generic;

using MonoTouch.CoreMotion;
using MonoTouch.CoreImage;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AssetsLibrary;
using System.Threading;
using System.Threading.Tasks;

namespace Zuma.Sparrow
{
	public partial class OneButtonViewController : UIViewController
	{
		public OneButtonViewController() : base("OneButtonViewController", null)
		{
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
			CreateControls();
			CreateButtonChoice ();
			PositionControls(this.InterfaceOrientation);
			pushed = false;

			choiceSwitcher = new ChoiceSwitcher(imgLeft, imgRight);
			SwitchToLeft();

			btnChoice.TouchUpInside += OnChoice;
			rotationHelper.ScreenRotated += OnScreenRotated;
		}

		private void SwitchToRight()
		{
			currentChoice = Choice.Right;
			choiceSwitcher.SelectRight();
			currentTimer = NSTimer.CreateScheduledTimer(3, SwitchToLeft);
		}

		private void SwitchToLeft()
		{
			currentChoice = Choice.Left;
			choiceSwitcher.SelectLeft();
			currentTimer = NSTimer.CreateScheduledTimer(3, SwitchToRight);
		}
			
		/// <summary>
		/// is called when the OS is going to rotate the application. It handles rotating the status bar
		/// if it's present, as well as it's controls like the navigation controller and tab bar, but you 
		/// must handle the rotation of your view and associated subviews. This call is wrapped in an 
		/// animation block in the underlying implementation, so it will automatically animate your control
		/// repositioning.
		/// </summary>
		public override void WillAnimateRotation (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillAnimateRotation (toInterfaceOrientation, duration);
			PositionControls(toInterfaceOrientation);
		}

		private void CreateControls()
		{
			var navigationController = (NavigationController) NavigationController;

			imgLeft = new UIImageView(new RectangleF(0, 0, 368, 368));
			imgRight = new UIImageView(new RectangleF(0, 0, 368, 368));

			if (navigationController.CurrentProfile.CurrentProfileType == ProfileType.Default)
			{
				imgLeft.Image = UIImage.FromFile(navigationController.CurrentProfile.FirstOption.ImageUrl);
				imgRight.Image = UIImage.FromFile(navigationController.CurrentProfile.SecondOption.ImageUrl);
			}
			else if(navigationController.CurrentProfile.CurrentProfileType == ProfileType.Custom)
			{
					if (navigationController.CurrentProfile.FirstOption.ImageUrl != "empty.png")
					{
						imgLeft.Image = UIImage.FromFile(navigationController.CurrentProfile.FirstOption.ImageUrl);
						var LeftAssetUrl = NSUrl.FromString(navigationController.CurrentProfile.FirstOption.ImageUrl);

						imgLeft.Image = LoadImageFromGallery(LeftAssetUrl).Result;
					}
					else
					{
						imgLeft.Image = UIImage.FromFile(navigationController.CurrentProfile.FirstOption.ImageUrl);
					}

					if (navigationController.CurrentProfile.SecondOption.ImageUrl != "empty.png")
					{
						imgRight.Image = LoadImageFromGallery(NSUrl.FromString(navigationController.CurrentProfile.SecondOption.ImageUrl)).Result;	
					}
					else
					{
						imgRight.Image = UIImage.FromFile(navigationController.CurrentProfile.SecondOption.ImageUrl);
					}

			}

			View.AddSubviews(imgLeft, imgRight);
		}

		/// <summary>
		/// Loads the image from gallery asynchronous.
		/// </summary>
		/// <returns>The image from gallery if successful, null if not found or when an error occured.</returns>
		/// <param name="url">The path to the image to load.</param>
		private Task<UIImage> LoadImageFromGallery(NSUrl url) {
		
			// Start a Task<UIImage> to load the image on a new thread.
			return Task.Run<UIImage>(() =>
			{
				UIImage result = null;	

				// DO NOT REMOVE!
				// Used for signaling the thread to continue when the success or failure callback
				// of the AssetForUrl method has been executed.
				// This makes sure that the task only returns when the AssetForUrl method tried to load the image.
				var waitEvent = new ManualResetEvent(false);

				library.AssetForUrl(url, (asset) =>
				{

					result = new UIImage(asset.DefaultRepresentation.GetImage());
					waitEvent.Set();
				}, (failure) =>
				{
					waitEvent.Set();
				});

				// Wait until the waitEvent has been signaled.
				waitEvent.WaitOne();
				return result;
			});
		}

		/// <summary>
		/// A helper method to position the controls appropriately, based on the 
		/// orientation
		/// </summary>
		private void PositionControls(UIInterfaceOrientation toInterfaceOrientation)
		{
			// depending one what orientation we start in, we want to position our controls
			// appropriately
			switch (toInterfaceOrientation) {
				case UIInterfaceOrientation.LandscapeLeft:
				case UIInterfaceOrientation.LandscapeRight:
					PositionControlsForLandscape();
					break;

				case UIInterfaceOrientation.Portrait:
				case UIInterfaceOrientation.PortraitUpsideDown:
					PositionControlsForPortrait();
					break;
			}
		}

		private void PositionControlsForLandscape()
		{
			imgLeft.Frame = new RectangleF(
				UIScreen.MainScreen.Bounds.Height / 4 - imgLeft.Frame.Width / 2,
				UIScreen.MainScreen.Bounds.Width / 2 - imgLeft.Frame.Height / 2,
				imgLeft.Frame.Width,
				imgLeft.Frame.Height
			);

			imgRight.Frame = new RectangleF(
				3 * UIScreen.MainScreen.Bounds.Height / 4 - imgRight.Frame.Width / 2,
				UIScreen.MainScreen.Bounds.Width / 2 - imgRight.Frame.Height / 2,
				imgRight.Frame.Width,
				imgRight.Frame.Height
			);

			btnChoice.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width);
		}

		private void PositionControlsForPortrait()
		{
			imgLeft.Frame = new RectangleF(
				UIScreen.MainScreen.Bounds.Width / 2 - imgLeft.Frame.Width / 2,
				UIScreen.MainScreen.Bounds.Height / 4 - imgLeft.Frame.Height / 2,
				imgLeft.Frame.Width,
				imgLeft.Frame.Height
			);

			imgRight.Frame = new RectangleF(
				UIScreen.MainScreen.Bounds.Width / 2 - imgRight.Frame.Width / 2,
				3 * UIScreen.MainScreen.Bounds.Height / 4 - imgRight.Frame.Height / 2,
				imgRight.Frame.Width,
				imgRight.Frame.Height
			);

			btnChoice.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
		}

		private void CreateButtonChoice()
		{
			btnChoice = UIButton.FromType (UIButtonType.RoundedRect);
			View.AddSubview (btnChoice);
		}

		private void OnChoice(object sender, EventArgs args)
		{
			currentTimer.Dispose();
			var navigationController = (NavigationController) NavigationController;
			currentTimer = NSTimer.CreateScheduledTimer(8, ResetSwitchAndButton);
			btnChoice.Hidden = true;

			if (currentChoice == Choice.Left)
			{
				currentSound.Play(navigationController.CurrentProfile.FirstOption.AudioUrl);
			}
			else if (currentChoice == Choice.Right)
			{
				currentSound.Play(navigationController.CurrentProfile.SecondOption.AudioUrl);
			}
		}

		private void ResetSwitchAndButton()
		{
			btnChoice.Hidden = false;

			if (currentChoice == Choice.Left)
			{
				SwitchToLeft();
			}
			else if (currentChoice == Choice.Right)
			{
				SwitchToRight();
			}
		}

		private void OnScreenRotated(object sender, RotationEventArgs e)
		{
			if(mainMenu == null)
			{
				mainMenu = new MainMenuViewController();
			}

			if (!pushed) 
			{
				NavigationController.PushViewController (mainMenu, false);
				pushed = true;
			}
		}
			
		private Sound currentSound = new Sound();
		private RotationHelper rotationHelper = new RotationHelper();
		private ALAssetsLibrary library = new ALAssetsLibrary();
		private Choice currentChoice;
		private NSTimer currentTimer;
		private UIButton btnChoice;
		private ChoiceSwitcher choiceSwitcher;
		private MainMenuViewController mainMenu;
		private bool pushed;
	}
}