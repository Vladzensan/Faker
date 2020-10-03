using System;

namespace Faker
{
    public interface IFaker
    {
        T Create<T>();
        bool IsCreatable(Type type);
    }
}
