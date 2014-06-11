
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Zuma.Sparrow
{
	public partial class ProfileMenuViewController : UIViewController
	{
		public ProfileMenuViewController() : base("ProfileMenuViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			tblProfiles.Source = new ProfileTableSource(this);

		}

		public void OnTabelRowSelected(string selectedProfile)
		{
			lblProfileName.Text = selectedProfile;
		}
	}
}

