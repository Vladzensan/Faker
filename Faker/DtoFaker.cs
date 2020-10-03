using System;

namespace Faker
{
    public class DtoFaker: IFaker
    {
        private GeneratorBundle genBundle;
        public DtoFaker(GeneratorBundle genBundle)
        {
            this.genBundle = genBundle;
        }

        public T Create<T>(T type)
        {
            throw new NotImplementedException();
        }

        public bool IsCreatable(Type type)
        {
            throw new NotImplementedException();
        }

        private bool IsDto(Type type)
        {
            return true;
        }
    }
}
