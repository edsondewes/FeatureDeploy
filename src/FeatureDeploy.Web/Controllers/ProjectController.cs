using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace FeatureDeploy.Web.Controllers
{
    /// <summary>
    /// Projects actions
    /// </summary>
    public class ProjectController : ApiController
    {
        /// <summary>
        /// Projects list
        /// </summary>
        public Lazy<IList<Project>> projects;

        /// <summary>
        /// Initialize ProjectController
        /// </summary>
        public ProjectController()
        {
            this.projects = new Lazy<IList<Project>>(() =>
            {
                var json = System.Web.Hosting.HostingEnvironment.MapPath("~/project.json");

                var loader = new ProjectLoader();
                return loader.LoadPath(json);
            });
        }

        /// <summary>
        /// List available projects
        /// </summary>
        /// <returns>List of projects</returns>
        [HttpGet]
        public IEnumerable<object> List()
        {
            return this.projects.Value.Select(p => new { p.Name, p.Url });
        }

        /// <summary>
        /// Get the features of a project
        /// </summary>
        /// <param name="projectName">Project name</param>
        [HttpGet]
        public async Task<IEnumerable<Build>> Features(string projectName)
        {
            var project = this.projects.Value.FirstOrDefault(p => p.Name == projectName);
            var features = await project.GetFeatures();

            return features.OrderBy(f => f.Branch);
        }

        /// <summary>
        /// Deploy a build
        /// </summary>
        /// <param name="buildId">Build Id</param>
        /// <returns>Async Task</returns>
        [HttpGet]
        public async Task Deploy(string projectName, string buildId)
        {
            var project = this.projects.Value.FirstOrDefault(p => p.Name == projectName);
            await project.Deploy(buildId);
        }
    }
}
