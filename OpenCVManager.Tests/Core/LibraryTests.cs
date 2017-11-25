using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCVManager.Core;

namespace OpenCVManager.Tests.Core
{
    [TestClass]
    public class LibraryTests
    {
        [TestMethod]
        public void GetName()
        {
            Assert.AreEqual("core", Library.GetName(@"D:\opencv_core300.lib"));
            Assert.AreEqual("core", Library.GetName(@"opencv_core300.lib"));
            Assert.AreEqual("core", Library.GetName(@"opencv_core300"));
            Assert.AreEqual("core", Library.GetName(@"opencv_core"));
            Assert.AreEqual("core", Library.GetName(@"core"));

            Assert.ThrowsException<ArgumentNullException>(() => Library.GetName(null));
            Assert.ThrowsException<ArgumentNullException>(() => Library.GetName(string.Empty));
        }

        [TestMethod]
        public void GetVersion()
        {
            Assert.AreEqual("300", Library.GetVersion(@"D:\opencv_core300.lib"));
            Assert.AreEqual("300", Library.GetVersion(@"opencv_core300.lib"));
            Assert.AreEqual("300", Library.GetVersion(@"opencv_core300"));
            Assert.AreEqual(string.Empty, Library.GetVersion(@"opencv_core"));
            Assert.AreEqual(string.Empty, Library.GetVersion(@"core"));

            Assert.ThrowsException<ArgumentNullException>(() => Library.GetVersion(null));
            Assert.ThrowsException<ArgumentNullException>(() => Library.GetVersion(string.Empty));
        }
    }
}
