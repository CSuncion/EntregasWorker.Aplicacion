using EntregasWorker.Dominio.Models;


namespace EntregasWorker.Dominio.Repositorios
{
    public interface IEntregaRepository : IRepository
    {
        Task<bool> Adicionar(Entrega entity);

        Task<bool> Modificar(Entrega entity);

        Task<bool> Eliminar(Entrega entity);

        Task<Entrega> Consultar(int id);

        Task<IEnumerable<Entrega>> Consultar(string nombre);
    }
}
