using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FolhaPag.Models;
using FolhaPag.Data;

namespace FolhaPag.Controllers
{
    [ApiController]
    [Route("api/folha")]
    public class FolhaController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public FolhaController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        // GET: api/folha/listar
        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Folha>>> ListarFolhas()
        {
            var folhas = await _ctx.Folhas.ToListAsync();
            return Ok(folhas);
        }

        // GET: api/folha/{FolhaId}
        [HttpGet("{FolhaId}")]
        public async Task<ActionResult<Folha>> ObterFolha(int FolhaId)
        {
            var folha = await _ctx.Folhas.FindAsync(FolhaId);

            if (folha == null)
            {
                return NotFound();
            }

            return Ok(folha);
        }

        // POST: api/folha/cadastrar
        [HttpPost("cadastrar")]
        public async Task<ActionResult<Folha>> CadastrarFolha([FromBody] FolhaInputModel folhaInput)
        {
            // Verifica se o funcionário com o ID fornecido existe
            var funcionario = await _ctx.Funcionarios.FindAsync(folhaInput.FuncionarioId);

            if (funcionario == null)
            {
                return NotFound("Funcionário não encontrado");
            }

            // Cria uma nova instância de Folha com os dados fornecidos
            var novaFolha = new Folha
            {
                Valor = folhaInput.Valor,
                Quantidade = folhaInput.Quantidade,
                Mes = folhaInput.Mes,
                Ano = folhaInput.Ano,
                funcionario = funcionario
            };

            _ctx.Folhas.Add(novaFolha);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterFolha), new { FolhaId = novaFolha.FolhaId }, novaFolha);
        }
    }
}