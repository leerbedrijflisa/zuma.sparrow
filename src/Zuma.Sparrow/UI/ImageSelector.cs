using System;

using MonoTouch.UIKit;

namespace Zuma.Sparrow
{
	public class ImageSelector
	{
		public ImageSelector()
		{
		}

		public void Handle_FinnishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
			imagePicker.View.RemoveFromSuperview ();

		}

		public void Handle_Canceled(object sender, EventArgs e)
		{
			Console.WriteLine("dismiss");
			imagePicker.DismissViewController(true, () => {});
			imagePicker.View.RemoveFromSuperview ();
			originalImage = UIImage.FromFile("empty.png");
		}


	}
}

