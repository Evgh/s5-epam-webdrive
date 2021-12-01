using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using s5_epam_webdrive.Helpers;

namespace s5_epam_webdrive.Pages
{
    class TradingPage : Bases.BasePage
    {
        private const string _homepage = @"https://inbar.best/";
        private const string _login = "lines.and.polosky@gmail.com";
        private const string _password = "rUYiPEbUpLYze";

        private string _inaccessibilityMessageBuffer;

        public int LastUpPositionId { get; set; }
        public DateTime LastUpPositionOpenTime { get; set; }
        public DateTime LastUpPositionCloseTime { get; set; }

        public IWebElement LogInButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".main-header__btn.btn.btn--black.p-open")));
        public IWebElement GoToTradingButton => _wait.Until(_driver => _driver.FindElement(By.ClassName("welcome__btn")));
        public IWebElement NoRiskManagementButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".overlay.a-open.overlay--active > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__wrapper > .btn.btn--black")));
        public IWebElement TradeUpButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".trading-btn.trading-btn2.trading-btn--up.content_btn_call.content_btn_call2")));

        public IWebElement LoginInput => _wait.Until(_driver => _driver.FindElement(By.Id("input-log-in1")));
        public IWebElement PassordInput => _wait.Until(_driver => _driver.FindElement(By.Id("input-log-in2")));
        public IWebElement TotalInput => _wait.Until(_driver => _driver.FindElement(By.Id("investment")));

        public IWebElement TradingIsUnawailableLabel => _wait.Until(_driver => _driver.FindElement(By.CssSelector("#modal_close_time > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__title")));
        public IWebElement CurrencyLabel => _wait.Until(_driver => _driver.FindElement(By.ClassName("investment_currency")));
        public IWebElement LastPositionDataLabel => _wait.Until(_driver => _driver.FindElement(By.XPath("/html/body/div[5]/div/div[3]/div[5]/div[2]/div[1]/table/tbody/tr[1]/th[2]"))); 
        public IWebElement LastUpPositionTotalLabel => _wait.Until(_driver => _driver.FindElement(By.Id($"total_{LastUpPositionId}")));

        public bool IsInaccessibilityMessageShown => TryToGetInaccessibilityMessage(out _inaccessibilityMessageBuffer) && !string.IsNullOrEmpty(_inaccessibilityMessageBuffer);
        public bool IsPlatformWorking =>  IsTodayIsWorkDay && IsNowIsWorkTime;
        public bool IsInaccessibilityMessageShownCorrectly => IsInaccessibilityMessageShown ^ IsPlatformWorking;
        public string CurrentTotal => string.Concat(TotalInput.GetAttribute("value"), ' ', CurrencyLabel.Text);
        public string LastPositionTotal => LastUpPositionTotalLabel.Text;


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
            catch(OpenQA.Selenium.WebDriverException e)
            {
                _inaccessibilityMessage = string.Empty;
                return false;
            }
        }

        public void TradeUp()
        {
            TradeUpButton.Click();            
        }

        public void InitializeLastUpPositionData()
        {
            var lastUpPositionInfo = LastPositionDataLabel.Text;

            LastUpPositionId = int.Parse(lastUpPositionInfo.Substring(0, 9));
            LastUpPositionOpenTime = DateTime.Parse(lastUpPositionInfo.Substring(11, 19)).TruncateSecond().TruncateMillisecond();
            LastUpPositionCloseTime = DateTime.Parse(lastUpPositionInfo.Substring(32, 19)).TruncateSecond().TruncateMillisecond();
        }
        public void SetTotalValue(int value)
        {
            TotalInput.SendKeys(Keys.Control + "a" + Keys.Control);
            TotalInput.SendKeys(Keys.Delete);
            TotalInput.SendKeys($"{value}");
        }

        private bool IsTodayIsWorkDay => DateTime.Now.DayOfWeek >= (DayOfWeek)1 && DateTime.Now.DayOfWeek <= (DayOfWeek)5;
        private bool IsNowIsWorkTime => DateTime.Now.Hour >= 4 && DateTime.Now.Hour <= 23;
    }
}
