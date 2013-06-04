using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content.PM;
using Android.OS;

using Microsoft.Xna.Framework;
using SpaceBeans.Xna;

namespace SpaceBeans.Android
{
	[Activity (Label = "SpaceBeans.Android.XNA", MainLauncher = true,ConfigurationChanges=ConfigChanges.Orientation|ConfigChanges.Keyboard|ConfigChanges.KeyboardHidden)]
	public class Activity1 : AndroidGameActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			AndroidGame.Activity = this;
            var xnaGame = new MobileGame(CreateGame());
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


