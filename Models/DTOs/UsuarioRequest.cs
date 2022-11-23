using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSWGAF.Models.DTOs.Auth
{
    public class UsuarioRequest
    {
        public string email { get; set; }
        public string passsword { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int docNumber { get; set; }
        public string tipoDoc { get; set; }
        public string tipoUsuario { get; set; }
        public int idStatus { get; set; }
 

    }
}
