using Faker;
using System;

namespace ObjectFaker
{
    class Program
    {
        private const string PLUGINS_PATH = "C:\\Users\\Vladzensan\\source\\repos\\FakerSolution\\Plugins";

        static void Main(string[] args)
        {
            IFaker faker = new DtoFaker(new GeneratorBundle(PLUGINS_PATH));
            Console.WriteLine("loader");
            DtoClass1 foo = faker.Create<DtoClass1>();
            Console.WriteLine("{0} {1} {2}", foo.intValue, foo.strValue, foo.timeValue);
            // foreach (var bar in foo.z)
            // {
            // Console.WriteLine("{0} {1}", bar.x, bar.y);
            // }
        }
    }
}
