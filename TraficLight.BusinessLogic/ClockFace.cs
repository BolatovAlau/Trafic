﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TraficLight.BusinessLogic.Entities;

namespace TraficLight.BusinessLogic
{
    public static class ClockFace
    {
        public static Dictionary<int, int> numbers = new Dictionary<int, int>
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

        static Result GetNums(int first, int second, int firstMis, int secondMis, byte deep = 0, List<NumInfo> baseNums = null)
        {
            var tempNumbers = new List<NumInfo>();

            bool firstFound = false;
            bool secondFound = false;

            int firstBroken = 127;
            int secondBroken = 127;

            var tempFirstNumbers = new Dictionary<int, int>(); // Все возможные первые числа, рядом не достающие палки
            var tempSecondNumbers = new Dictionary<int, int>(); // Все возможные вторые числа, рядом не достающие палки

            foreach (var number in numbers) // Для каждого числа ищем совпадение по палочкам
            {
                if ((number.Value & first) == first) // Если найден паттерн по первому числу
                {
                    if ((number.Value & first) == number.Value) // Если для первой палки подходят, то нет недостающих
                    {
                        firstFound = true;
                        tempFirstNumbers.Add(number.Key, 0);
                    }
                    else
                    {
                        firstBroken = firstBroken & (number.Value - first);
                        tempFirstNumbers.Add(number.Key, number.Value - first);
                    }
                }

                if ((number.Value & second) == second) // Если найден паттерн по второму числу
                {
                    if ((number.Value & second) == number.Value) // Если для второй палки подходят, то нет недостающих
                    {
                        secondFound = true;
                        tempSecondNumbers.Add(number.Key, 0);
                    }
                    else
                    {
                        secondBroken = secondBroken & (number.Value - second);
                        tempSecondNumbers.Add(number.Key, number.Value - second);
                    }
                }
            }

            int baseFirstBroken = 127;
            int baseSecondBroken = 127;

            foreach (var firstNum in tempFirstNumbers)
            {
                foreach (var secondNum in tempSecondNumbers)
                {
                    if (deep != 0) // Если глубина не равен 0, то проверяем порядок с предыдущими
                    {
                        foreach (var baseNum in baseNums)
                        {
                            if (firstNum.Key * 10 + secondNum.Key > 0 && firstNum.Key * 10 + secondNum.Key + deep == baseNum.Start)
                            {
                                tempNumbers.Add(new NumInfo
                                {
                                    Start = baseNum.Start,
                                    FirstMissing = baseNum.FirstMissing | firstNum.Value,
                                    SecondMissing = baseNum.SecondMissing | secondNum.Value
                                });
                                baseFirstBroken = baseFirstBroken & (baseNum.FirstMissing | firstNum.Value);
                                baseSecondBroken = baseSecondBroken & (baseNum.SecondMissing | secondNum.Value);
                            }
                        }
                    }
                    else
                    {
                        tempNumbers.Add(new NumInfo
                        {
                            Start = firstNum.Key * 10 + secondNum.Key,
                            FirstMissing = firstNum.Value,
                            SecondMissing = secondNum.Value
                        });

                        baseFirstBroken = baseFirstBroken & firstNum.Value;
                        baseSecondBroken = baseSecondBroken & secondNum.Value;
                    }
                }
            }
            var result = new Result
            {
                NumInfos = tempNumbers
            };

            var totalCount = tempNumbers.Count;

            if (totalCount == 0)
            {
                result.NoResult = true;
            }
            else if (totalCount == 1)
            {
                var firstTemp = tempNumbers.First();

                result.Start = firstTemp.Start;

                result.FirstMissing = firstTemp.FirstMissing;
                result.SecondMissing = firstTemp.SecondMissing;
            }
            else
            {
                result.FirstMissing = !firstFound ? firstMis | baseFirstBroken : 0;
                result.SecondMissing = !secondFound ? secondMis | baseSecondBroken : 0;
            }

            return result;
        }

        /// <summary>
        /// Ломатель датчиков
        /// </summary>
        /// <param name="num">Цифра для ломки</param>
        /// <param name="list">Индексы датчиков</param>
        /// <returns></returns>
        public static int Broke(int num, params int[] list) // Ломаем палки (для теста)
        {
            int all = 0b111_1111;

            foreach (var l in list)
                all = all - Convert.ToByte(Math.Pow(2, 6 - l));

            return num & all;
        }

        /// <summary>
        /// Если число уже найдено, то зачем тратить время?
        /// </summary>
        /// <param name="sequence">Готовые данные</param>
        /// <param name="first">Первый паттерн числа</param>
        /// <param name="second">Второй паттерн числа</param>
        /// <returns></returns>
        public static Sequence Validate(Sequence sequence, int first, int second)
        {
            var numToTest = sequence.StartNum - sequence.CurentDeep;

            if (numToTest < 1)
                throw new Exception("There isn't enough data");

            int firstNumber = Convert.ToInt16(Math.Floor((double)numToTest / 10));
            int secondNumber = numToTest - firstNumber * 10;

            if ((numbers[firstNumber] & first) == first && (numbers[secondNumber] & second) == second) // Если найден паттерн по второму числу
            {
                sequence.CurentDeep++;
                return sequence;
            }
            else
                throw new Exception("No solutions found");
        }

        /// <summary>
        /// Обновляем данные
        /// </summary>
        /// <param name="sequence">Текущие данные</param>
        /// <param name="first">Первый паттерн числа</param>
        /// <param name="second">Второй паттерн числа</param>
        /// <param name="isRed">Красное?</param>
        /// <returns></returns>
        public static Sequence Update(Sequence sequence, int first, int second, bool isRed = false)
        {
            if (!isRed && sequence.StartNum > 0) // Если число уже найдено то просто проходит валидацию
            {
                return Validate(sequence, first, second);
            }

            List<NumInfo> starts = null;

            if (!string.IsNullOrEmpty(sequence.Start))
                starts = JsonConvert.DeserializeObject<List<NumInfo>>(sequence.Start);

            if (!isRed)
            {
                var l = GetNums(first, second,
                    sequence.FirstMissing, sequence.SecondMissing,
                    sequence.CurentDeep, starts);

                sequence.Broken = l.NoResult;
                sequence.CurentDeep = ++sequence.CurentDeep;
                sequence.IsNotFirst = true;
                sequence.Start = JsonConvert.SerializeObject(l.NumInfos);
                sequence.StartNum = l.Start;
                sequence.FirstMissing = l.FirstMissing;
                sequence.SecondMissing = l.SecondMissing;
                sequence.Id = sequence.Id;
            }
            else
            {
                if (!sequence.IsNotFirst)
                    throw new Exception("There isn't enough data");

                var lasts = starts.Where(x => x.Start - sequence.CurentDeep == 0);

                if (lasts.Count() == 0)
                    throw new Exception("The red observation should be the last");
                else
                {
                    sequence.Broken = false;
                    sequence.CurentDeep = 0;
                    sequence.IsNotFirst = false;
                    sequence.Start = JsonConvert.SerializeObject(lasts);

                    if (lasts.Count() == 1)
                    {
                        sequence.StartNum = lasts.First().Start;
                        sequence.FirstMissing = lasts.First().FirstMissing;
                        sequence.SecondMissing = lasts.First().SecondMissing;
                    }
                }
            }

            return sequence;
        }
    }
}