using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSWGAF.Models.DTOs.Auth
{
    public class CategoryRequest
    {
 
        public string valor { get; set; }

        public string  descripcion { get; set; }

        public int  estado { get; set; }

    }
}
