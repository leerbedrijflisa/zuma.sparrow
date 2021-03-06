using System;
using System.Drawing;
using MonoTouch.AssetsLibrary;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreImage;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreMotion;
using Lisa.Zuma;
using MonoTouch.ObjCRuntime;

namespace ZumaKeuzesContrast2
{
	public partial class MainViewController : UIViewController
	{
		public MainViewController () : base () {}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.ExclusiveTouch = true;
			View.MultipleTouchEnabled = false;
			View.BackgroundColor = UIColor.White;

			NavigationController.SetNavigationBarHidden(true, true);
			UIApplication.SharedApplication.SetStatusBarHidden (true, true);

			SetProfile ();
			ScreenReturnToMenu ();
			pushed = false;

			InitializeUI ();

			//Create imageViews
			PositionControls (InterfaceOrientation);

			switch (selectedButtonSetting){
			case "0":
				lowDifficultySwitchingChoices ();
				btnChoice.TouchUpInside += CreateButtonChoice;
				break;
			case "1":
				btnChoiceLeft.TouchUpInside += CreateDoubleButtonChoice;
				btnChoiceRight.TouchUpInside += CreateDoubleButtonChoice;
				break;
			}
		}

		/// <summary>
		/// When the device rotates, the OS calls this method to determine if it should try and rotate the
		/// application and then call WillAnimateRotation
		/// </summary>
		[Obsolete("Deprecated in iOS6. Replace it with both GetSupportedInterfaceOrientations and PreferredInterfaceOrientationForPresentation", false)]
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// we're passed to orientation that it will rotate to. in our case, we could
			// just return true, but this switch illustrates how you can test for the 
			// different cases
			switch (toInterfaceOrientation)
			{
			case UIInterfaceOrientation.LandscapeLeft:
			case UIInterfaceOrientation.LandscapeRight:
			case UIInterfaceOrientation.Portrait:
			case UIInterfaceOrientation.PortraitUpsideDown:
			default:
				return true;
			}
		}

		/// <summary>
		/// is called when the OS is going to rotate the application. It handles rotating the status bar
		/// if it's present, as well as it's controls like the navigation controller and tab bar, but you 
		/// must handle the rotation of your view and associated subviews. This call is wrapped in an 
		/// animation block in the underlying implementation, so it will automatically animate your control
		/// repositioning.
		/// </summary>
		public override void WillAnimateRotation (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillAnimateRotation (toInterfaceOrientation, duration);

			// call our helper method to position the controls
			PositionControls (toInterfaceOrientation);
		}

		public override bool PrefersStatusBarHidden ()
		{
			return true;
		}

		/// <summary>
		/// A helper method to position the controls appropriately, based on the 
		/// orientation
		/// </summary>
		protected void PositionControls (UIInterfaceOrientation toInterfaceOrientation)
		{
			// depending one what orientation we start in, we want to position our controls
			// appropriately
			clearImageIMV (imvChoiceLeft);
			clearImageIMV (imvChoiceRight);
			switch (toInterfaceOrientation) {
			// if we're switchign to landscape
			case UIInterfaceOrientation.LandscapeLeft:
			case UIInterfaceOrientation.LandscapeRight:

				imvChoiceLeft = new UIImageView (new RectangleF (50, 250, 412, 274));
				imvChoiceRight = new UIImageView (new RectangleF (562, 250, 412, 274));
				FilterRotation = "landscape";

				break;

				// we're switch back to portrait
			case UIInterfaceOrientation.Portrait:
			case UIInterfaceOrientation.PortraitUpsideDown:

				imvChoiceLeft = new UIImageView (new RectangleF (175, 100, 412, 274));
				imvChoiceRight = new UIImageView (new RectangleF (175, 612, 412, 274));
				FilterRotation = "portrait";

				break;
			}
			if (profile [6] == "0") {
				var leftAssetUrl = NSUrl.FromString(profile[1]);
				var rightAssetUrl = NSUrl.FromString(profile [2]);
				library.AssetForUrl(leftAssetUrl, (asset)=>{imvChoiceLeft.Image = new UIImage(asset.DefaultRepresentation.GetImage());}, (failure)=>{});
				library.AssetForUrl(rightAssetUrl, (asset)=>{imvChoiceRight.Image = new UIImage(asset.DefaultRepresentation.GetImage());}, (failure)=>{});
			} else if (profile [6] == "1") {
				imvChoiceLeft.Image = UIimageOne;
				imvChoiceRight.Image = UIimageTwo;
			}

			View.AddSubview (imvChoiceLeft);
			View.AddSubview (imvChoiceRight);

			SelectBtnDifficulty ();
		}

		public override void ViewWillAppear (bool animated) {
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, animated);
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			this.NavigationController.SetNavigationBarHidden (false, animated);
		}

		private void CreateButtonChoice(object sender, EventArgs args)
		{
			switch (soundSelect){
			case "left":
				profileSound.Play(soundOne);
				break;
			case "right":
				profileSound.Play (soundTwo);
				break;
			}
			SwitchingChoices.Dispose();
			btnChoice.Enabled = false;

			switch (count) {
			case 0:
				blackOutTimer = NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_clickTimer), delegate {
					blackOutPart();
					NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_darkTimer), resetbtnForLowDifficulty);
				});
				break;
			case 1:
				blackOutTimer = NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_clickTimer), delegate {
					blackOutPart();
					NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_darkTimer), resetbtnForLowDifficulty);
				});
				break;
			}
		}

		private void CreateDoubleButtonChoice(object sender, EventArgs args)
		{
			if (sender == btnChoiceLeft) {
				profileSound.Play (soundOne);
				imvChoiceRight.Image = empty;
			} else if (sender == btnChoiceRight) {
				profileSound.Play (soundTwo);
				imvChoiceLeft.Image = empty;
			}

			btnChoiceRight.Enabled = false;
			btnChoiceLeft.Enabled = false;

			blackOutTimer = NSTimer.CreateScheduledTimer (TimeSpan.FromSeconds (_clickTimer), delegate {
				BlackOutHighDifficulty ();
				resetbtnForHighDifficulty ();
			});

			right_pt = imvChoiceRight.Center;
			left_pt = imvChoiceLeft.Center;
			startAnimation (sender);
		}

