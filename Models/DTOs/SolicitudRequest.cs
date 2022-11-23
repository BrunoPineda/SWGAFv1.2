using System;

namespace BackendSWGAF.Models.DTOs
{
    public class SolicitudRequest
    {
        public string descripcion { get; set; }
        public int cantProductos { get; set; }
        public string tipoDePago { get; set; }
        public string tipoDeMoneda { get; set; }
        public double totaPrecio { get; set; }
        public DateTime fecha { get; set; }
        public Boolean aceptado { get; set; }
        public int idUsuario { get; set; }
    }
}
