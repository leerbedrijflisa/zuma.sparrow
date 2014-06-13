using System;
using SQLite;

namespace Zuma.Sparrow
{
	public class ChoiceProfile
	{
		[PrimaryKey, AutoIncrement]
		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		[Ignore]
		public Option FirstOption
		{
			get;
			set;
		}

		[Ignore]
		public Option SecondOption
		{
			get;
			set;
		}
			
		public int FirstOptionId
		{
			get;
			set;
		}

		public int SecondOptionId
		{
			get;
			set;
		}
	}
}