using Bright.ScreenPlay.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Bright.ScreenPlay.Abilities
{
    public class OpenAWebPage : Ability
    {
        protected readonly WebSettings settings;
        /// <summary>
        /// This is the NLog property
        /// </summary>
        protected readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// This is the WebDriver property
        /// </summary>
        public IWebDriver WebDriver { get; set; }

        public OpenAWebPage()
        {
            settings = new()
            {
                TakeScreenShot = false
            };
            Settings = settings;
        }
        /// <summary>
        /// This function will click an Element using XPath
        /// </summary>
        /// <param name="xpath">The xpath</param>
        public void ClickElementByXpath(string xpath)
        {
            log.Debug($"Clicking ellement using xpath: {xpath}");
            Thread.Sleep(settings.ShortTimeOutInMilSec);
            try
            {
                var fluentWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(settings.LongTimeOutInSec));
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
                try
                {
                    IWebElement element = fluentWait.Until(x => x.FindElement(By.XPath(xpath)));
                    element.Click();
                }
                catch (Exception)
                {
                    throw;
                }
                Thread.Sleep(settings.ShortTimeOutInMilSec);
            }
            catch (StaleElementReferenceException ex)
            {
                log.Error(ex);
                IWebElement element = WebDriver.FindElement(By.XPath(xpath));
                element.Click();
            }
        }
        /// <summary>
        /// This function will click an ellement using CSS
        /// </summary>
        /// <param name="CSS">The CSS</param>
        public void ClickElementByCSS(string CSS)
        {
            log.Debug($"Clicking ellement using css: {CSS}");
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(settings.LongTimeOutInSec));
            IWebElement element = wait.Until(Condition(By.CssSelector(CSS)));
            element.Click();
            Thread.Sleep(settings.ShortTimeOutInMilSec);
        }
        /// <summary>
        /// This function will enter a given text in a TextBox using the Xpath
        /// </summary>
        /// <param name="xPath">The XPath</param>
        /// <param name="textToEnter">The text to enter</param>
        public void EnterInTextboxByXPath(string xPath, string textToEnter)
        {
            log.Debug($"Enter text: {textToEnter} using xpath: {xPath}");
            try
            {
                Thread.Sleep(settings.ShortTimeOutInMilSec);
                var fluentWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(settings.LongTimeOutInSec));
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                IWebElement element = fluentWait.Until(x => x.FindElement(By.XPath(xPath)));
                element.Click();
                element.Clear();
                element.SendKeys(textToEnter);
                Thread.Sleep(settings.ShortTimeOutInMilSec);
            }
            catch (StaleElementReferenceException ex)
            {
                log.Error(ex.ToString());
                IWebElement element = WebDriver.FindElement(By.XPath(xPath));
                element.Click();
                element.SendKeys(textToEnter);
                Thread.Sleep(settings.ShortTimeOutInMilSec);
            }
        }
        /// <summary>
        /// This function will enter a Date in a DateTime textbox using the Xpath
        /// </summary>
        /// <param name="xPath"></param>
        /// <param name="dateTime"></param>
        public void EnterDateTimeByXPath(string xPath, DateTime dateTime)
        {
            log.Debug($"Enter date: {dateTime:dd-MM-yyyy HH:mm} using xpath: {xPath}");
            Thread.Sleep(settings.ShortTimeOutInMilSec);
            var fluentWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(settings.LongTimeOutInSec))
            {
                PollingInterval = TimeSpan.FromMilliseconds(settings.PollingIntervallInMilSec)
            };
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
            IWebElement element = fluentWait.Until(x => x.FindElement(By.XPath(xPath)));
            element.Click();
            element.Clear();
            element.SendKeys($"{dateTime:MMddyyyy}");
            SendTab(By.XPath(xPath));
            element.SendKeys($"{dateTime:hh:mm}");
            element.SendKeys($"{dateTime:tt}");
        }
        /// <summary>
        /// This function will send a tab to an element
        /// </summary>
        /// <param name="by"></param>
        public void SendTab(By by)
        {
            IWebElement element = WebDriver.FindElement(by);
            element.SendKeys(Keys.Tab);
        }
        /// <summary>
        /// This function will select an option from a dropdown using the value
        /// </summary>
        /// <param name="xPath">The Xpath</param>
        /// <param name="value">The value to select</param>
        public void SelectValueInDropDownByXpath(string xPath, string value)
        {
            log.Debug($"Select value: {value} in dropdown by xpath: {xPath}");
            Thread.Sleep(settings.ShortTimeOutInMilSec);
            try
            {
                var fluentWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(settings.LongTimeOutInSec));
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                IWebElement element = fluentWait.Until(x => x.FindElement(By.XPath(xPath)));
                var selectElement = new SelectElement(element);
                selectElement.SelectByValue(value);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }
        /// <summary>
        /// This function will select an option from a dropdown using the text
        /// </summary>
        /// <param name="xPath">The Xpath</param>
        /// <param name="text">The text to select</param>
        public void SelectTektInDropDownByXpath(string xPath, string text)
        {
            log.Debug($"Select text {text} in dropdown by xpath: {xPath}");
            Thread.Sleep(settings.ShortTimeOutInMilSec);
            try
            {
                var fluentWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(settings.LongTimeOutInSec));
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                IWebElement element = fluentWait.Until(x => x.FindElement(By.XPath(xPath)));
                var selectElement = new SelectElement(element);
                selectElement.SelectByText(text, true);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }
        /// <summary>
        /// This function will enter a given text in a TextBox using CSS
        /// </summary>
        /// <param name="CSS">The CSS</param>
        /// <param name="textToEnter">The text to enter</param>
        public void EnterInTextboxByCSS(string CSS, string textToEnter)
        {
            log.Debug($"Enter text: {textToEnter} ellement by CSS: {0}");
            try
            {
                var fluentWait = new DefaultWait<IWebDriver>(WebDriver)
                {
                    Timeout = TimeSpan.FromSeconds(settings.LongTimeOutInSec),
                    PollingInterval = TimeSpan.FromMilliseconds(150)
                };
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                IWebElement element = fluentWait.Until(Condition(By.CssSelector(CSS)));
                element.Click();
                element.SendKeys(textToEnter);
                Thread.Sleep(settings.ShortTimeOutInMilSec);
            }
            catch (Exception e) 
            { 
                log.Error(e.Message); 
                throw; 
            }
        }
        /// <summary>
        /// This function will return the text from a WebElement selected by XPath
        /// </summary>
        /// <param name="xpath">The XPath</param>
        /// <returns>The text from the element</returns>
        public string TekstFromElementByXpath(string xpath)
        {
            log.Debug($"Get tekst from ellement by xpath: {xpath}");
            try
            {
                var fluentWait = new DefaultWait<IWebDriver>(WebDriver)
                {
                    Timeout = TimeSpan.FromSeconds(settings.LongTimeOutInSec),
                    PollingInterval = TimeSpan.FromMilliseconds(settings.PollingIntervallInMilSec)
                };
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                IWebElement element = fluentWait.Until(x => x.FindElement(By.XPath(xpath)));
                return element.Text;
            }
            catch (Exception e) 
            {
                log.Error(e.Message); 
                throw; 
            }
        }
        /// <summary>
        /// This function will get the text from a textbox
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public string TekstFromTextBox(string xpath)
        {
            log.Debug("Get tekst from ellement by xpath: {0}", xpath);
            try
            {
                var fluentWait = new DefaultWait<IWebDriver>(WebDriver)
                {
                    Timeout = TimeSpan.FromSeconds(settings.LongTimeOutInSec),
                    PollingInterval = TimeSpan.FromMilliseconds(settings.PollingIntervallInMilSec)
                };
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                IWebElement element = fluentWait.Until(x => x.FindElement(By.XPath(xpath)));
                return element.GetAttribute("value");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }
        /// <summary>
        /// This function will get the selected value from a dropdown using the Xpath
        /// </summary>
        /// <param name="xpath">The XPATH</param>
        /// <returns></returns>
        public string GetSelectedValueFromDropDownByXpath(string xpath)
        {
            log.Debug($"Get selected value from Dropdown by xpath: {xpath}");
            try
            {
                var fluentWait = new DefaultWait<IWebDriver>(WebDriver)
                {
                    Timeout = TimeSpan.FromSeconds(settings.LongTimeOutInSec),
                    PollingInterval = TimeSpan.FromMilliseconds(settings.PollingIntervallInMilSec)
                };
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                IWebElement element = fluentWait.Until(x => x.FindElement(By.XPath(xpath)));
                SelectElement selectElement = new(element);
                return selectElement.SelectedOption.Text;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }
        /// <summary>
        /// This function will return the text from a WebElement selected by CSS
        /// </summary>
        /// <param name="css">The CSS</param>
        /// <returns>The text from the element</returns>
        public string TekstFromElementByCss(string css)
        {
            log.Debug($"Get tekst from ellement by css: {css}");
            try
            {
                var fluentWait = new DefaultWait<IWebDriver>(WebDriver)
                {
                    Timeout = TimeSpan.FromSeconds(settings.LongTimeOutInSec),
                    PollingInterval = TimeSpan.FromMilliseconds(settings.PollingIntervallInMilSec)
                };
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                IWebElement element = fluentWait.Until(Condition(By.CssSelector(css)));
                return element.Text;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }
        /// <summary>
        /// This function will wait until an element is visable using XPath
        /// </summary>
        /// <param name="xptah">The xpath</param>
        public void WaitUntilElmentVisableByXpath(string xptah)
        {
            log.Debug($"Wait Until Element Vissable by Xpath: {xptah}");
            try
            {
                var fluentWait = new DefaultWait<IWebDriver>(WebDriver)
                {
                    Timeout = TimeSpan.FromSeconds(settings.LongTimeOutInSec),
                    PollingInterval = TimeSpan.FromMilliseconds(settings.PollingIntervallInMilSec)
                };
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                fluentWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(xptah)));
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }
        /// <summary>
        /// This function will wait until an element is visable using CSS
        /// </summary>
        /// <param name="CSS">The CSS</param>
        public void WaitUntilElmentVisableByCSS(string CSS)
        {
            log.Debug($"Wait Until Element Vissable by CSS: {CSS}");
            try
            {
                var fluentWait = new DefaultWait<IWebDriver>(WebDriver)
                {
                    Timeout = TimeSpan.FromSeconds(settings.LongTimeOutInSec),
                    PollingInterval = TimeSpan.FromMilliseconds(settings.PollingIntervallInMilSec)
                };
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                fluentWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(CSS)));
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }
        /// <summary>
        /// This function will return true if the element is visable and and enabled
        /// </summary>
        /// <param name="by">The way you want to select the element</param>
        /// <returns>bool</returns>
        public bool IsElementVisable(By by)
        {
            try
            {
                object p = WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(settings.ShortTimeOutInSec);
                var element = WebDriver.FindElement(by);
                return element.Displayed && element.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        /// <summary>
        /// This function will return the value of a property for an Element found by XPath
        /// </summary>
        /// <param name="xpath">The xpath</param>
        /// <param name="property">The property</param>
        /// <returns></returns>
        public string GetAttributeFromXpath(string xpath, string property)
        {
            log.Debug($"Get property {property} from ellement by xpath: {xpath}");
            try
            {
                var fluentWait = new DefaultWait<IWebDriver>(WebDriver)
                {
                    Timeout = TimeSpan.FromSeconds(settings.LongTimeOutInSec),
                    PollingInterval = TimeSpan.FromMilliseconds(settings.PollingIntervallInMilSec)
                };
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                IWebElement element = fluentWait.Until(x => x.FindElement(By.XPath(xpath)));
                return element.GetAttribute(property);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }
        /// <summary>
        /// This will make a screenshot
        /// </summary>
        /// <param name="step"></param>
        public void TakeScreenShot(string step)
        {
            if (Settings.TakeScreenShot)
            {
                ITakesScreenshot takesScreenshot = (ITakesScreenshot)WebDriver;
                var screenshot = takesScreenshot.GetScreenshot();
                string folder = step.Split("_")[0];
                var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "");
                Directory.CreateDirectory(Path.Combine(path, @"../../../Screenshots/", folder));
                string fileName = $"{step.Split("_")[1]}_{step.Split("_")[2]}_{DateTime.Now:yyyy-MM-dd'T'HH-mm-ss}.png";
                string tempFileName = Path.Combine(path, @$"../../../Screenshots/{folder}/", fileName);
                screenshot.SaveAsFile(tempFileName);
                log.Debug("Screenshot saved: {0}", tempFileName);
            }
        }
        /// <summary>
        /// This function will scoll to an given ellemt 
        /// </summary>
        /// <param name="by">the way you want to select the element</param>
        public void ScrollToElement(By by)
        {
            Thread.Sleep(2000);
            IWebElement s = WebDriver.FindElement(by);
            IJavaScriptExecutor je = (IJavaScriptExecutor)WebDriver;
            je.ExecuteScript("arguments[0].scrollIntoView(false);", s);
        }
        /// <summary>
        /// This funtion will make shure the Webdriver is closed
        /// </summary>
        public new void Dispose()
        {   
            WebDriver?.Quit();
        }
        private static Func<IWebDriver, IWebElement> Condition(By locator)
        {
            return (WebDriver) =>
            {
                try
                {
                    var element = WebDriver.FindElements(locator).FirstOrDefault();
                    return element != null && element.Displayed && element.Enabled ? element : null;
                }
                catch (WebDriverException)
                {
                    throw;
                }
            };
        }
    }
}
