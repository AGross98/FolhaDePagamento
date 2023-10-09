using FolhaPag.Data;
using FolhaPag.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FolhaPag.Controllers
{
    [ApiController]
    [Route("api/funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public FuncionarioController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        // GET: api/funcionario/listar
        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                List<Funcionario> funcionarios = _ctx.Funcionarios.ToList();
                return funcionarios.Count == 0 ? NotFound() : Ok(funcionarios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/funcionario/cadastrar
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Funcionario funcionario)
        {
            try
            {
                _ctx.Funcionarios.Add(funcionario);
                _ctx.SaveChanges();
                return Created("", funcionario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
