using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ZumaKeuzesStaticImage
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

		MainViewController viewController;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// MainMenuKeuze version 1.0
			// this MainMenuKeuze does not include a database connection yet

			btnStart.TouchUpInside += (sender, e) => {


				if (scChoice.SelectedSegment == 0)
				{


					int segmetSingleChoice = scSingleChoiceOptions.SelectedSegment;

					Console.WriteLine(segmetSingleChoice);

					if(viewController == null)
					{
						viewController = new MainViewController();
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