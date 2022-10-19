using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmos_de_cifrado
{
    class memoria
    {
        List<Proceso> _lstProcesos = new List<Proceso>();

        public List<Proceso> Procesos
        {
            get { return _lstProcesos; }
            set { _lstProcesos = value; }
        }
    }
}
