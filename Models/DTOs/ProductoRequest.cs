using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSWGAF.Models.Entities
{
    public class ProductoRequest
    {
        public string nombre { get; set; }
        public float pVenta { get; set; }
        public float pCompra { get; set; }
        public string laboratorio { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string marca { get; set; }
        public int idStatus { get; set; }
        public int idCategory { get; set; }
        public int idRack { get; set; }
    }
}
