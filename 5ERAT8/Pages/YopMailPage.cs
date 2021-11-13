using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace _5ERAT8.Pages
{
    public class YopMailPage
    {
        private const string HomePage = "https://yopmail.com/ru/";
        private IWebDriver _driver;

        public YopMailPage(IWebDriver driver)
        {
            _driver = driver;
        }
        protected DefaultWait<IWebDriver> FluentWait()
        {
            var fluentWait = new DefaultWait<IWebDriver>(_driver)
            {
                Timeout = TimeSpan.FromSeconds(600),
                PollingInterval = TimeSpan.FromSeconds(1)
            };
            return fluentWait;
        }

        protected IWebElement WaitUntilVisible(By by)
        {
            var fluentWait = FluentWait();
            fluentWait.IgnoreExceptionTypes(typeof(Exception));
            return fluentWait.Until(driver =>
            {
                IWebElement tempElement = _driver.FindElement(by);
                return (tempElement.Displayed) ? tempElement : null;
            });
        }

        protected IWebElement WaitUntilClickable(By by)
        {
            var fluentWait = FluentWait();
            fluentWait.IgnoreExceptionTypes(typeof(Exception));
            return fluentWait.Until(driver =>
            {
                IWebElement tempElement = _driver.FindElement(by);
                return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
            });
        }

        protected void WaitAndClick(By by) => WaitUntilClickable(by).Click();
        protected static By ByXPathTagWithText(string tagName, string text) => By.XPath($"//{tagName}[text()='{text}']");


        public YopMailPage OpenPage()
        {
            _driver.Navigate().GoToUrl(HomePage);
            return this;
        }

        public YopMailPage EnterEmailName(string name)
        {
            var textBox = WaitUntilClickable(By.Id("login"));
            textBox.SendKeys(name);
            return this;
        }

        public YopMailPage CreateEmail()
        {
            var textBox = WaitUntilClickable(By.Id("login"));
            textBox.SendKeys(Keys.Enter);
            return this;
        }

        public YopMailPage RefreshMail()
        {
            WaitAndClick(By.Id("refresh"));
            return this;
        }

        public string GetEstimatedPriceFromEmail()
        {
            _driver.SwitchTo().Frame("ifinbox");
            WaitAndClick(ByXPathTagWithText("span", "Google Cloud Sales"));
            _driver.SwitchTo().DefaultContent();
            _driver.SwitchTo().Frame("ifmail");
            var priceSpan = WaitUntilVisible(By.XPath("//*[contains(text(), 'USD')]"));
            var price = priceSpan.Text.Split(' ')[4];
            _driver.SwitchTo().DefaultContent();
            return price;
        }
    }
}
