#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using SpaceBeans.MacOS;

#endregion
namespace SpaceBeans.Windows
{
	static class Program
	{
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
		[STAThread]
        static void Main()
		{
			var setup = new SpaceBeansGameSetup();
			setup.AddTrader(new Trader("1"));
			setup.AddTrader(new Trader("2"));
			var game = new SpaceBeansGame(setup);
			game.Start();

			var allCards = StandardRules.GenerateStandardCards();
			allCards = Randomize(allCards);
			((SetupDrawPileDecision)game.Decisions.First()).AddBeans(allCards);

			var xnaGame = new Game1(game);
			xnaGame.Run();
		}

		private static IEnumerable<T> Randomize<T>(IEnumerable<T> source)
		{
			var sourceCopy = source.ToList();
			var rand = new Random();
			while(sourceCopy.Count > 0)
			{
				var nextIndex = rand.Next(0, sourceCopy.Count - 1);
				yield return sourceCopy[nextIndex];
				sourceCopy.RemoveAt(nextIndex);
			}
		}
	}
}
