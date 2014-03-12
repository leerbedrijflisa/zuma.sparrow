using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.AssetsLibrary;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreImage;
using Lisa.Zuma;

namespace ZumaKeuzesContrast2
{
	public partial class MainViewController3 : UIViewController
	{
		public MainViewController3 () : base ("MainViewController3", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		UIImage cup;
		CIColorControls contrastCtrls;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			cup = UIImage.FromFile ("CupCloseUp_0.jpg");
			imvDisplayChoice.Image = cup;
			btnChoice.Hidden = false;

			contrastCtrls = new CIColorControls () {
				Image = CIImage.FromCGImage (cup.CGImage),
			};

			contrastCtrls.Contrast = 4;

			var output = contrastCtrls.OutputImage;
			var context = CIContext.FromOptions (null);
			var result = context.CreateCGImage (output, output.Extent);

			imvDisplayChoice.Image = UIImage.FromImage (result);


		}
	}
}

