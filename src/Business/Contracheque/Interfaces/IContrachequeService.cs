using Business.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracheque.Interfaces
{
    public interface IContrachequeService
    {
        public Resultado ExtratoMensal(int id);
    }
}
