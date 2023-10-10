using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

// POST: api/folha/cadastrar
[HttpPost("cadastrar")]
public IActionResult CadastrarFolha([FromBody] Folha folhaInput)
{
    // Verifique se o funcionário com o ID especificado existe no banco de dados
    var funcionario = _ctx.Funcionarios.SingleOrDefault(f => f.FuncionarioId == folhaInput.FuncionarioId);

    if (funcionario == null)
    {
        return NotFound("Funcionário não encontrado");
    }

    // Calcule o salário bruto
    decimal salarioBruto = folhaInput.Valor * folhaInput.Quantidade;

    // Calcule o imposto de renda (impostoIrrf) de acordo com a tabela
    decimal impostoIrrf = CalcularImpostoIRRF(salarioBruto);

    // Calcule o imposto INSS (impostoInss) de acordo com a tabela
    decimal impostoInss = CalcularImpostoINSS(salarioBruto);

    // Calcule o FGTS (impostoFgts)
    decimal impostoFgts = salarioBruto * 0.08m;

    // Calcule o salário líquido
    decimal salarioLiquido = salarioBruto - impostoIrrf - impostoInss;

    // Crie uma nova instância de Folha com os valores calculados
    var novaFolha = new Folha
    {
        Valor = folhaInput.Valor,
        Quantidade = folhaInput.Quantidade,
        Mes = folhaInput.Mes,
        Ano = folhaInput.Ano,
        FuncionarioId = folhaInput.FuncionarioId,
        SalarioBruto = salarioBruto,
        ImpostoIRRF = impostoIrrf,
        ImpostoINSS = impostoInss,
        ImpostoFGTS = impostoFgts,
        SalarioLiquido = salarioLiquido
    };

    // Adicione a nova folha de pagamento ao contexto do banco de dados e salve
    _ctx.Folhas.Add(novaFolha);
    _ctx.SaveChanges();

    // Crie um objeto anônimo com as informações desejadas para a resposta
    var resposta = new
    {
        valor = novaFolha.Valor,
        quantidade = novaFolha.Quantidade,
        mes = novaFolha.Mes,
        ano = novaFolha.Ano,
        funcionarioId = novaFolha.FuncionarioId
    };

    // Retorne um código de status 201 (Created) com as informações da nova folha
    return Created("", resposta);
}
// Função para calcular o imposto de renda (IRRF) com base na tabela
private decimal CalcularImpostoIRRF(decimal salarioBruto)
{
    decimal impostoIrrf = 0m;

    if (salarioBruto <= 1903.98m)
    {
        impostoIrrf = 0m;
    }
    else if (salarioBruto <= 2826.65m)
    {
        impostoIrrf = (salarioBruto * 0.075m) - 142.80m;
    }
    else if (salarioBruto <= 3751.05m)
    {
        impostoIrrf = (salarioBruto * 0.15m) - 354.80m;
    }
    else if (salarioBruto <= 4664.68m)
    {
        impostoIrrf = (salarioBruto * 0.225m) - 636.13m;
    }
    else
    {
        impostoIrrf = (salarioBruto * 0.275m) - 869.36m;
    }

    return impostoIrrf;
}

// Função para calcular o imposto INSS com base na tabela
private decimal CalcularImpostoINSS(decimal salarioBruto)
{
    decimal impostoInss = 0m;

    if (salarioBruto <= 1693.72m)
    {
        impostoInss = salarioBruto * 0.08m;
    }
    else if (salarioBruto <= 2822.90m)
    {
        impostoInss = salarioBruto * 0.09m;
    }
    else if (salarioBruto <= 5645.80m)
    {
        impostoInss = salarioBruto * 0.11m;
    }
    else
    {
        impostoInss = 621.03m; // Valor fixo
    }

    return impostoInss;
}
    }
}