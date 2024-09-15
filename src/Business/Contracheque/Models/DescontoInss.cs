using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracheque.Models
{
    public class DescontoInss
    {
        private const double PrimeiraFaixaSalarial = 0.075;
        private const double SegundaFaixaSalarial = 0.09;
        private const double TerceiraFaixaSalarial = 0.12;
        private const double QuartaFaixaSalarial = 0.14;

        public static decimal CalcularDescontoInss(decimal salarioBruto)
        {
            switch (salarioBruto)
            {
                case decimal s when s <= 1412m:
                    return DescontoPrimeiraFaixaSalarial(salarioBruto);
                case decimal s when s > 1412m && s <= 2666.68m:
                    return DescontoSegundaFaixaSalarial(salarioBruto);
                case decimal s when s > 2666.68m && s <= 4000.03m:
                    return DescontoTerceiraFaixaSalarial(salarioBruto);
                case decimal s when s > 4000.03m && s <= 77786.02m:
                    return DescontoQuartaFaixaSalarial(salarioBruto);
                default:
                    return 0.0m;
            }
        }

        private static decimal DescontoPrimeiraFaixaSalarial(decimal salarioBruto)
        {
            return salarioBruto * (decimal)PrimeiraFaixaSalarial;
        }

        private static decimal DescontoSegundaFaixaSalarial(decimal salarioBruto)
        {
            const decimal valorDeducao = 21.18m;
            return (salarioBruto * (decimal)SegundaFaixaSalarial) - valorDeducao;
        }

        private static decimal DescontoTerceiraFaixaSalarial(decimal salarioBruto)
        {
            const decimal valorDeducao = 101.18m;
            return (salarioBruto * (decimal)TerceiraFaixaSalarial) - valorDeducao;
        }

        private static decimal DescontoQuartaFaixaSalarial(decimal salarioBruto)
        {
            const decimal valorDeducao = 181.18m;
            return (salarioBruto * (decimal)QuartaFaixaSalarial) - valorDeducao;
        }
    }
}
