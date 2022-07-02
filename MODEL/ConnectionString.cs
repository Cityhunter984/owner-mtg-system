using System.Data.SqlClient;

namespace MODEL
{
    public class ConnectionString
    {
        public static SqlConnection getConnectionString()
        {
            //string connectionString = @"Data Source=USERS\SQLEXPRESS;Initial Catalog=DB_A4AD31_BetProject;Integrated Security=True";

            string connectionString = @"Data Source=SQL5032.site4now.net;Initial Catalog=DB_A4AD31_BetProject;User Id=DB_A4AD31_BetProject_admin;Password=ss12345678;";

            SqlConnection cn = new SqlConnection(connectionString);
            return cn;
        }
    }
}
