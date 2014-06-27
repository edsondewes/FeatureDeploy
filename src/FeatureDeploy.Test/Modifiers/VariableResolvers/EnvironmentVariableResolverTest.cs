using System;
using FeatureDeploy.Modifiers.VariableResolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatureDeploy.Test.Modifiers.VariableResolvers
{
    [TestClass]
    public class EnvironmentVariableResolverTest
    {
        private const string EnvVariableName = "FD_TEST_ENV";

        [TestInitialize]
        public void TestInitialize()
        {
            Environment.SetEnvironmentVariable(EnvVariableName, "test value");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Environment.SetEnvironmentVariable(EnvVariableName, string.Empty);
        }

        [TestMethod]
        public void Resolve_should_return_the_environment_value()
        {
            var resolver = new EnvironmentVariableResolver();
            var value = resolver.Resolve(EnvVariableName);

            Assert.AreEqual("test value", value);
        }
    }
}
