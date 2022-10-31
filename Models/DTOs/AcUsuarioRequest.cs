namespace BackendSWGAF.Models.DTOs
{
    public class AcUsuarioRequest
    {
        public string email { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int docNumber { get; set; }

        public string tipoDoc { get; set; }
        public string tipoUsuario { get; set; }

        public int idStatus { get; set; }
    }
}
