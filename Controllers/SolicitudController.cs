using BackendSWGAF.Helpers;
using BackendSWGAF.Models.DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using BackendSWGAF.Models.DTOs;
using BackendSWGAF.Context;
using BackendSWGAF.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BackendSWGAF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly AppDbContext context;
        public SolicitudController(AppDbContext context)
        {
            this.context = context;
        }
        //linea para mostrar los mensajes del sql procedimientos almacenados
        public static string InfoMessageHandler(object mySender, SqlInfoMessageEventArgs myEvent)
        {
            return (myEvent.Message);
        }

        [HttpGet]
        public IActionResult listarSolicitud()
        {
            List<Solicitud> soli = context.solicitud.ToList();
           /* var query = from s in context.solicitud
                        join u in context.usuario on s.idUsuario equals u.id
                        select u;*/

            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = soli
            }); ; ;
        }

        [HttpPost]
        public IActionResult registrarSolicitud([FromBody] SolicitudRequest request)
        {
            try
            {
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_registrarSolicitud", CommandType.StoredProcedure,
                 new SqlParameter("@nombre", request.nombre),
                 new SqlParameter("@Cantidad", request.cantidad),
                 new SqlParameter("@tipoDePago", request.tipoDePago),
                 new SqlParameter("@total", request.total),
                 new SqlParameter("@fecha", request.fecha),
                 new SqlParameter("@aceptado", request.aceptado),
                 new SqlParameter("@idUsuario", request.idUsuario)
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

        [HttpPut("AceptarSolicitud/{id}")]
        public IActionResult AceptarSolicitud([FromBody] int id, int estado)
        {
            try
            {
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_AceptarSolicitud", CommandType.StoredProcedure,
                 new SqlParameter("@id", id),
                 new SqlParameter("@Aceptado", estado)
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
        public IActionResult ActualizarSolicitud(int id, [FromBody] SolicitudRequest request)
        {
            try
            {
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_ActualizarSolicitud", CommandType.StoredProcedure,
                 new SqlParameter("@id", id),
                 new SqlParameter("@nombre", request.nombre),
                 new SqlParameter("@Cantidad", request.cantidad),
                 new SqlParameter("@tipoDePago", request.tipoDePago),
                 new SqlParameter("@total", request.total),
                 new SqlParameter("@fecha", request.fecha),
                 new SqlParameter("@aceptado", request.aceptado),
                 new SqlParameter("@idUsuario", request.idUsuario)
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
        public IActionResult EliminarSolicitud(int id)
        {
            try
            {
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_EliminarSolicitud", CommandType.StoredProcedure,
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
