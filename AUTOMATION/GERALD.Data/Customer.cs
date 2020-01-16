using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GERALD.Data
{
    public class Customer
    {
       // private const string CUSTOMER_FULL_NAME_FORMAT = "{0} {1}";

        public string Name { get; set; }
      

        public string CustomerName()
        {
            return Name;
        }
    }
}
