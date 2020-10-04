using ObjectFaker;
using PluginBase;
using System;
using System.Collections.Generic;

namespace Faker
{
    public class GeneratorBundle: IGeneratorBundle
    {
        private const string NO_GENERATOR_MSG = "No generator corresponding to type: ";
        private List<IGenerator> generators;


        public GeneratorBundle()
        {
            PluginLoader<IGenerator> loader = new PluginLoader<IGenerator>();

            string[] pluginPaths =
           {
                @"PrimitivesGenerator\bin\Debug\BaseTypeGenerator.dll",
                @"DateTimeGenerator\bin\Debug\DateTimeGenerator.dll"
            };

            generators = loader.LoadPlugins(pluginPaths);

        }
        public GeneratorBundle(List<IGenerator> generators)
        {
            this.generators = generators;
        }

        public object GenerateObject(Type type)
        {
            object target = null;
            foreach (IGenerator generator in generators)
            {
                if (generator.IsGeneratable(type))
                {
                    target =  generator.Generate(type);
                    break;
                }
            }

            if(target == null)
            {
                throw new ArgumentException(NO_GENERATOR_MSG + type.ToString());
            }

            return target;
        }

        public bool HasTypeGenerator(Type type)
        {
            foreach(IGenerator generator in generators) {
                if (generator.IsGeneratable(type))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
