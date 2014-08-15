using System;
using LibraryToTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class StringExtensionMethodTests
    {
        [TestMethod]
        public void GuidToStringTestPass()
        {
            var testGuid = Guid.NewGuid();
            var testString = testGuid.ToString();

            var guidUnderTest = testString.ToGuid();

            Assert.AreEqual(guidUnderTest, testGuid);
        }

        [TestMethod]
        public void GuidToStringTestFail()
        {
            var testGuid = Guid.NewGuid();
            const string testString = "7452897598174958237598";

            var guidUnderTest = testString.ToGuid();

            Assert.AreNotEqual(guidUnderTest, testGuid);
        }

        [TestMethod]
        public void RightTestPass()
        {
            const string compare = "Playoffs";
            const string testString = "TheIslandersWillMakeThePlayoffs";

            var test = testString.Right(8);

            Assert.AreEqual(compare, test);
        }

        [TestMethod]
        public void TestReplaceSafe()
        {
            const string originalString = "Kenneth _";
            const string compareString = "Kenneth";

            var result = originalString.ReplaceSafe("_", "").Trim();

            Assert.IsTrue(compareString == result);
        }

        [TestMethod]
        public void TestReplaceSafeNull()
        {
            const string originalString = null;
            const string compareString = null;

            var result = originalString.ReplaceSafe("_", "");

            Assert.IsTrue(compareString == result);
        }

        [TestMethod]
        public void TestReplaceSafeEmpty()
        {
            var originalString = string.Empty;
            string compareString = null;

            var result = originalString.ReplaceSafe("_", "");

            Assert.IsTrue(compareString == result);
        }

        [TestMethod]
        public void TestSanitizeString()
        {
            const string originalString = "vday2014                            ";
            const string compareString = "vday2014";

            var result = originalString.Trim();

            Assert.IsTrue(compareString == result);
        }

        [TestMethod]
        public void UrlStringToGuidTest()
        {
            var original = Guid.NewGuid();
            var url = original.ToString("N");
            var result = Guid.Parse(url);

            Assert.IsTrue(original == result);
        }
    }
}