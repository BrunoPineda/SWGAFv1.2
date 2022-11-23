using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendSWGAF.Models.Entities
{
    public class Kardex
    {
        [Key]
        public int id { get; set; }

        public string descripcion { get; set; }

        public int catidad { get; set; }
        public string tipoDePago { get; set; }
        public float totaPrecio { get; set; }
        public DateTime fecha { get; set; }

        public int idFactura { get; set; }

        public Factura factura { get; set; }        
    }
}
