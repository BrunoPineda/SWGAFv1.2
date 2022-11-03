using BackendSWGAF.Context;
using BackendSWGAF.Helpers;
using BackendSWGAF.Models.DTOs;
using BackendSWGAF.Models.DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;

namespace BackendSWGAF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext context;
        public AccountController(AppDbContext context)
        {
            this.context = context;
        }
        //linea para mostrar los mensajes del sql procedimientos almacenados
        public static string InfoMessageHandler(object mySender, SqlInfoMessageEventArgs myEvent)
        {
            return (myEvent.Message);
        }
        [HttpPost]
        public IActionResult login([FromBody] LoginRequest request)
        {
            try
            { 
                var cons = context.usuario.Where(u => u.email == request.email).Select(u => new { email=u.email,nombre=u.nombre,apellido=u.apellido,tipoUsuario=u.tipoUsuario,idusuario=u.idStatus }).FirstOrDefault();

                var log = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_login", CommandType.StoredProcedure,
                new SqlParameter("@email", request.email),
                 new SqlParameter("@passsword", request.passsword)
                 );

                if(log == "No se encontre ese usuario")
                 {
                     return NotFound(new
                     {
                         Res = true,
                         StatusCode = 404,
                         Message = log,
                         Data = ""
                     }); ;
                 }
               
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = log,
                    Data = cons
                }); ;
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    Res = false,
                    StatusCode = 500,
                    Message = "Problema del servidor" + e,
                    Data = ""
                }); ;
            }
        }

    }
}
