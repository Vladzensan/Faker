using Faker;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectFaker
{
    class Program
    {

        static void Main(string[] args)
        {
            IFaker faker = new DtoFaker(new GeneratorBundle());
            DtoClass1 foo = faker.Create<DtoClass1>();
            Console.WriteLine("{0} {1} {2}", foo.intValue, foo.strValue, foo.timeValue);
            // foreach (var bar in foo.z)
            // {
            // Console.WriteLine("{0} {1}", bar.x, bar.y);
            // }
        }
    }
}
