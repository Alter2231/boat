using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using boat;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MainWindow mWind = new MainWindow();
            Assert.IsFalse(mWind.Per("1", "1"));
            Assert.IsFalse(mWind.Per("1", "221"));
            Assert.IsFalse(mWind.Per("fds", "araf"));
            Assert.IsFalse(mWind.Per("11", "1"));
            Assert.IsFalse(mWind.Per("^", "1"));
            Assert.IsFalse(mWind.Per("1", " "));
            Assert.IsFalse(mWind.Per(" ", "q"));
            Assert.IsFalse(mWind.Per("441", "&&"));
        }
    }
}
