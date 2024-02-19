using AutoMapper;


namespace EntregasWorker.Aplicacion.CasosUso.AdministrarEntrega.RegistrarEntrega
{
    public class RegistrarEntregaMapper : Profile
    {
        public RegistrarEntregaMapper()
        {
            CreateMap<RegistrarEntregaRequest, Dominio.Models.Entrega>();
        }
    }
}
