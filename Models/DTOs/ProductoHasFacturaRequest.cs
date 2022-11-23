namespace BackendSWGAF.Models.DTOs
{
    public class ProductoHasFacturaRequest
    {
        public int cantidad { get; set; }
        public double precio { get; set; }    
        public int idFactura { get; set; }
        public int idProducto { get; set; }
    }
}
