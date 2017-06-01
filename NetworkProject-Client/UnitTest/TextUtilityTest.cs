using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkProject;

namespace UnitTest
{
    [TestClass]
    public class TextUtilityTest
    {
        [TestMethod]
        public void ReplaceVariablesByNumbers()
        {
            string text = "Count is [Count] and Const is [Const]";
            var test = new Test();
            test.Count = 4;

            string returnedText = TextUtility.ReplaceVariablesByNumbers(text, test);

            Assert.AreEqual("Count is 4 and Const is 3.14", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_OnlyNumber()
        {
            string text = "Text <4> otherText";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text 4 otherText", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_Addition()
        {
            string text = "Text <4 +5> otherText";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text 9 otherText", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_Multiply()
        {
            string text = "Text <3* 8> otherText";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text 24 otherText", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_AdditionAndMultiply()
        {
            string text = "Text <3* 8+ 8> otherText";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text 32 otherText", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_Difference()
        {
            string text = "Text <5 - 8> otherText";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text -3 otherText", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_Difference_Float()
        {
            string text = "Text <5 - 8.4> otherText";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text -3.4 otherText", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_Division()
        {
            string text = "Text <10/4> otherText";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text 2.5 otherText", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_Division_RoundingDown()
        {
            string text = "Text <10/3> otherText";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text 3.33 otherText", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_Division_RoundingUp()
        {
            string text = "Text <10/6> otherText";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text 1.67 otherText", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_Division_Rounding5ToUp()
        {
            string text = "Text <1.11/2> otherText";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text 0.56 otherText", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_Parenthesis()
        {
            string text = "Text <3* (8+ 8 )> otherText";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text 48 otherText", returnedText);
        }

        [TestMethod]
        public void ReplaceOperationsByNumbers_TwoOperations()
        {
            string text = "Text <3* (8+ 8 )> otherText <5 * 5 * 5>";

            string returnedText = TextUtility.ReplaceMathExpresionsByNumbers(text);

            Assert.AreEqual("Text 48 otherText 125", returnedText);
        }
    }

    class Test
    {
        public int Count { get; set; }

        public float Const
        {
            get { return 3.14f; }
        }
    }
}
