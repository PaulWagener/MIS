using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleManager.Web.Controllers;

namespace ModuleManager.WebTests.controller
{
    [TestClass]
    public class AcountController_Test
    {
        [TestMethod]
        public void GetSwcSH1_success()
        {
            //1. arrange
            //2. act
            String result = AccountController.GetSwcSH1("admin");
            //3. assert
            Assert.AreEqual("D033E22AE348AEB5660FC2140AEC35850C4DA997", null);
        }
    }
}
