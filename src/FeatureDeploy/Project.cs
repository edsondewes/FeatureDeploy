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
        public IBuildServer BuildServer { get; set; }

        /// <summary>
        /// Deploy definition
        /// </summary>
        public IDeploy DeployMethod { get; set; }

        /// <summary>
        /// VCS definition
        /// </summary>
        public IVCSSource VCS { get; set; }

        /// <summary>
        /// List the built project features
        /// </summary>
        /// <returns>Build list</returns>
        public async Task<IEnumerable<Build>> GetFeatures()
        {
            if (this.BuildServer == null)
                throw new ArgumentNullException("buildServer", "Build server definition cannot be null");

            if (this.VCS == null)
                throw new ArgumentNullException("vcs", "VCS definition cannot be null");

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
            if (this.BuildServer == null)
                throw new ArgumentNullException("buildServer", "Build server definition cannot be null");

            if (this.DeployMethod == null)
                throw new ArgumentNullException("deploy", "Deploy definition cannot be null");

            var artifactsPath = await this.BuildServer.DownloadArtifacts(buildId);
            await this.DeployMethod.Deploy(artifactsPath.FullName);

            await Task.Factory.StartNew(() => artifactsPath.Delete(true));
        }
    }
}