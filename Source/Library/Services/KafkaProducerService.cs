using Confluent.Kafka;
using Contracts.Interfaces;

namespace Library.Services;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly ProducerConfig _config;
    private readonly string _topicName;

    public KafkaProducerService(KafkaConnectionStringsProvider kafkaconnectionStringsProvider)
    {
        _config = new ProducerConfig { BootstrapServers = kafkaconnectionStringsProvider.ConnectionString };
        _topicName = "test-topic";
    }

    public async Task ProduceMessageAsync(string message)
    {
        using var producer = new ProducerBuilder<Null, string>(_config).Build();
        var deliveryReport = await producer.ProduceAsync(_topicName, new Message<Null, string> { Value = message });
    }
}
