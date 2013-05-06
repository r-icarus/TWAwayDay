using NUnit.Framework;

namespace SpaceBeans {
    [TestFixture]
    public class BeanFormatterTest {
        private readonly BeanFormatter formatter = new BeanFormatter();

        [Test]
        public void ShouldFormatBeanLong() {
            var beanToFormat = new Bean(8, Suit.Orange);
            var formattedBean = formatter.FormatBeanLong(beanToFormat);
            Assert.AreEqual("8 of Orange", formattedBean);
        }

        [Test]
        public void ShouldFormatBeanShort() {
            var beanToFormat = new Bean(8, Suit.Orange);
            var formattedBean = formatter.FormatBeanShort(beanToFormat);
            Assert.AreEqual("8O", formattedBean);
        }

        [Test]
        public void ShouldFormatBeans() {
            var beansToFormat = new[] {new Bean(8, Suit.Orange), new Bean(2, Suit.Red),};
            var formattedBean = formatter.FormatBeans(beansToFormat);
            Assert.AreEqual("8O 2R", formattedBean);
        }
    }
}
