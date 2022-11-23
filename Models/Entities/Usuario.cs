using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSWGAF.Models.Entities
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; }
        public byte[] passsword { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int docNumber { get; set; }

        public string tipoDoc { get; set; }
        public string tipoUsuario { get; set; }

        public int idStatus { get; set; }
        [ForeignKey("idStatus")]
        public UsuarioStatus usuariostatus { get; set; }
        public Factura factura { get; set; }
        public List<Solicitud> solicitudes { get; set; }

    }
}
