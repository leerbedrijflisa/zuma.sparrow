using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Timers;
using Lisa.Zuma;

namespace ZumaKeuzes
{
	public partial class MainScreenViewController : UIViewController
	{
		public MainScreenViewController () : base ("MainScreenViewController", null)
		{

		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		NSTimer drinken;
		//Sound drink = new Sound(), call = new Sound();
		UIImageView imvLayer;
		UIImage cupFar, cupNear, layer, empty;

		public void inActive ()
		{
			btnTrigger.Hidden = false;

			imvLayer.Image = empty;
			imvImageTest.Image = cupFar;

			//drink.Play("drinking.mp3");

			drinken = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(5), delegate {
				//drink.Play("drinking.mp3");
			});

		}

		public override void ViewDidLoad ()
		{

			base.ViewDidLoad ();

			cupFar = UIImage.FromFile ("bekerVeraf.jpg");
			cupNear = UIImage.FromFile ("bekerDichtbij.jpg");
			layer = UIImage.FromFile ("layer_transparent.png");
			empty = UIImage.FromFile ("");

			imvLayer = new UIImageView (UIScreen.MainScreen.Bounds);

			View.Add (imvLayer);

			inActive ();

			btnTrigger.TouchUpInside += (sender, e) => {

				imvImageTest.Image = cupNear;
				btnTrigger.Hidden = true;
				drinken.Dispose();

				//call.Play("thirsty.mp3");


				NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds (5), delegate {
					imvLayer.Image = layer;

					NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds (5), delegate {
						inActive();

					});

				});
					
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

