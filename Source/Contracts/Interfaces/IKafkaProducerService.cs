namespace Contracts.Interfaces;

public interface IKafkaProducerService
{
    Task ProduceMessageAsync(string message);
}