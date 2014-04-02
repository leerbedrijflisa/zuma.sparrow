using System;
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
		public DetailViewController (QueryProfile queryProfile) : base ()
		{
			this.queryProfile = queryProfile;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnSaveProfile.Hidden = true;
		}

		public void DoSomethingInteresting(int Row)
		{
			vwHidden.Hidden = true;

			Console.WriteLine (Row.ToString ());
			databaseRow = queryProfile.returnDatabaseRow (Row);
			Console.WriteLine (databaseRow [4] + " test databaseRow");

			UIImage ImgLeft = UIImage.FromFile (databaseRow[0]);
			UIImage ImgRight = UIImage.FromFile (databaseRow[1]);
			imvLeft.Image = ImgLeft;
			imvRight.Image = ImgRight;

			DatabaseRequests.StoreMenuSettings (0, 0, 5, 5, databaseRow [4]);
			Console.WriteLine (databaseRow[4] + " databaseRow");

		}

		private QueryProfile queryProfile;
		private string[] databaseRow = new string[5];
		private UIImage ImgLeft, ImgRight;


	}
}