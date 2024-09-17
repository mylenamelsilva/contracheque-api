using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracheque.Models
{
    public class DescontoIrpf : DescontoBase
    {
        protected override double PrimeiraFaixaSalarial => 0.075;

        protected override double SegundaFaixaSalarial => 0.15;

        protected override double TerceiraFaixaSalarial => 0.225;

        protected override double QuartaFaixaSalarial => 0.275;

        public decimal CalcularDescontoIrpf(decimal salarioBruto)
        {
            switch (salarioBruto)
            {
                case decimal s when s <= 1903.98m:
                    return 0.0m;
                case decimal s when s > 1903.98m && s <= 2826.65m:
                    return DescontoPrimeiraFaixaSalarial(salarioBruto);
                case decimal s when s > 2826.65m && s <= 3751.05m:
                    return DescontoSegundaFaixaSalarial(salarioBruto);
                case decimal s when s > 3751.05m && s <= 4664.68m:
                    return DescontoTerceiraFaixaSalarial(salarioBruto);                
                case decimal s when s > 4664.68m:
                    return DescontoQuartaFaixaSalarial(salarioBruto);
                default:
                    return 0.0m;
            }
        }


        protected override decimal DescontoPrimeiraFaixaSalarial(decimal salarioBruto)
        {
            const decimal valorDeducao = 142.80m;
            return (salarioBruto * (decimal)PrimeiraFaixaSalarial) - valorDeducao;
        }

        protected override decimal DescontoQuartaFaixaSalarial(decimal salarioBruto)
        {
            const decimal valorDeducao = 869.36m;
            return (salarioBruto * (decimal)QuartaFaixaSalarial) - valorDeducao;
        }

        protected override decimal DescontoSegundaFaixaSalarial(decimal salarioBruto)
        {
            const decimal valorDeducao = 354.80m;
            return (salarioBruto * (decimal)SegundaFaixaSalarial) - valorDeducao;
        }

        protected override decimal DescontoTerceiraFaixaSalarial(decimal salarioBruto)
        {
            const decimal valorDeducao = 636.13m;
            return (salarioBruto * (decimal)TerceiraFaixaSalarial) - valorDeducao;
        }
    }
}
