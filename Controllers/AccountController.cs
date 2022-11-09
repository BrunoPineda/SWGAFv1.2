using BackendSWGAF.Context;
using BackendSWGAF.Helpers;
using BackendSWGAF.Models.DTOs;
using BackendSWGAF.Models.DTOs.Auth;
using BackendSWGAF.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BackendSWGAF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly TokenKey tokenkey;
        public AccountController(AppDbContext context)
        {
            this.context = context;
            this.tokenkey = new TokenKey();
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
  
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(tokenkey.tokenkey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Name, cons.idusuario.ToString())
            }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
 
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
