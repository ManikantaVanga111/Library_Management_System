using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
   public  class Connectionclass
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection("Data source=US-1C4R8S3;Database=LMS;Integrated security=true");
            return conn;
        }
        public static SqlDataAdapter GetAdapter(string query)
        {
            SqlConnection con = GetConnection();
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            return adp;
        }
    }
}
