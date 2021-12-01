using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using System.Text.RegularExpressions;
using s5_epam_webdrive.Pages;
using s5_epam_webdrive.Helpers;

namespace s5_epam_webdrive
{
    [TestFixture]
    class Test
    {
        WebDriver driver;

        [SetUp]
        public void Startup()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(200);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(200);
        }

        [Test]
        public void IsInaccessibilityMessageShownCorrectly_ReturnsTrue()
        {
            var tradingPage = new TradingPage(driver);
            tradingPage.OpenPage();
            tradingPage.OpenLoginWindow();
            tradingPage.InputLogin();
            tradingPage.InputPasswordAndConfirm();
            tradingPage.GoToTrading();

            Assert.IsTrue(tradingPage.IsInaccessibilityMessageShownCorrectly);
        }

        [Test]
        public void TradeUp_DefaultValidValues_ReturnsTrue()
        {
            var tradingPage = new TradingPage(driver);
            tradingPage.OpenPage();
            tradingPage.OpenLoginWindow();
            tradingPage.InputLogin();
            tradingPage.InputPasswordAndConfirm();
            tradingPage.GoToTrading();

            var expectedTotal = tradingPage.CurrentTotal;
            var expectedDateTime = DateTime.Now.TruncateSecond().TruncateMillisecond();
 
            tradingPage.TradeUp();
            Thread.Sleep(5000);            
            tradingPage.InitializeLastUpPositionData();

            var assertAccumulator = new AssertAccumulator();
            assertAccumulator.Accumulate(() => Assert.AreEqual(expectedTotal, tradingPage.LastUpPositionTotalLabel.Text));
            assertAccumulator.Accumulate(() => Assert.AreEqual(expectedDateTime, tradingPage.LastUpPositionOpenTime));
            assertAccumulator.Release();
        }
    }
}
