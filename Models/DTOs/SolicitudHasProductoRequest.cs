namespace BackendSWGAF.Models.DTOs
{
    public class SolicitudHasProductoRequest
    {
        public int cantidad { get; set; }
        public double precio { get; set; }
        public int idSolicitud { get; set; }
        public int idProducto { get; set; }
    }
}
