using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCVManager.Forms;

namespace OpenCVManager.Tests.Forms
{
    [TestClass]
    public class ProjectSettingsFormTests
    {
        [TestMethod]
        public void GetName()
        {
            Assert.AreEqual("core", ProjectSettingsForm.GetName(@"D:\opencv_core300.lib"));
            Assert.AreEqual("core", ProjectSettingsForm.GetName(@"opencv_core300.lib"));
            Assert.AreEqual("core", ProjectSettingsForm.GetName(@"opencv_core300"));
            Assert.AreEqual("core", ProjectSettingsForm.GetName(@"opencv_core"));
            Assert.AreEqual("core", ProjectSettingsForm.GetName(@"core"));

            Assert.ThrowsException<ArgumentNullException>(() => ProjectSettingsForm.GetName(null));
            Assert.ThrowsException<ArgumentNullException>(() => ProjectSettingsForm.GetName(string.Empty));
        }

        [TestMethod]
        public void GetVersion()
        {
            Assert.AreEqual("300", ProjectSettingsForm.GetVersion(@"D:\opencv_core300.lib"));
            Assert.AreEqual("300", ProjectSettingsForm.GetVersion(@"opencv_core300.lib"));
            Assert.AreEqual("300", ProjectSettingsForm.GetVersion(@"opencv_core300"));
            Assert.AreEqual(string.Empty, ProjectSettingsForm.GetVersion(@"opencv_core"));
            Assert.AreEqual(string.Empty, ProjectSettingsForm.GetVersion(@"core"));

            Assert.ThrowsException<ArgumentNullException>(() => ProjectSettingsForm.GetVersion(null));
            Assert.ThrowsException<ArgumentNullException>(() => ProjectSettingsForm.GetVersion(string.Empty));
        }
    }
}
