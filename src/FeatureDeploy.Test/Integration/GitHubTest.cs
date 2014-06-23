using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatureDeploy.Test.Integration
{
    [TestClass]
    public class GitHubTest
    {
        [TestMethod]
        public async Task GetBranches_return_branches()
        {
            var client = Helper.VCSSource.First(v => v.Name == "GitHub");
            var branches = await client.GetBranches();

            Assert.IsTrue(branches.Count() > 0);
        }
    }
}
