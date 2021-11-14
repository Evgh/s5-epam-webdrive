using System;
using OpenQA.Selenium;

namespace s5_epam_webdrive.Pages
{
    class TradingPage : Bases.BasePage
    {
        private const string _homepage = @"https://inbar.best/";
        private const string _login = "lines.and.polosky@gmail.com";
        private const string _password = "rUYiPEbUpLYze";

        public IWebElement LogInButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".main-header__btn.btn.btn--black.p-open")));
        public IWebElement LoginInput => _wait.Until(_driver => _driver.FindElement(By.Id("input-log-in1")));
        public IWebElement PassordInput => _wait.Until(_driver => _driver.FindElement(By.Id("input-log-in2")));
        public IWebElement GoToTradingButton => _wait.Until(_driver => _driver.FindElement(By.ClassName("welcome__btn")));
        public IWebElement TradingIsUnawailableLabel => _wait.Until(_driver => _driver.FindElement(By.CssSelector("#modal_close_time > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__title")));

        public bool IsInaccessibilityMessageShown => !string.IsNullOrEmpty(TradingIsUnawailableLabel.Text);
        public bool IsPlatformWorking =>  IsTodayIsWorkDay() ? IsNowIsWorkTime() : false;
        public bool IsInaccessibilityMessageShownCorrectly => IsInaccessibilityMessageShown ^ IsPlatformWorking;

        public TradingPage(IWebDriver driver) : base(driver)
        {
        }

        public void OpenPage()
        {
            //_driver.Navigate().GoToUrl(_homepage);
            _driver.Url = _homepage;
        }

        public void OpenLoginWindow()
        {
            LogInButton.Click();
        }

        public void InputLogin()
        {
            LoginInput.SendKeys(_login);
        }

        public void InputPasswordAndConfirm()
        {
            PassordInput.SendKeys(_password + Keys.Enter);
        }

        public void GoToTrading()
        {
            GoToTradingButton.Click();
        }


        private bool IsTodayIsWorkDay() => DateTime.Now.DayOfWeek >= (DayOfWeek)1 && DateTime.Now.DayOfWeek <= (DayOfWeek)5;

        private bool IsNowIsWorkTime() => DateTime.Now.Hour >= 4 && DateTime.Now.Hour <= 18;
    }
}
