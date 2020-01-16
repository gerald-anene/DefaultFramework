
using GERALD.JiraClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject;
using Ninject.Extensions.Conventions;
using System.Configuration;
using GERALD.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using GERALD.Data;


namespace GERALD.Config
{
    public class Factory : NinjectModule
    {
        private static IKernel kernel;
        private static Factory instance;

        private Factory()
        {
        }

        public static Factory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Factory();
                    // initialise static ninject kernel
                    kernel = new StandardKernel(instance);
                }
                return instance;
            }
        }

        public T Get<T>()
        {
            return kernel.Get<T>();
        }

        public void Inject(object inject)
        {
            kernel.Inject(inject);
        }


        public override void Load()
        {
            // zapi client
            Bind<IZapi>().ToConstant(
                new JiraHttpClient(Settings.JiraUrl, Settings.JiraUser, Settings.JiraPassword));

            BindPageModels();
            BindWebDriver();
            bindDataModel();

        }

        private void BindWebDriver()
        {
            var browserType = Settings.BrowserType;
            var implicitTimeout = Settings.ImplicitTimeoutSeconds;

            switch (browserType)
            {
                case "IE":
                    Bind<IWebDriver>().To<InternetExplorerDriver>().WithConstructorArgument(Settings.IeDriverPath);
                    break;
                case "CHROME":
                    Bind<IWebDriver>().To<ChromeDriver>().WithConstructorArgument(Settings.ChromeDriverPath);
                    break;
                case "FIREFOX":
                    Bind<IWebDriver>().To<FirefoxDriver>();
                    break;
                default:
                    throw new InvalidOperationException(string.Format("Unsupported browser type [{0}]", browserType));
            }

            Rebind<WebDriverPageOps>().ToSelf().InSingletonScope().WithConstructorArgument(TimeSpan.FromSeconds(implicitTimeout));
        }

        private void BindPageModels()
        {
            this.Kernel.Bind(kernel =>
            {
                kernel.FromAssemblyContaining<LeopardSoftwareBasePage>().SelectAllClasses().BindToSelf();

            });
            Rebind<LoginPage>().ToSelf().WithConstructorArgument(Settings.asdaUrl);

        }

        private void bindDataModel()
        {

            Bind<IDbConnectionFactory>().To<SqlConnectionFactory>().InSingletonScope()
                .WithConstructorArgument(ConfigurationManager.ConnectionStrings["kc"].ConnectionString);


            Bind<CustomerCollection>().ToSelf().InSingletonScope();
            Bind<CustomerDao>().ToSelf().InSingletonScope();
        }



    }
}
