using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Zuma.Sparrow
{
	public partial class MainMenuViewController : UIViewController
	{
		public MainMenuViewController() : base("MainMenuViewController", null)
		{
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.LandscapeLeft | UIInterfaceOrientationMask.LandscapeRight;
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

			btnStart.TouchUpInside += OnStartTouch;
		}

		private void OnStartTouch(object sender, EventArgs e)
		{
			var choiceViewController = new OneButtonViewController();
			NavigationController.PushViewController(choiceViewController, true);
		}
	}
}