using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace _5ERAT8.Pages
{
    class GoogleCloudPage
    {
        private const string HomePage = "https://cloud.google.com/";
        private IWebDriver _driver;

        public GoogleCloudPage(IWebDriver driver)
        {
            _driver = driver;
        }

        #region Public methods

        public GoogleCloudPage OpenPage()
        {
            _driver.Navigate().GoToUrl(HomePage);
            return this;
        }

        public GoogleCloudPage WriteToSearchBox(string text)
        {
            WaitAndClick(By.Name("q"));
            WaitUntilClickable(By.Name("q")).SendKeys(text);
            return this;
        }

        public GoogleCloudPage SelectSearchBoxOption(string text)
        {
            WaitAndClick(By.PartialLinkText(text));
            return this;
        }

        public GoogleCloudPage SwitchToCalculatorIFrame()
        {
            _driver.SwitchTo().Frame(WaitUntilClickable(By.XPath("//*[@id='cloud-site']/devsite-iframe/iframe")));
            _driver.SwitchTo().Frame(WaitUntilClickable(By.Id("myFrame")));
            return this;
        }

        public GoogleCloudPage AddNumberOfInstances(int number)
        {
            WaitUntilClickable(By.Id("input_74")).SendKeys(number.ToString());
            return this;
        }
        public GoogleCloudPage SelectMachineTypeStandard2()
        {
            WaitAndClick(By.Id("select_value_label_70"));
            WaitAndClick(By.Id("select_option_271"));
            return this;
        }

        public GoogleCloudPage AddNodesCount(int count)
        {
            WaitUntilClickable(By.Name("nodesCount")).SendKeys(count.ToString());
            return this;
        }

        public GoogleCloudPage CheckAddGPUs()
        {
            WaitAndClick(By.XPath("//md-checkbox[contains(@aria-label, 'Add GPUs')]"));
            return this;
        }
        public GoogleCloudPage SelectNvidiaTeslaP4()
        {
            WaitAndClick(By.Id("select_413"));
            WaitAndClick(By.Id("select_option_420"));
            return this;
        }

        public GoogleCloudPage Select4NumberOfGPUs()
        {
            WaitAndClick(By.Id("select_411"));
            WaitAndClick(By.Id("select_option_427"));
            return this;
        }

        public GoogleCloudPage Select24x375GbSSDs()
        {
            WaitAndClick(By.Id("select_133"));
            WaitAndClick(By.Id("select_option_132"));
            return this;
        }

        public GoogleCloudPage SelectFrankfurtLocation()
        {
            WaitAndClick(By.Id("select_136"));
            WaitAndClick(By.Id("select_option_292"));
            return this;
        }

        public GoogleCloudPage SelectCommitedUsage1Year()
        {
            WaitAndClick(By.Id("select_141"));
            WaitAndClick(By.Id("select_option_139"));
            return this;
        }

        public GoogleCloudPage AddToEstimates()
        {
            var buttons = _driver.FindElements(By.XPath("//button[contains(@aria-label, 'Add to Estimate')]"));
            buttons[0].SendKeys(Keys.Enter);
            buttons[1].SendKeys(Keys.Enter);
            return this;
        }

        public string GetEstimatePrice()
        {
            var estimatePrice = _driver.FindElement(By.XPath("/html/body/md-content/md-card/div/md-card-content[2]/md-card/md-card-content/div/div/div/h2/b")).Text;

            return estimatePrice.Split(' ')[4];
        }

        public void OpenNewTab()
        {
            _driver.SwitchTo().NewWindow(WindowType.Tab);
        }

        public GoogleCloudPage EmailEstimate(string email)
        {
            WaitAndClick(By.Id("email_quote"));
            WaitUntilClickable(By.Name("description"));
            var emailField = _driver.FindElements(By.Name("description"))[2];
            emailField.SendKeys(email);
            WaitAndClick(By.XPath("//button[@aria-label = 'Send Email']"));
            return this;
        }

        #endregion

        #region Private and protected methods
        protected DefaultWait<IWebDriver> FluentWait()
        {
            var fluentWait = new DefaultWait<IWebDriver>(_driver)
            {
                Timeout = TimeSpan.FromSeconds(600),
                PollingInterval = TimeSpan.FromSeconds(1)
            };
            return fluentWait;
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

        protected void WaitAndClick(By by)
        {
            WaitUntilClickable(by).Click();
        }

        #endregion
    }
}
