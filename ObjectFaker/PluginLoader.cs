using Faker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ObjectFaker
{
    class PluginLoader<T>
    {

        public List<T> LoadPlugins(String[] pluginPaths)
        {
            return pluginPaths.SelectMany(pluginPath =>
            {
                Assembly pluginAssembly = LoadAssembly(pluginPath);
                return CreateGenerators(pluginAssembly);
            }).ToList();
        }

        private Assembly LoadAssembly(string relativePath)
        {
            // Navigate up to the solution root
            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

            string pluginLocation = Path.GetFullPath(Path.Combine(root,
                relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            Console.WriteLine($"Loading commands from: {pluginLocation}");
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(
                new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        private IEnumerable<T> CreateGenerators(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(type))
                {
                    T result = (T)Activator.CreateInstance(type);
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements IGenerator in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }

    }
}
