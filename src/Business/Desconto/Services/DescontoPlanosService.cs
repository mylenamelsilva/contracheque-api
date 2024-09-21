using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Desconto.Services
{
    public class DescontoPlanosService
    {
        public static readonly decimal DescontoPlanoSaude = 10.0m;
        public static readonly decimal DescontoPlanoDental = 5.0m;
        private static readonly double DescontoValeTransportePorcentagem = 0.06;

        public decimal CalcularDescontoValeTransporte(decimal salarioBruto)
        {
            return salarioBruto * (decimal)DescontoValeTransportePorcentagem;
        }
    }
}
