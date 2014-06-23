namespace FeatureDeploy
{
    /// <summary>
    /// Build information
    /// </summary>
    public class Build
    {
        /// <summary>
        /// Build id from build server
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Branch name
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Build status from build server
        /// </summary>
        public BuildStatus Status { get; set; }
    }
}
