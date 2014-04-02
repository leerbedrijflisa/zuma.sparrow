using System;
using System.IO;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;

namespace ZumaKeuzesContrast2
{
	public class QueryProfile
	{
		public QueryProfile ()
		{
		}

		public void Test (int Row)
		{
			int adjust = Row + 1;
			int _row = adjust;

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) {
				conn.Open ();
				using (var cmd = conn.CreateCommand ()) {

					cmd.CommandText = "SELECT * FROM Profile WHERE ID = @ID";
					cmd.Parameters.AddWithValue ("@ID", _row);
					using (SqliteDataReader rdr = cmd.ExecuteReader ()) {
						while (rdr.Read ()) {
							returnRow = rdr ["ID"];
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

		private object returnRow, returnImageOne, returnImageTwo, returnSoundOne, returnSoundTwo;
		public string ImageOne { 
			get { return ImageOne; }
			set { }
		}
		public string ImageTwo { 
			get { return ImageTwo; } 
			set { }
		}
		public string SoundOne { 
			get { return SoundOne; } 
			set { }
		}
		public string SoundTwo { 
			get { return SoundTwo; } 
			set { }
		}
		public int rowReturned { 
			get { return rowReturned; }
			set { }
		}
	}
}

