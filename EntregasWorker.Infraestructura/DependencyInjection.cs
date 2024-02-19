using Confluent.Kafka;
using EntregasWorker.CrossCutting.Configs;
using EntregasWorker.Dominio.Repositorios;
using EntregasWorker.Dominio.Services.Events;
using EntregasWorker.Infraestructura.Repositorios;
using EntregasWorker.Infraestructura.Services.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Net;

namespace EntregasWorker.Infraestructura
{
    public static class DependencyInjection
    {
        public static void AddInfraestructure(
            this IServiceCollection services, IConfiguration configInfo)
        {

            services.AddDataBaseFactories(configInfo);
            services.AddRepositories();
            services.AddProducer(configInfo);
            services.AddEventServices();
            services.AddConsumer(configInfo);
        }

        private static void AddDataBaseFactories(this IServiceCollection services, IConfiguration configInfo)
        {
            var appConfiguration = new AppConfiguration(configInfo);

            services.AddSingleton(mongoDatabase =>
            {
                var mongoClient = new MongoClient(appConfiguration.DbEntregasCnx);
                return mongoClient.GetDatabase(appConfiguration.DbEntregasDb);
            });

        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEntregaRepository, EntregaRepository>();

        }

        private static IServiceCollection AddProducer(this IServiceCollection services, IConfiguration configInfo)
        {
            var appConfiguration = new AppConfiguration(configInfo);

            var config = new ProducerConfig
            {
                Acks = Acks.Leader,
                BootstrapServers = appConfiguration.UrlKafkaServer,
                ClientId = Dns.GetHostName(),
            };

            services.AddSingleton<IPublisherFactory>(sp => new PublisherFactory(config));
            return services;
        }

        private static IServiceCollection AddConsumer(this IServiceCollection services, IConfiguration configInfo)
        {
            var appConfiguration = new AppConfiguration(configInfo);

            var config = new ConsumerConfig
            {
                BootstrapServers = appConfiguration.UrlKafkaServer,
                GroupId = "venta-registrar-entregas",
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            services.AddSingleton<IConsumerFactory>(sp => new ConsumerFactory(config));
            return services;
        }

        private static void AddEventServices(this IServiceCollection services)
        {
            services.AddSingleton<IEventSender, EventSender>();
        }
    }
}
