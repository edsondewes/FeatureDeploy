using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FeatureDeploy
{
    /// <summary>
    /// Build server definition
    /// </summary>
    public interface IBuildServer
    {
        /// <summary>
        /// Configuration name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Download and extract the artifacts from a specific build
        /// </summary>
        /// <param name="buildId">TeamCirt build id</param>
        /// <returns>Extracted files directory path</returns>
        Task<DirectoryInfo> DownloadArtifacts(string buildId);

        /// <summary>
        /// Get information about the builds of specific branches
        /// </summary>
        /// <param name="branches">Names of branches</param>
        /// <returns>List of build info</returns>
        Task<IEnumerable<Build>> GetBuilds(IEnumerable<string> branches);
    }
}
