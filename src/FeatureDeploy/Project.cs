using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureDeploy
{
    /// <summary>
    /// Project definition
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Build server definition
        /// </summary>
        public IBuildServer BuildServer { get; private set; }

        /// <summary>
        /// Deploy definition
        /// </summary>
        public IDeploy DeployMethod { get; private set; }

        /// <summary>
        /// VCS definition
        /// </summary>
        public IVCSSource VCS { get; private set; }

        /// <summary>
        /// Create a project to watch and deploy
        /// </summary>
        /// <param name="name">Project name</param>
        /// <param name="buildServer">Build server definition</param>
        /// <param name="vcs">VCS definition</param>
        /// <param name="deploy">Deploy definition</param>
        public Project(string name, IBuildServer buildServer, IVCSSource vcs, IDeploy deploy)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name", "Project name cannot be null");

            if (buildServer == null)
                throw new ArgumentNullException("buildServer", "Build server definition cannot be null");

            if (vcs == null)
                throw new ArgumentNullException("vcs", "VCS definition cannot be null");

            if (deploy == null)
                throw new ArgumentNullException("deploy", "Deploy definition cannot be null");

            this.Name = name;
            this.BuildServer = buildServer;
            this.DeployMethod = deploy;
            this.VCS = vcs;
        }

        /// <summary>
        /// List the built project features
        /// </summary>
        /// <returns>Build list</returns>
        public async Task<IEnumerable<Build>> GetFeatures()
        {
            var branches = await this.VCS.GetBranches();
            var builds = await this.BuildServer.GetBuilds(branches);

            return builds;
        }

        /// <summary>
        /// Deploy a specific build
        /// </summary>
        /// <param name="buildId">Build id from build server</param>
        /// <returns>Async task</returns>
        public async Task Deploy(string buildId)
        {
            var artifactsPath = await this.BuildServer.DownloadArtifacts(buildId);
            await this.DeployMethod.Deploy(artifactsPath.FullName);

            await Task.Factory.StartNew(() => artifactsPath.Delete(true));
        }
    }
}
