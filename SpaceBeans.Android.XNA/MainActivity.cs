using System;
using System.Collections.Generic;
using System.Linq;

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
			var xnaGame = new Game1(CreateGame());
			SetContentView(xnaGame.Window);
            xnaGame.Run();
		}

	    private static SpaceBeansGame CreateGame() {
            var setup = new SpaceBeansGameSetup();
            setup.AddTrader(new Trader("1"));
            setup.AddTrader(new Trader("2"));
            var game = new SpaceBeansGame(setup);
            game.Start();

            var allCards = StandardRules.GenerateStandardCards();
            allCards = Randomize(allCards);
            ((SetupDrawPileDecision)game.Decisions.First()).AddBeans(allCards);

	        return game;
	    }

        private static IEnumerable<T> Randomize<T>(IEnumerable<T> source) {
            var sourceCopy = source.ToList();
            var rand = new Random();
            while (sourceCopy.Count > 0) {
                var nextIndex = rand.Next(0, sourceCopy.Count - 1);
                yield return sourceCopy[nextIndex];
                sourceCopy.RemoveAt(nextIndex);
            }
        }
    }
}


