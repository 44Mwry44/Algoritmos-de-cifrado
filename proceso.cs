using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmos_de_cifrado
{
    class Proceso
    {
        string _strMetodo = "";
        List<int> _lstLista = new List<int>();
        string _strNombre = "";
        bool _boolEsMensajeLlave = false;

        public Proceso()
        {

        }

        public Proceso(string strMetodo, string strNombre, List<int> lstLista, bool boolEsMensajeLlave)
        {
            _strMetodo = strMetodo;
            _strNombre = strNombre;
            _lstLista = lstLista;
            _boolEsMensajeLlave = boolEsMensajeLlave;
        }
        
        public string Metodo
        {
            get { return _strMetodo; }
            set { _strMetodo = value; }
        }

        public string Nombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }

        public List<int> Lista
        {
            get { return _lstLista; }
            set { _lstLista = value; }
        }

        public override string ToString()
        {
            string strResultado = "";

            if(_boolEsMensajeLlave)
            {
                foreach (int intValor in _lstLista)
                {
                    if(_strMetodo == "Alberti")
                    {
                        strResultado += (char)(intValor) + "   ";
                        continue;
                    }

                    strResultado += (char)(intValor + 65) + "  ";
                }
                strResultado += "\n";
            }

            foreach (int intValor in _lstLista)
            {
                if (_strMetodo == "Alberti")
                {

                    if (intValor.ToString().Length == 2)
                    {
                        strResultado += intValor.ToString() + "  ";
                        continue;
                    }

                    if (intValor.ToString().Length == 3)
                    {
                        strResultado += intValor.ToString() + " ";
                        continue;
                    }

                    strResultado += intValor.ToString() + "   ";
                    continue;
                }

                if (intValor.ToString().Length == 1)
                {
                    strResultado += intValor.ToString() + " ";
                    continue;
                }

                strResultado += intValor.ToString() + "  ";
                
            }

            return Nombre + "\n" + strResultado;
        }
    }
}
