using BackendSWGAF.Models.Entities;
using System;
using System.Collections.Generic;

namespace BackendSWGAF.Models.DTOs
{
    public class FacturaRequest
    {
        public string descripcion { get; set; }
        public float cantidad { get; set; }
        public string tipoPago { get; set; }
        public float totaPrecio { get; set; }
        public DateTime fecha { get; set; }
        public int idUsuario { get; set; }
    }
}
