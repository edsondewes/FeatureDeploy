using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureDeploy
{
    public interface IVCSSource
    {
        /// <summary>
        /// Configuration name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Get a branches list from VCS
        /// </summary>
        /// <returns>List of branches names</returns>
        Task<IEnumerable<string>> GetBranches();
    }
}
