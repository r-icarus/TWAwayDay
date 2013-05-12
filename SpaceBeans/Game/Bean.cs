using System.Runtime.Serialization;

namespace SpaceBeans {
    [DataContract]
    public class Bean {
        [DataMember]
        private readonly int rank;
        [DataMember]
        private readonly Suit suit;

        public Bean(int rank, Suit suit) {
            this.rank = rank;
            this.suit = suit;
        }

        public int Rank {
            get { return rank; }
        }

        public Suit Suit {
            get { return suit; }
        }
    }
}