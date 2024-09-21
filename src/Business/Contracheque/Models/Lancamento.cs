using Business.Contracheque.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracheque.Models
{
    public class Lancamento
    {
        public static readonly string DescricaoInss = "INSS mês";
        public static readonly string DescricaoImposto = "IRRF mês";
        public static readonly string DescricaoValeTransporte = "Vale Transporte mês";
        public static readonly string DescricaoPlanoDental = "Plano Dental mês";
        public static readonly string DescricaoPlanoSaude = "Plano de Saúde mês";

        public TipoLancamentoEnum Tipo { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }

        public Lancamento(TipoLancamentoEnum tipo, string descricao, string valor)
        {
            Tipo = tipo;
            Descricao = descricao;
            Valor = valor;
        }
    }
}
