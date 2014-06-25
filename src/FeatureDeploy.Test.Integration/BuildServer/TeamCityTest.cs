using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatureDeploy.Test.Integration.BuildServer
{
    [TestClass]
    public class TeamCityTest
    {
        [TestMethod]
        public async Task GetBuilds_return_builds()
        {
            var client = Helper.BuildServer.First(b => b.Name == "TeamCity");
            var builds = await client.GetBuilds(new[] { "master" });

            Assert.IsTrue(builds.Count() > 0);
        }

        [TestMethod]
        public async Task GetArtifacts_return_directory_with_files()
        {
            var client = Helper.BuildServer.First(b => b.Name == "TeamCity");
            var builds = await client.GetBuilds(new[] { "master" });

            var lastBuild = builds.Last();
            var directory = await client.DownloadArtifacts(lastBuild.Id);

            Assert.IsTrue(directory.Exists);

            var itemsCount = directory.GetFiles().Length + directory.GetDirectories().Length;
            Assert.IsTrue(itemsCount > 0);

            directory.Delete(true);
        }
    }
}
