using log4net;
using System.Data;

namespace GERALD.Data
{
    public class CustomerDao
    {
        private const string name = "name";
       

        private const string COMMAND = @"SELECT TOP 5
                                       [name]
                                      ,[column_id]
                                      ,[system_type_id]
                                      FROM [model].[sys].[columns]";


        private readonly IDbConnectionFactory connectionFactory;

        public CustomerDao(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void RepopulateCustomerCollection(CustomerCollection customerCollection)
        {
            customerCollection.Clear();
            using (var connection = connectionFactory.OpenConnection()) 
            using (var command= connection.CreateCommand(COMMAND))
            using (var reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    customerCollection.AddCustomer(GetCustomer(reader));
                }
            }
        }

        private Customer GetCustomer(IDataReader reader)
        {
            var CUSTOMER_NAME = reader.GetString(reader.GetOrdinal(name));
           

            return new Customer
            {
                Name = CUSTOMER_NAME,
                

            };

        }
    }
}
