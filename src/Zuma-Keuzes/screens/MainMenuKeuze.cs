using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ZumaKeuzes
{
	public partial class MainMenuKeuze : UIViewController
	{
		public MainMenuKeuze () : base ("MainMenuKeuze", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		MainScreenViewController viewController;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnStart.TouchUpInside += (sender, e) => {

				if(scChoice.SelectedSegment == 0)
				{
					if(viewController == null)
					{
						viewController = new MainScreenViewController();
					}

					NavigationController.PushViewController(viewController, true);
				}
			};
		}

		public override void ViewWillAppear (bool animated) {
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, animated);
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			this.NavigationController.SetNavigationBarHidden (false, animated);
		}
	}
}

