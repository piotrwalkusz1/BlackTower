using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkProject;

namespace UnitTest
{
    [TestClass]
    public class CopierTest
    {
        [TestMethod]
        public void CopyProperties_sourceIsBaseTergetIsDerived()
        {
            Base source = new Base();
            Derived target = new Derived();
            source.Number = 12.7f;

            Copier.CopyAllSourceProperties(source, target);

            Assert.AreEqual(source.Number, target.Number);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyProperties_sourceIsDerivedTergetIsBase()
        {
            Derived source = new Derived();
            Base target = new Base();

            Copier.CopyAllSourceProperties(source, target);

        }
    }

    class Base
    {
        public float Number { get; set; }
    }

    class Derived : Base
    {
        public int Count
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

        private int _count;
    }
}
