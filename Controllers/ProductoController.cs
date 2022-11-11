using BackendSWGAF.Context;
using BackendSWGAF.Helpers;
using BackendSWGAF.Models.DTOs;
using BackendSWGAF.Models.DTOs.Auth;
using BackendSWGAF.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BackendSWGAF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly TokenKey tokenkey;
        public ProductoController(AppDbContext context)
        {
            this.context = context;
            this.tokenkey = new TokenKey();
        }
        //linea para mostrar los mensajes del sql procedimientos almacenados
        public static string InfoMessageHandler(object mySender, SqlInfoMessageEventArgs myEvent)
        {
            return (myEvent.Message);
        }
        [HttpGet]
        public IActionResult listarProducto()
        {
            var soli = context.producto.Include(p => p.rack).Include(p => p.productoStatus).Include(p => p.categoria).Select(p => new { nombre = p.nombre,pVenta = p.pVenta,pCompra = p.pCompra
                ,laboratorio=p.laboratorio,fechaVencimiento=p.fechaVencimiento,
                marca=p.marca,rack=p.rack.valor,estado = p.productoStatus.descripcion }).ToList();
            
            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = soli
            }); ; ;
        }
        [HttpGet("{id}")]
        public IActionResult listarProductoPorId(int id)
        {
            var soli = context.producto.
                Include(p => p.rack).
                Include(p => p.productoStatus).
                Include(p => p.categoria).Select(p => new {
                id = p.id,
                nombre = p.nombre,
                pVenta = p.pVenta,
                pCompra = p.pCompra,
                laboratorio = p.laboratorio,
                fechaVencimiento = p.fechaVencimiento,
                marca = p.marca,
                rack = p.rack.valor,
                estado = p.productoStatus.descripcion
            }).FirstOrDefault(p => p.id == id);
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
        [HttpPost("/productStatus")]
        public IActionResult registrarProductoStatus([FromBody] ProductoStatusRequest request)
        {
            try
            {
                ProductoStatus ps = new ProductoStatus()
                {
                    valor = request.valor,
                    descripcion = request.descripcion
                };
                context.productostatus.Add(ps);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = ps,
                    Data = ""
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
        [HttpPost]
        public IActionResult registrarProducto([FromBody] ProductoRequest request)
        {
            try
            {
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_agregarProducto", CommandType.StoredProcedure,
                 new SqlParameter("@nombre", request.nombre),
                 new SqlParameter("@Pventa", request.pVenta),
                 new SqlParameter("@Pcompra", request.pCompra),
                 new SqlParameter("@laboratorio", request.laboratorio),
                 new SqlParameter("@FechaVencimiento", request.fechaVencimiento),
                 new SqlParameter("@marca", request.marca),
                 new SqlParameter("@IdStatus", request.idStatus),
                 new SqlParameter("@IdCategory", request.idCategory),
                 new SqlParameter("@IdRack", request.idRack)
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

        [HttpPut]
        public IActionResult ActualizarProducto(int id, [FromBody] ProductoRequest request)
        {
            try
            {
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_ActualizarProducto", CommandType.StoredProcedure,
                 new SqlParameter("@id", id),
                 new SqlParameter("@nombre", request.nombre),
                 new SqlParameter("@Pventa", request.pVenta),
                 new SqlParameter("@Pcompra", request.pCompra),
                 new SqlParameter("@laboratorio", request.laboratorio),
                 new SqlParameter("@FechaVencimiento", request.fechaVencimiento),
                 new SqlParameter("@marca", request.marca),
                 new SqlParameter("@IdStatus", request.idStatus),
                 new SqlParameter("@IdCategory", request.idCategory),
                 new SqlParameter("@IdRack", request.idRack)
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
        public IActionResult EliminarProducto(int id, [FromBody] ProductoRequest request)
        {
            try
            {
                var usu = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_EliminarProducto", CommandType.StoredProcedure,
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
