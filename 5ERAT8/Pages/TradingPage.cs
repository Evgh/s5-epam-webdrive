using System;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace s5_epam_webdrive.Pages
{
    class TradingPage : Bases.BasePage
    {
        private const string _homepage = @"https://inbar.best/";
        private const string _login = "lines.and.polosky@gmail.com";
        private const string _password = "rUYiPEbUpLYze";

        private string _inaccessibilityMessageBuffer;
        private int _lastOpenPositionId;
        private DateTime _lastOpenUpPositionTime;
        private DateTime _lastCloseUpPositionTime;

        public IWebElement LogInButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".main-header__btn.btn.btn--black.p-open")));
        public IWebElement LoginInput => _wait.Until(_driver => _driver.FindElement(By.Id("input-log-in1")));
        public IWebElement PassordInput => _wait.Until(_driver => _driver.FindElement(By.Id("input-log-in2")));
        public IWebElement GoToTradingButton => _wait.Until(_driver => _driver.FindElement(By.ClassName("welcome__btn")));
        public IWebElement TradingIsUnawailableLabel => _wait.Until(_driver => _driver.FindElement(By.CssSelector("#modal_close_time > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__title")));
        public IWebElement NoRiskManagementButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".overlay.a-open.overlay--active > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__wrapper > .btn.btn--black")));
        public IWebElement TradingButtonUp => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".trading-btn.trading-btn2.trading-btn--up.content_btn_call.content_btn_call2")));
        public IWebElement LastUpPositionData => _wait.Until(_driver => _driver.FindElement(By.XPath("/html/body/div[5]/div/div[3]/div[5]/div[2]/div[1]/table/tbody/tr[1]/th[2]")));


        public bool IsInaccessibilityMessageShown => TryToGetInaccessibilityMessage(out _inaccessibilityMessageBuffer) && !string.IsNullOrEmpty(_inaccessibilityMessageBuffer);
        public bool IsPlatformWorking =>  IsTodayIsWorkDay && IsNowIsWorkTime;
        public bool IsInaccessibilityMessageShownCorrectly => IsInaccessibilityMessageShown ^ IsPlatformWorking;


        public TradingPage(IWebDriver driver) : base(driver)
        {
        }

        public void OpenPage()
        {
            _driver.Url = _homepage;
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
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

        public bool TryToGetInaccessibilityMessage(out string _inaccessibilityMessage)
        {
            try
            {
                IWebElement TradingIsUnawailableLabel = _wait.Until(_driver => _driver.FindElement(By.CssSelector("#modal_close_time > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__title")));
                _inaccessibilityMessage = TradingIsUnawailableLabel.Text;
                return true;
            }
            catch(OpenQA.Selenium.WebDriverTimeoutException e)
            {
                _inaccessibilityMessage = string.Empty;
                return false;
            }
        }

        public void TradeUp()
        {
            TradingButtonUp.Click();            
        }

        public void InitializeLastUpPositionData()
        {
            var lastUpPositionInfo = LastUpPositionData.Text;

            _lastOpenPositionId = int.Parse(lastUpPositionInfo.Substring(0, 9));
            _lastOpenUpPositionTime = DateTime.Parse(lastUpPositionInfo.Substring(11, 19));
            _lastCloseUpPositionTime = DateTime.Parse(lastUpPositionInfo.Substring(32, 19));
        }

        private bool IsTodayIsWorkDay => DateTime.Now.DayOfWeek >= (DayOfWeek)1 && DateTime.Now.DayOfWeek <= (DayOfWeek)5;
        private bool IsNowIsWorkTime => DateTime.Now.Hour >= 4 && DateTime.Now.Hour <= 23;
    }
}
