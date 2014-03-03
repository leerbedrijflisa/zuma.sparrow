using System;
using MonoTouch.UIKit;

namespace ZumaKeuzesContrast2
{
	public class RotationNavigationController : UINavigationController
	{
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return this.VisibleViewController.GetSupportedInterfaceOrientations ();
		}

	}
}

