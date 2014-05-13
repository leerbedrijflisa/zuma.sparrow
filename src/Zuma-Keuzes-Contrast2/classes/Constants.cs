using System;

namespace DatabaseNew
{
	public class Constants
	{
		public Constants ()
		{
		}

		public const string DBClauseSyncOff = "PRAGMA SYNCHRONOUS=OFF";
		public const string DBClauseVacuum = "VACUUM;";
	}
}

