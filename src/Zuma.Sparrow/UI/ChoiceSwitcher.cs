using System;

using MonoTouch.CoreGraphics;
using MonoTouch.CoreImage;
using MonoTouch.UIKit;

namespace Zuma.Sparrow
{
	public class ChoiceSwitcher
	{
		public ChoiceSwitcher(UIImageView imgLeft, UIImageView imgRight)
		{
			this.imgLeft = imgLeft;
			this.imgRight = imgRight;
			normalLeft = imgLeft.Image;
			normalRight = imgRight.Image;

			CreateBrightImages();
		}

		public void SelectLeft()
		{
			imgLeft.Image = normalLeft;
			imgRight.Image = brightRight;
		}

		public void SelectRight()
		{
			imgLeft.Image = brightLeft;
			imgRight.Image = normalRight;
		}

		private void CreateBrightImages()
		{
			var controls = new CIColorControls () {
				Image = CIImage.FromCGImage(normalLeft.CGImage)
			};

			controls.Brightness = 0.9f;
			var output = controls.OutputImage;
			var context = CIContext.FromOptions (null);
			var result = context.CreateCGImage (output, output.Extent);
			brightLeft = UIImage.FromImage(result);

			controls = new CIColorControls () {
				Image = CIImage.FromCGImage(normalRight.CGImage)
			};

			controls.Brightness = 0.9f;
			output = controls.OutputImage;
			context = CIContext.FromOptions (null);
			result = context.CreateCGImage (output, output.Extent);
			brightRight = UIImage.FromImage(result);
		}

		private UIImageView imgLeft;
		private UIImageView imgRight;
		private UIImage brightLeft;
		private UIImage brightRight;
		private UIImage normalLeft;
		private UIImage normalRight;
	}
}

