namespace Library;

public class KafkaConnectionStringsProvider
{
    public static string PostgreConnectionStringName = "KafkaBootstrapServers";

    public KafkaConnectionStringsProvider(string connectionString)
    {
        ConnectionString = connectionString;
    }
    public string ConnectionString { get; init; }
}
