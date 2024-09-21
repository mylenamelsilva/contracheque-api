using Business.Base;
using Business.Contracheque.Enums;
using Business.Contracheque.Interfaces;
using Business.Contracheque.Models;
using Business.Desconto.Services;
using Business.Users.Interfaces;
using Business.Users.Model;
using System.Globalization;


namespace Business.Contracheque
{
    public class ContrachequeService : IContrachequeService
    {
        private readonly DescontoInssService _inssService;
        private readonly DescontoIrpfService _impostoService;
        private readonly IFuncionarioService _funcionarioService;

        public ContrachequeService(DescontoInssService inssService, DescontoIrpfService impostoService, IFuncionarioService funcionarioService)
        {
            _inssService = inssService;
            _impostoService = impostoService;
            _funcionarioService = funcionarioService;
        }

        public Resultado ExtratoMensal(int id)
        {
            var funcionario = _funcionarioService.RetornarInformacoesExtrato(id);
           
            if (funcionario == null)
            {
                return Resultado.Falha("Funcionário com esse ID não encontrado", id);
            }

            var salarioBruto = funcionario.SalarioBruto;

            var descontoInss = _inssService.CalcularDescontoInss(salarioBruto);
            var descontoImposto = _impostoService.CalcularDescontoIrpf(salarioBruto);
            var totalDescontos = (descontoImposto + descontoInss);
            var salarioLiquido = salarioBruto - totalDescontos;
            var mesAtual = DateTime.Today.ToString("dd/MM/yyyy");

            var lancamentos = new List<Lancamento>()
            {
                new Lancamento(TipoLancamentoEnum.Remuneracao, "Salário bruto", salarioBruto.ToString("C2", new CultureInfo("pt-BR"))),
                new Lancamento(TipoLancamentoEnum.Desconto, Lancamento.DescricaoInss, descontoInss.ToString("C2", new CultureInfo("pt-BR"))),
                new Lancamento(TipoLancamentoEnum.Desconto, Lancamento.DescricaoImposto, descontoImposto.ToString("C2", new CultureInfo("pt-BR")))
            };

            var extrato = new Extrato(mesAtual, 
                                      salarioBruto.ToString("C2", new CultureInfo("pt-BR")),
                                      salarioLiquido.ToString("C2", new CultureInfo("pt-BR")),
                                      totalDescontos.ToString("C2", new CultureInfo("pt-BR")),
                                      lancamentos);

            return Resultado.Sucesso("Extrato do mês atual", extrato);
        }

    }
}
