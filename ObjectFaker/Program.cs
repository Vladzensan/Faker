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
            Console.WriteLine("int:{0} \nstring:{1} \ntime:{2} \ninnerObj.int:{3} \ninnerObj.obj: {4}", foo.intValue, foo.strValue, foo.timeValue, foo.dtoObj.x, foo.dtoObj.dtoObj == null);
            // foreach (var bar in foo.z)
            // {
            // Console.WriteLine("{0} {1}", bar.x, bar.y);
            // }
        }
    }
}
