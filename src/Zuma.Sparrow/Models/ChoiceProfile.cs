using System;

namespace Zuma.Sparrow
{
	public class ChoiceProfile
	{
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
			
		public Option FirstOption
		{
			get;
			set;
		}
			
		public Option SecondOption
		{
			get;
			set;
		}
	}
}