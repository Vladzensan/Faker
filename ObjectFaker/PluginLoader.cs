using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ObjectFaker
{
    class PluginLoader<T>
    {

        private string _pluginPath;

        public List<T> Plugins { get; }

        public PluginLoader(string pluginPath)
        {
            Plugins = new List<T>();
            _pluginPath = pluginPath;
            RefreshPlugins();
        }

        private void RefreshPlugins()
        {
            Plugins.Clear();

            DirectoryInfo pluginDirectory = new DirectoryInfo(_pluginPath);
            if (!pluginDirectory.Exists)
            {
                pluginDirectory.Create();
                return;
            }

            var pluginFiles = Directory.GetFiles(_pluginPath, "*.dll");
            foreach (var file in pluginFiles)
            {
                Assembly asm = Assembly.LoadFrom(file);
                var types = asm.GetTypes().
                    Where(t => t.GetInterfaces().
                        Where(i => i.FullName == typeof(T).FullName).Any());
                foreach (var type in types)
                {
                    var plugin = (T)asm.CreateInstance(type.FullName);
                    Plugins.Add(plugin);
                }
            }
        }

    }
}
