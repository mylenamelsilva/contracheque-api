using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Funcionario.Records
{
    public record FuncionarioInformacaoExtratoDto(decimal SalarioBruto,
                                                bool DescontoPlanoSaude,
                                                bool DescontoPlanoDental,
                                                bool DescontoValeTransporte);
}
