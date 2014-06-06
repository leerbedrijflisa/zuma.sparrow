using System;
using System.Drawing;

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
			PositionControls(this.InterfaceOrientation);
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
				1024 / 4 - imgLeft.Frame.Width / 2,
				768 / 2 - imgLeft.Frame.Height / 2,
				imgLeft.Frame.Width,
				imgLeft.Frame.Height);

			imgRight.Frame = new RectangleF(
				3 * 1024 / 4 - imgRight.Frame.Width / 2,
				768 / 2 - imgRight.Frame.Height / 2,
				imgRight.Frame.Width,
				imgRight.Frame.Height
			);
		}

		private void PositionControlsForPortrait()
		{
			imgLeft.Frame = new RectangleF(
				768 / 2 - imgLeft.Frame.Width / 2,
				1024 / 4 - imgLeft.Frame.Height / 2,
				imgLeft.Frame.Width,
				imgLeft.Frame.Height);

			imgRight.Frame = new RectangleF(
				768 / 2 - imgRight.Frame.Width / 2,
				3 * 1024 / 4 - imgRight.Frame.Height / 2,
				imgRight.Frame.Width,
				imgRight.Frame.Height);
		}
	}
}