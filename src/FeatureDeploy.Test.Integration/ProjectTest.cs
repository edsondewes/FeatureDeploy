using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatureDeploy.Test.Integration
{
    [TestClass]
    public class ProjectTest
    {
        Project project;

        public ProjectTest()
        {
            this.project = new Project
            {
                Name = "WebIntegrador",
                BuildServer = Helper.BuildServer.First(),
                VCS = Helper.VCSSource.First(),
                DeployMethod = Helper.Deploy.First()
            };
        }

        [TestMethod]
        public async Task GetFeatures_should_return_builds_list()
        {
            var features = await this.project.GetFeatures();
            Assert.IsTrue(features.Count() > 0);
        }

        [TestMethod]
        public async Task DeployTest()
        {
            var features = await this.project.GetFeatures();
            var masterBuild = features.Where(f => f.Branch == "master").First();

            await this.project.Deploy(masterBuild.Id);
        }
    }
}
