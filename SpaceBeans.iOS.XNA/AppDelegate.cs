using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SpaceBeans.MacOS;

namespace SpaceBeans.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		Game1 game;
		public override void FinishedLaunching (UIApplication app)
		{
			game = new Game1 ();
			game.Run ();

		}

	
	}
}

