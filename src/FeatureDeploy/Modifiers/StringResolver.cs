using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FeatureDeploy.Modifiers
{
    /// <summary>
    /// Resolve variables in a string
    /// </summary>
    public class StringResolver
    {
        /// <summary>
        /// Resolver definitions
        /// </summary>
        public IList<IVariableResolver> Resolvers { get; set; }

        /// <summary>
        /// Initializes a new instance of the StringResolver
        /// </summary>
        public StringResolver()
        {
            this.Resolvers = new List<IVariableResolver>();
        }

        /// <summary>
        /// Find and replace variables using defined resolvers
        /// </summary>
        /// <param name="phrase">Phrase to be replaced</param>
        /// <returns>Phrase with resolved values</returns>
        public string Resolve(string phrase)
        {
            foreach (var resolver in this.Resolvers)
            {
                var regex = new Regex(string.Format(@":{0}\(([\w\d\s]+\)|\))", resolver.Key));
                var regexMatches = regex.Matches(phrase);

                var matches = new List<string>();
                foreach (var item in regexMatches)
                    matches.Add(item.ToString());

                foreach (var match in matches.Distinct())
                {
                    var argsBegin = match.IndexOf('(');
                    var argsEnd = match.IndexOf(')');
                    var args = match.ToString().Substring(argsBegin + 1, argsEnd - argsBegin - 1);

                    phrase = phrase.Replace(match, resolver.Resolve(args));
                }
            }

            return phrase;
        }
    }
}
