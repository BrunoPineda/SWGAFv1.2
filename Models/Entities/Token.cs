using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSWGAF.Models.Entities
{
    public class Token
    {
        //cada vez que se inicializa va a darle ese valor constante al token 
         public Token()
        {
            token = "SWGAF";
        }
        public string token { get; }
    }
}
