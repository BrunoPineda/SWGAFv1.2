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
            var soli = context.producto.Include(p => p.rack).Include(p => p.productoStatus).Include(p => p.categoria).Select(p => new {
                id = p.id,
                codigo = p.codigo,
                descripcion = p.descripcion,
                pVenta = p.pVenta,
                pCompra = p.pCompra
                ,
                laboratorio = p.laboratorio,
                fechaVencimiento = p.fechaVencimiento,
                marca = p.marca,
                rack = p.rack.valor,
                estado = p.productoStatus.descripcion
            }).ToList();

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
                    codigo = p.codigo,
                    descripcion = p.descripcion,
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
                var existsStatusId = context.productostatus.Where(p => p.id == request.idStatus).FirstOrDefault();
                if (existsStatusId == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El estado id de producto no existe",
                        Data = ""
                    }); ;
                }
                var existsIdCategory = context.category.Where(p => p.id == request.idCategory).FirstOrDefault();
                if (existsIdCategory == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El estado de la categoria no existe",
                        Data = ""
                    }); ;
                }
                var existsIdrack = context.rack.Where(p => p.id == request.idRack).FirstOrDefault();
                if (existsIdrack == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El estado del rack no existe",
                        Data = ""
                    }); ;
                }
                var log = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_insertarProducto", CommandType.StoredProcedure,
              new SqlParameter("@descripcion", request.descripcion),
               new SqlParameter("@pVenta", request.pVenta),
               new SqlParameter("@pCompra", request.pCompra),
               new SqlParameter("@laboratorio", request.laboratorio),
               new SqlParameter("@fechaVencimiento", request.fechaVencimiento),
               new SqlParameter("@marca", request.marca),
                new SqlParameter("@stock", request.stock),
                 new SqlParameter("@tipoProducto", request.tipoProducto),
                 new SqlParameter("@idStatus", request.idStatus),
                 new SqlParameter("@idCategory", request.idCategory),
                 new SqlParameter("@idRack", request.idRack)
               );
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Se insert correctamente el producto",
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

        [HttpPut]
        public IActionResult ActualizarProducto(int id, [FromBody] ProductoRequest request)
        {
            try
            {
                Producto pro = context.producto.FirstOrDefault(e => e.id == id);
                if (pro == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El producto no existe",
                        Data = ""
                    });
                }
                pro.descripcion = request.descripcion;
                pro.pVenta = request.pVenta;
                pro.pCompra = request.pCompra;
                pro.laboratorio = request.laboratorio;
                pro.fechaVencimiento = request.fechaVencimiento;
                pro.marca = request.marca;
                pro.stock = request.stock;
                pro.tipoProducto = request.tipoProducto;
                pro.idStatus = 1;
                pro.idCategory = request.idCategory;
                pro.idRack = request.idRack;

                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Producto actualizado",
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
        public IActionResult EliminarProducto(int id, [FromBody] ProductoRequest request)
        {
            try
            {
                Producto pro = context.producto.FirstOrDefault(e => e.id == id);
                if (pro == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El producto no existe",
                        Data = ""
                    });
                }

                pro.idStatus = 2;


                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Producto eliminado" ,
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
   

        [HttpPut("comprarProducto/{id}")]
        public IActionResult ComprarProducto(int id, [FromBody] CproductoRequest request)
        {
            try
            {
                var existsStatusId = context.producto.Where(p => p.id == id).FirstOrDefault();
                if (existsStatusId == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El id de producto no existe",
                        Data = ""
                    }); ;
                }
                var log = SqlHelper.ExecuteNonQueryShowMessage(context, "sp_comprarProducto", CommandType.StoredProcedure,
              new SqlParameter("@id", id),
              new SqlParameter("@cantidad", request.cantidad)
              );

               return Ok(new
               {
                   Res = true,
                   StatusCode = 200,
                   Message = log,
                   Data = ""
               });
            }
            catch(Exception e)
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
