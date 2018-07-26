using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{

    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {

            string datasource = @"sqltest";

            string database = "AdventureWorksLT2012";
            string username = ConfigurationSettings.AppSettings["Username"].ToString();
            string password = ConfigurationSettings.AppSettings["Password"].ToString();

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}
