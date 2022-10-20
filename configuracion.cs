using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmos_de_cifrado
{
    internal class configuracion
    {
        string _strMensaje = "vernam";//"laverdadquesediga";.////"CPYOIMEQARPRSLQF";
        string _strLlave = "playas";//"algoritmoclasico";
        bool _boolCifrar = true;
        bool _boolMostrarProceso = false;
        bool _boolMemoria = true;

        int _intCaracteresPorRotacion = 5;
        int _intRotacion = 5;
        bool _boolDireccion = true;

        List<char> _lstDiscoExterno = new List<char> { 'A', 'O', 'D', 'X', '1', 'C', 'G', 'K', 'B', '8', 'M', '4', 'R', 'F', 'U', 'I', 'Y', '2', 'L', '6', 'Ñ', 'S', 'H', 'Q', '5', 'W', '7', 'E', 'V', '9', 'N', 'J', 'P', 'Z', 'T', '3' };
        List<char> _lstDiscoInterno = new List<char> { 'a', '!', 'x', '0', 'j', 't', '@', '$', 'g', 'z', 'w', '&', 'e', 'o', 'p', 'h', '%', 'ñ', 'd', 'r', '=', 'c', 's', '?', 'm', '#', 'n', 'f', 'k', 'u', 'l', 'i', 'b', 'y', 'q', 'v' };

        public string Mensaje
        {
            get { return _strMensaje; }
            set { _strMensaje = value; }
        }

        public string Llave 
        {
            get { return _strLlave; }
            set { _strLlave = value; }
        }

        public int CaracteresPorRotacion
        {
            get { return _intCaracteresPorRotacion; }
            set { _intCaracteresPorRotacion = value; }
        }

        public int Rotacion
        {
            get { return _intRotacion; }
            set { _intRotacion = value; }
        }

        public bool Direccion
        {
            get { return _boolDireccion; }
            set { _boolDireccion = value; }
        }

        public List<char> DiscoExterno
        {
            get { return _lstDiscoExterno; }
            set { _lstDiscoExterno = value; }
        }

        public List<char> DiscoInterno
        {
            get { return _lstDiscoInterno; }
            set { _lstDiscoInterno = value; }
        }

        public bool Cifrar
        {
            get { return _boolCifrar; }
            set { _boolCifrar = value; }
        }

        public bool MostrarProceso
        {
            get { return _boolMostrarProceso; }
            set { _boolMostrarProceso = value; }
        }

        public bool Memoria
        {
            get { return _boolMemoria; }
            set { _boolMemoria = value; }
        }
    }
}
