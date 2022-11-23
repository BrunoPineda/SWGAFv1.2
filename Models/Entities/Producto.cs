using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSWGAF.Models.Entities
{
    public class Producto
    {
        [Key]
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }

        public double pVenta { get; set; }
        public double pCompra { get; set; }

        public string laboratorio { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string marca { get; set; }
        public int stock { get; set; }
        public string tipoProducto { get; set; }
        public int idStatus { get; set; }
        public int idCategory { get; set; }
        public int idRack { get; set; }
        public Category categoria { get; set; }
        public ProductoStatus productoStatus { get; set; }
        public Rack rack { get; set; }
        public List<solicitudhasProducto> solicitudhasProductos { get; set; }
        public List<productoHasFactura> productoHasFacturas { get; set; }

    }
}
