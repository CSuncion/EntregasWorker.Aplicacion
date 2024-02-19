
using AutoMapper;
using EntregasWorker.Aplicacion.CasosUso.AdministrarEntrega.RegistrarEntrega;
using EntregasWorker.Dominio.Repositorios;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace EntregasWorker.Test.Aplicacion.Testes
{
    public class AdministrarEntregasTests
    {
        private readonly IEntregaRepository _entregaRepository;
        private readonly IMapper _mapper;
        private readonly RegistrarEntregaHandler _registrarEntregaHandler;

        public AdministrarEntregasTests()
        {
            _entregaRepository = Substitute.For<IEntregaRepository>();
            _mapper = Substitute.For<IMapper>();
            _registrarEntregaHandler = Substitute.For<RegistrarEntregaHandler>(_entregaRepository, _mapper);

        }

        [Fact]
        public async Task RegistrarEntregaOK()
        {
            var request = new RegistrarEntregaRequest() { IdVenta = 123 };
            CancellationTokenSource cts = new();
            CancellationToken cancellationToken = cts.Token;

            //Escenarios
            _entregaRepository.Adicionar(default).ReturnsForAnyArgs(true);

            cts.Cancel();
            var retorno = await _registrarEntregaHandler.Handle(request, cancellationToken);

            /// Assert
            Assert.True(retorno.HasSucceeded);

        }

        [Fact]
        public async Task RegistrarEntregaError()
        {
            var request = new RegistrarEntregaRequest() { IdVenta = 123 };
            CancellationTokenSource cts = new();
            CancellationToken cancellationToken = cts.Token;

            //Escenarios
            _entregaRepository.Adicionar(default).ReturnsForAnyArgs(false);

            cts.Cancel();
            var retorno = await _registrarEntregaHandler.Handle(request, cancellationToken);

            /// Assert
            Assert.False(retorno.HasSucceeded);

        }

        [Fact]
        public async Task RegistrarEntregaException()
        {

            var request = new RegistrarEntregaRequest() { IdVenta = 123 };
            CancellationTokenSource cts = new();
            CancellationToken cancellationToken = cts.Token;

            //Escenarios
            _entregaRepository.Adicionar(default).ThrowsForAnyArgs<Exception>();

            cts.Cancel();
            /// Assert
            Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await _registrarEntregaHandler.Handle(request, cancellationToken);
            });


        }
    }
}
