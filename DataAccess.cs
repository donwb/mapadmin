using System.Text;
using System.Data.SqlClient;

namespace NancyApplication
{
    public class DataAccess 
    {
        private const string CONN_STRING = 
            "Server=tcp:mapprogram.database.windows.net,1433;Initial Catalog=map;Persist Security Info=False;User ID=mapuser;Password=MapProgram!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private string FOR_JSON = " FOR JSON AUTO";

        public string GetActivity(string username = "")
        {
            
            var viewQuery = "select * from vwActivity";

            if(username.Length > 0)
            {
                viewQuery += " WHERE email LIKE '" + username + "'";
            }
            viewQuery += FOR_JSON;
            
            var res = QueryDb(viewQuery);

            return res;
        
        }

        public string GetActions()
        {
            var query = "SELECT * FROM actions" + FOR_JSON;
            var res = QueryDb(query);
            return res;
        }

        public bool UserExists(string username)
        {
            var query = "SELECT * FROM mapuser where email LIKE '" + username + "'" + FOR_JSON;
            var res = QueryDb(query);
            return (res.Length != 2);
        }

        private string QueryDb(string query)
        {
            using (var conn = new SqlConnection(CONN_STRING))
            {
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                
                var res = new StringBuilder();
                var reader = cmd.ExecuteReader();
                
                if(!reader.HasRows)
                {
                    res.Append("[]");
                } else 
                {
                    while(reader.Read())
                    {
                        res.Append(reader.GetValue(0).ToString());
                    }
                }

                return res.ToString();
            }
        }
    }
}