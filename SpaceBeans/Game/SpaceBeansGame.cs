using System.Collections.Generic;
using System.Linq;

using CodePhile.Games;

namespace SpaceBeans {
    public class SpaceBeansGame : Game {

        public SpaceBeansGame(SpaceBeansGameSetup setup) 
            : base(new SpaceBeansGamePart(setup)) {
        }

        private new SpaceBeansGamePart GamePart {
            get {
                return (SpaceBeansGamePart)base.GamePart;
            }
        }

        public Trader Winner {
            get {
                // TODO: handle ties
                return GamePart.Traders
                    // TODO: extract MaxBy
                        .OrderByDescending(t => t.TrophyPoints)
                        .First();
            }
        }

        public new IEnumerable<ISpaceBeansDecision> Decisions {
            get { return base.Decisions.Cast<ISpaceBeansDecision>(); }
        }
    }
}
