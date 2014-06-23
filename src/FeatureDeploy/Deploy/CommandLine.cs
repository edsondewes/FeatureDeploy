using System.Diagnostics;
using System.Threading.Tasks;

namespace FeatureDeploy.Deploy
{
    /// <summary>
    /// Command line deploy deploy definition
    /// </summary>
    public class CommandLine : IDeploy
    {
        /// <summary>
        /// Configuration name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Command to be executed
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Command arguments
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// Deploy the artifacts from path
        /// </summary>
        /// <param name="artifactsPath">Artifacts full path</param>
        /// <returns>Async task</returns>
        public async Task Deploy(string artifactsPath)
        {
            var process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            startInfo.FileName = string.Format("{0}\\{1}", artifactsPath, this.Command);
            startInfo.Arguments = this.Arguments;
            process.StartInfo = startInfo;
            process.Start();

            await Task.Factory.StartNew(() => process.WaitForExit());
        }
    }
}