using System;
using System.Drawing;
using MonoTouch.AssetsLibrary;
using MonoTouch.UIKit;
using System.IO;
using System.Data;
using Mono.Data.Sqlite;
using MonoTouch.Foundation;
using MonoTouch.CoreImage;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreMotion;
using Lisa.Zuma;

//voor refacteren: Kunnen alle using directives voor database nu weg!??

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

			//voor refacteren wat doet dit okalweer!
			pushed = false;

			Console.WriteLine (darkTimer.ToString () + "test dark");

			InitializeUI ();

			//Create imageViews
			PositionControls (InterfaceOrientation);

			if (selectedButtonSetting == "0") {

				lowDifficultySwitchingChoices ();

				//btn handler
				btnChoice.TouchUpInside += delegate {

					if(soundSelect == "left")
					{
						profileSound.Play(soundOne);
					}
					else if(soundSelect == "right")
					{
						profileSound.Play(soundTwo);
					}
					SwitchingChoices.Dispose();
					btnChoice.Enabled = false;
					if(count == 0) {
					
						blackOutTimer = NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_clickTimer), delegate {
							blackOutLowDifficulty();
							NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_darkTimer), resetbtnForLowDifficulty);
						
						});

					} else if(count == 1) {

						blackOutTimer = NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_clickTimer), delegate {
							blackOutPart();
							NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_darkTimer), resetbtnForLowDifficulty);

						});
					}
				};
			} 
				
			else if (selectedButtonSetting == "1") {
			
				btnChoiceLeft.TouchUpInside += delegate {
					btnChoiceRight.Enabled = false;
					btnChoiceLeft.Enabled = false;
					blackout = "left";
					profileSound.Play(soundOne);
					rightFilterDark ("On", FilterRotation);

					Console.WriteLine("left btn");

					blackOutTimer = NSTimer.CreateScheduledTimer (TimeSpan.FromSeconds (_clickTimer), delegate {
						blackOutLowDifficulty ();
						NSTimer.CreateScheduledTimer (TimeSpan.FromSeconds (_darkTimer), resetbtnForHighDifficulty);
					});
				};

				//btn handler right
				btnChoiceRight.TouchUpInside += delegate {
					btnChoiceLeft.Enabled = false;
					btnChoiceRight.Enabled = false;
					blackout = "right";
					profileSound.Play(soundTwo);
					leftFitlerDark ("On", FilterRotation);

					Console.WriteLine("right btn");

					blackOutTimer = NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_clickTimer), delegate {
						blackOutLowDifficulty();
						NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(_darkTimer), resetbtnForHighDifficulty);
					});
				};
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
				FilterRotation = "landscape";
				Console.WriteLine ("Landscape");

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
				FilterRotation = "portrait";
				Console.WriteLine ("portrait");

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
			imvLayerLeft.Image = empty;
			imvLayerRight.Image = empty;
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
			leftFitlerDark ("On", FilterRotation);
			rightFilterDark ("Off", FilterRotation);

			blackout = "right";
			soundSelect = "right";

			int count = 0;

			SwitchingChoices = NSTimer.CreateRepeatingScheduledTimer (TimeSpan.FromSeconds(5), delegate {

				if(count == 0)
				{
					//left
					count++;
					imvLayerLeft.Image = empty;
					rightFilterDark ("On", FilterRotation);
					blackout = "left";
					soundSelect = "left";

				} else if(count == 1)
				{
					//right
					count--;
					imvLayerRight.Image = empty;
					leftFitlerDark("On", FilterRotation);
					blackout = "right";
					soundSelect = "right";
				}

				Console.WriteLine(soundSelect);
				Console.WriteLine(count);
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
			Console.WriteLine (FilterRotation);
		}

		private void leftFitlerDark(string Switch, string setFilterRotation)
		{
			Console.WriteLine (setFilterRotation);

			if (setFilterRotation == "landscape") {
				imvLayerLeft = new UIImageView (new RectangleF (0, 0, 512, 768));
			} else if (setFilterRotation == "portrait") {
				imvLayerLeft = new UIImageView (new RectangleF (0, 0, 768, 512));
			}
			if (Switch == "On") {
				imvLayerLeft.Image = filterImage;
			} else if (Switch == "Off") {
				imvLayerLeft.Image = empty;
			}

			View.AddSubview (imvLayerLeft);
		}

		private void rightFilterDark(string Switch, string setFilterRotation)
		{
			if (setFilterRotation == "landscape") {
				imvLayerRight = new UIImageView (new RectangleF (512, 0, 512, 768));
			} else if (setFilterRotation == "portrait") {
				imvLayerRight = new UIImageView (new RectangleF (0, 512, 768, 512)); 
			}
			if (Switch == "On") {
				imvLayerRight.Image = filterImage;
			} else if (Switch == "Off") {
				imvLayerRight.Image = empty;
			}

			View.AddSubview (imvLayerRight);
		}

		private void blackOutPart()
		{
			if (count == 0) {
				rightFilterDark ("On", FilterRotation);

			} else if (count == 1) {
				leftFitlerDark ("On", FilterRotation);

			}
		}

		private void blackOutLowDifficulty()
		{
			if (blackout == "left") {
				leftFitlerDark ("On", FilterRotation);

	
			} else if (blackout == "right") {
				rightFilterDark ("On", FilterRotation);
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
			Console.WriteLine ("selectedProfile " + selectedProfile);

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
		QueryProfile queryProfile;
	
		private string blackout, soundSelect, screenPositionHighDifficulty, FilterRotation, stringSecond;
		private string imageOne, imageTwo, soundOne, soundTwo, selectedProfile, selectedButtonSetting, clickTimer, darkTimer; 
		private int count, _selectedProfile, _clickTimer, _darkTimer;
		private bool pushed = true, playingLeft = true, playingRight = true;
		private string[] menuSettings = new string[4], profile = new string[6];

	}
}