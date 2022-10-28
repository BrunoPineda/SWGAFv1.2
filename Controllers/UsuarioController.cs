using BackendSWGAF.Context;
using BackendSWGAF.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BackendSWGAF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext context;
        public UsuarioController(AppDbContext context)
        {
            this.context = context;
        }
        //linea para mostrar los mensajes del sql procedimientos almacenados
        public static string InfoMessageHandler(object mySender, SqlInfoMessageEventArgs myEvent)
        {
            return (myEvent.Message);
        }
        [HttpGet]
        public IActionResult listarUsuarios()
        {
            List<Usuario> usua = context.usuario.ToList();
      
            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = usua
            }); ; ;
        }
    }
}
