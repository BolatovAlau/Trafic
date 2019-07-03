using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TraficLight.BusinessLogic;
using TraficLight.BusinessLogic.Entities;
using TraficLight.BusinessLogic.Models;

namespace Tests
{
    public class Tests
    {
        List<Sequence> sequenceInMemory = new List<Sequence>();
        static Mock<ISequenceRepository> repository = new Mock<ISequenceRepository>();
        ISequenceRepository library = repository.Object;
        string guid = Guid.NewGuid().ToString();

        public static Dictionary<int, byte> numbers = new Dictionary<int, byte>
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

        [SetUp]
        public void Setup()
        {
            repository.Setup(x => x.Clear()).Callback(() => sequenceInMemory.Clear());
            repository.Setup(x => x.Create()).Callback(() => sequenceInMemory.Add(new Sequence
            {
                Id = guid
            }));
            repository.Setup(x => x.Add(It.IsAny<Request>())).Callback(() => sequenceInMemory.Add(new Sequence
            {
                Id = guid
            }));
        }

        [Test]
        public void CreateAndClear() // Базовые тесты
        {
            library.Clear();

            Assert.IsEmpty(sequenceInMemory);

            library.Create();

            Assert.IsNotEmpty(sequenceInMemory);
            Assert.IsTrue(!sequenceInMemory[0].IsNotFirst);
            Assert.IsTrue(!sequenceInMemory[0].Broken);
            Assert.AreEqual(sequenceInMemory[0].CurentDeep, 0);

            library.Clear();

            Assert.IsEmpty(sequenceInMemory);
        }

        [Test]
        public void Broke() // Ломаем палочку в ручную для теста
        {
            int[] brokenFirstNums = { 1 };
            int[] brokenSecondNums = { 3, 5 };
            int[] brokenThirdNums = { 0, 2, 4, 6 };

            int q1 = ClockFace.Broke(numbers[8], brokenFirstNums);
            int q2 = ClockFace.Broke(numbers[7], brokenSecondNums);
            int q3 = ClockFace.Broke(numbers[2], brokenThirdNums);

            Assert.AreEqual(q1, 95);
            Assert.AreEqual(q2, 80);
            Assert.AreEqual(q3, 8);
        }

        [Test]
        public void AddUntilZero() // 99-1 (broken 1[0,3,4], 2[2,5,6])
        {
            library.Clear();
            library.Create();

            List<int> nums = new List<int>();

            for (int i = 99; i > 0; i--)
                nums.Add(i);

            int[] brokenFirstNums = { 0, 3, 4 }; // 76 (1001100)
            int[] brokenSecondNums = { 2, 5, 6 }; // 19 (0011001)

            for (int i = 0; i < nums.Count; i++)
            {
                int firstNumber = Convert.ToInt16(Math.Floor((double)nums[i] / 10));
                int secondNumber = nums[i] - firstNumber * 10;

                int brokenFirst = ClockFace.Broke(numbers[firstNumber], brokenFirstNums);
                int brokenSecond = ClockFace.Broke(numbers[secondNumber], brokenSecondNums);

                sequenceInMemory[0] = ClockFace.Update(sequenceInMemory[0], brokenFirst, brokenSecond);

                if (sequenceInMemory[0].Broken)
                    break;
            }

            Assert.IsTrue(sequenceInMemory[0].StartNum == 99);
            Assert.IsTrue(sequenceInMemory[0].FirstMissing == 76);
            Assert.IsTrue(sequenceInMemory[0].SecondMissing == 19);
        }

