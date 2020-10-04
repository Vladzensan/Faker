using System;

namespace PluginBase
{
    public interface IGenerator
    {
        object Generate(Type type);
        bool IsGeneratable(Type type);
    }
}
