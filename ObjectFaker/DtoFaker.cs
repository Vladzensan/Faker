using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Faker
{
    public class DtoFaker: IFaker
    {
        private GeneratorBundle genBundle;
        private Stack<Type> typesProcessed;
        public DtoFaker(GeneratorBundle genBundle)
        {
            this.genBundle = genBundle;
            typesProcessed = new Stack<Type>();
        }

        public T Create<T>()
        {

            Type type = typeof(T);
            if (!IsDto(type))
            {
                return default(T);
            }

            typesProcessed.Push(typeof(T));

            var constructor = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null);
            object obj = constructor.Invoke(null);

            SetFieldsAndProps(obj);

            typesProcessed.Pop();
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

            if(IsDto(type) && !typesProcessed.Contains(type))
            {
                MethodInfo create = typeof(DtoFaker).GetMethod("Create").MakeGenericMethod(type);
                return create.Invoke(this, null);
            }

            if (IsDto(type) && typesProcessed.Contains(type))
            {
                return null;
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                GenerateEnumerable(type);
            }

            return null;
        }

        private object GenerateEnumerable(Type type)
        {
            if (type.GetGenericTypeDefinition() == typeof(List<>))
            {
                var listType = type.GetGenericTypeDefinition();
                var genericType = type.GetGenericArguments()[0];
                var constructedList = listType.MakeGenericType(genericType);
                var random = new Random();
                byte length = (byte)random.Next(1, 11);
                object[] parameters = { length };
                var instance = Activator.CreateInstance(constructedList, parameters);
                for (int i = 0; i < length; i++)
                {
                    instance.GetType().GetMethod("Add")
                        .Invoke(instance, new[] { GenerateTypeValue(genericType) });
                }

                return instance;
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
