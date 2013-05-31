using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using CodePhile;

namespace SpaceBeans {
    public class Trader {

        private readonly HashSet<Bean> beansInHand = new HashSet<Bean>();
        private readonly ICollection<Bean> trophies = new HashSet<Bean>();
        private BeanCargo revealedCollection = new BeanCargo();
        private BeanCargo hiddenCollection = new BeanCargo();
        private readonly string name;

        public Trader(string name) {
            this.name = name;
        }

        public BeanCargo HiddenCollection {
            get { return hiddenCollection; }
        }

        public BeanCargo RevealedCollection {
            get { return revealedCollection; }
        }

        public int TrophyPoints {
            get { return trophies.Sum(c => c.Rank) + (IsWinner ? 3 : 0); }
        }

        public int HandCount {
            get { return beansInHand.Count; }
        }

        public void AddBeansToHand(IEnumerable<Bean> cardsToAdd) {
            foreach(var card in cardsToAdd) {
                beansInHand.Add(card);
            }
        }

        public IEnumerable<Bean> SellRevealedCollection() {
            Bean trophyCard;
            var collectionCount = revealedCollection.Count;
            // TODO: move to Collection?
            if(revealedCollection.TryFirst(c => c.Rank == collectionCount, out trophyCard)) {
                trophies.Add(trophyCard);
            }
            var discardedCards = revealedCollection.ToList();
            discardedCards.Remove(trophyCard);
            revealedCollection = hiddenCollection;
            hiddenCollection = new BeanCargo();
            return discardedCards;
        }

        public ISet<Bean> PassHand() {
            var beansToPass = beansInHand.ToSet();
            beansInHand.Clear();
            return beansToPass;
        }

        public bool HasLegalPlay() {
            return (from card in beansInHand
                    from collection in Collections
                    where collection.CanAddBean(card)
                    select card).Any();
        }

        private IEnumerable<BeanCargo> Collections {
            get { 
                yield return RevealedCollection;
                yield return HiddenCollection;
            }
        }

        public IEnumerable<Bean> BeansInHand {
            get { return beansInHand; } // TODO: expose as readonly
        }

        public bool IsWinner { get; set; }

        public string Name {
            get { return name; }
        }

        public BeanCargo GetCollection(CollectionIdentifier targetCollection) {
            switch(targetCollection) {
                case CollectionIdentifier.Revealed:
                    return this.RevealedCollection;
                case CollectionIdentifier.Hidden:
                    return this.HiddenCollection;
                default:
                    throw new InvalidEnumArgumentException("targetCollection", (int)targetCollection, typeof(CollectionIdentifier));
            }
        }

        public void PlayCardsToCollection(CollectionIdentifier targetCollection, ISet<Bean> cardsToPlay) {
            // TODO: validate here?
            beansInHand.RemoveWhere(cardsToPlay.Contains);
            GetCollection(targetCollection).AddBeans(cardsToPlay);
        }
    }
}