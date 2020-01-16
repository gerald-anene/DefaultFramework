using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBehave.Narrator.Framework.Hooks;
using log4net;
using GERALD.Config;
using NBehave.Narrator.Framework;
using Ninject;
using GERALD.Pages;

namespace GERALD.Steps
{
    [Hooks]
    public class GeneralSteps
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(GeneralSteps));
        [Inject]
        public WebDriverPageOps pageOps { get; set; }

        [Inject]
        public SignedOutPage SignedOutPage { get; set; }

        public GeneralSteps()
        {
            Factory.Instance.Inject(this);
        }

        [NBehave.Narrator.Framework.Hooks.BeforeStep]
        public void BeforeStep()
        {
            // TODO: can we tell if a step has failed here??? If so, don't log the step.
            logger.Info(StepContext.Current.Step);
        }

        [NBehave.Narrator.Framework.Hooks.AfterScenario]
        public void AfterScenario()
        {
            try
            {
                logger.InfoFormat("Scenario execution complete: {0}", ScenarioContext.Current.ScenarioTitle);
               // BasePage.Logout();
                SignedOutPage.WaitUntilSignedOut();

            }
            catch (Exception e)
            {
                logger.Error("An exception occurred attempting to logout:", e);
            }
        }
    }
}
