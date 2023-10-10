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
        public int FuncionarioId { get; set; } // Propriedade para o ID do funcionário
        public Funcionario Funcionario { get; set; } // Propriedade de navegação para o funcionário

        public decimal SalarioBruto { get; set; } // Salário bruto da folha de pagamento
        public decimal ImpostoIRRF { get; set; } // Imposto de Renda Retido na Fonte
        public decimal ImpostoINSS { get; set; } // Imposto sobre a Previdência Social (INSS)
        public decimal ImpostoFGTS { get; set; } // Fundo de Garantia por Tempo de Serviço (FGTS)
        public decimal SalarioLiquido { get; set; } // Salário líquido após deduções

        // Outras propriedades da folha de pagamento, se necessário
    }


}
