using System;
using System.Drawing;
using MonoTouch.AssetsLibrary;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreImage;
using MonoTouch.CoreMotion;
using Lisa.Zuma;

namespace ZumaKeuzesContrast2
{
	public partial class MainViewController4 : UIViewController
	{
		public MainViewController4 (QueryProfile queryProfile) : base ()
		{
			this.queryProfile = queryProfile;
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationController.SetNavigationBarHidden(true, true);
			UIApplication.SharedApplication.SetStatusBarHidden (true, true);

			SetProfile ();
			ScreenReturnToMenu ();

			pushed = false;

			InitializeUI ();

			//Create imageViews
			PositionControls (InterfaceOrientation);

			if (selectedButtonSetting == "0") {
				lowDifficultyfillFilteringChoices ();
				btnChoice.TouchUpInside += CreateButtonChoice;
			} 
				
			else if (selectedButtonSetting == "1") {
				btnChoiceLeft.TouchUpInside += CreateDoubleButtonChoice;
				btnChoiceRight.TouchUpInside += CreateDoubleButtonChoice;
			}
		}

		/// <summary>
		/// When the device rotates, the OS calls this method to determine if it should try and rotate the
		/// application and then call WillAnimateRotation
		/// </summary>
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
			switch (toInterfaceOrientation) {
				// if we're switchign to landscape
			case UIInterfaceOrientation.LandscapeLeft:
			case UIInterfaceOrientation.LandscapeRight:

				clearImageIMV (imvChoiceLeft);
				clearImageIMV (imvChoiceRight);

				imvChoiceLeft = new UIImageView (new RectangleF (50, 250, 412, 274));
				imvChoiceRight = new UIImageView (new RectangleF (562, 250, 412, 274));
				FilterRotation = setFilterRotation.landscape;

					imvChoiceLeft.Image = UIimageOne;
					View.AddSubview (imvChoiceLeft);

					imvChoiceRight.Image = UIimageTwo;
					View.AddSubview (imvChoiceRight);

				break;

				// we're switch back to portrait
			case UIInterfaceOrientation.Portrait:
			case UIInterfaceOrientation.PortraitUpsideDown:

				clearImageIMV (imvChoiceLeft);
				clearImageIMV (imvChoiceRight);

				imvChoiceLeft = new UIImageView (new RectangleF (175, 100, 412, 274));
				imvChoiceRight = new UIImageView (new RectangleF (175, 612, 412, 274));
				 FilterRotation = setFilterRotation.landscape;

					imvChoiceLeft.Image = UIimageOne;
					View.AddSubview (imvChoiceLeft);

					imvChoiceRight.Image = UIimageTwo;
					View.AddSubview (imvChoiceRight);

				break;
			}
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

		private void resetbtnForHighDifficulty()
		{
			blackOutTimer.Dispose();
			btnChoiceLeft.Enabled = true;
			btnChoiceRight.Enabled = true;
			sideSet = Side.Both;
			darkFilter ();
		}

		private void resetbtnForLowDifficulty()
		{
			filledFilter = fillFilter.OFF;
			blackOutTimer.Dispose ();
			btnChoice.Enabled = true;
			imvLayerLeft.Image = empty;
			imvLayerRight.Image = empty;
			View.AddSubview (imvLayerRight);
			View.AddSubview (imvLayerRight);
//			lowDifficultyfillFilteringChoices ();
		}

		private void lowDifficultyfillFilteringChoices()
		{
//			sideSet = Side.Right;
//			filledFilter = fillFilter.OFF;
//			soundSelect = "right";
			darkFilter ();
			count = 0;


			fillFilteringChoices = NSTimer.CreateRepeatingScheduledTimer (TimeSpan.FromSeconds(5), delegate {
				switch(count)
				{
					case 0:
					count++;
					imvLayerLeft.Image = empty;
					sideSet = Side.Left;
					soundSelect = "left";
					darkFilter();
					break;

					case 1:
					Console.WriteLine(count.ToString() + " count");
					count--;
					imvLayerRight.Image = empty;
					sideSet = Side.Right;
					soundSelect = "right";
					darkFilter();
					break;
				}
			});
		}

		private void SelectBtnDifficulty()
		{
			switch(selectedButtonSetting + " && " + FilterRotation)
			{
				case "0 && landscape":
					btnChoice.Frame = new RectangleF (0, 0, 1024, 768);
				break;
				case "0 && portrait":
					btnChoice.Frame = new RectangleF (0, 0, 768, 1024);
				break;
				case "1 && landscape":
					btnChoiceLeft.Frame = new RectangleF (0, 0, 512, 768);
					btnChoiceRight.Frame = new RectangleF (512, 0, 512, 768);
				break;
				case "1 && portrait":
					btnChoiceLeft.Frame = new RectangleF (0, 0, 768, 512);
					btnChoiceRight.Frame = new RectangleF (0, 512, 768, 512);
				break;
			}
		}

