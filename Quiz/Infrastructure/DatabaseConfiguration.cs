
namespace Quiz.Infrastructure
{
    public static class DatabaseConfiguration
    {
        public static string ConnectionString { get; set; }

        static DatabaseConfiguration()
        {
            ConnectionString =
                @"Data Source=SETAREH\SQLEXPRESS;Initial Catalog=BankSystem;User Id=sa;Password=سثظش1020; TrustServerCertificate=True";
        }
    }

}
