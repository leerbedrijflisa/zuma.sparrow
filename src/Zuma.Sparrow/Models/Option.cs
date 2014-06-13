using System;
using SQLite;

namespace Zuma.Sparrow
{
	public class Option
	{
		[PrimaryKey, AutoIncrement]
		public int Id
		{
			get;
			set;
		}

		public string ImageUrl
		{
			get;
			set;
		}

		public string AudioUrl
		{
			get;
			set;
		}
	}
}