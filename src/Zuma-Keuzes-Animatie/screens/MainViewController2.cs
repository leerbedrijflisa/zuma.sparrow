using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreImage;

namespace ZumaKeuzesAnimatie
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

		CIColorControls colorContrast;
		UIImage displayCup;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			displayCup = UIImage.FromFile ("CupCloseUp.jpg");

			colorContrast = new CIColorControls () {
				Image = CIImage.FromCGImage(displayCup.CGImage),
			};

			colorContrast.Contrast = 1;

			var output = colorContrast.OutputImage;
			var context = CIContext.FromOptions (null);
			var result = context.CreateCGImage (output, output.Extent);

			imvDisplayChoice.Image = UIImage.FromImage (result);

		}
	}
}

