using BackendSWGAF.Context;
using BackendSWGAF.Helpers;
using BackendSWGAF.Models.DTOs;
using BackendSWGAF.Models.DTOs.Auth;
using BackendSWGAF.Models.Entities;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
        [HttpGet("UsuariosHabilitados")]
        public IActionResult listarUsuariosHabilitados()
        {
            var usua = from u in context.usuario
                       where u.idStatus == 1
                       select u;
            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = usua
            }); ; ;
        }
        [HttpGet("UsuariosInhabilitados")]
        public IActionResult listarUsuariosInhabilitados()
        {
            var usua = from u in context.usuario
                       where u.idStatus == 2
                       select u;
            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = usua
            }); ; ;
        }
        [HttpPost]
        public IActionResult CrearUsuario([FromForm] UsuarioRequest request)
        {
            try
            {
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_registrarUsuario", CommandType.StoredProcedure,
                 new SqlParameter("@email", request.email),
                 new SqlParameter("@password", request.passsword),
                 new SqlParameter("@nombre", request.nombre),
                 new SqlParameter("@apellido", request.apellido),
                 new SqlParameter("@docNumber", request.docNumber),
                 new SqlParameter("@tipoDoc", request.tipoDoc),
                 new SqlParameter("@tipoUsuario", request.tipoUsuario),
                 new SqlParameter("@idStatus", request.idStatus)
                 );
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = usu,
                    Data = ""
                }); ;
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    Res = false,
                    StatusCode = 500,
                    Message = "Problema del servidor",
                    Data = ""
                }); ;
            }
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarUsuario(int id, [FromForm] AcUsuarioRequest request)
        {
            try
            {
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_ActualizarUsuario", CommandType.StoredProcedure,
                 new SqlParameter("@id", id),
                 new SqlParameter("@email", request.email),
                 new SqlParameter("@nombre", request.nombre),
                 new SqlParameter("@apellido", request.apellido),
                 new SqlParameter("@docNumber", request.docNumber),
                 new SqlParameter("@tipoDoc", request.tipoDoc),
                 new SqlParameter("@tipoUsuario", request.tipoUsuario),
                 new SqlParameter("@idStatus", request.idStatus)
                 );
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = usu,
                    Data = ""
                }); ;
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    Res = false,
                    StatusCode = 500,
                    Message = "Problema del servidor",
                    Data = ""
                }); ;
            }
        }

        [HttpPut("habilitar/{id}")] 
         
        public IActionResult HabilitarUsuario(int id)
        {
            try
            {
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_HabilitarUsuario", CommandType.StoredProcedure,
                 new SqlParameter("@id", id)
                 );
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = usu,
                    Data = ""
                }); ;
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    Res = false,
                    StatusCode = 500,
                    Message = "Problema del servidor",
                    Data = ""
                }); ;
            }
        } 

        [HttpDelete("{id}")]
        //[Route("especialidad")]
        //[AllowAnonymous]
        //[Authorize]
        public IActionResult EliminarUsuario(int id)
        {
            try
            {
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_EliminarUsuario", CommandType.StoredProcedure,
                 new SqlParameter("@id", id)

                 );


                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = usu,
                    Data = ""
                }); ;
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    Res = false,
                    StatusCode = 500,
                    Message = "Problema del servidor " + e.Message,
                    Data = ""
                });
            }

        }

    }
}
