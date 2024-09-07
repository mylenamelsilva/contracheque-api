
namespace Business.Users.Records;

public record AtualizarInformacoesRequestDto(string Nome,
                                string Sobrenome,
                                string Setor,
                                decimal SalarioBruto,
                                bool DescontoPlanoSaude,
                                bool DescontoPlanoDental,
                                bool DescontoValeTransporte);