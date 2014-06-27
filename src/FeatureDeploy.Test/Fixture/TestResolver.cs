using FeatureDeploy.Modifiers;

namespace FeatureDeploy.Test.Fixture
{
    public class TestResolver : IVariableResolver
    {
        public string Key
        {
            get { return "test"; }
        }

        public string Resolve(string args)
        {
            return string.Format("[[{0}]]", args);
        }
    }

    public class TestResolver2 : IVariableResolver
    {
        public string Key
        {
            get { return "test2"; }
        }

        public string Resolve(string args)
        {
            return string.Format("**{0}**", args);
        }
    }
}
