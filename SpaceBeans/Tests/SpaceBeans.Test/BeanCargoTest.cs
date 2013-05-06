using System.Linq;

using NUnit.Framework;

namespace SpaceBeans {
    [TestFixture]
    public class BeanCargoTest {
        [Test]
        public void ShouldAddBeans() {
            var cargo = new BeanCargo();
            var existingBeans = new[] { new Bean(1, Suit.Blue), new Bean(2, Suit.Blue), };
            cargo.AddBeans(existingBeans);
            var beansToAdd = new[] {new Bean(3, Suit.Blue)};
            cargo.AddBeans(beansToAdd);
            CollectionAssert.AreEqual(existingBeans.Concat(beansToAdd), cargo);
        }

        [Test]
        public void ShouldAllowBeanWhenEmpty() {
            var cargo = new BeanCargo();
            var beanToAdd = new Bean(3, Suit.Blue);
            Assert.IsTrue(cargo.CanAddBean(beanToAdd));
        }

        [Test]
        public void ShouldAllowBeanOfSameSuit() {
            var cargo = new BeanCargo();
            var existingBeans = new[] { new Bean(1, Suit.Blue), };
            cargo.AddBeans(existingBeans);
            var beanToAdd = new Bean(3, Suit.Blue);
            Assert.IsTrue(cargo.CanAddBean(beanToAdd));
        }

        [Test]
        public void ShouldRejectBeanOfSameSuit() {
            var cargo = new BeanCargo();
            var existingBeans = new[] { new Bean(1, Suit.Blue), new Bean(2, Suit.Blue), };
            cargo.AddBeans(existingBeans);
            var beanToAdd = new Bean(3, Suit.Green);
            Assert.IsFalse(cargo.CanAddBean(beanToAdd));
        }
    }
}
