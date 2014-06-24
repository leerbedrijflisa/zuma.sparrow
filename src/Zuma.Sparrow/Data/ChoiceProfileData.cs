using System;
using SQLite;

namespace Zuma.Sparrow
{
	public class ChoiceProfileData
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

		public string FirstOptionImageUrl
		{
			get;
			set;
		}

		public string FirstOptionAudioUrl
		{
			get;
			set;
		}

		public string SecondOptionImageUrl
		{
			get;
			set;
		}

		public string SecondOptionAudioUrl
		{
			get;
			set;
		}

		public int ProfileType
		{
			get;
			set;
		}
	}
}

