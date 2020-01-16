using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GERALD.Pages
{
    public class LeopardSoftwareBasePage
    {

           private readonly WebDriverPageOps pageOps;

        public LeopardSoftwareBasePage(WebDriverPageOps pageOps)
        {
            this.pageOps = pageOps;
        }

        protected WebDriverPageOps GetPageOps()
        {
            return pageOps;
        }
    }
}
