using System;
using System.Collections.Generic;
using SQLite;
using Mono.Data.Sqlite;

namespace DatabaseNew
{
	public class DataManager
	{
		public DataManager (string path)
		{
			dataLock = new object ();
			dataBasePath = path;
		}

		private string dataBasePath;
		private object dataLock;

		public string DataPath
		{
			get 
			{
				return dataBasePath;
			}
		}

		public bool Setup()
		{
			lock (dataLock) 
			{
				try {
					using (var sqlCon = new SQLiteConnection(dataBasePath))
					{
						sqlCon.CreateTable<Persons>();
					}
					return true;
				}
				catch (SQLiteException ex) {
					throw ex;
				}
				catch(Exception ex) {
					throw ex;
				}
			}
		}

		public List<Persons> getAllListOfRows()
		{
			lock (dataLock) {
				using (var sqlCon = new SQLiteConnection (dataBasePath)) {
					sqlCon.Execute (Constants.DBClauseSyncOff);
					sqlCon.BeginTransaction ();
					List<Persons> toReturn = new List<Persons> ();
					toReturn = sqlCon.Query<Persons> ("SELECT * FROM Persons"); 
					return toReturn.Count != 0 ? toReturn : new List<Persons> ();
				}
			}
		}

		public string getNameForID(int id)
		{
			lock (dataLock) {
				using (var sqlCon = new SQLiteConnection(dataBasePath))
				{
					sqlCon.Execute (Constants.DBClauseSyncOff);
					sqlCon.BeginTransaction ();
					string toReturn = string.Empty;
					toReturn = sqlCon.ExecuteScalar<string> ("SELECT Name FROM Persons WHERE ID =?", id);
					return !string.IsNullOrEmpty (toReturn) ? toReturn : "No name found";
				}
			}
		}

		public void AddOrUpdateTable(Persons dRow)
		{
			lock (dataLock) {
				using (var sqlCon = new SQLiteConnection (dataBasePath)) {
					sqlCon.Execute (Constants.DBClauseSyncOff);
					sqlCon.BeginTransaction ();
					try {
						if(sqlCon.Execute("UPDATE Persons SET " +
							"ID=?, " +
							"Name=?, " +
							"Password=? WHERE " +
							"ID=?",
							dRow.ID,
							dRow.Name,
							dRow.Password,
							dRow.ID) == 0)
						{
							sqlCon.Insert(dRow, typeof(Persons));
						}
						sqlCon.Commit();
					}
					catch (Exception ex) {
						Console.WriteLine ("Error in AddOrUpdateTable : {0}-{1}", ex.Message, ex.StackTrace);
						sqlCon.Rollback ();
					}
				}
			}
		}

		public void AddOrUpdateTables(List<Persons>rows)
		{
			foreach (Persons row in rows) {
				AddOrUpdateTable (row);
			}
		}
	}
}