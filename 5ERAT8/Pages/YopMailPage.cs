using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace s5_epam_webdrive.Pages
{
    public class YopMailPage : Bases.BasePage
    {
        private const string HomePage = "https://yopmail.com/ru/";

        public YopMailPage(IWebDriver driver) : base(driver)
        {
        }

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


        #region Private and protected methods 
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

        protected static By ByXPathTagWithText(string tagName, string text)
        {
            return By.XPath($"//{tagName}[text()='{text}']");
        }

        #endregion
    }
}
