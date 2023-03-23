using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmos_de_cifrado
{
    internal class configuracion
    {
        string _strMensaje = "luis";//"ALZIARINMOEOGNASTLMSEOLUVAJTECEOSIKILTROPRSUD";//"laverdadquesediga";.////"CPYOIMEQARPRSLQF";
        string _strLlave = "mauricio";//"algoritmoclasico";
        bool _boolCifrar = true;
        bool _boolMostrarProceso = false;
        bool _boolMemoria = true;

        int _intCaracteresPorRotacion = 5;
        int _intRotacion = 5;
        bool _boolDireccion = true;

        List<char> _lstDiscoExterno = new List<char> { 'A', 'O', 'D', 'X', '1', 'C', 'G', 'K', 'B', '8', 'M', '4', 'R', 'F', 'U', 'I', 'Y', '2', 'L', '6', 'Ñ', 'S', 'H', 'Q', '5', 'W', '7', 'E', 'V', '9', 'N', 'J', 'P', 'Z', 'T', '3' };
        List<char> _lstDiscoInterno = new List<char> { 'a', '!', 'x', '0', 'j', 't', '@', '$', 'g', 'z', 'w', '&', 'e', 'o', 'p', 'h', '%', 'ñ', 'd', 'r', '=', 'c', 's', '?', 'm', '#', 'n', 'f', 'k', 'u', 'l', 'i', 'b', 'y', 'q', 'v' };

        List<Regla> _lstReglas = new List<Regla>();

        bool _iterarHastaDesencriptar = true;

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

        public bool IterarHastaDesencriptar
        {
            get { return _iterarHastaDesencriptar; }
            set { _iterarHastaDesencriptar = value; }
        }

        public List<Regla> LstReglas
        {
            get { return _lstReglas; }
            set { _lstReglas = value; }
        }

        public configuracion()
        {
            //Regla regla1 = new Regla();
            //regla1.Multiplo = true;
            //regla1.MultiploDe = 4;

            //Regla regla2 = new Regla();
            //regla2.Multiplo = true;
            //regla2.MultiploDe = 3;

            //Regla regla3 = new Regla();
            //regla3.NumerosImpares = true;

            //Regla regla4 = new Regla();
            //regla4.NumerosPares = true;

            //LstReglas.Add(regla1);
            //LstReglas.Add(regla2);
            //LstReglas.Add(regla3);
            //LstReglas.Add(regla4);

            Regla regla1 = new Regla();
            regla1.NumerosPrimos = true;

            //Regla regla2 = new Regla();
            //regla2.Multiplo = true;
            //regla2.MultiploDe = 5;

            //Regla regla3 = new Regla();
            //regla3.NumerosNaturales = true;

            Regla regla2 = new Regla();
            regla2.NumerosPares = true;

            Regla regla3 = new Regla();
            regla3.NumerosImpares = true;

            LstReglas.Add(regla1);
            LstReglas.Add(regla2);
            LstReglas.Add(regla3);
        }
    }
}
