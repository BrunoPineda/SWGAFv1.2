namespace BackendSWGAF.Models.Entities
{
    public class productoHasFactura
    {
        public int Id { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }

        public Factura factura { get; set; }
        public Producto producto { get; set; }
    }
}
