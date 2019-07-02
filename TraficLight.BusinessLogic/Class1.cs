using System;
using System.Collections.Generic;

namespace TraficLight.BusinessLogic
{
    public static class ClockFace
    {
        public static Dictionary<byte, byte> numbers = new Dictionary<byte, byte>
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

        public static NumsAndBroken GetNums(byte first, byte second, byte deep = 0, int[] baseNums = null)
        {
            var tempNumbers = new List<int>();

            var tempFirstNumbers = new List<int>();
            var tempSecondNumbers = new List<int>();

            int brokenFirst = 0b111_1111;
            int brokenSecond = 0b111_1111;

            foreach (var number in numbers)
            {
                if ((number.Value & first) == first)
                {
                    tempFirstNumbers.Add(number.Key);
                    brokenFirst = brokenFirst & number.Value;
                }

                if ((number.Value & second) == second)
                {
                    tempSecondNumbers.Add(number.Key);
                    brokenSecond = brokenSecond & number.Value;
                }
            }

            brokenFirst = brokenFirst - first;
            brokenSecond = brokenSecond - second;

            foreach (var firstNum in tempFirstNumbers)
            {
                foreach (var secondNum in tempSecondNumbers)
                {
                    if (deep != 0)
                    {
                        foreach (var baseNum in baseNums)
                        {
                            if (firstNum * 10 + secondNum + deep == baseNum)
                            {
                                tempNumbers.Add(baseNum);
                            }
                        }
                    }
                    else
                    {
                        tempNumbers.Add(firstNum * 10 + secondNum);
                    }
                }
            }

            return new NumsAndBroken
            {
                BrokenFirst = brokenFirst,
                BrokenSecond = brokenSecond,
                Nums = tempNumbers
            };
        }
    }

    public class NumsAndBroken
    {
        public List<int> Nums { get; set; }
        public int BrokenFirst { get; set; }
        public int BrokenSecond { get; set; }
    }
}