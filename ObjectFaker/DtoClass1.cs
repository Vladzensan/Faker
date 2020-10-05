using Faker;
using System;

namespace ObjectFaker
{
    [DTO]
    class DtoClass1
    {
        private DtoClass1(){}

        public int intValue;
        public string strValue;
        public DateTime timeValue;

        public DtoClass2 dtoObj;
    }
}
