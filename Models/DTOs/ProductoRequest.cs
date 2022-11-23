using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSWGAF.Models.Entities
{
    public class ProductoRequest
    {
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
    }
}
