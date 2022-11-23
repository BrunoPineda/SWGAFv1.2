using BackendSWGAF.Context;
using BackendSWGAF.Models.DTOs;
using BackendSWGAF.Models.DTOs.Auth;
using BackendSWGAF.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackendSWGAF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly AppDbContext context;
        public FacturaController(AppDbContext context)
        {
            this.context = context;
        }
        //linea para mostrar los mensajes del sql procedimientos almacenados
        public static string InfoMessageHandler(object mySender, SqlInfoMessageEventArgs myEvent)
        {
            return (myEvent.Message);
        }
        [HttpGet]
        public IActionResult listarFactura()
        {
            List<Factura> Factura = context.factura.ToList();

            /*
             * var query = from f in context.factura
                        join u in context.usuario on f.idUsuario equals u.id 
                        select u;
            */

            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = Factura
            }); ; ;
        }
        [HttpPost]
        public IActionResult crearFactura([FromBody] FacturaRequest request)
        {
            try
            {
                var exist = context.factura.FirstOrDefault(p => p.id == request.idUsuario);
                if(exist == null) 
                { 
                return NotFound(new
                {
                    Res = false,
                    StatusCode = 404,
                    Message = "La factura id no existe",
                    Data = ""
                }); ;
                }
                Factura fac = new Factura()
                {
                    descripcion = request.descripcion,
                    cantidad = request.cantidad,
                    tipoPago = request.tipoPago,
                    totaPrecio = request.totaPrecio,  
                    fecha = request.fecha,
                    idUsuario = request.idUsuario,
                   
                };
                context.factura.Add(fac);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Factura registrado correctamente",
                    Data = ""
                }); ;
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    Res = false,
                    StatusCode = 500,
                    Message = "Problema del servidor"+ e,
                    Data = ""
                }); ;
            }
        }
        [HttpPut("{id}")]

        public IActionResult ActualizarFactura(int id, [FromBody] FacturaRequest request)
        {
            try
            {
                var exist = context.factura.FirstOrDefault(p => p.id == id);
                if (exist == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El id de la factura no existe",
                        Data = ""
                    }); ;
                }
                Factura fac = context.factura.FirstOrDefault(e => e.id == id);
                if (fac == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "La factura no existe",
                        Data = ""
                    });
                }
                fac.descripcion = request.descripcion;
                    fac.cantidad = request.cantidad;
                   fac.tipoPago = request.tipoPago;
                    fac.totaPrecio = request.totaPrecio;  
                    fac.fecha = request.fecha;
                fac.idUsuario = request.idUsuario;

                context.SaveChanges();

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

            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "Factura actualizada",
                Data = ""
            }); ;

        }
        [HttpDelete("{id}")]
        //[Route("especialidad")]
        //[AllowAnonymous]
        //[Authorize]
        public IActionResult EliminarFactura(int id)
        {
            try
            {
                Factura ope = context.factura.FirstOrDefault(e => e.id == id);
                if (ope == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "La Factura no existe",
                        Data = ""
                    });
                }
                context.factura.Remove(ope);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Factura eliminada correctamente",
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
