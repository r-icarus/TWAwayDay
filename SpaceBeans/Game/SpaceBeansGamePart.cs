using System.Collections.Generic;
using CodePhile.Games;

namespace SpaceBeans
{
	internal class SpaceBeansGamePart : SequentialParentGamePart
	{
		private readonly DiscardPile discardPile;
		private readonly DrawPile drawPile;
		private readonly Trader[] traders;

		public SpaceBeansGamePart(SpaceBeansGameSetup setup)
		{
			setup.Validate();

			discardPile = new DiscardPile();
			drawPile = new DrawPile(discardPile);
			traders = setup.GetTraders();
		}

		public IEnumerable<Trader> Traders
		{
			get { return traders; }
		}

		public Trader CurrentTrader
		{
			get { return CurrentGamePart is PlayTurnsGamePart ? ((PlayTurnsGamePart)CurrentGamePart).CurrentTurn.Trader : null; }
		}

		protected override IEnumerator<GamePart> GetGameParts()
		{
			yield return new GameSetupPart(drawPile);
			yield return new PlayTurnsGamePart(traders, discardPile, drawPile);
		}
	}
}