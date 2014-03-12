using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ZumaKeuzesOplichten
{
	public partial class MainViewScreenController : UICollectionViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("MainViewScreenController", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("MainViewScreenController");

		public MainViewScreenController (IntPtr handle) : base (handle)
		{
		}

		public static MainViewScreenController Create ()
		{
			return (MainViewScreenController)Nib.Instantiate (null, null) [0];
		}
	}
}

