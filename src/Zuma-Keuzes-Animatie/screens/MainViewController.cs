using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ZumaKeuzesAnimatie
{
	public partial class MainViewController : UIViewController
	{
		public MainViewController () : base ("MainViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Console.WriteLine("The ViewDidLoad");


			imvDrinkingCup.AnimationImages = new UIImage[] {
				UIImage.FromBundle("herfst.jpg"),
				UIImage.FromBundle("winter.jpeg"),
				UIImage.FromBundle("lente.jpeg"),
				UIImage.FromBundle("zomer.jpg"),

			};

			imvDrinkingCup.AnimationRepeatCount = 0;
			imvDrinkingCup.AnimationDuration = .5;
			imvDrinkingCup.StartAnimating ();

			btnTrigger.TouchUpInside += (sender, e) => {
		
				imvDrinkingCup.AnimationImages = new UIImage[] {
					UIImage.FromBundle("herfst.jpg"),
					UIImage.FromBundle("winter.jpeg"),
					UIImage.FromBundle("lente.jpeg"),
					UIImage.FromBundle("zomer.jpg"),

				};

				imvDrinkingCup.AnimationRepeatCount = 0;
				imvDrinkingCup.AnimationDuration = 4.5;
				imvDrinkingCup.StartAnimating ();
				Console.WriteLine("Button pushed");

				btnTrigger.Hidden = true;


				//triggers a timer
				NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds (4.0), delegate {
					imvDrinkingCup.AnimationImages = new UIImage[] {
						UIImage.FromBundle("herfst.jpg"),
						UIImage.FromBundle("winter.jpeg"),
						UIImage.FromBundle("lente.jpeg"),
						UIImage.FromBundle("zomer.jpg"),

					};

					imvDrinkingCup.AnimationRepeatCount = 0;
					imvDrinkingCup.AnimationDuration = .5;
					imvDrinkingCup.StartAnimating ();
					btnTrigger.Hidden = false;
					Console.WriteLine("Timer Activeded");
				});

			};
		}
	}
}

