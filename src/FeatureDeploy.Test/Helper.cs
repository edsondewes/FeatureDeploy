using System;
using System.Collections.Generic;
using FeatureDeploy.BuildServer;
using FeatureDeploy.Deploy;
using FeatureDeploy.VCS;

namespace FeatureDeploy.Test
{
    public static class Helper
    {
        public static IEnumerable<IVCSSource> VCSSource { get; set; }
        public static IEnumerable<IBuildServer> BuildServer { get; set; }
        public static IEnumerable<IDeploy> Deploy { get; set; }

        static Helper()
        {
            VCSSource = new List<IVCSSource>
            {
                new GitHub
                {
                    Name = "GitHub",
                    ProjectName = "FeatureDeploy",
                    ProjectOwner = "edsondewes",
                    Username = Environment.GetEnvironmentVariable("FD_GITHUBUSERNAME"),
                    Password = Environment.GetEnvironmentVariable("FD_GITHUBPASSWORD")
                }
            };

            BuildServer = new List<IBuildServer>
            {
                new TeamCity
                {
                    Name = "TeamCity",
                    ConfigurationId = "FeatureDeploy_Main",
                    Password = "123@qwe",
                    Username = "admin",
                    Url = "localhost:8081"
                }
            };

            Deploy = new List<IDeploy>
            {
                new CommandLine
                {
                    Name = "FeatureDeployWeb",
                    Command = "DeployPackage\\FeatureDeploy.Web.deploy.cmd",
                    Arguments = "/y /M:https://localhost:8172/MsDeploy.axd /A:Basic -allowUntrusted"
                }
            };
        }
    }
}