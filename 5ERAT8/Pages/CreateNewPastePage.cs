using OpenQA.Selenium;

namespace s5_epam_webdrive.Pages
{
    public class CreateNewPastePage : Bases.BasePage
    {
        public IWebElement CodeInputForm => _wait.Until(_driver => _driver.FindElement(By.CssSelector("textarea#postform-text")));
        public IWebElement PasteExpirationDropdown => _wait.Until(_driver => _driver.FindElement(By.CssSelector("#w0 > div.post-form__bottom > div.post-form__left > div.form-group.field-postform-expiration > div > span")));
        public IWebElement PasteExpirationOption => _wait.Until(_driver => _driver.FindElement(By.XPath("//*[contains(@id,'10M')]")));
        public IWebElement SyntaxHighlightingDropdown => _wait.Until(_driver => _driver.FindElement(By.CssSelector("#w0 > div.post-form__bottom > div.post-form__left > div.form-group.field-postform-format > div > span")));
        public IWebElement SyntaxHighlightingOption => _wait.Until(_driver => _driver.FindElement(By.XPath("//*[contains(@id,'15')]")));
        public IWebElement SyntaxHighlightingInput => _wait.Until(_driver => _driver.FindElement(By.XPath("//input[contains(@type,'search')]")));
        public IWebElement PasteNameForm => _wait.Until(_driver => _driver.FindElement(By.CssSelector("input#postform-name")));
        public IWebElement CreateNewPasteButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector("button")));

        public CreateNewPastePage(IWebDriver driver) : base(driver)
        {
        }

        public void OpenPage()
        {
            _driver.Navigate().GoToUrl("https://pastebin.com/");
        }

        public void SetPasteExpiration()
        {
            PasteExpirationDropdown.Click();
            PasteExpirationOption.Click();
        }

        public void SetSyntaxHighlighting()
        {
            SyntaxHighlightingDropdown.Click();
            SyntaxHighlightingInput.SendKeys("Bash" + Keys.Enter);
        }

        public void SetPasteName(string name)
        {
            PasteNameForm.SendKeys(name);
        }

        public PasteCreatedPage CreatePaste()
        {
            CreateNewPasteButton.Click();
            return new PasteCreatedPage(_driver);
        }
    }
}
