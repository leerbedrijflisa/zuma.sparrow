using System;
using System.Drawing;

using MonoTouch.CoreImage;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

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

			choiceSwitcher = new ChoiceSwitcher(imgLeft, imgRight);
			SwitchToLeft();

			btnChoice.TouchUpInside += OnChoice;
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
			imgLeft.Image = UIImage.FromFile(navigationController.CurrentProfile.FirstOption.ImageUrl);

			imgRight = new UIImageView(new RectangleF(0, 0, 368, 368));
			imgRight.Image = UIImage.FromFile(navigationController.CurrentProfile.SecondOption.ImageUrl);

			View.AddSubviews(imgLeft, imgRight);
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

		private Sound currentSound = new Sound();
		private Choice currentChoice;
		private NSTimer currentTimer;
		private UIButton btnChoice;
		private ChoiceSwitcher choiceSwitcher;
	}
}