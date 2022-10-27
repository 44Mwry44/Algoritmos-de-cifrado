using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmos_de_cifrado
{
    class Regla
    {
        bool _boolNumerosNaturales;
        bool _boolNumerosPrimos;
        bool _boolNumerosPares;
        bool _boolNumerosImpares;
        bool _boolMultiplo;
        int _intMultiploDe;

        public bool NumerosNaturales
        {
            get { return _boolNumerosNaturales; }
            set { _boolNumerosNaturales = value; }
        }

        public bool NumerosPrimos
        {
            get { return _boolNumerosPrimos; }
            set { _boolNumerosPrimos = value; }
        }

        public bool NumerosPares
        {
            get { return _boolNumerosPares; }
            set { _boolNumerosPares = value; }
        }

        public bool NumerosImpares
        {
            get { return _boolNumerosImpares; }
            set { _boolNumerosImpares = value; }
        }

        public bool Multiplo
        {
            get { return _boolMultiplo; }
            set { _boolMultiplo = value; }
        }

        public int MultiploDe
        {
            get { return _intMultiploDe; }
            set { _intMultiploDe = value; }
        }
    }
}
