using System;

namespace BackendSWGAF.Models.DTOs
{
    public class KardexRequest
    {
        public string descripcion { get; set; }

        public int catidad { get; set; }
        public string tipoDePago { get; set; }
        public float totaPrecio { get; set; }
        public DateTime fecha { get; set; }

        public int idFactura { get; set; }
    }
}
