using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracheque.Records
{
    public record ListagemLancamentosDto(decimal valorInss,
                                         decimal? valorIrpf,
                                         decimal? valorDental,
                                         decimal? valorSaude,
                                         decimal? valorTransporte,
                                         decimal salarioBruto);
}
