using System;
using System.Linq;
using System.Reflection;

namespace Faker
{
    public class DtoFaker: IFaker
    {
        private GeneratorBundle genBundle;
        public DtoFaker(GeneratorBundle genBundle)
        {
            this.genBundle = genBundle;
        }

        public T Create<T>()
        {
            Type type = typeof(T);
            if (!IsDto(type))
            {
                return default(T);
            }

            var constructor = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
            object obj = constructor.Invoke(null);

            SetFieldsAndProps(obj);
                
            return (T)obj;
        }

        private void SetFieldsAndProps(object obj)
        {
            obj.GetType()
                .GetFields()
                .ToList()
                .ForEach(f => f.SetValue(obj, GenerateTypeValue(f.FieldType)));

            obj.GetType()
                .GetProperties()
                .ToList()
                .ForEach(p => p.SetValue(obj, GenerateTypeValue(p.PropertyType)));
        }

        private object GenerateTypeValue(Type type)
        {
            if (genBundle.HasTypeGenerator(type))
            {
                return genBundle.GenerateObject(type);
            }

            if(IsDto(type))
            {
                MethodInfo create = typeof(DtoFaker).GetMethod("Create").MakeGenericMethod(type);
                return create.Invoke(this, null);
            }

            return null;
        }

        public bool IsCreatable(Type type)
        {
            return IsDto(type);
        }   

        private bool IsDto(Type type)
        {
            return type.GetCustomAttributes(typeof(DTO), false).Length == 1;
        }
    }
}
