using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using log4net;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace GERALD.Pages
{
    public class WebDriverPageOps: IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(WebDriverPageOps));

        private const string INPUT = "input";
        private const string VALUE = "value";

        private readonly IWebDriver driver;
        private readonly TimeSpan implicitTimeout;

        public WebDriverPageOps(IWebDriver driver, TimeSpan implicitTimeout)
        {
            this.driver = driver;
            this.driver.Manage().Window.Maximize();
            this.implicitTimeout = implicitTimeout;

            setImplicitTimeout(implicitTimeout);
        }

        private void setImplicitTimeout(TimeSpan timeout)
        {
            logger.InfoFormat("Setting implicit timeout [{0}]", timeout.ToString());
            driver.Manage().Timeouts().ImplicitlyWait(timeout);
        }

        private void switchOffImplicitWaiting()
        {
            setImplicitTimeout(TimeSpan.FromSeconds(0));
        }

        private void resetImplicitTimeOut()
        {
            setImplicitTimeout(implicitTimeout);
        }

        // Common page actions ========================================================================

        internal void OpenPage(string url)
        {
            logger.InfoFormat("Opening page [{0}]", url);
            driver.Navigate().GoToUrl(url);
        }

        public void ClearAndSendKeys(By identifier, string text)
        {
            logger.InfoFormat("Clear [{0}] and send keys [{1}]", identifier, text);
            driver.FindElement(identifier).Clear();
            driver.FindElement(identifier).SendKeys(text);
        }

        public void Click(By identifier)
        {
            logger.InfoFormat("Click [{0}]", identifier);
            WaitForElementToBeVisible(identifier);
            driver.FindElement(identifier).Click();
        }

        internal void SelectOptionByText(By identifier, string text)
        {
            logger.InfoFormat("Select [{0}] from [{1}]", text, identifier);
            SelectElement select = new SelectElement(driver.FindElement(identifier));
            select.SelectByText(text);
        }

        internal string GetElementText(By identifier)
        {
            IWebElement element = driver.FindElement(identifier);
            string text = getText(element);

            logger.InfoFormat("Element [{0}] contains text [{1}]", identifier, text);
            return text;
        }

        private string getText(IWebElement element)
        {
            string text = "";

            if (INPUT.ToLower().Equals(element.TagName.ToLower()))
            {
                text = element.GetAttribute(VALUE);
            }
            else
            {
                text = element.Text;
            }

            return text;
        }

        internal List<string> GetTextFromAllElementsMatching(By identifier)
        {
            logger.InfoFormat("Getting text from all elements matching [{0}]", identifier);
            List<string> result = new List<string>();

            foreach (IWebElement element in driver.FindElements(identifier))
            {
                string elementText = getText(element);
                result.Add(elementText);
            }

            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="expectedText"></param>
        /// <returns>true if the expected text (ignoring case) is found within the text of all elements matching the given identifier, false otherwise.</returns>
        internal bool AllMatchingElementsContainText(By identifier, string expectedText)
        {
            if (expectedText != null)
            {
                logger.InfoFormat("Checking if all elements identified by [{0}] contain [{1}]", identifier, expectedText);

                List<string> values = GetTextFromAllElementsMatching(identifier);
                logger.InfoFormat("[{0}] elements found matching [{1}]", values.Count(), identifier);

                foreach (string value in values)
                {
                    if (!value.ToLower().Contains(expectedText.ToLower()))
                    {
                        logger.WarnFormat("Value [{0}] does not contain [{1}]", value, expectedText);
                        return false;
                    }
                }
            }

            return true;
        }

        internal bool ElementIsNotDisplayed(By identifier)
        {
            return !driver.FindElement(identifier).Displayed;
        }

        internal void DeleteCookies()
        {
            logger.Info("Deleting cookies...");
            driver.Manage().Cookies.DeleteAllCookies();
        }

        internal void Exit()
        {
            logger.Info("Exiting webDriver...");
            driver.Quit();
            driver.Dispose();
        }

        // Common wait operations ========================================================================

        internal bool ElementExists(By identifier)
        {
            return ElementExists(identifier, implicitTimeout);
        }

        internal bool ElementExists(By identifier, TimeSpan timeout)
        {
            bool result = false;
            try
            {
                switchOffImplicitWaiting();
                logger.InfoFormat("Checking if element [{0}] exists...", identifier);
                WebDriverWait wait = new WebDriverWait(driver, timeout);
                wait.Until(ExpectedConditions.ElementExists(identifier));
                result = true;
            }
            catch (WebDriverTimeoutException)
            {
                result = false;

            }
            finally
            {
                resetImplicitTimeOut();
            }

            logger.InfoFormat("Element [{0}] was{1} found", identifier, result ? "" : " not");
            return result;
        }

        internal void WaitForElementToBeVisible(By identifier)
        {
            WaitForElementToBeVisible(identifier, implicitTimeout);
        }

        public void WaitForElementToBeVisible(By identifier, TimeSpan timeout)
        {
            try
            {
                switchOffImplicitWaiting();
                logger.InfoFormat("Waiting for element [{0}] to be visible...", identifier);
                WebDriverWait wait = new WebDriverWait(driver, timeout);
                wait.Until(ExpectedConditions.ElementExists(identifier));
                wait.Until(ExpectedConditions.ElementIsVisible(identifier));
                logger.InfoFormat("Element [{0}] is now visible", identifier);
            }
            catch (WebDriverTimeoutException)
            {
                logger.WarnFormat("Element [{0}] was not visible within timeout period [{1}]", identifier, timeout);
            }
            finally
            {
                resetImplicitTimeOut();
            }
        }

        internal void WaitForElementNotToBeVisible(By identifier)
        {
            WaitForElementNotToBeVisible(identifier, implicitTimeout);
        }

        internal void WaitForElementNotToBeVisible(By identifier, TimeSpan timeout)
        {
            try
            {
                switchOffImplicitWaiting();
                logger.InfoFormat("Waiting for element [{0}] not to be visible...", identifier);
                WebDriverWait wait = new WebDriverWait(driver, timeout);
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(identifier));
                logger.InfoFormat("Element [{0}] is no longer visible", identifier);
            }
            catch (WebDriverTimeoutException)
            {
                logger.WarnFormat("Element [{0}] was still visible after timeout period [{1}]", identifier, timeout);
            }
            finally
            {
                resetImplicitTimeOut();
            }
        }

        internal void WaitForElementToContainText(By identifier, string expectedText)
        {
            WaitForElementToContainText(identifier, expectedText, implicitTimeout);
        }

        internal void WaitForElementToContainText(By identifier, string expectedText, TimeSpan timeout)
        {
            try
            {
                switchOffImplicitWaiting();
                logger.InfoFormat("Waiting for element [{0}] to contain text [{1}]...", identifier, expectedText);
                WebDriverWait wait = new WebDriverWait(driver, timeout);
                wait.Until(elementContainsText(identifier, expectedText));
                logger.InfoFormat("Element [{0}] contains text [{1}]", identifier, expectedText);
            }
            catch (WebDriverTimeoutException)
            {
                logger.WarnFormat("Element [{0}] does not contain text [{1}] after timeout period", identifier, expectedText);
            }
            finally
            {
                resetImplicitTimeOut();
            }
        }

        private Func<IWebDriver, bool> elementContainsText(By identifier, String expectedText)
        {
            return (d) =>
            {
                bool result = false;
                try
                {
                    result = getText(driver.FindElement(identifier)).Contains(expectedText);

                }
                catch (StaleElementReferenceException)
                {
                    logger.Warn("Stale element reference exception encountered");
                }

                return result;
            };
        }

        public void Dispose()
        {
            Exit();
        }
        internal void validateElement(By identifier)
        {

            driver.FindElement(identifier);

        }

        public void HoverOVerElement(By identifier)
        {
            Actions action = new Actions(driver);
            IWebElement ele= driver.FindElement(identifier);
            action.MoveToElement(ele).Perform();
           
            
            
        }

    }
}
