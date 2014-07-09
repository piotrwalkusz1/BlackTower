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

            var getProperty = (ITestInterface)Properter.GetProperter(setProperty);

            Assert.AreEqual(setProperty.BreedAndGender.Breed, getProperty.BreedAndGender.Breed);
            Assert.AreEqual(setProperty.BreedAndGender.IsMale, getProperty.BreedAndGender.IsMale);
            Assert.AreEqual(setProperty.Count, getProperty.Count);
        }

        [TestMethod]
        public void ImplementAllInterfaces_DerivedAndBaseInterfaces()
        {
            var test = new DerivedInterfaceTestClass();
            test.FirstCount = 13.5f;
            test.SecoundCount = -99.2f;

            var propertyBase = (IBaseInterface)Properter.GetProperter(test);

            IDerivedInterface propertyDerived = (IDerivedInterface)propertyBase;

            Assert.AreEqual(propertyDerived.FirstCount, test.FirstCount);
            Assert.AreEqual(propertyDerived.SecoundCount, test.SecoundCount);
        }

        /*[TestMethod]
        public void ImplementAllInterfaces_TwoInterfacesHaveSameAccessors()
        {
            var test = new TestClassSameAccessors();
            test.Count = 5;

            var properter = Properter<ITestInterfaceSameAccessor1>.GetProperter(test);

            var properter2 = (ITestInterfaceSameAccessor2)properter;

            Assert.AreEqual(test.Count, properter2.Count);
        }*/
    }

    public interface ITestInterface
    {
        BreedAndGender BreedAndGender { get; }

        int Count { get; set; }
    }

    public interface IDerivedInterface : IBaseInterface
    {   
        float FirstCount { get; set; }
    }
    
    public interface IBaseInterface
    {
        float SecoundCount { get; set; }
    }

    public interface ITestInterfaceSameAccessor1
    {
        float Count { get; set; }
    }

    public interface ITestInterfaceSameAccessor2
    {
        float Count { get; set; }
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

        public float SecoundCount
        {
            get
            {
                return _secoundCount;
            }
            set
            {
                _secoundCount = value;
            }
        }

        public BreedAndGender _breedAndGender;

        private int _count;

        private float _secoundCount;
    }

    class DerivedInterfaceTestClass : IDerivedInterface
    {
        private float _firstCount;

        public float FirstCount
        {
            get { return _firstCount; }
            set { _firstCount = value; }
        }

        private float _secoundCount;

        public float SecoundCount
        {
            get { return _secoundCount; }
            set { _secoundCount = value; }
        }
    }

    class TestClassSameAccessors : ITestInterfaceSameAccessor1, ITestInterfaceSameAccessor2
    {
        public float Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }

        private float _count;
    }
}