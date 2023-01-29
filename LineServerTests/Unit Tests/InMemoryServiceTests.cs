using AutoFixture;
using LineServer.Service;
using NUnit.Framework;
using System;
using System.Linq;

namespace LineServer.Tests
{
    [TestFixture]
    [Category("Services tests")]
    public class InMemoryServiceTests
    {
        private Fixture fixture = new Fixture();

        /* 
         * For the first implementation of the service that was reviewed for performance purposes
         * 
        [Test]
        public void GetLine_Should_ThrowException_When_IndexIsNegative()
        {
            //arrange
            IFileService service = new InMemoryService();
            Random rand = new Random();

            //act & assert
            var exception = Assert.Throws<IndexOutOfRangeException>(() => service.GetLine(rand.Next(int.MinValue, -1)));
            Assert.AreEqual("Index was outside the bounds of the array.", exception.Message);
        }

        [Test]
        public void GetLine_Should_ThrowException_When_IndexIsTooHigh()
        {
            //arrange
            IFileService service = new InMemoryService();
            Random rand = new Random();
            InMemoryService.FileInfo = fixture.CreateMany<string>(3).ToArray();

            //act & assert
            var exception = Assert.Throws<IndexOutOfRangeException>(() => service.GetLine(rand.Next(3, int.MaxValue)));
            Assert.AreEqual("Index was outside the bounds of the array.", exception.Message);
        }
        */

        [Test]
        public void GetLine_Should_ReturnNull_When_IndexIsNegative()
        {
            //arrange
            IFileService service = new InMemoryService();
            Random rand = new Random();

            //act
            var result = service.GetLine(rand.Next(int.MinValue, -1));

            //assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetLine_Should_ReturnNull_When_IndexIsTooHigh()
        {
            //arrange
            IFileService service = new InMemoryService();
            Random rand = new Random();
            InMemoryService.FileInfo = fixture.CreateMany<string>(3).ToArray();

            //act
            var result = service.GetLine(rand.Next(3, int.MaxValue));

            //assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetLine_Should_ReturnValue_When_IndexAskedIsWithValue()
        {
            //arrange
            IFileService service = new InMemoryService();
            Random rand = new Random();
            InMemoryService.FileInfo = fixture.CreateMany<string>(3).ToArray();
            int index = rand.Next(0, 2);

            //act
            var value = service.GetLine(index);

            //asset
            Assert.AreEqual(InMemoryService.FileInfo[index], value);
        }

    }
}