//		[Export("slideAnimationFinished:")]
//		public void SlideStopped (NSObject obj)
//		{
//			imvChoiceRight.Center = right_pt;
//			imvChoiceLeft.Center = left_pt;
//		}

		private void startAnimation(object _sender)
		{
			if (_sender == btnChoiceLeft && FilterRotation == "landscape") {
				UIView.Animate (_clickTimer /2, 0, UIViewAnimationOptions.CurveEaseInOut | UIViewAnimationOptions.Autoreverse,
					() => {
						float x = UIScreen.MainScreen.Bounds.Right - imvChoiceLeft.Frame.Width / 2;
						if(UIScreen.MainScreen.Bounds.Right > UIScreen.MainScreen.Bounds.Bottom)
						{
							x = UIScreen.MainScreen.Bounds.Bottom - imvChoiceLeft.Frame.Width / 2;
						}
						imvChoiceLeft.Center = new PointF (x, imvChoiceLeft.Center.Y);
//						imvChoiceLeft.Center = 
//							new PointF (UIScreen.MainScreen.Bounds.Right - imvChoiceLeft.Frame.Width / 2, 
//							imvChoiceLeft.Center.Y);
						NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_clickTimer-0.1), delegate {
							TimerSetImage(_sender);
						});
					}, 
					() => {
						if(FilterRotation == "landscape")
						{
							imvChoiceLeft.Center = left_pt;
						} else if (FilterRotation == "portrait")
						{
							imvChoiceLeft.Center = new PointF(381, 237);
						}
					}
				);
			} else if (_sender == btnChoiceRight && FilterRotation == "landscape") {
				UIView.Animate (_clickTimer /2, 0, UIViewAnimationOptions.CurveEaseInOut | UIViewAnimationOptions.Autoreverse,
					() => {
						float x = UIScreen.MainScreen.Bounds.Right - imvChoiceLeft.Frame.Width / 2;
						if(UIScreen.MainScreen.Bounds.Right > UIScreen.MainScreen.Bounds.Bottom)
						{
							x = UIScreen.MainScreen.Bounds.Bottom - imvChoiceLeft.Frame.Width / 2;
						}
						imvChoiceRight.Center = new PointF (x, imvChoiceLeft.Center.Y);
						NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_clickTimer-0.1), delegate {
							TimerSetImage(_sender);
						});
					}, 
					() => {
						if(FilterRotation == "landscape")
						{
							imvChoiceRight.Center = right_pt;
						} else if (FilterRotation == "portrait")
						{
							imvChoiceRight.Center = new PointF (381, 749);
						}
					}
				);
			} else if (_sender == btnChoiceLeft && FilterRotation == "portrait") {
				UIView.Animate (_clickTimer /2, 0, UIViewAnimationOptions.CurveEaseInOut | UIViewAnimationOptions.Autoreverse,
					() => {
						imvChoiceLeft.Center = 
							new PointF (imvChoiceLeft.Center.X, 
								UIScreen.MainScreen.Bounds.Right - imvChoiceLeft.Frame.Width / 2 );
						NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_clickTimer-0.1), delegate {
							TimerSetImage(_sender);
						});
					}, 
					() => {
						if(FilterRotation == "portrait")
						{
							imvChoiceLeft.Center = left_pt;
						} else if (FilterRotation == "landscape")
						{
							imvChoiceLeft.Center = new PointF (256, 387);
						}
					}
				);
			} else if (_sender == btnChoiceRight && FilterRotation == "portrait") {
				UIView.Animate (_clickTimer /2, 0, UIViewAnimationOptions.CurveEaseInOut | UIViewAnimationOptions.Autoreverse,
					() => {
						imvChoiceRight.Center = 
							new PointF (imvChoiceLeft.Center.X, 
								UIScreen.MainScreen.Bounds.Right - imvChoiceRight.Frame.Width / 2 );
						NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_clickTimer-0.1), delegate { 
							TimerSetImage(_sender);
						});
					}, 
					() => {
						if(FilterRotation == "portrait")
						{
							imvChoiceRight.Center = right_pt;
						} else if (FilterRotation == "landscape")
						{
							imvChoiceRight.Center = new PointF (768, 387);
						}
					}
				);
			}
		}

		private void TimerSetImage(object sender)
		{
			if (sender == btnChoiceLeft) {
				imvChoiceLeft.Center = left_pt;
			} else if (sender == btnChoiceRight) {
				imvChoiceRight.Center = right_pt;
			}
		}

		private void resetbtnForHighDifficulty()
		{
			blackOutTimer.Dispose();
			btnChoiceLeft.Enabled = true;
			btnChoiceRight.Enabled = true;
			if (profile [6] == "0") {
				var leftAssetUrl = NSUrl.FromString(profile[1]);
				var rightAssetUrl = NSUrl.FromString(profile [2]);
				library.AssetForUrl(leftAssetUrl, (asset)=>{imvChoiceLeft.Image = new UIImage(asset.DefaultRepresentation.GetImage());}, (failure)=>{});
				library.AssetForUrl(rightAssetUrl, (asset)=>{imvChoiceRight.Image = new UIImage(asset.DefaultRepresentation.GetImage());}, (failure)=>{});
			} else if (profile [6] == "1") {
				imvChoiceLeft.Image = UIimageOne;
				imvChoiceRight.Image = UIimageTwo;
			}
		}

		private void resetbtnForLowDifficulty()
		{
			blackOutTimer.Dispose ();
			btnChoice.Enabled = true;
			imvLayerLeft.Image = empty;
			imvLayerRight.Image = empty;
			lowDifficultySwitchingChoices ();
		}

		private void lowDifficultySwitchingChoices()
		{
			count = 0;
			leftFilterDark ("On", FilterRotation);
			rightFilterDark ("Off", FilterRotation);

			blackout = "right";
			soundSelect = "right";

			SwitchingChoices = NSTimer.CreateRepeatingScheduledTimer (TimeSpan.FromSeconds(5), delegate {
				switch (count)
				{
					case 0:
					count++;
					imvLayerLeft.Image = empty;
					rightFilterDark ("On", FilterRotation);
					blackout = "left";
					soundSelect = "left";
					break;

					case 1:
					count--;
					imvLayerRight.Image = empty;
					leftFilterDark("On", FilterRotation);
					blackout = "right";
					soundSelect = "right";
					break;
				}
			});
		}

		private void SelectBtnDifficulty()
		{
			if (selectedButtonSetting == "0") {

				if (FilterRotation == "landscape") {
					btnChoice.Frame = new RectangleF (0, 0, 1024, 768);
				} else if (FilterRotation == "portrait") {
					btnChoice.Frame = new RectangleF (0, 0, 768, 1024);
				}

			} else if (selectedButtonSetting == "1") {
			
				if (FilterRotation == "landscape") {
					btnChoiceLeft.Frame = new RectangleF (0, 0, 512, 768);
					btnChoiceRight.Frame = new RectangleF (512, 0, 512, 768);
				} else if (FilterRotation == "portrait") {
					btnChoiceLeft.Frame = new RectangleF (0, 0, 768, 512);
					btnChoiceRight.Frame = new RectangleF (0, 512, 768, 512);
				}	

			}
		}

		private void leftFilterDark(string Switch, string setFilterRotation)
		{
			switch (setFilterRotation){
			case "landscape":
				imvLayerLeft = new UIImageView (new RectangleF (0, 0, 512, 768));
				break;
			case "portrait":
				imvLayerLeft = new UIImageView (new RectangleF (0, 0, 768, 512));
				break;
			}
			switch (Switch){
			case "On":
				imvLayerLeft.Image = filterImage;
				break;
			case "Off":
				imvLayerLeft.Image = empty;
				break;
			}
			View.AddSubview (imvLayerLeft);
		}

		private void rightFilterDark(string Switch, string setFilterRotation)
		{
			switch (setFilterRotation){ 
			case "landscape":
							imvLayerRight = new UIImageView (new RectangleF (512, 0, 512, 768));
				break;
			case "portrait":
							imvLayerRight = new UIImageView (new RectangleF (0, 512, 768, 512));
				break;
			}
			switch (Switch){
			case "On":
				imvLayerRight.Image = filterImage;
				break;
			case "Off":
				imvLayerRight.Image = empty;
				break;
			}
			View.AddSubview (imvLayerRight);
		}

		private void blackOutPart()
		{
			switch (count){
			case 0:
				rightFilterDark ("On", FilterRotation);
				break;
			case 1:
				leftFilterDark ("On", FilterRotation);
				break;
			}
		}

		private void BlackOutHighDifficulty()
		{
			switch (blackout){
			case "left":
				leftFilterDark ("On", FilterRotation);
				break;
			case "right":
				rightFilterDark ("On", FilterRotation);
				break;
			}
		}

		private void PushMainMenu()
		{
			if(mainMenu == null)
			{
				mainMenu = new MainMenu();
			}

			if (pushed == false) {
				NavigationController.PushViewController (mainMenu, false);
				pushed = true;
			}
		}

		private CMMotionManager _motionManager;

		private void ScreenReturnToMenu()
		{
			_motionManager = new CMMotionManager ();
			_motionManager.StartAccelerometerUpdates (NSOperationQueue.CurrentQueue, (data, error) => {
				if (data.Acceleration.Z > 0.890) {
					PushMainMenu ();
				}
			});
		}

		private void clearImageIMV(UIImageView imvImageCleared)
		{
			if (imvImageCleared != null) {
				imvImageCleared.Image = empty;
			}
		}

		private void InitializeUI()
		{
			filterImage = UIImage.FromFile ("images/layer_transparent.png");
			empty = UIImage.FromFile ("");

			switch (selectedButtonSetting){
			case "0":
				btnChoice = UIButton.FromType (UIButtonType.RoundedRect);
				View.AddSubview (btnChoice);
				break;
			case "1":
				btnChoiceLeft = UIButton.FromType (UIButtonType.RoundedRect);
				btnChoiceRight = UIButton.FromType (UIButtonType.RoundedRect);
				View.AddSubview (btnChoiceLeft);
				View.AddSubview (btnChoiceRight);
				btnChoiceRight.ExclusiveTouch = true;
				btnChoiceLeft.ExclusiveTouch = true;
				break;
			}

			UIimageOne = UIImage.FromFile (imageOne);
			UIimageTwo = UIImage.FromFile (imageTwo);

		}

		private void SetProfile()
		{
			menuSettings = dataHelper.ReadMenuSettings ();

			selectedProfile = menuSettings [3];
			_selectedProfile = Convert.ToInt32 (selectedProfile);

			profile = dataHelper.returnProfileRow (_selectedProfile);

			imageOne = profile [1];
			imageTwo = profile [2];
			soundOne = profile [3];
			soundTwo = profile [4];

			selectedButtonSetting = menuSettings[0];
			clickTimer = menuSettings [1];
			darkTimer = menuSettings [2];
			_clickTimer = Convert.ToInt32 (clickTimer);
			_darkTimer = Convert.ToInt32 (darkTimer);
		}

		UIButton btnChoice, btnChoiceLeft, btnChoiceRight;
		UIImageView imvChoiceLeft, imvChoiceRight, imvLayerLeft, imvLayerRight;
		UIImage UIimageOne, UIimageTwo, filterImage, empty;
		NSTimer SwitchingChoices, blackOutTimer;
		Sound profileSound = new Sound ();
		MainMenu mainMenu;
		DataHelper dataHelper = new DataHelper ();
		ALAssetsLibrary library = new ALAssetsLibrary();
	
		private string blackout, soundSelect, FilterRotation;
		private string imageOne, imageTwo, soundOne, soundTwo, selectedProfile, selectedButtonSetting, clickTimer, darkTimer; 
		private int count, _selectedProfile, _darkTimer;
		private bool pushed = true;
		private string[] menuSettings = new string[4], profile = new string[6];
		private float _clickTimer;
		private PointF right_pt, left_pt;
	}
}