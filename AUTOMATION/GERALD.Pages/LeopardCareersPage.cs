using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ninject;
using GERALD.Pages;
using OpenQA.Selenium;
using GERALD.Data;



namespace GERALD.Pages
{
    public class LeopardCareersPage: LeopardSoftwareBasePage
    {

        private static string CAREER_LINK_XPATH = "//a[contains(text(),'Careers')]";
        private static string NAME_FIELD_ID = "name";
        private static string EMAIL_FIELD_ID = "email";
        private static string CONTACT_NUMBER_FIELD_ID = "phone";
        private static string MESSAGE_BOX_ID = "message";
        private static string SUBMIT_BUTTON_XPATH = "//button[contains(text(),'')]";

    public LeopardCareersPage(WebDriverPageOps pageOps) : base(pageOps)
        {
        }


    [Inject]
    public WebDriverPageOps pageops { get; set; }

    [Inject]
    public Customer customer { get; set; }

    [Inject]
    public CustomerCollection customerCollection { get; set; }

    [Inject]
    public CustomerDao customerdao { get; set; }

    private static Customer currentCustomer;

    public void NavigateToCareerPage()
    {
        GetPageOps().Click(By.XPath(CAREER_LINK_XPATH));
    }

    private void enterName()
    {
        GetPageOps().ClearAndSendKeys(By.Id(NAME_FIELD_ID), "Gerald");
    }
    private void enterEmail()
    {
        GetPageOps().ClearAndSendKeys(By.Id(EMAIL_FIELD_ID), "anenegerald@gmail.com");
    }
    private void enterContactNumber()
    {
        GetPageOps().ClearAndSendKeys(By.Id(CONTACT_NUMBER_FIELD_ID), "077899887484");
    }

    private void enterMessage()
    {
        GetPageOps().ClearAndSendKeys(By.Id(MESSAGE_BOX_ID), "I am awesome");
    }
    private void submitForm()
    {
        GetPageOps().Click(By.XPath(SUBMIT_BUTTON_XPATH));
    }

    public void completeForm()
    {
        enterName();
        enterEmail();
        enterContactNumber();
        enterMessage();
        //submitForm();
    }
    public void closeForm()
    {
        GetPageOps().Dispose();
    }
    

  
    }
}
