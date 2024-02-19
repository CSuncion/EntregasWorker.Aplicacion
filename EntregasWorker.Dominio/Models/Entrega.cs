using MongoDB.Bson;

namespace EntregasWorker.Dominio.Models
{
    public class Entrega
    {
        public ObjectId Id { get; set; }
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }

        public string NombreCliente { get; set; }

        public string DireccionEntrega { get; set; }

        public string Ciudad { get; set; }

        public List<EntregaDetalle> Detalle { get; set; }
    }
    public class EntregaDetalle
    {
        public string Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
