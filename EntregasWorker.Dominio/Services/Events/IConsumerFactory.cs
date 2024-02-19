using Confluent.Kafka;

namespace EntregasWorker.Dominio.Services.Events
{
    public interface IConsumerFactory
    {
        IConsumer<string, string> GetConsumer();
    }
}
