using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreImage;
using MonoTouch.CoreGraphics;
using MonoTouch.AssetsLibrary;


namespace ZumaKeuzesContrast
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

		NSTimer btnUnPushedTimer;
		NSTimer btnpushedTimer;
		UIImage bekerImage;
		CIColorControls colorContrast;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnTrigger.Hidden = false;
			bekerImage = UIImage.FromFile ("bekerDicht.jpg");
			imvDisplayChoice.Image = bekerImage;

			colorContrast = new CIColorControls()
			{
				Image = CIImage.FromCGImage(bekerImage.CGImage),
			};

			int count = 0;
			//default contrast waarde is 1
			colorContrast.Contrast = 1;

			btnUnPushedTimer = NSTimer.CreateRepeatingScheduledTimer (TimeSpan.FromSeconds (1.0), delegate {

				count++;
				Console.WriteLine(colorContrast.Contrast);
				Console.WriteLine(count);
				if(count <=6)
				{
					colorContrast.Contrast += 0.1f;
					Console.WriteLine("increase");
				}
				else if(count <=12)
				{
					colorContrast.Contrast -= 0.1f;
					Console.WriteLine("decrease");
				}
				else
				{
					count = 0;

				}




				var output = colorContrast.OutputImage;
				var context = CIContext.FromOptions (null);
				var result = context.CreateCGImage(output, output.Extent);

				imvDisplayChoice.Image = UIImage.FromImage (result);
			});

			//een plaatje wisselt met contrast van laag naar hoog en weer terug.
			//als het contrast van laag naar hoog gaat hoor je een geluidje! (slurp geluid)

			btnTrigger.TouchUpInside += (sender, e) => {
			
				//De knop word hidden
				btnTrigger.Hidden = true;

				//als er op de knop gedrukt is gaat het contrast om hoog en blijft het plaatje staan in het hoogste contrast.

				//tegelijker tijd word de keuze uit gesprooken


				//als de keuze uit gesprooken is word het scherm voor 5 seconde zwart (zo donker mogelijk).
			};

		}
	}
}