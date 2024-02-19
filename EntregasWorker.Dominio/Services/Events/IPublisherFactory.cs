using Confluent.Kafka;


namespace EntregasWorker.Dominio.Services.Events
{
    public interface IPublisherFactory
    {
        IProducer<string, string> GetProducer();
    }
}
