using System.Threading.Tasks;
namespace FeatureDeploy
{
    /// <summary>
    /// Deploy method definition
    /// </summary>
    public interface IDeploy
    {
        /// <summary>
        /// Configuration name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Deploy the artifacts from path
        /// </summary>
        /// <param name="artifactsPath">Artifacts full path</param>
        /// <returns>Async task</returns>
        Task Deploy(string artifactsPath);
    }
}
