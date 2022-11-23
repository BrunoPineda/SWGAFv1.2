using BackendSWGAF.Context;
using BackendSWGAF.Models.DTOs;
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
    public class KardexController : ControllerBase
    {
        private readonly AppDbContext context;
        public KardexController(AppDbContext context)
        {
            this.context = context;
        }
        //linea para mostrar los mensajes del sql procedimientos almacenados
        public static string InfoMessageHandler(object mySender, SqlInfoMessageEventArgs myEvent)
        {
            return (myEvent.Message);
        }
        [HttpGet]
        public IActionResult listarKardex()
        {
            List<Kardex> Kardex = context.kardex.ToList();

            return Ok(new
            {
                Res = true,
                StatusCode = 200,
                Message = "",
                Data = Kardex
            }); ; ;
        }
        [HttpPost]
        public IActionResult crearKardex([FromBody] KardexRequest request)
        {
            try
            {
                Kardex cat = new Kardex()
                {
                    descripcion = request.descripcion,
                    catidad = request.catidad,
                    tipoDePago = request.tipoDePago,
                    totaPrecio = request.totaPrecio,    
                    fecha = request.fecha,
                };
                context.kardex.Add(cat);
                context.SaveChanges();
                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Kardex registrado correctamente",
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
