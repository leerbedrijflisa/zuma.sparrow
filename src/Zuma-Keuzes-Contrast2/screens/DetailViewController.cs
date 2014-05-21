using System;
using System.Collections.Generic;
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
		public DetailViewController () : base ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			RefreshDetialView (0);

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

			btnSaveProfile.TouchUpInside += SaveOrRemoveProfile;
		}

		public void RefreshDetialView(int Row)
		{
			vwHidden.Hidden = true;
			btnSetLeftSnd.Hidden = true;
			btnSetRightSnd.Hidden = true;
			profileRow = dataHelper.returnProfileRow(Row);

			if (profileRow [6] == "0") 
			{
				btnSaveProfile.Hidden = false;
				btnSaveProfile.SetTitle ("Profiel verwijderen", UIControlState.Normal);
				leftAssetUrl = NSUrl.FromString(profileRow[1]);
				rightAssetUrl = NSUrl.FromString(profileRow [2]);
				library.AssetForUrl(leftAssetUrl, (asset)=>{imvLeft.Image = new UIImage(asset.DefaultRepresentation.GetImage());}, (failure)=>{});
				library.AssetForUrl(rightAssetUrl, (asset)=>{imvRight.Image = new UIImage(asset.DefaultRepresentation.GetImage());}, (failure)=>{});
			} 
			else if (profileRow [6] == "1") 
			{
				btnSaveProfile.Hidden = true;
				UIImage ImgLeft = UIImage.FromFile (profileRow[1]);
				UIImage ImgRight = UIImage.FromFile (profileRow[2]);
				imvLeft.Image = ImgLeft;
				imvRight.Image = ImgRight;
			}

			dataHelper.StoreMenuSettings (0, 5, 5, profileRow [7]);
		}

		public void CreateEmptyProfile()
		{
			vwHidden.Hidden = true;
			isNewProfile = true;

			btnPlayLeftSnd.Hidden = true;
			btnPlayRightSnd.Hidden = true;
			btnSetLeftImage.Hidden = false;
			btnSetRightImage.Hidden = false;
			btnSetLeftSnd.Hidden = false;
			btnSetRightSnd.Hidden = false;
			btnSaveProfile.Hidden = false;
			txtProfileName.Hidden = false;

			btnSaveProfile.SetTitle ("Bewaar Profiel", UIControlState.Normal);

			UIImage ImgLeft = UIImage.FromFile ("images/empty.png");
			UIImage ImgRight = UIImage.FromFile ("images/empty.png");
			imvLeft.Image = ImgLeft;
			imvRight.Image = ImgRight;

			this.View.ExclusiveTouch = true;
		}

		public void SetBackCreateNewProfile ()
		{
			txtProfileName.Hidden = true;
			btnSetLeftImage.Hidden = true;
			btnSetRightImage.Hidden = true;
			btnSetLeftSnd.Hidden = true;
			btnSetRightSnd.Hidden = true;
			isNewProfile = false;
			lblNameRequired.Text = "";
		}

		private void PlaySnd (object sender, EventArgs args)
		{
			if (isNewProfile) 
			{
				if (sender == btnPlayLeftSnd) 
				{
					profileSnd.Play (leftSndPath);
				} 
				else if (sender == btnPlayRightSnd) 
				{
					profileSnd.Play (rightSndPath);
				}
			} 
			else if (!isNewProfile) 
			{
				if (sender == btnPlayLeftSnd) 
				{
					profileSnd.Play (profileRow [3]);
				} 
				else if (sender == btnPlayRightSnd) 
				{
					profileSnd.Play (profileRow [4]);
				}
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
						library.WriteImageToSavedPhotosAlbum (originalImage.CGImage,meta, (assetUrl, error) =>
						{
							Console.WriteLine ("assetUrl:"+assetUrl);
							leftAssetUrl = assetUrl;
						});

						imvLeft.Image = originalImage;
						imagePicker.View.RemoveFromSuperview ();
					} 
					else if (isSide == 1) 
					{
						library.WriteImageToSavedPhotosAlbum(originalImage.CGImage,meta, (assetUrl, error) =>
							{
								Console.WriteLine ("assetUrl:"+assetUrl);
								rightAssetUrl = assetUrl;
							});

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
		}

		private void Handle_Canceled(object sender, EventArgs e)
		{
			Console.WriteLine("dismiss");
			imagePicker.DismissViewController(true, () => {});
			imagePicker.View.RemoveFromSuperview ();
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
				btnPlayLeftSnd.Hidden = false;
				btnSetLeftSnd.SetTitle ("record", UIControlState.Normal);
				leftSndPath = recordSound.StopRecording (true);
				isRecording = true;
				Console.WriteLine (isRecording);
			}
			else if (!isRecording && sender == btnSetRightSnd) 
			{
				btnPlayRightSnd.Hidden = false;
				btnSetRightSnd.SetTitle ("record", UIControlState.Normal);
				rightSndPath = recordSound.StopRecording (false);
				isRecording = true;
				Console.WriteLine (isRecording);
			}
		}

		private void SaveOrRemoveProfile(object sender, EventArgs args)
		{
			if (btnSetLeftSnd.Hidden == false) {
				string storeName = txtProfileName.Text;
				if (storeName.Length != 0 && leftAssetUrl != null && rightAssetUrl != null && leftSndPath != null && rightSndPath != null) {
					var ProfileNames = dataHelper.ReadProfilesNames ();
					var rows = ProfileNames.Count;
					dataHelper.StoreNewProfile (storeName, leftAssetUrl, rightAssetUrl, leftSndPath, rightSndPath, rows);
					masterViewController.ProfileSaved ();
					SetBackCreateNewProfile ();
					btnSaveProfile.Hidden = true;
					RefreshDetialView (rows);
				} else {
					lblNameRequired.Text = "Er zijn velden niet ingevuld.";
				}
			} else {
				dataHelper.RemoveProfile (profileRow[7]);
				RefreshDetialView (0);
			}
		}

		private string[] profileRow = new string[6];
		private int isSide;
		private bool isRecording = true, isNewProfile;
		private string leftSndPath, rightSndPath;

		Sound profileSnd = new Sound();
		UIImagePickerController imagePicker;
		RecordSound recordSound = new RecordSound();
		NSDictionary meta = new NSDictionary ();
		ALAssetsLibrary library = new ALAssetsLibrary();
		DataHelper dataHelper = new DataHelper ();

		MasterViewController masterViewController = new MasterViewController();

		NSUrl leftAssetUrl, rightAssetUrl;

		enum side 
		{
			left,
			right
		}
	}
}