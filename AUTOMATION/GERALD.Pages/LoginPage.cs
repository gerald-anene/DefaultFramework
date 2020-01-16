using System;
using OpenQA.Selenium;


namespace GERALD.Pages
{
    public class LoginPage
    {
       
        private readonly string baseUrl;
        private readonly WebDriverPageOps pageOps;


        public LoginPage(string baseUrl, WebDriverPageOps pageOps)
        {
            this.baseUrl = baseUrl;
            this.pageOps = pageOps;
        }
        public void OpenPage()
        {
            pageOps.OpenPage(baseUrl);
           
        }


    }
}
