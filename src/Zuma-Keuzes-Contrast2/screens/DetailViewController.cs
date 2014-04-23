using System;
using System.Drawing;
using MonoTouch.AssetsLibrary;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.IO;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;
using Lisa.Zuma;

namespace ZumaKeuzesContrast2
{
	public partial class DetailViewController : UIViewController
	{
		public DetailViewController (UINavigationController navigationController) : base ()
		{
			this.navigationController = navigationController;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnSaveProfile.Hidden = true;
			btnSetLeftSnd.Hidden = true;
			btnSetRightSnd.Hidden = true;
			btnSaveProfile.Hidden = true;
			txtProfileName.Hidden = true;

			btnSetLeftImage.TouchUpInside += SetNewProfileImage;
			btnSetRightImage.TouchUpInside += SetNewProfileImage;

			btnSetLeftSnd.TouchUpInside += RecordNewProfileSnd;
			btnSetRightSnd.TouchUpInside += RecordNewProfileSnd;

			btnPlayLeftSnd.TouchUpInside += PlaySnd;
			btnPlayRightSnd.TouchUpInside += PlaySnd;
		}

		public void RefreshDetialView(int Row)
		{
			vwHidden.Hidden = true;
			btnSetLeftSnd.Hidden = true;
			btnSetRightSnd.Hidden = true;
			_row = Row + 1;

			databaseRow = queryProfile.returnProfileRow(_row);

			UIImage ImgLeft = UIImage.FromFile (databaseRow[1]);
			UIImage ImgRight = UIImage.FromFile (databaseRow[2]);
			imvLeft.Image = ImgLeft;
			imvRight.Image = ImgRight;

			DatabaseRequests.StoreMenuSettings (0, 5, 5, databaseRow [5]);
		}

		public void CreateEmptyProfile()
		{
			vwHidden.Hidden = true;
			isNewProfile = true;

			btnSetLeftImage.Hidden = false;
			btnSetRightImage.Hidden = false;
			btnSetLeftSnd.Hidden = false;
			btnSetRightSnd.Hidden = false;
			btnSaveProfile.Hidden = false;
			txtProfileName.Hidden = false;

			UIImage ImgLeft = UIImage.FromFile ("images/empty.png");
			UIImage ImgRight = UIImage.FromFile ("images/empty.png");
			imvLeft.Image = ImgLeft;
			imvRight.Image = ImgRight;
		}

		public void SetBackCreateNewProfile ()
		{
			txtProfileName.Hidden = true;
			btnSetLeftImage.Hidden = true;
			btnSetRightImage.Hidden = true;
			btnSetLeftSnd.Hidden = true;
			btnSetRightSnd.Hidden = true;
			btnSaveProfile.Hidden = true;
		}

		private void PlaySnd(object sender, EventArgs args)
		{
			if (sender == btnPlayLeftSnd) 
			{
				recordSound.PlayTempAudio (true);
			} 
			else if (sender == btnPlayRightSnd) 
			{
				recordSound.PlayTempAudio (false);
			} 
		}

		private void SetNewProfileImage(object sender, EventArgs args)
		{
			imagePicker = new UIImagePickerController ();

			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;

			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);

			imagePicker.FinishedPickingMedia += Handle_FinnishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;

			View.AddSubview (imagePicker.View);

			if (sender == btnSetLeftImage) 
			{
				isSide = (int)side.left;
			} 
			else if (sender == btnSetRightImage) 
			{
				isSide = (int)side.right;
				Console.WriteLine (isSide.ToString());
			}
		}

		private void Handle_FinnishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			// determine what was selected, video or image
			bool isImage = false;
			switch(e.Info[UIImagePickerController.MediaType].ToString())
			{
			case "public.image":
				isImage = true;
				break;

			case "public.video":
				break;
			}

			Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");

			// get common info (shared between images and video)
			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
			if (referenceURL != null) 
				Console.WriteLine(referenceURL.ToString ());

			// if it was an image, get the other image info
			if(isImage) {

				// get the original image
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if(originalImage != null) {
					if (isSide == 0) 
					{
						imvLeft.Image = originalImage;
						imagePicker.View.RemoveFromSuperview ();
					} 
					else if (isSide == 1) 
					{
						imvRight.Image = originalImage;
						imagePicker.View.RemoveFromSuperview ();
					}
				}

				// get the edited image
				UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
				if(editedImage != null) {
					if (isSide == 0) 
					{
						imvLeft.Image = editedImage;
						imagePicker.View.RemoveFromSuperview ();
					} 
					else if (isSide == 1) 
					{
						imvRight.Image = editedImage;
						imagePicker.View.RemoveFromSuperview ();
					}
				}

				//- get the image metadata
				NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
				if(imageMetadata != null) {
				}

			}
			// if it's a video
			else {
				// get video url
				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
				if(mediaURL != null) {
					//
					Console.WriteLine(mediaURL.ToString());
				}
			}

			// dismiss the picker
			imagePicker.DismissModalViewControllerAnimated (true);
		}

		private void Handle_Canceled(object sender, EventArgs e)
		{
			imagePicker.DismissModalViewControllerAnimated(true);
		}

		private void RecordNewProfileSnd(object sender, EventArgs args)
		{
			if (isRecording && sender == btnSetLeftSnd) 
			{
				btnSetLeftSnd.SetTitle ("stop", UIControlState.Normal);
				recordSound.StartRecording (true);
				isRecording = false;
				Console.WriteLine (isRecording);
			} 
			else if (isRecording && sender == btnSetRightSnd) 
			{
				btnSetRightSnd.SetTitle ("stop", UIControlState.Normal);
				recordSound.StartRecording (false);
				isRecording = false;
				Console.WriteLine (isRecording);
			} 
			else if (!isRecording && sender == btnSetLeftSnd) 
			{
				btnSetLeftSnd.SetTitle ("Record", UIControlState.Normal);
				recordSound.StopRecording ();
				isRecording = true;
				Console.WriteLine (isRecording);
			}
			else if (!isRecording && sender == btnSetRightSnd) 
			{
				btnSetRightSnd.SetTitle ("Record", UIControlState.Normal);
				recordSound.StopRecording ();
				isRecording = true;
				Console.WriteLine (isRecording);
			}
		}

		private string[] databaseRow = new string[5];
		private int _row, isSide;
		private bool isRecording = true, isNewProfile;
		Sound profileSnd = new Sound();
		QueryProfile queryProfile = new QueryProfile();
		UIImagePickerController imagePicker;
		private UINavigationController navigationController;
		RecordSound recordSound = new RecordSound();


		enum side 
		{
			left,
			right
		}
	}
}