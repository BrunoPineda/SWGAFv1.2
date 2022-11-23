using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSWGAF.Models.Entities
{
    public class Solicitud
    {
        [Key]
        public int id { get; set; }
        public string descripcion { get; set; }
        public int cantProductos { get; set; }
        public string tipoDePago { get; set; }
        public string tipoDeMoneda { get; set; }
        public double totaPrecio { get; set; }
        public DateTime fecha { get; set; }
        public Boolean aceptado { get; set; }
        public int idUsuario { get; set; }
        [ForeignKey("idUsuario")]
        public Usuario usuario { get; set; }
        public List<solicitudhasProducto> solicitudhasProductos { get; set; }

    }
}
