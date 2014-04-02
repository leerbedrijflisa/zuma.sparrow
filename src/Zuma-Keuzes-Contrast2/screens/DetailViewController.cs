﻿using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.IO;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;

namespace ZumaKeuzesContrast2
{
	public partial class DetailViewController : UIViewController
	{
		public DetailViewController () : base ()
		{
//			this.queryProfile = queryProfile;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnSaveProfile.TouchUpInside += saveProfile;

		}

		public void DoSomethingInteresting(int Row)
		{
			vwHidden.Hidden = true;

			Console.WriteLine (Row.ToString ());
			QueryProfile (Row);

			UIImage ImgLeft = UIImage.FromFile (ImageOne);
			UIImage ImgRight = UIImage.FromFile (ImageTwo);
			imvLeft.Image = ImgLeft;
			imvRight.Image = ImgRight;

		}


		private void QueryProfile(int Row)
		{
			int adjustRow = Row + 1;
			var _row = adjustRow;
			Console.WriteLine ("AdjustRow " + _row);

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) 
			{
				conn.Open ();
				using(var cmd = conn.CreateCommand())
				{

					cmd.CommandText = "SELECT * FROM Profile WHERE ID = @ID";
					cmd.Parameters.AddWithValue ("@ID", _row);
					using (SqliteDataReader rdr = cmd.ExecuteReader ()) {
						while (rdr.Read ()) {
//							returnRow = rdr ["ID"];
							returnImageOne = rdr ["ImageOne"];
							returnImageTwo = rdr ["ImageTwo"];
							returnSoundOne = rdr ["SoundOne"];
							returnSoundTwo = rdr ["SoundTwo"];
						}
					}
				}
			}

			ImageOne = returnImageOne.ToString ();
			ImageTwo = returnImageTwo.ToString ();
			SoundOne = returnSoundOne.ToString ();
			SoundTwo = returnSoundTwo.ToString ();

			Console.WriteLine (ImageOne + ImageTwo + SoundOne + SoundTwo);
			rowReturned = Convert.ToInt32 (returnRow);
			Console.WriteLine (rowReturned);

		}

		private void saveProfile(object sender, EventArgs e) 
		{
//			DatabaseRequests.StoreMenuSettings (0, 0, 5, rowReturned);
		}

		private QueryProfile queryProfile;
		private object returnRow, returnImageOne, returnImageTwo, returnSoundOne, returnSoundTwo;
		private string ImageOne, ImageTwo, SoundOne, SoundTwo;
		private int rowReturned;

	}
}