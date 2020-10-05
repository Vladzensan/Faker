using PluginBase;
using System;
using System.Text;

namespace PrimitivesGenerator
{
    class PrimitivesGenerator : IGenerator
    {
        private readonly Random _random = new Random();
        public object Generate(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean: return _random.Next(0, 2) > 0;

                case TypeCode.Byte: return (byte)_random.Next();

                case TypeCode.SByte: return (sbyte)_random.Next();

                case TypeCode.Int16: return (short)_random.Next();

                case TypeCode.UInt16: return (ushort)_random.Next();

                case TypeCode.Int32: return _random.Next();

                case TypeCode.UInt32: return (uint)_random.Next();

                case TypeCode.Int64: return GenerateLong();

                case TypeCode.UInt64: return (ulong)GenerateLong();

                case TypeCode.Single: return (float)_random.NextDouble();

                case TypeCode.Double: return _random.NextDouble();

                case TypeCode.Decimal: return new decimal(_random.NextDouble());

                case TypeCode.Char: return (char)_random.Next('A', 'z');

                case TypeCode.String: return GenerateString();

                default: return null;
            }
        }

        private long GenerateLong()
        {
            byte[] buffer = new byte[8];
            _random.NextBytes(buffer);

            return BitConverter.ToInt64(buffer, 0);
        }

        private String GenerateString()
        {
            int length = _random.Next(10, 20);
            var builder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                 builder.Append((char)_random.Next('A', 'z'));
            }

            return builder.ToString();
        }

        public bool IsGeneratable(Type type)
        {
            if (type == typeof(byte) || type == typeof(sbyte) || type == typeof(short)
                || type == typeof(ushort) || type == typeof(int) || type == typeof(uint)
                || type == typeof(long) || type == typeof(ulong) || type == typeof(float)
                || type == typeof(double) || type == typeof(decimal) || type == typeof(char)
                || type == typeof(string) || type == typeof(bool))
            {
                return true;
            }

            return false;
        }
    }
}
