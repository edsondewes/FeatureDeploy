using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace FeatureDeploy.VCS
{
    /// <summary>
    /// GitHub VCS definition
    /// </summary>
    public class GitHub : IVCSSource
    {
        /// <summary>
        /// GitHub client
        /// </summary>
        private Lazy<GitHubClient> client;

        /// <summary>
        /// Configuration name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Project owner username
        /// </summary>
        public string ProjectOwner { get; set; }

        /// <summary>
        /// Project name
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Username to authenticate 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password to authenticate
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GitHub()
        {
            this.client = new Lazy<GitHubClient>(() =>
            {
                var c = new GitHubClient(new ProductHeaderValue(this.ProjectName));
                c.Credentials = new Credentials(this.Username, this.Password);
                return c;
            });
        }

        /// <summary>
        /// Get a branches list from VCS
        /// </summary>
        /// <returns>List of branches names</returns>
        public async Task<IEnumerable<string>> GetBranches()
        {
            var branches = await this.client.Value.Repository.GetAllBranches(this.ProjectOwner, this.ProjectName);
            var names = branches.Select(b => b.Name).ToList();

            return names;
        }
    }
}
