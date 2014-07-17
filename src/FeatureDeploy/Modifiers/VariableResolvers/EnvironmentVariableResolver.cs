using System;

namespace FeatureDeploy.Modifiers.VariableResolvers
{
    /// <summary>
    /// Resolve the variable using environment variable value
    /// </summary>
    public class EnvironmentVariableResolver : IVariableResolver
    {
        /// <summary>
        /// Environment key
        /// </summary>
        public string Key
        {
            get { return "env"; }
        }

        /// <summary>
        /// Replace the variable to a environment variable value
        /// </summary>
        /// <param name="args">Name of the environment variable</param>
        /// <returns>Resolved value</returns>
        public string Resolve(string args)
        {
            return Environment.GetEnvironmentVariable(args);
        }
    }
}
