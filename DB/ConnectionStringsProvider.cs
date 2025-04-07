namespace DB;

public class ConnectionStringsProvider
{
    public static string PostgreConnectionStringName = "PostgreConnectionString";

    public ConnectionStringsProvider(string connectionString)
    {
        ConnectionString = connectionString;
    }
    public string ConnectionString { get; init; }
}
