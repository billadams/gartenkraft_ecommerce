using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Gartenkraft.Models
{
    public class GartenkraftConnection
    {
        
        public static SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=184.168.194.77;Initial Catalog=gartenkraft;Persist Security Info=True;User ID=gkraft;Password=Exb3t3^5;";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}