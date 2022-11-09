using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSWGAF.Models.Entities
{
    public class Category
    {
        [Key]
        public int id { get; set; }

        public string valor { get; set; }

        public string descripcion { get; set; }

        public int estado { get; set; }

        public List<Producto> productos { get; set; }
    }
}
