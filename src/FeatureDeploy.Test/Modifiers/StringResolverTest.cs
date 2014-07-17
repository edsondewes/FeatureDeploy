using FeatureDeploy.Modifiers;
using FeatureDeploy.Test.Fixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatureDeploy.Test.Modifiers
{
    [TestClass]
    public class StringResolverTest
    {
        StringResolver resolver;

        [TestInitialize]
        public void TestInitialize()
        {
            this.resolver = new StringResolver();
            this.resolver.Resolvers.Add(new TestResolver());
        }

        [TestMethod]
        public void Resolve_should_replace_the_variable_with_the_generated_value()
        {
            var phrase = "test phrase :test(arg) more words";
            var result = resolver.Resolve(phrase);

            Assert.AreEqual("test phrase [[arg]] more words", result);
        }

        [TestMethod]
        public void Resolve_can_receive_empty_args()
        {
            var phrase = "test phrase :test() more words";
            var result = resolver.Resolve(phrase);

            Assert.AreEqual("test phrase [[]] more words", result);
        }

        [TestMethod]
        public void Resolve_should_replace_multiple_resolvers()
        {
            this.resolver.Resolvers.Add(new TestResolver2());

            var phrase = "test :test2(args2) phrase :test(args) more words";
            var result = resolver.Resolve(phrase);

            Assert.AreEqual("test **args2** phrase [[args]] more words", result);
        }

        [TestMethod]
        public void Resolve_should_not_replace_if_resolver_is_not_defined()
        {
            this.resolver.Resolvers.Clear();

            var phrase = "test phrase :test(arg) more words";
            var result = resolver.Resolve(phrase);

            Assert.AreEqual(phrase, result);
        }
    }
}
