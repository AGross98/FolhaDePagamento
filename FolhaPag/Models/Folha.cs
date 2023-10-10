using System;

namespace FolhaPag.Models
{
public class Folha
{
    public int FolhaId { get; set; }
    public decimal Valor { get; set; }
    public int Quantidade { get; set; }
    public int Mes { get; set; }
    public int Ano { get; set; }
    public int FuncionarioId { get; set; }
    public Funcionario Funcionario { get; set; }
    
    // Adicione as propriedades calculadas
    public decimal SalarioBruto { get; set; }
    public decimal ImpostoIRRF { get; set; }
    public decimal ImpostoINSS { get; set; }
    public decimal ImpostoFGTS { get; set; }
    public decimal SalarioLiquido { get; set; }
}
}