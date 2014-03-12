using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AssetsLibrary;
using Lisa.Zuma;


namespace ZumaKeuzesContrast2
{
	public partial class MainViewController2 : UIViewController
	{
		public MainViewController2 () : base ("MainViewController2", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		//UIImage bekerImage;
		Sound Drinking = new Sound();
		Sound Thirsty = new Sound();

		public void inActive()
		{
			btnTrigger.Hidden = false;

			imvDisplayChoice.AnimationImages = new UIImage[] {
				UIImage.FromBundle ("CupCloseUp_-50.jpg"),
				UIImage.FromBundle ("CupCloseUp_-40.jpg"),
				UIImage.FromBundle ("CupCloseUp_-30.jpg"),
				UIImage.FromBundle ("CupCloseUp_-20.jpg"),
				UIImage.FromBundle ("CupCloseUp_-10.jpg"),
				UIImage.FromBundle ("CupCloseUp_0.jpg"),
				UIImage.FromBundle ("CupCloseUp_10.jpg"),
				UIImage.FromBundle ("CupCloseUp_20.jpg"),
				UIImage.FromBundle ("CupCloseUp_30.jpg"),
				UIImage.FromBundle ("CupCloseUp_40.jpg"),
				UIImage.FromBundle ("CupCloseUp_50.jpg"),
			};

			imvDisplayChoice.AnimationRepeatCount = 1;
			imvDisplayChoice.AnimationDuration = 8;
			imvDisplayChoice.StartAnimating ();
			NSTimer.CreateScheduledTimer (TimeSpan.FromSeconds (2), delegate {
				Drinking.Play ("Drinking.mp3");
				Console.WriteLine ("drinken");
			});

			NSTimer.CreateScheduledTimer (TimeSpan.FromSeconds (8), delegate {

				Console.WriteLine("timer");

				imvDisplayChoice.AnimationImages = new UIImage[] {

					UIImage.FromBundle ("CupCloseUp_50.jpg"),
					UIImage.FromBundle ("CupCloseUp_40.jpg"),
					UIImage.FromBundle ("CupCloseUp_30.jpg"),
					UIImage.FromBundle ("CupCloseUp_20.jpg"),
					UIImage.FromBundle ("CupCloseUp_10.jpg"),
					UIImage.FromBundle ("CupCloseUp_0.jpg"),
					UIImage.FromBundle ("CupCloseUp_-10.jpg"),
					UIImage.FromBundle ("CupCloseUp_-20.jpg"),
					UIImage.FromBundle ("CupCloseUp_-30.jpg"),
					UIImage.FromBundle ("CupCloseUp_-40.jpg"),
					UIImage.FromBundle ("CupCloseUp_-50.jpg"),
				};

				imvDisplayChoice.AnimationRepeatCount = 1;
				imvDisplayChoice.AnimationDuration = 8;
				imvDisplayChoice.StartAnimating ();

			});

		}





		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


		
			inActive ();

			NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(8), delegate {
				inActive();
				Console.WriteLine("reaptingscheduletimer started");
			});

			btnTrigger.TouchUpInside += (sender, e) => {
				btnTrigger.Hidden = true;
				imvDisplayChoice.Image = UIImage.FromFile("CupCloseUp_50.jpg");
				Thirsty.Play("Thirsty.mp3");
				Console.WriteLine("btnpushed");
			};

		}
	}
}

