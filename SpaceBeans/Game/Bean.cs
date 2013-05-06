namespace SpaceBeans {
    public class Bean {
        private readonly int rank;
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