		private void darkFilter()
		{
			Console.WriteLine (filledFilter.ToString() + " && " + sideSet.ToString());

			if (FilterRotation == setFilterRotation.landscape) 
			{
				imvLayerLeft = new UIImageView (new RectangleF (0, 0, 512, 768));
				imvLayerRight = new UIImageView (new RectangleF (512, 0, 512, 768));
			} 
			else if (FilterRotation == setFilterRotation.portrait) 
			{
				imvLayerLeft = new UIImageView (new RectangleF (0, 0, 768, 512));
				imvLayerRight = new UIImageView (new RectangleF (0, 512, 768, 512));
			}
 
			if (sideSet != Side.Left && filledFilter == fillFilter.OFF) 
			{
				imvLayerLeft.Image = filterImage;
			}
		
			if (sideSet != Side.Right && filledFilter == fillFilter.OFF) 
			{
				imvLayerRight.Image = filterImage;
			}

			if (filledFilter == fillFilter.ON) {
				if (sideSet == Side.Left) {
					imvLayerLeft.Image = filterImage;
				}
				else if (sideSet == Side.Right) {
					imvLayerRight.Image = filterImage;
				}
			}

			filledFilter = fillFilter.OFF;

			View.AddSubview (imvLayerLeft);
			View.AddSubview (imvLayerRight);
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

			if (selectedButtonSetting == "0") {
				btnChoice = UIButton.FromType (UIButtonType.RoundedRect);

				View.AddSubview (btnChoice);
			} else if (selectedButtonSetting == "1") {
				btnChoiceLeft = UIButton.FromType (UIButtonType.RoundedRect);
				btnChoiceRight = UIButton.FromType (UIButtonType.RoundedRect);

				View.AddSubview (btnChoiceLeft);
				View.AddSubview (btnChoiceRight);
			}

			UIimageOne = UIImage.FromFile (imageOne);
			UIimageTwo = UIImage.FromFile (imageTwo);
		}

		private void SetProfile()
		{
			menuSettings = queryProfile.ReadMenuSettings ();

			selectedProfile = menuSettings [3];
			_selectedProfile = Convert.ToInt32 (selectedProfile);

			profile = queryProfile.returnProfileRow (_selectedProfile);

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

		private void CreateButtonChoice(object sender, EventArgs args)
		{
			if(soundSelect == "left")
			{
				profileSound.Play(soundOne);
			}
			else if(soundSelect == "right")
			{
				profileSound.Play(soundTwo);
			}
			fillFilteringChoices.Dispose();

			Console.WriteLine (sideSet.ToString () + " sideSet");
			btnChoice.Enabled = false;
			blackOutTimer = NSTimer.CreateScheduledTimer (TimeSpan.FromSeconds (_clickTimer), BlackOutLowDifficulty);
		}

		private void CreateDoubleButtonChoice(object sender, EventArgs args)
		{
			btnChoiceLeft.Enabled = false;
			btnChoiceRight.Enabled = false;
			if(sender == btnChoiceRight)
			{
				sideSet = Side.Right;
//				blackout = "right";
				profileSound.Play(soundTwo);
				darkFilter ();
//				leftFilterDark ("On", FilterRotation);
			}
			else if(sender == btnChoiceLeft)
			{
				sideSet = Side.Left;
//				blackout = "left";
				profileSound.Play(soundOne);
				darkFilter ();
//				rightFilterDark ("On", FilterRotation);
			}
			blackOutTimer = NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_clickTimer), BlackOutHighDifficulty);
		}

		private void BlackOutHighDifficulty()
		{
			darkFilter();
			NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_darkTimer), resetbtnForHighDifficulty);
		}

		private void BlackOutLowDifficulty()
		{
			filledFilter = fillFilter.ON;
			darkFilter();
			NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_darkTimer), resetbtnForLowDifficulty);
		}

		UIButton btnChoice, btnChoiceLeft, btnChoiceRight;
		UIImageView imvChoiceLeft, imvChoiceRight, imvLayerLeft, imvLayerRight;
		UIImage UIimageOne, UIimageTwo, filterImage, empty;
		NSTimer fillFilteringChoices, blackOutTimer;
		Sound profileSound = new Sound ();
		MainMenu mainMenu;
		QueryProfile queryProfile;
		setFilterRotation FilterRotation;
		Side sideSet;
		fillFilter filledFilter;
	
		private string blackout, soundSelect, screenPositionHighDifficulty, /*FilterRotation*/ stringSecond;
		private string imageOne, imageTwo, soundOne, soundTwo, selectedProfile, selectedButtonSetting, clickTimer, darkTimer; 
		private int count, _selectedProfile, _clickTimer, _darkTimer;
		private bool pushed = true, playingLeft = true, playingRight = true;
		private string[] menuSettings = new string[4], profile = new string[6];

		enum fillFilter 
		{
			ON,
			OFF
		}

		enum Side
		{
			Left,
			Right,
			Both
		}

		enum setFilterRotation
		{
			landscape,
			portrait
		}

	}
}