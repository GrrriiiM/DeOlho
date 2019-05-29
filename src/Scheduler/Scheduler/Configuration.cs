using Hangfire.MySql.Core;

namespace DeOlho.Scheduler.Dashboard
{
    public class Configuration
    {
        public string ConnectionString { get; set; }

        public MySqlStorage CreateStorage()
        {
            return new MySqlStorage(ConnectionString);
        }
    }
}