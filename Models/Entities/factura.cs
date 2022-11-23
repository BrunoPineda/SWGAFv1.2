using System;
using System.Collections.Generic;

namespace BackendSWGAF.Models.Entities
{
    public class Factura
    {
        public int id  { get; set; }
        public string descripcion { get; set; }
        public double cantidad { get; set; }
        public string tipoPago { get; set; }
        public double totaPrecio { get; set; }
        public DateTime fecha { get; set; }
        public int idUsuario { get; set; }


        public List<Kardex> kardexs { get; set; }
        public List<Usuario> usuarios { get; set; }
        public List<productoHasFactura> productoHasFacturas { get; set; }
    }
}
