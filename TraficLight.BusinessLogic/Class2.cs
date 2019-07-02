using System;
using System.Collections.Generic;

namespace TraficLight.BusinessLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            var tempNumbers = new List<int>();

            var tempFirstNumbers = new List<int>();
            var tempSecondNumbers = new List<int>();

            byte first = 0b1110111;
            byte second = 0b0011101;

            int brokenFirst = 0b111_1111;
            int brokenSecond = 0b111_1111;

            var numbers = new Dictionary<byte, byte>
            {
                { 0, 0b111_0111 },
                { 1, 0b001_0010 },
                { 2, 0b101_1101 },
                { 3, 0b101_1011 },
                { 4, 0b011_1010 },
                { 5, 0b110_1011 },
                { 6, 0b110_1111 },
                { 7, 0b101_0010 },
                { 8, 0b111_1111 },
                { 9, 0b111_1011 }
            };

            foreach (var temp in numbers)
            {
                if ((temp.Value & first) == first)
                {
                    tempFirstNumbers.Add(temp.Key);

                    brokenFirst = brokenFirst & temp.Value;
                }

                if ((temp.Value & second) == second)
                {
                    tempSecondNumbers.Add(temp.Key);

                    brokenSecond = brokenSecond & temp.Value;
                }
            }

            brokenFirst = brokenFirst - first;
            brokenSecond = brokenSecond - second;

            Console.WriteLine(Convert.ToString(brokenFirst, 2).PadLeft(7, '0'));
            Console.WriteLine(Convert.ToString(brokenSecond, 2).PadLeft(7, '0'));

            foreach (var firstNum in tempFirstNumbers)
            {
                foreach (var secondNum in tempSecondNumbers)
                {
                    tempNumbers.Add(firstNum * 10 + secondNum);
                }
            }

            foreach (var firstNum in tempNumbers)
            {
                Console.WriteLine(firstNum);
            }

            Console.Read();
        }
    }
}
