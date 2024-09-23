using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Users.Records
{
    public record UserResponseDto(int Id,
                                    string Nome,
                                    string Sobrenome,
                                    string Documento,
                                    string Setor,
                                    string SalarioBruto,
                                    string DataAdmissao,
                                    bool DescontoPlanoSaude,
                                    bool DescontoPlanoDental,
                                    bool DescontoValeTransporte);
}
