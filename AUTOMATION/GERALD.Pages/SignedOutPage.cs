using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GERALD.Pages
{
    public class SignedOutPage
    {
        private static readonly By RETURN_TO_APPLICATION = By.LinkText("Return to the application");

        private readonly WebDriverPageOps pageOps;

        public SignedOutPage(WebDriverPageOps pageOps)
        {
            this.pageOps = pageOps;
        }

        public void WaitUntilSignedOut()
        {
            pageOps.WaitForElementToBeVisible(RETURN_TO_APPLICATION);
        }

        public void ClickReturnToApplication()
        {
            pageOps.Click(RETURN_TO_APPLICATION);
        }
    }
}
