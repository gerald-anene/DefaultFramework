using GERALD.Config;
using GERALD.Pages;
using NBehave.Narrator.Framework;
using Ninject;
using System;

namespace GERALD.Steps
{
     [ActionSteps]
   public  class LeopardCareersSteps
    {

           [Inject]
        public LoginPage loginPage { get; set; }

           [Inject]
           public LeopardCareersPage leopardCarrersPage { get; set; }

        public LeopardCareersSteps()
        {
            Factory.Instance.Inject(this);
        }

        [Given("I navigate to the Careers page")]
        public void navigateToCarrersPage()
        {
            leopardCarrersPage.NavigateToCareerPage();
        
        }
        [Then("I complete and submit Careers form")]
        public void applyForBjssGraduateProgram()
        {
            leopardCarrersPage.completeForm();
        
        }
        [Then("I will recieve a message that confirms that my form has been submitted")]
        public void FormSubmissionConfirmation()
        {
            leopardCarrersPage.closeForm();
        }

    }
}
