using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using SpaceBeans.MacOS;
using Microsoft.Xna.Framework;

namespace SpaceBeans.Android.XNA
{
	[Activity (Label = "SpaceBeans.Android.XNA", MainLauncher = true,ConfigurationChanges=ConfigChanges.Orientation|ConfigChanges.Keyboard|ConfigChanges.KeyboardHidden)]
	public class Activity1 : AndroidGameActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			Game1.Activity = this;
			var game = new Game1();
			SetContentView(game.Window);
			game.Run();
		}
	}
}


