using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracheque.Models
{
    public class Extrato
    {
        public string MesReferencia { get; set; }
        public string SalarioBruto { get; set; }
        public string SalarioLiquido { get; set; }
        public string TotalDescontos { get; set; }
        public List<Lancamento> Lancamentos { get; set; } = new List<Lancamento>();

        public Extrato(string mesReferencia, string salarioBruto, string salarioLiquido, string totalDescontos, List<Lancamento> lancamentos)
        {
            MesReferencia = mesReferencia;
            SalarioBruto = salarioBruto;
            SalarioLiquido = salarioLiquido;
            TotalDescontos = totalDescontos;
            Lancamentos = lancamentos;
        }
    }
}
