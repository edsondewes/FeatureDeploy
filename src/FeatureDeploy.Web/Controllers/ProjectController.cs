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
        public Lazy<IList<Project>> projects;

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
        public IEnumerable<string> List()
        {
            return this.projects.Value.Select(p => p.Name);
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

            return features;
        }
    }
}
