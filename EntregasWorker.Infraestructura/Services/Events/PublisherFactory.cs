using Confluent.Kafka;
using EntregasWorker.Dominio.Services.Events;

namespace EntregasWorker.Infraestructura.Services.Events
{
    public class PublisherFactory : IPublisherFactory
    {
        private static Lazy<IProducer<string, string>> LazyKafkaConnection = null;

        public PublisherFactory(ProducerConfig config)
        {
            LazyKafkaConnection = new Lazy<IProducer<string, string>>(() => new ProducerBuilder<string, string>(config).Build());
        }

        public IProducer<string, string> GetProducer()
            => LazyKafkaConnection.Value;
    }
}
