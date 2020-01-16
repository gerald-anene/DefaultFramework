using GERALD.Config;
using GERALD.Pages;
using NBehave.Narrator.Framework;
using Ninject;
using System;
using GERALD.Data;

namespace GERALD.Steps
{
    [ActionSteps]
    public class LoginSteps
    {
   

        [Inject]
        public LoginPage loginPage { get; set; }

        [Inject]
        public LeopardCareersPage bjssPage { get; set; }

      

        public LoginSteps()
        {
            Factory.Instance.Inject(this);
        }

        [Given("I am logged in to Asda Website")]
        public void login()
        {
            loginPage.OpenPage();  
            
        
        }
        

    }
}
