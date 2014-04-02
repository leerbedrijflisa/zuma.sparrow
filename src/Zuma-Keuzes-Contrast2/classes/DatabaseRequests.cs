using System;
using System.IO;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;

namespace ZumaKeuzesContrast2
{
	public class DatabaseRequests
	{

		public DatabaseRequests ()
		{
		}

		public static void CreateDatabase()
		{

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatebase = Path.Combine (documents, "db_Zuma_Keuzes.db");
			SqliteConnection.CreateFile (pathToDatebase);

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatebase);
			using (var conn = new SqliteConnection (connectionString)) {

				conn.Open ();
				using (var cmd = conn.CreateCommand ()) {

					cmd.CommandText = "CREATE TABLE MenuOptions (MenuOptionsID INTEGER PRIMARY KEY AUTOINCREMENT, scFirst INTEGER, scSecond INTEGER, Timer INTERGER, StoredProfile INTERGER);";
					cmd.CommandType = CommandType.Text;
					cmd.ExecuteNonQuery ();
				}

				using (var cmd = conn.CreateCommand ()) {

					cmd.CommandText = "CREATE TABLE Profile (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name VARCHAR(255), ImageOne VARCHAR(255), ImageTwo VARCHAR(255), SoundOne VARCHAR(255), SoundTwo VARCHAR(255), selectedRow INTEGER);";
					cmd.CommandType = CommandType.Text;
					cmd.ExecuteNonQuery ();
				}

			}
		}

		public static void StoreMenuSettings(int scFirst, int scSecond, int Timer)
		{
			var varScFirst = scFirst;
			var varScSecond = scSecond;
			var varTimer = Timer;

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) {

				conn.Open ();

				using (var cmd = conn.CreateCommand ()) {

					cmd.CommandText = "INSERT INTO MenuOptions (scFirst, scSecond, Timer) VALUES (@First, @Second, @Timer)";
					cmd.Parameters.AddWithValue ("@First", varScFirst);
					cmd.Parameters.AddWithValue ("@Second", varScSecond);
					cmd.Parameters.AddWithValue ("@Timer", varTimer);
					cmd.ExecuteNonQuery ();

				}
			}
		}

		public static void CreateDefaultProfiles()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) 
			{
				conn.Open ();
				using (var cmd = conn.CreateCommand ()) 
				{
					cmd.CommandText = "INSERT INTO Profile (Name, ImageOne, ImageTwo, SoundOne, SoundTwo) VALUES ('Links/Rechts', 'images/LeftArrow2.png', 'images/RightArrow2.png', 'sounds/Left.mp3', 'sounds/Right.mp3')";
					cmd.ExecuteNonQuery ();
				}

				using (var cmd = conn.CreateCommand ()) 
				{
					cmd.CommandText = "INSERT INTO Profile (Name, ImageOne, ImageTwo, SoundOne, SoundTwo) VALUES ('Ja/Nee', 'images/Yes.jpg', 'images/No.jpg', 'sounds/Yes.mp3', 'sounds/No.mp3')";
					cmd.ExecuteNonQuery ();
				}
			}
		}

		public static void SetSelectedRow(int rowSelected)
		{
			var addRowSelected = rowSelected; 

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var pathToDatabase = Path.Combine (documents, "db_Zuma_Keuzes.db");

			var connectionString = String.Format ("Data source={0};Version=3", pathToDatabase);
			using (var conn = new SqliteConnection (connectionString)) 
			{
				conn.Open ();
				using (var cmd = conn.CreateCommand ()) 
				{
					cmd.CommandText = "INSERT INTO Profile (selectedRow) VALUES (@rowSelected)";
					cmd.Parameters.AddWithValue ("@rowSelected", addRowSelected);
					cmd.ExecuteNonQuery ();
				}
			}
		}
	}
}