
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

			var tableSource = new ProfileTableSource();
			tblProfiles.Source = tableSource;
			tableSource.ProfileSelected += OnProfileSelected;
		}

		private void OnProfileSelected(object sender, ProfileEventArgs e)
		{
			lblProfileName.Text = e.Profile;
		}
	}
}

