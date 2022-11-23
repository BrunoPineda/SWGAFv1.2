using BackendSWGAF.Context;
using BackendSWGAF.Helpers;
using BackendSWGAF.Models.DTOs;
using BackendSWGAF.Models.Entities;
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
    public class ProductoHasFacturaController : ControllerBase
    {
        private readonly AppDbContext context;

        public ProductoHasFacturaController(AppDbContext context)
        {
            this.context = context;
        }
        //linea para mostrar los mensajes del sql procedimientos almacenados
        public static string InfoMessageHandler(object mySender, SqlInfoMessageEventArgs myEvent)
        {
            return (myEvent.Message);
        }
        [HttpGet("productohasfacturas/{id}")]
        public IActionResult listarLosProductosdeunafactura(int id)
        {
            var soli = context.productoHasFactura.
                Include(p => p.producto).
                Include(p => p.factura).
                Select(p => new {
                    id = p.producto.id,
                    codigo = p.producto.id,
                    descripcion = p.producto.descripcion,
                    precioUnidad = p.producto.pVenta,
                    cantidad = p.cantidad,
                    precio = p.precio,
                    idFactura = p.IdFactura,
                    idproducto = p.IdProducto,
                    tipodepago = p.factura.tipoPago
                }).Where(p => p.idFactura == id).ToList();

            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = soli
            }); ; ;
        }

        [HttpGet]
        public IActionResult listarFactura()
        {
            List<productoHasFactura> soli = context.productoHasFactura.ToList();
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
                    Message = "Producto no encontrado",
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
        public IActionResult registrarSolicitud([FromBody] ProductoHasFacturaRequest request)
        {
            try
            {
                var existifactura = context.factura.Where(p => p.id == request.idFactura).FirstOrDefault();
                if (existifactura == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "error no hay idfactura",
                        Data = ""
                    }); ;
                }
                var existiProducto = context.producto.Where(p => p.id == request.idProducto).FirstOrDefault();
                if (existiProducto == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "error no hay idproducto",
                        Data = ""
                    }); ;
                }
                productoHasFactura phf = new productoHasFactura()
                {
                    cantidad = request.cantidad,
                    precio =  request.precio,
                    IdFactura = request.idFactura,
                    IdProducto = request.idProducto,
                };
                context.productoHasFactura.Add(phf);
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
