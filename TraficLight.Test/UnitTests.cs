using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TraficLight.BusinessLogic;
using TraficLight.BusinessLogic.Entities;

namespace Tests
{
    public class Tests
    {
        List<Sequence> sequenceInMemory = new List<Sequence>();
        static Mock<ISequenceRepository> repository = new Mock<ISequenceRepository>();
        ISequenceRepository library = repository.Object;

        [SetUp]
        public void Setup()
        {
            repository.Setup(x => x.Clear()).Callback(() => sequenceInMemory.Clear());
            repository.Setup(x => x.Create()).Callback(() => sequenceInMemory.Add(new Sequence
            {
                Id = Guid.NewGuid().ToString()
            }));
        }

        [Test]
        public void Create()
        {
            Assert.IsEmpty(sequenceInMemory);

            library.Create();

            Assert.IsNotEmpty(sequenceInMemory);

            Assert.IsTrue(!sequenceInMemory[0].IsNotFirst);
        }

        [Test]
        public void Clear()
        {
            Assert.IsEmpty(sequenceInMemory);

            library.Create();

            Assert.IsNotEmpty(sequenceInMemory);

            library.Clear();

            Assert.IsEmpty(sequenceInMemory);
        }

        [Test]
        public void Add()
        {
            Assert.IsEmpty(sequenceInMemory);

            library.Create();

            Assert.IsNotEmpty(sequenceInMemory);

            library.Clear();

            Assert.IsEmpty(sequenceInMemory);
        }
    }
}