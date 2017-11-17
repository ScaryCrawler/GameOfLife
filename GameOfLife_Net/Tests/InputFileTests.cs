using GameOfLife_Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class InputFileTests
    {
        [TestMethod]
        public void OnFileExisting()
        {
            string path = "StartedState.txt";
            Assert.AreEqual(true, FileChecker.CheckFile(path));
        }
    }
}