        [Test]
        public void AddWithError() // 8,7,9!
        {
            library.Clear();
            library.Create();

            List<int> nums = new List<int>()
            {
                8,
                7,
                9 // здесь ошибка, должно было быть 6
            };

            int[] brokenFirstNums = { 0 };
            int[] brokenSecondNums = { 5, 6 };

            for (int i = 0; i < nums.Count; i++)
            {
                int firstNumber = Convert.ToInt16(Math.Floor((double)nums[i] / 10));
                int secondNumber = nums[i] - firstNumber * 10;

                int brokenFirst = ClockFace.Broke(numbers[firstNumber], brokenFirstNums);
                int brokenSecond = ClockFace.Broke(numbers[secondNumber], brokenSecondNums);

                sequenceInMemory[0] = ClockFace.Update(sequenceInMemory[0], brokenFirst, brokenSecond);

                if (sequenceInMemory[0].Broken)
                    break;
            }

            Assert.IsTrue(sequenceInMemory[0].Broken); // Поймал ли ошибку?
        }

        [Test]
        public void NotFoundStartButFoundErrors() // Как в примере на задаче
        {
            library.Clear();
            library.Create();

            List<int> nums = new List<int>()
            {
                2,
                1
            };

            int[] brokenSecondNums = { 0, 5 };

            for (int i = 0; i < nums.Count; i++)
            {
                int firstNumber = Convert.ToInt16(Math.Floor((double)nums[i] / 10));
                int secondNumber = nums[i] - firstNumber * 10;

                int brokenSecond = ClockFace.Broke(numbers[secondNumber], brokenSecondNums);

                sequenceInMemory[0] = ClockFace.Update(sequenceInMemory[0], numbers[firstNumber], brokenSecond);

                if (sequenceInMemory[0].Broken)
                    break;
            }

            Assert.AreEqual(sequenceInMemory[0].SecondMissing, 64);
        }

        [Test]
        public void TheRedObservationShouldBeTheLast() // 3,2,1 red
        {
            library.Clear();
            library.Create();

            List<int> nums = new List<int>()
            {
                3,
                2,
                1
            };

            for (int i = 0; i < nums.Count; i++)
            {
                int firstNumber = Convert.ToInt16(Math.Floor((double)nums[i] / 10));
                int secondNumber = nums[i] - firstNumber * 10;

                sequenceInMemory[0] = ClockFace.Update(sequenceInMemory[0], numbers[firstNumber], numbers[secondNumber]);

                if (sequenceInMemory[0].Broken)
                    break;
            }

            Assert.IsTrue(sequenceInMemory[0].StartNum != 3); //Есть несколько вариантов, но ток у 3 есть доступ к нулю

            sequenceInMemory[0] = ClockFace.Update(sequenceInMemory[0], 0, 0, true); // Красный свет 

            Assert.IsTrue(sequenceInMemory[0].StartNum == 3);
        }

        [Test]
        public void TheRedObservationShouldBeTheLastWithBroken() // 25-1 (broken 1[0,1,2], 2[3,4,5])
        {
            library.Clear();
            library.Create();

            List<int> nums = new List<int>();

            for (int i = 25; i > 0; i--)
                nums.Add(i);

            int[] brokenFirstNums = { 0, 1, 2 };
            int[] brokenSecondNums = { 3, 4, 5 };

            for (int i = 0; i < nums.Count; i++)
            {
                int firstNumber = Convert.ToInt16(Math.Floor((double)nums[i] / 10));
                int secondNumber = nums[i] - firstNumber * 10;

                int brokenFirst = ClockFace.Broke(numbers[firstNumber], brokenFirstNums);
                int brokenSecond = ClockFace.Broke(numbers[secondNumber], brokenSecondNums);

                sequenceInMemory[0] = ClockFace.Update(sequenceInMemory[0], brokenFirst, brokenSecond);

                if (sequenceInMemory[0].Broken)
                    break;
            }

            Assert.IsTrue(sequenceInMemory[0].StartNum != 25); //Есть несколько вариантов (25, 85), но ток у 25 есть доступ к нулю (25-25=0)

            sequenceInMemory[0] = ClockFace.Update(sequenceInMemory[0], 0, 0, true); // Красный свет 

            Assert.IsTrue(sequenceInMemory[0].StartNum == 25);
        }
    }
}