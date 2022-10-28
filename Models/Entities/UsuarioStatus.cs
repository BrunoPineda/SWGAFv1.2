using System.Collections.Generic;

namespace BackendSWGAF.Models.Entities
{
    public class UsuarioStatus
    {
        public int id { get; set; }
        public string valor { get; set; }
        public string descripcion { get; set; }

        public List<Usuario> usuarios { get; set; }
    }
}
