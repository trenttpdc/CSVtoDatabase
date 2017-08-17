using System;
using System.Collections.Generic;

namespace CSVtoDatabase
{
    class Parser
    {
        public static List<string> ParseLinesContaining(List<string> lines, string searchParam)
        {
            //Returns only lines containing the search parameter

            List<string> parsedLines = new List<string>();
            foreach (string line in lines)
            {
                if (line.Contains(searchParam))
                {
                    parsedLines.Add(line);
                }
            }
            return parsedLines;
        }

        public static List<Object> parseConfigFile(List<string> lines)
        {
            string rootPath = "";
            string importPath = "";
            string tablePath = "";

            foreach (string line in lines)
            {
                if (line.StartsWith(Configuration.RootPathPrefix)) //downloadPath
                {
                    rootPath = line.Remove(0, Configuration.RootPathPrefix.Length);
                    continue;
                }
                else if (line.StartsWith(Configuration.ImportPathPrefix)) //importPath
                {
                    importPath = line.Remove(0, Configuration.ImportPathPrefix.Length);
                    continue;
                }
                else if (line.StartsWith(Configuration.TablePathPrefix)) //tablePath
                {
                    tablePath = line.Remove(0, Configuration.TablePathPrefix.Length);
                    continue;
                }
            }

            List<Object> configurationValues = new List<Object>();
            configurationValues.Add(rootPath);
            configurationValues.Add(importPath);
            configurationValues.Add(tablePath);
            
            return configurationValues;
        }
    }
}