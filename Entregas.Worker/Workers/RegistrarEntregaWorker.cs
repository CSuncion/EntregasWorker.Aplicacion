using EntregasWorker.Aplicacion.CasosUso.AdministrarEntrega.RegistrarEntrega;
using EntregasWorker.Dominio.Services.Events;
using MediatR;
using Newtonsoft.Json;

namespace Entregas.Worker.Workers
{
    public class RegistrarEntregaWorker : BackgroundService
    {
        private readonly IConsumerFactory _consumerFactory;
        private readonly IServiceProvider _serviceProvider;

        public RegistrarEntregaWorker(IConsumerFactory consumerFactory, IServiceProvider serviceProvider)
        {
            _consumerFactory = consumerFactory;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumer = _consumerFactory.GetConsumer();
            consumer.Subscribe("entregas");

            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var consumeResult = consumer.Consume(cancellationToken);
                //Llamar al handler para registrar la información de la entrega
                RegistrarEntregaRequest request = JsonConvert.DeserializeObject<RegistrarEntregaRequest>(consumeResult.Value);

                await mediator.Send(request);
            }

            consumer.Close();

        }
    }
}
