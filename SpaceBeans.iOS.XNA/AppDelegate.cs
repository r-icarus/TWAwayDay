using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.UIKit;
using MonoTouch.Foundation;
using SpaceBeans.Xna;

namespace SpaceBeans.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override void FinishedLaunching (UIApplication app)
		{
			var xnaGame = new MobileGame(CreateGame());
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

