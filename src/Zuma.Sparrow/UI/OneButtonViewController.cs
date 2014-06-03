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
			imgLeft.Image = UIImage.FromFile(navigationController.CurrentProfile.FirstOption.ImageUrl);
			imgRight.Image = UIImage.FromFile(navigationController.CurrentProfile.SecondOption.ImageUrl);
		}
	}
}