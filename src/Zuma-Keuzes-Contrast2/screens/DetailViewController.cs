﻿using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.IO;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;
using Lisa.Zuma;

namespace ZumaKeuzesContrast2
{
	public partial class DetailViewController : UIViewController
	{
		public DetailViewController (/*QueryProfile queryProfile*/) : base ()
		{
//			this.queryProfile = queryProfile;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnSaveProfile.Hidden = true;

			btnSetLeftSnd.TouchUpInside += SetSnd;

			btnSetRightSnd.TouchUpInside += SetSnd;
		}

		public void RefreshDetialView(int Row)
		{
			vwHidden.Hidden = true;
			_row = Row + 1;
			Console.WriteLine (_row.ToString ());

			databaseRow = queryProfile.returnProfileRow(_row);
			Console.WriteLine (databaseRow [5] + " test databaseRow");

			UIImage ImgLeft = UIImage.FromFile (databaseRow[1]);
			UIImage ImgRight = UIImage.FromFile (databaseRow[2]);
			imvLeft.Image = ImgLeft;
			imvRight.Image = ImgRight;

			DatabaseRequests.StoreMenuSettings (0, 5, 5, databaseRow [5]);
			Console.WriteLine (databaseRow[4] + " databaseRow");

		}

		private void SetSnd(object sender, EventArgs args)
		{
			if (sender == btnSetLeftSnd)
			{
				profileSnd.Play (databaseRow [3]);
			} 
			else if (sender == btnSetRightSnd) 
			{
				profileSnd.Play (databaseRow [4]);
			}
		}

//		private QueryProfile queryProfile;
		private string[] databaseRow = new string[5];
		private int _row;
		Sound profileSnd = new Sound();
		QueryProfile queryProfile = new QueryProfile();


	}
}