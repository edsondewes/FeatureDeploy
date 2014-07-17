using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatureDeploy.Test
{
    [TestClass]
    public class ProjectLoaderTest
    {
        private const string JsonPath = "fixture\\project.json";

        [TestMethod]
        public void LoadPath_should_load_a_list()
        {
            var projects = new ProjectLoader().LoadPath(JsonPath);

            Assert.IsNotNull(projects);
            Assert.IsTrue(projects.Count > 0);
        }

        [TestMethod]
        public void LoadPath_should_load_interface_properties()
        {
            var projects = new ProjectLoader().LoadPath(JsonPath);
            var p = projects.First();

            Assert.IsNotNull(p.BuildServer);
            Assert.IsNotNull(p.DeployMethod);
            Assert.IsNotNull(p.VCS);
        }

        [TestMethod]
        public void Load_from_json_should_return_a_list_even_the_definition_is_not_an_array()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Load_from_json_should_return_the_same_number_of_definitions()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Load_should_use_the_type_property_if_defined()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Load_should_use_the_T_type_if_not_defined_in_json()
        {
            throw new NotImplementedException();
        }

        public void Load_should_load_complex_types()
        {
            throw new NotImplementedException();
        }
    }
}