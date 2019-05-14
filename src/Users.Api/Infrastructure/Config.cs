using System;

namespace Users.Api.Infrastructure
{
    public class Config
    {
        private static string _connectionString;

        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("MYSQL_CONN")))
                    {
                        throw new Exception("MYSQL_CONN environment variable is empty");
                    }
                    _connectionString = Environment.GetEnvironmentVariable("MYSQL_CONN");
                }
                return _connectionString;
            }
        }
    }
}