using System;
using System.Collections.Generic;

namespace Faker
{
    public class GeneratorBundle: IGeneratorBundle
    {
        private List<IGenerator> generators;

        public GeneratorBundle(List<IGenerator> generators)
        {
            this.generators = generators;
        }

        public object GenerateObject(Type type)
        {
            throw new NotImplementedException();
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
