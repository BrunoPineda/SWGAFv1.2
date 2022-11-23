using BackendSWGAF.Context;
using BackendSWGAF.Helpers;
using BackendSWGAF.Models.DTOs;
using BackendSWGAF.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BackendSWGAF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudHasProductoController : ControllerBase
    {
        private readonly AppDbContext context;
        public SolicitudHasProductoController(AppDbContext context)
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
            List<solicitudhasProducto> soli = context.solicitudhasProducto.ToList();
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
        public IActionResult registrarsolicitudhasProducto([FromBody] solicitudhasProducto request)
        {
            try
            {
                var existisolicitud = context.solicitud.Where(p => p.id == request.IdSolicitud).FirstOrDefault();
                if (existisolicitud == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "error no hay idsolicitud",
                        Data = ""
                    }); ;
                }
                var existiproducto = context.producto.Where(p => p.id == request.IdProducto).FirstOrDefault();
                if (existiproducto == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "error no hay idproducto",
                        Data = ""
                    }); ;
                }
                solicitudhasProducto shp = new solicitudhasProducto()
                {
                    cantidad = request.cantidad,
                    precio = request.precio,
                    IdSolicitud = request.IdSolicitud,
                    IdProducto = request.IdProducto,    
                };
                context.solicitudhasProducto.Add(shp);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Se inserto correctamente la relación entre solicitud y producto",
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
    }
}
