using BackendSWGAF.Context;
using BackendSWGAF.Helpers;
using BackendSWGAF.Models.DTOs;
using BackendSWGAF.Models.DTOs.Auth;
using BackendSWGAF.Models.Entities;
using Microsoft.AspNetCore.Authorization;
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
            //List<Usuario> usua = context.usuario.ToList();
            var usua = context.usuario.Where(p => p.idStatus == 1).Select(p => new {
                id = p.id,
                email = p.email,    
                nombre = p.nombre,
                apellido = p.apellido,
                docNumber = p.docNumber,    
                tipoDoc = p.tipoDoc,
                tipoUsuario = p.tipoUsuario,
                idStatus = p.idStatus,  
            }).ToList();
            /*
            var usua = from u in context.usuario
                       where u.idStatus == 1
                       select u;
            */
            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = usua
            }); ; ;
        }
        [HttpGet("UsuariosHabilitados/{id}")]
        public IActionResult listarUsuariosHabilitadosporid(int id)
        {
            var usua = context.usuario.Where(p => p.idStatus == 2).Select(p => new {
                id = p.id,
                email = p.email,
                nombre = p.nombre,
                apellido = p.apellido,
                docNumber = p.docNumber,
                tipoDoc = p.tipoDoc,
                tipoUsuario = p.tipoUsuario,
                idStatus = p.idStatus,
            }).FirstOrDefault(e => e.id == id && e.idStatus == 1);

            if (usua == null)
            {
                return NotFound(new
                {
                    Res = true,
                    StatusCode = 404,
                    Message = "Usuario no encontrado",
                    Data = usua
                }); ; ;
            }

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
            var usua = context.usuario.Select(p => new {
                id = p.id,
                email = p.email,
                nombre = p.nombre,
                apellido = p.apellido,
                docNumber = p.docNumber,
                tipoDoc = p.tipoDoc,
                tipoUsuario = p.tipoUsuario,
                idStatus = p.idStatus,
            }).FirstOrDefault(p => p.idStatus == 2);

            if (usua == null)
            {
                return NotFound(new
                {
                    Res = true,
                    StatusCode = 404,
                    Message = "Usuario no encontrado",
                    Data = usua
                }); ; ;
            }
            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = usua
            }); ; ;
        }
        [HttpGet("UsuariosInhabilitados/{id}")]
        public IActionResult listarUsuariosInhabilitados(int id)
        {

            var usua = context.usuario.Select(p => new {
                id = p.id,
                email = p.email,
                nombre = p.nombre,
                apellido = p.apellido,
                docNumber = p.docNumber,
                tipoDoc = p.tipoDoc,
                tipoUsuario = p.tipoUsuario,
                idStatus = p.idStatus,
            }).FirstOrDefault(e => e.id == id && e.idStatus == 2);
            /*  
             var usua = from u in context.usuario
                        where u.idStatus == 2
                        where u.id == id
                        select u;
            */
            if (usua == null)
            {
                return NotFound(new
                {
                    Res = true,
                    StatusCode = 404,
                    Message = "Usuario no encontrado",
                    Data = usua
                }); ; ;
            }
            else { 

            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = usua
            }); ; ;
            }
        }
        [HttpPost]
        public IActionResult CrearUsuario([FromBody] UsuarioRequest request)
        {
            try
            {
                 //var existsStatusId = context.usuario.Select(p => new {idStatus=p.idStatus}).Where(u => u.idStatus == request.idStatus).Firstor();
                var existsStatusId = context.usuariostatus.Where(p=>p.id==request.idStatus).FirstOrDefault();
                if (existsStatusId == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El estado id de usuario no existe"+existsStatusId,
                        Data = ""
                    }); ;
                }
                
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
                    Message = "Problema del servidor "+e,
                    Data = ""
                }); ;
            }
        }
        [HttpPost("/usuarioStatus/")]
        public IActionResult CrearUsuarioStatus([FromBody] UsuarioStatusRequest request)
        {
            try
            {
                UsuarioStatus us = new UsuarioStatus()
                {
                    valor = request.valor,
                    descripcion = request.descripcion,
                };
                context.usuariostatus.Add(us);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = us,
                    Data = ""
                }); ;
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    Res = false,
                    StatusCode = 500,
                    Message = "Problema del servidor " + e,
                    Data = ""
                }); ;
            }
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] AcUsuarioRequest request)
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
                   new SqlParameter("@tipoUsuario", request.tipoUsuario)
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
                    Message = "Problema del servidor" +e,
                    Data = ""
                }); ;
            }
        }

        [HttpPut("habilitar/{id}")] 
         
        public IActionResult HabilitarUsuario(int id)
        {
            try
            {
                var Existid = context.usuario.Select(p => new { id = p.id }).Where(u => u.id == id).FirstOrDefault();
                if (Existid == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El id de usuario no existe",
                        Data = ""
                    }); ;
                }
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "[sp_HabilitarUsuario]", CommandType.StoredProcedure,
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
                    Message = "Problema del servidor" +e ,
                    Data = ""
                }); ;
            }
        } 

        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            try
            {
                var Existid = context.usuario.Select(p => new { id = p.id}).Where(u => u.id == id).FirstOrDefault();
                if (Existid == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El id de usuario no existe",
                        Data = ""
                    }); ;
                }
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "[sp_EliminarUsuario]", CommandType.StoredProcedure,
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
