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

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			var navigationController = (NavigationController) NavigationController;
			imgPortraitUp.Image = UIImage.FromFile(navigationController.CurrentProfile.FirstOption.ImageUrl);
			imgPortraitDown.Image = UIImage.FromFile(navigationController.CurrentProfile.SecondOption.ImageUrl);
			imgLandscapeLeft.Image = UIImage.FromFile(navigationController.CurrentProfile.FirstOption.ImageUrl);
			imgLandscapeRight.Image = UIImage.FromFile(navigationController.CurrentProfile.SecondOption.ImageUrl);
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

			// call our helper method to position the controls
			PositionControls (toInterfaceOrientation);
		}

		/// <summary>
		/// A helper method to position the controls appropriately, based on the 
		/// orientation
		/// </summary>
		protected void PositionControls (UIInterfaceOrientation toInterfaceOrientation)
		{
			// depending one what orientation we start in, we want to position our controls
			// appropriately
			switch (toInterfaceOrientation) {
				case UIInterfaceOrientation.LandscapeLeft:
				case UIInterfaceOrientation.LandscapeRight:
					viewPortrait.Hidden = true;
					viewLandscape.Hidden = false;
					break;

				case UIInterfaceOrientation.Portrait:
				case UIInterfaceOrientation.PortraitUpsideDown:
					viewPortrait.Hidden = false;
					viewLandscape.Hidden = true;
					break;
			}
		}
	}
}