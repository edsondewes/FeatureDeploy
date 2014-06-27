namespace FeatureDeploy.Modifiers
{
    public interface IVariableResolver
    {
        /// <summary>
        /// Key to search
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Resolve the variable
        /// </summary>
        /// <param name="args">Parameters to resolve</param>
        /// <returns>Resolved value</returns>
        string Resolve(string args);
    }
}
