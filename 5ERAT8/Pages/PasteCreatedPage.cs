using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace _5ERAT8.Pages
{
    public class PasteCreatedPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public IWebElement RawPasteDataText => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".post-view > textarea")));
        public IWebElement HighlightLanguage => _wait.Until(_driver => _driver.FindElement(By.CssSelector("div.top-buttons > div.left > a")));
        public IWebElement ExpirationTime => _wait.Until(_driver => _driver.FindElement(By.CssSelector("div.info-bottom > div.expire")));
        public IWebElement Title => _wait.Until(_driver => _driver.FindElement(By.CssSelector("div.info-bar > div.info-top > h1")));

        public PasteCreatedPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, System.TimeSpan.FromSeconds(15));
            _wait.Until(_driver => _driver.FindElement(By.ClassName("user-icon")));
        }
    }
}
