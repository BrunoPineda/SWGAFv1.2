using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendSWGAF.Context;
using BackendSWGAF.Helpers;
using BackendSWGAF.Models;
using BackendSWGAF.Models.DTOs.Auth;
using BackendSWGAF.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSWGAF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext context;
        public CategoryController(AppDbContext context)
        {
            this.context = context;
        }
        //linea para mostrar los mensajes del sql procedimientos almacenados
        public static string InfoMessageHandler(object mySender, SqlInfoMessageEventArgs myEvent)
        {
            return (myEvent.Message);
        }
        [HttpGet]
        public IActionResult listarCategoria()
        {
            List<Category> categoria = context.category.ToList();

            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = categoria
            }); ; ;
        }
        [HttpPost]
        public IActionResult crearCategoria([FromForm] CategoryRequest request)
        {
            try
            {
                Category cat = new Category()
                {
                    valor = request.valor,
                    descripcion = request.descripcion,
                    estado = request.estado
                };
                context.category.Add(cat);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Categoria registrado correctamente",
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

        public IActionResult ActualizarCategoria(int id, CategoryRequest request)
        {
            try
            {
                Category cat = context.category.FirstOrDefault(e => e.id == id);
                if (cat == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "La categoria no existe",
                        Data = ""
                    });
                }
                cat.valor = request.valor;
                cat.descripcion = request.descripcion;
                cat.estado = request.estado;

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
                Message = "Categoria actualizada",
                Data = ""
            }); ;

        }
        [HttpDelete("{id}")]
        //[Route("especialidad")]
        //[AllowAnonymous]
        //[Authorize]
        public IActionResult EliminarCategoria(int id)
        {
            try
            {
                Category ope = context.category.FirstOrDefault(e => e.id == id);
                if (ope == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "La Categoria no existe",
                        Data = ""
                    });
                }
                context.category.Remove(ope);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Categoria eliminada correctamente",
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
