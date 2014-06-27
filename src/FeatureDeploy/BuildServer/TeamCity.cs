using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using TeamCitySharp;
using TeamCitySharp.Locators;

namespace FeatureDeploy.BuildServer
{
    /// <summary>
    /// Build server definition for TeamCity
    /// </summary>
    public class TeamCity : IBuildServer
    {
        /// <summary>
        /// TeamCity client
        /// </summary>
        private Lazy<TeamCityClient> client;

        /// <summary>
        /// Configuration name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// TeamCity build configuration Id
        /// </summary>
        public string ConfigurationId { get; set; }

        /// <summary>
        /// TeamCity Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Username to authenticate
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password to authenticate
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Download folder used to store artifacts
        /// </summary>
        public string DownloadFolder { get; set; }

        /// <summary>
        /// Default construtor
        /// </summary>
        public TeamCity()
        {
            this.client = new Lazy<TeamCityClient>(() =>
            {
                var c = new TeamCityClient(this.Url);
                c.Connect(this.Username, this.Password);
                return c;
            });
        }

        /// <summary>
        /// Download and extract the artifacts from a specific build
        /// </summary>
        /// <param name="buildId">TeamCirt build id</param>
        /// <returns>Extracted files directory path</returns>
        public async Task<DirectoryInfo> DownloadArtifacts(string buildId)
        {
            var directoryPath = this.DownloadFolder ?? string.Empty; // TODO: validate if ends with \
            await Task.Factory.StartNew(() =>
            {
                this.client.Value.Artifacts.DownloadArtifactsByBuildId(buildId, download =>
                {
                    directoryPath += download.Substring(0, download.LastIndexOf('.'));
                    ZipFile.ExtractToDirectory(download, directoryPath);
                });
            });

            return new DirectoryInfo(directoryPath);
        }

        /// <summary>
        /// Get information about the builds of specific branches
        /// </summary>
        /// <param name="branches">Names of branches</param>
        /// <returns>List of build info</returns>
        public async Task<IEnumerable<Build>> GetBuilds(IEnumerable<string> branches)
        {
            var builds = new List<Build>();
            await Task.Factory.StartNew(() =>
            {
                foreach (var branchName in branches)
                {
                    var locator = BuildLocator.WithDimensions(branch: branchName, maxResults: 1, buildType: BuildTypeLocator.WithId(this.ConfigurationId));
                    var build = this.client.Value.Builds.ByBuildLocator(locator).FirstOrDefault();
                    if (build != null)
                    {
                        builds.Add(new Build
                        {
                            Id = build.Id,
                            Branch = branchName,
                            Status = build.Status == "SUCCESS" ? BuildStatus.Success : BuildStatus.Fail
                        });
                    }
                }
            });

            return builds;
        }
    }
}
