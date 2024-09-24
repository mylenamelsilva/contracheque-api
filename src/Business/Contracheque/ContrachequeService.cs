using Business.Base;
using Business.Contracheque.Enums;
using Business.Contracheque.Interfaces;
using Business.Contracheque.Models;
using Business.Contracheque.Records;
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
        private readonly DescontoPlanosService _planosService;

        public ContrachequeService(DescontoInssService inssService, DescontoPlanosService planosService, DescontoIrpfService impostoService, IFuncionarioService funcionarioService)
        {
            _inssService = inssService;
            _impostoService = impostoService;
            _funcionarioService = funcionarioService;
            _planosService = planosService;
        }

        public Resultado ExtratoMensal(int id)
        {
            var funcionario = _funcionarioService.RetornarInformacoesExtrato(id);
           
            if (funcionario == null)
            {
                return Resultado.Falha("Funcionário com esse ID não encontrado", id);
            }
            var mesAtual = DateTime.Today.ToString("MMMM", new CultureInfo("pt-BR"));
            var salarioBruto = funcionario.SalarioBruto;

            var descontoInss = _inssService.CalcularDescontoInss(salarioBruto);
            var descontoImposto = _impostoService.CalcularDescontoIrpf(salarioBruto);
            decimal? descontoPlanoSaude = funcionario.DescontoPlanoSaude ? _planosService.CalcularDescontoPlanoSaude(salarioBruto) : null;
            decimal? descontoPlanoDental = funcionario.DescontoPlanoDental ? _planosService.CalcularDescontoPlanoDental(salarioBruto) : null;
            decimal? descontoValeTransporte = funcionario.DescontoValeTransporte ? _planosService.CalcularDescontoValeTransporte(salarioBruto) : null;

            var totalDescontos = descontoInss + descontoImposto;

            if (descontoPlanoDental != null)
            {
                totalDescontos += (decimal)descontoPlanoDental;
            }


            if (descontoPlanoSaude != null)
            {
                totalDescontos += (decimal)descontoPlanoSaude;
            }


            if (descontoValeTransporte != null)
            {
                totalDescontos += (decimal)descontoValeTransporte;
            }

            var salarioLiquido = salarioBruto - totalDescontos;

            var lancamentos = OrganizarLancamentos(new ListagemLancamentosDto(descontoInss, descontoImposto, descontoPlanoDental, descontoPlanoSaude, descontoValeTransporte, salarioBruto));

            var extrato = new Extrato(mesAtual, 
                                      salarioBruto.ToString("C2", new CultureInfo("pt-BR")),
                                      salarioLiquido.ToString("C2", new CultureInfo("pt-BR")),
                                      totalDescontos.ToString("C2", new CultureInfo("pt-BR")),
                                      lancamentos);

            return Resultado.Sucesso("Extrato do mês atual", extrato);
        }

        private List<Lancamento> OrganizarLancamentos(ListagemLancamentosDto lista)
        {
            var salarioBruto = lista.salarioBruto;
            var descontoInss = lista.valorInss;

            var listagem = new List<Lancamento>()
            {
                 new Lancamento(TipoLancamentoEnum.Remuneracao, Lancamento.DescricaoSalarioBruto, salarioBruto.ToString("C2", new CultureInfo("pt-BR"))),
                 new Lancamento(TipoLancamentoEnum.Desconto, Lancamento.DescricaoInss, descontoInss.ToString("C2", new CultureInfo("pt-BR"))),
            };

            if (lista.valorSaude.HasValue)
            {
                var descontoSaude = lista.valorSaude.Value;
                listagem.Add(new Lancamento(TipoLancamentoEnum.Desconto, Lancamento.DescricaoPlanoSaude, descontoSaude.ToString("C2", new CultureInfo("pt-BR"))));
            }

            if (lista.valorIrpf.HasValue && lista.valorIrpf.Value != 0.0m)
            {
                var descontoImposto = lista.valorIrpf.Value;
                listagem.Add(new Lancamento(TipoLancamentoEnum.Desconto, Lancamento.DescricaoImposto, descontoImposto.ToString("C2", new CultureInfo("pt-BR"))));
            }

            if (lista.valorDental.HasValue)
            {
                var descontoDental = lista.valorDental.Value;
                listagem.Add(new Lancamento(TipoLancamentoEnum.Desconto, Lancamento.DescricaoPlanoDental, descontoDental.ToString("C2", new CultureInfo("pt-BR"))));
            }

            if (lista.valorTransporte.HasValue && lista.valorTransporte.Value != 0.0m)
            {
                var descontoTransporte = lista.valorTransporte.Value;
                listagem.Add(new Lancamento(TipoLancamentoEnum.Desconto, Lancamento.DescricaoValeTransporte, descontoTransporte.ToString("C2", new CultureInfo("pt-BR"))));
            }

            return listagem;
        }
    }
}
