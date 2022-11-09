using BackendSWGAF.Context;
using BackendSWGAF.Helpers;
using BackendSWGAF.Models.DTOs;
using BackendSWGAF.Models.DTOs.Auth;
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
    public class RackController : ControllerBase
    {
        private readonly AppDbContext context;
        public RackController(AppDbContext context)
        {
            this.context = context;
        }
        //linea para mostrar los mensajes del sql procedimientos almacenados
        public static string InfoMessageHandler(object mySender, SqlInfoMessageEventArgs myEvent)
        {
            return (myEvent.Message);

        }
        [HttpGet]
        public IActionResult ListarRack()
        {
            List<Rack> racks = context.rack.ToList();
            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = racks
            });
        }
        [HttpPost]
        public IActionResult crearRack([FromBody] RackRequest request)
        {
            try
            {
                Rack rac = new Rack()
                {
                    valor = request.valor,
                    descripcion = request.descripcion,

                };
                context.rack.Add(rac);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,//esta bien
                    Message = "Rack registrado correctamente",
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

        [HttpPut("{id}")]
        public IActionResult ActualizarRack(int id, RackRequest request)
        {
            try
            {
                Rack rac = context.rack.FirstOrDefault(e => e.id == id);
                if (rac == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "EL rack no existe",
                        Data = ""
                    });
                }
                rac.valor = request.valor;
                rac.descripcion = request.descripcion;


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
                Message = "Rack actualizada",
                Data = ""
            }); ;
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarCategoria(int id)
        {
            try
            {
                Rack rac = context.rack.FirstOrDefault(e => e.id == id);
                if (rac == null)
                {
                    return NotFound(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El rack no existe",
                        Data = ""
                    });
                }
                context.rack.Remove(rac);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Rack eliminado correctamente",
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