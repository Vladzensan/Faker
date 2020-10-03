using System;

namespace Faker
{
    public interface IFaker
    {
        T Create<T>(T type);
        bool IsCreatable(Type type);
    }
}
