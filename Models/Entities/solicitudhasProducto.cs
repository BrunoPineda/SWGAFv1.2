namespace BackendSWGAF.Models.Entities
{
    public class solicitudhasProducto
    {
        public int Id { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public int IdSolicitud { get; set; }
        public int IdProducto { get; set; }

        public Solicitud solicitud { get; set; }
        public Producto producto { get; set; }
    }
}
