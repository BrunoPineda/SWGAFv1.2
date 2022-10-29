using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSWGAF.Models.Entities
{
    public class Solicitud
    {
        [Key]
        public int id { get; set; }

        public string nombre { get; set; }

        public float cantidad { get; set; }

        public string tipoDePago { get; set; }

        public float total { get; set; }
        public DateTime fecha { get; set; }
        public int aceptado { get; set; }
        public int idUsuario { get; set; }
        [ForeignKey("idUsuario")]
        public Usuario usuario { get; set; }

    }
}
