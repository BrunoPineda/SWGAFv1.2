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
        [HttpGet("{id}")]
        public IActionResult listarSolicitudporid(int id)
        {
            var soli = context.solicitud.FirstOrDefault(p => p.id == id);
            /* var query = from s in context.solicitud
                         join u in context.usuario on s.idUsuario equals u.id
                         select u;*/
            if (soli == null)
            {
                return NotFound(new
                {
                    Res = true,
                    StatusCode = 404,
                    Message = "Solicitud no encontrada",
                    Data = soli
                }); ; ;
            }
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
                var existsStatusId = context.usuariostatus.Where(p => p.id == request.idUsuario).FirstOrDefault();
                if (existsStatusId == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El estado id de solicitud no existe",
                        Data = ""
                    }); ;
                }
                Solicitud sol = new Solicitud()
                {
                  descripcion = request.descripcion,
                  cantProductos = request.cantProductos,
                  tipoDePago = request.tipoDePago,
                  tipoDeMoneda = request.tipoDeMoneda,
                  totaPrecio = request.totaPrecio,
                  fecha = request.fecha,
                  aceptado = request.aceptado,
                  idUsuario = request.idUsuario,
                };
                context.solicitud.Add(sol);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Se inserto la solicitud correctamente",
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

        [HttpPut("AceptarSolicitud/{id}")]
        public IActionResult AceptarSolicitud(int id, [FromBody] AcSoliRequest request)
        {
            try
            {
                Solicitud sol = context.solicitud.FirstOrDefault(e => e.id == id);
                if (sol == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "La solicitud no existe",
                        Data = ""
                    });
                }
                sol.aceptado = request.aceptado;

                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Se aprobo la solicitud",
                    Data = ""
                });
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
                Solicitud sol = context.solicitud.FirstOrDefault(e => e.id == id);
                if (sol == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "La solicitud no existe",
                        Data = ""
                    });

                }
                sol.descripcion = request.descripcion;
                sol.cantProductos = request.cantProductos;
                sol.tipoDePago = request.tipoDePago;
                sol.tipoDeMoneda = request.tipoDeMoneda;
                sol.totaPrecio = request.totaPrecio;
                sol.fecha = request.fecha;

                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Se actualizo la solicitud",
                    Data = ""
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Res = false,
                    StatusCode = 500,
                    Message = "Error en el servidor " + e.Message,
                    Data = ""
                });
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
                Solicitud sol = context.solicitud.FirstOrDefault(e => e.id == id);
                if (sol == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "La Solicitud no existe",
                        Data = ""
                    });
                }
                context.solicitud.Remove(sol);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Se elimino la solicitud correctamente",
                    Data = ""
                });
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
