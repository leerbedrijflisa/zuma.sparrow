using System;
using MonoTouch.UIKit;

namespace Zuma.Sparrow
{
	/// <summary>
	/// 	Allows you to lock a view controller in a specific orientation. To do this, you should override
	/// 	the GetSupportedInterfaceOrientations-method in your ViewController and return the orientations
	/// 	you wish to support. For example, <c>return UIInterfaceOrientationMask.LandscapeLeft |
	/// 	UIInterfaceOrientationMask.LandscapeRight;</c>.
	/// </summary>
	public class NavigationController : UINavigationController
	{
		public ChoiceProfile CurrentProfile
		{
			get;
			set;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return this.VisibleViewController.GetSupportedInterfaceOrientations();
		}
	}
}