using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.AssetsLibrary;
using MonoTouch.UIKit;
using MonoTouch.CoreImage;
using MonoTouch.CoreGraphics;
using Lisa.Zuma;

namespace ZumaKeuzesOplichten
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

		UIImage sourceImage;
		CIColorControls colorBrightness;
		NSTimer timer;
		NSTimer inActiveTimer;
		Sound active = new Sound();
		Sound inActive = new Sound();
		Sound youChoose = new Sound();

		public override void ViewDidLoad ()
		{


			base.ViewDidLoad ();

			inActiveTimer = NSTimer.CreateRepeatingScheduledTimer((4.0), delegate {
				inActive.Play ("schudden1.mp3");
			});


			sourceImage = UIImage.FromFile ("drinken_filter.jpg");

			imvDisplayChoice.Image = UIImage.FromFile ("beker.jpg");

			btnTrigger.TouchUpInside += (sender, e) => {
				inActiveTimer.Dispose();

				active.Play("drinken2.mp3");

				NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(6.0), delegate {	 
					active.Play("drinken2.mp3", () =>
					{
						//youChoose.Play("dorst.mp3");
					});
				});

				//hid the button.
				btnTrigger.Hidden = true;
				imvDisplayChoice.Image = sourceImage;
				Console.WriteLine("btn pushed");
			

					//puts the picture in an instance to lay a filter on top of it
					colorBrightness = new CIColorControls () {
						Image = CIImage.FromCGImage (sourceImage.CGImage),
					};

					int count = 0;

					timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromMilliseconds (10), delegate {

						count++;
						
						if (count <= 6)
						{
							colorBrightness.Brightness += 0.1f;
							Console.WriteLine("increase");
						}
						else if(count <= 12)
						{
							colorBrightness.Brightness -= 0.1f;
							Console.WriteLine("decrease");
						}
						else
						{
							count = 0;
						}


						Console.WriteLine(count);
						Console.WriteLine(colorBrightness.Brightness);

						//this will transform the image
						var output = colorBrightness.OutputImage;
						var context = CIContext.FromOptions(null);
						var result = context.CreateCGImage(output, output.Extent);

						//display result
						imvDisplayChoice.Image = UIImage.FromImage(result);


					});

					//triggers a timer that displays the old image and makes button visable again
					NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds (12.0), delegate {
						youChoose.Play("Dorst.mp3", () => {
							timer.Dispose();
							imvDisplayChoice.Image = UIImage.FromFile("beker.jpg");
							btnTrigger.Hidden = false;
							inActiveTimer = NSTimer.CreateRepeatingScheduledTimer((4.0), delegate {
								inActive.Play ("schudden1.mp3");
							});
						});
					});

				};



			// De SetSatusBarHidden lijkt niet te werken
			UIApplication.SharedApplication.SetStatusBarHidden (true, true);
			UIApplication.SharedApplication.IdleTimerDisabled = true;

		}

		public override void ViewWillAppear (bool animated){
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, animated);
		}
		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			this.NavigationController.SetNavigationBarHidden (false, animated);
		}



	}
}

