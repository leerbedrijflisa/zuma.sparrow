using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ZumaKeuzes
{
	public partial class SingleChoiceViewController : UIViewController
	{
		public SingleChoiceViewController () : base ("SingleChoiceViewController", null)
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

			this.btnSingleChoice.TouchUpInside += (sender, e) => 
			{
				UIImage Attention = UIImage.FromFile ("attention.jpg");
			};

			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

