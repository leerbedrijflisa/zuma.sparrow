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
//using Lisa.Zuma.Keuzes;
using Lisa.Zuma;

namespace ZumaKeuzesContrast2
{
	public partial class MainViewController4 : UIViewController
	{
		public MainViewController4 () : base ("MainViewController4", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{

			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}


		UIButton btnChoice, btnChoiceLeft, btnChoiceRight;
		UIImageView imvChoiceLeft, imvChoiceRight, imvLayerLeft, imvLayerRight;
		UIImage leftImage, rightImage, filterImage, empty;
		NSTimer SwitchingChoices, blackOutTimer;
		Sound IChooseLeft = new Sound(), IChooseRight = new Sound();
		MainMenu mainMenu;

		private string blackout, soundSelect, screenPositionHighDifficulty, FilterRotation;
		private int count, TimerSetting;
		private object scFirst, scSecond, Timer;
		private bool pushed = true, playingLeft = true, playingRight = true;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationController.SetNavigationBarHidden(true, true);
			UIApplication.SharedApplication.SetStatusBarHidden (true, true);

			pushed = false;

			ScreenReturnToMenu ();

			//reads out the value of the scOption and TimerSettings
			Read_Zuma_DB ();

			TimerSetting = Convert.ToInt32 (Timer);

			//Select and create UIImages.
			selectLeftImage ();
			selectRightImage ();
			filterImage = UIImage.FromFile ("layer_transparent.png");
			empty = UIImage.FromFile ("");

			//selects and creates the button or buttons depending on what was chosen in MainMenu
			if (scFirst.ToString() == "0") {
				btnChoice = UIButton.FromType (UIButtonType.RoundedRect);

				View.AddSubview (btnChoice);
			} else if (scFirst.ToString() == "1") {
				btnChoiceLeft = UIButton.FromType (UIButtonType.RoundedRect);
				btnChoiceRight = UIButton.FromType (UIButtonType.RoundedRect);
			
				View.AddSubview (btnChoiceLeft);
				View.AddSubview (btnChoiceRight);
			}

			//Create imageViews
			PositionControls (InterfaceOrientation);

			//If scFirst is "0" there will be one btn.
			if (scFirst.ToString () == "0") {

				lowDifficultySwitchingChoices ();

				//btn handler
				btnChoice.TouchUpInside += delegate {

					if(soundSelect == "left")
					{
						selectLeftSound();
					}
					else if(soundSelect == "right")
					{
						selectRightSound();
					}
					Console.WriteLine(soundSelect);
					SwitchingChoices.Dispose();
					btnChoice.Enabled = false;
					if(count == 0) {
					
						blackOutTimer = NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(TimerSetting), delegate {
							blackOutLowDifficulty();
							NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(TimerSetting), resetbtnForLowDifficulty);
						
						});

					} else if(count == 1) {

						blackOutTimer = NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(TimerSetting), delegate {
							blackOutPart();
							NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(TimerSetting), resetbtnForLowDifficulty);

						});
					}
				};
			} 

			//If scFirst is "1" the screen is split up in two buttons.
			else if (scFirst.ToString () == "1") {


				btnChoiceLeft.TouchUpInside += delegate {
						btnChoiceRight.Enabled = false;
						btnChoiceLeft.Enabled = false;
						blackout = "left";
						selectLeftSound();
						rightFilterDark ("On", FilterRotation);

						Console.WriteLine("left btn");

					blackOutTimer = NSTimer.CreateScheduledTimer (TimeSpan.FromSeconds (TimerSetting), delegate {
							blackOutLowDifficulty ();
						NSTimer.CreateScheduledTimer (TimeSpan.FromSeconds (TimerSetting), resetbtnForHighDifficulty);
						});
				};

				//btn handler right
				btnChoiceRight.TouchUpInside += delegate {
						btnChoiceLeft.Enabled = false;
						btnChoiceRight.Enabled = false;
						blackout = "right";
						selectRightSound();
						leftFitlerDark ("On", FilterRotation);

						Console.WriteLine("right btn");

					blackOutTimer = NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(TimerSetting), delegate {
							blackOutLowDifficulty();
						NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(TimerSetting), resetbtnForHighDifficulty);
						});
				};
			}
		}

		// end view did load!

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

//			ScreenReturnToMenu ();
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
			if (scFirst.ToString () == "0") {

				if (FilterRotation == "landscape") {
					btnChoice.Frame = new RectangleF (0, 0, 1024, 768);
				} else if (FilterRotation == "portrait") {
					btnChoice.Frame = new RectangleF (0, 0, 768, 1024);
				}

			} else if (scFirst.ToString () == "1") {
			
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

		// Filters for 5 seconds of darkness part.

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

		private void selectLeftSound()
		{
			if(scSecond.ToString() == "0")
			{
				IChooseLeft.Play ("Left.mp3");
			}
			else if(scSecond.ToString() == "1")
			{
				IChooseLeft.Play("Yes.mp3");
			}
		}

		private void selectRightSound()
		{
			if(scSecond.ToString() == "0")
			{
				IChooseLeft.Play ("Right.mp3");
			}
			else if(scSecond.ToString() == "1")
			{
				IChooseLeft.Play("No.mp3");
			}
		}

		private void selectLeftImage()
		{
			if(scSecond.ToString() == "0")
			{
				leftImage = UIImage.FromFile ("LeftArrow2.png");
			}
			else if(scSecond.ToString() == "1")
			{
				leftImage = UIImage.FromFile ("Yes.jpg");
			}

		}

		private void selectRightImage()
		{
			if(scSecond.ToString() == "0")
			{
				rightImage = UIImage.FromFile ("RightArrow2.png");
			}
			else if(scSecond.ToString() == "1")
			{
				rightImage = UIImage.FromFile ("No.jpg");
			}

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



		private void PushView()
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

		public void Read_Zuma_DB()
		{

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatebase = Path.Combine (documents, "db_Zuma_Keuzes.db");
			//SqliteConnection.CreateFile (pathToDatebase);

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatebase);
			using (var conn = new SqliteConnection (connectionString)) {

				conn.Open ();
				string stm = "SELECT * FROM MenuOptions";

				using (SqliteCommand cmd = new SqliteCommand(stm, conn))
				{
					using (SqliteDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read()) {
							scFirst = rdr ["scFirst"];
							scSecond = rdr["scSecond"];
							Timer = rdr ["Timer"];

						}
					}

				}

			}
		}

		public override void ViewWillAppear (bool animated) {
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, animated);
		}

		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
			this.NavigationController.SetNavigationBarHidden (false, animated);
		}

		// Handle Rotation

		private CMMotionManager _motionManager;

		private void ScreenReturnToMenu()
		{
			_motionManager = new CMMotionManager ();
			_motionManager.StartAccelerometerUpdates (NSOperationQueue.CurrentQueue, (data, error) => {
				if (data.Acceleration.Z > 0.890) {
					PushView ();
				}
			});
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

				imvChoiceLeft.Image = leftImage;
				View.AddSubview (imvChoiceLeft);

				imvChoiceRight.Image = rightImage;
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

				imvChoiceLeft.Image = leftImage;
				View.AddSubview (imvChoiceLeft);

				imvChoiceRight.Image = rightImage;
				View.AddSubview (imvChoiceRight);

				break;
			}

			SelectBtnDifficulty ();

		}

		private void clearImageIMV(UIImageView imvImageCleared)
		{
			if (imvImageCleared != null) {
				imvImageCleared.Image = empty;
			}
		}

	}
}