using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Desconto
{
    public abstract class DescontoBase
    {
        protected abstract double PrimeiraFaixaSalarial { get; }
        protected abstract double SegundaFaixaSalarial { get; }
        protected abstract double TerceiraFaixaSalarial { get; }
        protected abstract double QuartaFaixaSalarial { get; }

        protected abstract decimal DescontoPrimeiraFaixaSalarial(decimal salarioBruto);

        protected abstract decimal DescontoSegundaFaixaSalarial(decimal salarioBruto);

        protected abstract decimal DescontoTerceiraFaixaSalarial(decimal salarioBruto);

        protected abstract decimal DescontoQuartaFaixaSalarial(decimal salarioBruto);
    }
}
