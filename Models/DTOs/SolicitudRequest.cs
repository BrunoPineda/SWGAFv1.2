using System;

namespace BackendSWGAF.Models.DTOs
{
    public class SolicitudRequest
    {
        public string nombre { get; set; }

        public double cantidad { get; set; }

        public string tipoDePago { get; set; }

        public int total { get; set; }
        public DateTime fecha { get; set; }
        public int aceptado { get; set; }
        public int idUsuario { get; set; }
    }
}
