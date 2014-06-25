using System;
using System.Collections.Generic;
using JsonFx.Json;

namespace FeatureDeploy
{
    /// <summary>
    /// Project loader
    /// </summary>
    public class ProjectLoader
    {
        /// <summary>
        /// JSON deserializer
        /// </summary>
        private JsonReader js = new JsonReader();

        /// <summary>
        /// Load a project list from json file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>List of projects</returns>
        public IList<Project> LoadPath(string path)
        {
            string json;
            using (var reader = new System.IO.StreamReader(path))
            {
                json = reader.ReadToEnd();
            }

            return this.Load(json);
        }

        /// <summary>
        /// Load a projects list from json format
        /// </summary>
        /// <param name="json">JSON content</param>
        /// <returns>List of projects</returns>
        public IList<Project> Load(string json)
        {
            var projects = new List<Project>();
            dynamic projectsDefinition = js.Read(json);
            if (projectsDefinition is System.Dynamic.ExpandoObject[])
            {
                foreach (var project in projectsDefinition)
                    projects.Add(Load<Project>(project));
            }
            else
            {
                projects.Add(Load<Project>(projectsDefinition));
            }

            return projects;
        }

        /// <summary>
        /// Load a property from json file
        /// </summary>
        /// <typeparam name="T">Property type</typeparam>
        /// <param name="json">JSON property</param>
        /// <returns>Object with properties loaded</returns>
        public T Load<T>(dynamic json)
        {
            var properties = (IDictionary<string, object>)json;
            var type = properties.ContainsKey("Type") ? Type.GetType(json.Type as string) : typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var p in properties)
            {
                var property = type.GetProperty(p.Key);
                if (property != null)
                {
                    var value = property.PropertyType.IsValueType || property.PropertyType == typeof(string)
                        ? p.Value
                        : Load<object>(p.Value);

                    // TODO: add support to environment variables
                    property.SetValue(obj, value);
                }
            }

            return (T)obj;
        }
    }
}