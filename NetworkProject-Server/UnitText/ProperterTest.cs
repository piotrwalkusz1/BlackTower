using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkProject;

namespace UnitTest
{
    [TestClass]
    public class ProperterTest
    {
        [TestMethod]
        public void Get_ReturnSameValueLikeSet()
        {
            var setProperty = new TestClass();
            setProperty._breedAndGender = new BreedAndGender(10, true);
            setProperty.Count= 132;

            var properter = new Properter<ITestInterface>(setProperty);
            var getProperty = properter.Get();

            Assert.AreEqual(setProperty.BreedAndGender.Breed, getProperty.BreedAndGender.Breed);
            Assert.AreEqual(setProperty.BreedAndGender.IsMale, getProperty.BreedAndGender.IsMale);
            Assert.AreEqual(setProperty.Count, getProperty.Count);
        }
    }

    public interface ITestInterface
    {
        BreedAndGender BreedAndGender { get; }

        int Count { get; set; }
    }

    class TestClass : ITestInterface
    {
        public BreedAndGender BreedAndGender
        {
            get { return _breedAndGender; }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public BreedAndGender _breedAndGender;

        private int _count;

        private int _settler;
    }
}
