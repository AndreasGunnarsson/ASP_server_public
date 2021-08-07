using MySqlConnector;
using Core.Interfaces;

namespace Application
{
    /* public class AppDb : IDisposable */
    public class AppDb : IAppDb 
    {
        public MySqlConnection Connection { get; }

        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
