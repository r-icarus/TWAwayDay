using NUnit.Framework;

namespace SpaceBeans {
    [TestFixture]
    public class DrawPileTest {
        [Test]
        public void ShouldBeAbleToDrawSingleBean() {
            var drawPile = new DrawPile(new DiscardPile());
            var bean1 = new Bean(1, Suit.Blue);
            var bean2 = new Bean(2, Suit.Green);
            drawPile.AddBeans(new[] {bean1, bean2});
            Assert.AreEqual(new[] {bean1}, drawPile.Draw(1));
        }

        [Test]
        public void ShouldBeAbleToDrawMultipleBeans() {
            var drawPile = new DrawPile(new DiscardPile());
            var bean1 = new Bean(1, Suit.Blue);
            var bean2 = new Bean(2, Suit.Green);
            var bean3 = new Bean(3, Suit.Red);
            drawPile.AddBeans(new[] { bean1, bean2, bean3 });
            Assert.AreEqual(new [] {bean1, bean2}, drawPile.Draw(2));
        }

        [Test]
        public void ShouldNotReshuffleIfDrawingFewerCardsThanAvailable() {
            var discardPile = new DiscardPile();
            var drawPile = new DrawPile(discardPile);
            var bean1 = new Bean(1, Suit.Blue);
            var bean2 = new Bean(2, Suit.Green);
            var bean3 = new Bean(3, Suit.Red);
            drawPile.AddBeans(new[] { bean1, bean2, bean3 });
            discardPile.DiscardBeans(new[] { new Bean(4, Suit.Orange), new Bean(5, Suit.Purple), });
            Assert.AreEqual(new[] { bean1, bean2 }, drawPile.Draw(2));
            Assert.AreEqual(2, discardPile.Count);
        }

        [Test]
        public void ShouldReshuffleIfDrawingMoreCardsThanAvailable() {
            var discardPile = new DiscardPile();
            var drawPile = new DrawPile(discardPile);
            var bean1 = new Bean(1, Suit.Blue);
            drawPile.AddBeans(new[] { bean1 });
            var bean2 = new Bean(4, Suit.Orange);
            var bean3 = new Bean(5, Suit.Purple);
            discardPile.DiscardBeans(new[] { bean2, bean3, });
            Assert.AreEqual(new[] { bean1, bean2 }, drawPile.Draw(2));
            Assert.AreEqual(0, discardPile.Count);
        }
    }
}
