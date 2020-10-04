using System;

namespace Faker
{
    public interface IGeneratorBundle
    {
        object GenerateObject(Type type);
        bool HasTypeGenerator(Type type);
    }
}
