using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmos_de_cifrado
{
    abstract class Algoritmos
    {
        //direccion = false | izquierda
        //direccion = true  | derecha
        public static string Alberti(List<char> discoExterno, List<char> discoInterno, string mensaje, string llave, int caracteresPorRotacion, int rotacion, bool direccion, bool cifrar)
        {
            int aux = 1;
            string criptograma = "";

            if(discoExterno.Count != discoInterno.Count)
            {
                throw new Exception ("Los discos son de diferente longitudes");
            }

            if(llave.Length > 2)
            {
                throw new Exception("La llave no esta en un formato valido"); 
            }

            if (mensaje.Length == 0)
            {
                throw new Exception("El mensaje no puede estar vacio");
            }

            if (llave.Length == 0)
            {
                throw new Exception("La llave no puede estar vacia");
            }

            Alberti_AjustarMatriz(discoExterno, discoInterno, llave);

            if(cifrar)
            {
                mensaje = mensaje.ToUpper();
            }
            else
            {
                mensaje = mensaje.ToLower();
            }

            llave = llave.ToUpper();

            for (int posicion = 0; posicion < mensaje.Length; posicion++)
            {
                if (aux > caracteresPorRotacion && caracteresPorRotacion != 0)
                {
                    Alberti_AjustarMatriz(discoExterno, discoInterno, rotacion, direccion);
                    aux = 1;
                }

                if (cifrar)
                {
                    int charIndex = discoExterno.FindIndex(item => mensaje[posicion] == item);
                    criptograma += discoInterno.ElementAt(charIndex);
                }
                else
                {
                    int charIndex = discoInterno.FindIndex(item => mensaje[posicion] == item);
                    criptograma += discoExterno.ElementAt(charIndex);
                }

                aux++;
            }

            return criptograma;
        }

        //Ajusta la matriz segun la llave -- Se usa al inicio como configuracion inicial antes de comenzar cualquier proceso
        static void Alberti_AjustarMatriz(List<char> discoExterno, List<char> discoInterno, string llave)
        {
            int posicionLlave1 = discoExterno.FindIndex(item => llave[0] == item);
            //Console.WriteLine("Posicion 1: " + posicionLlave1.ToString());

            int posicionLlave2 = discoInterno.FindIndex(item => llave[1] == item);
            //Console.WriteLine("Posicion 2: " + posicionLlave2.ToString());

            char aux = ' ';

            while (posicionLlave1 != posicionLlave2)
            {
                for (int posicion = 35; posicion > 0; posicion--)
                {
                    if (posicion == 35)
                    {
                        aux = discoInterno[posicion];
                    }

                    discoInterno[posicion] = discoInterno[posicion - 1];
                }
                discoInterno[0] = aux;
                posicionLlave1 = discoExterno.FindIndex(item => llave[0] == item);
                posicionLlave2 = discoInterno.FindIndex(item => llave[1] == item);
            }
        }

        //Ajusta la matriz segun la rotación -- Si se pide, se rota el disco cada x caracter
        static void Alberti_AjustarMatriz(List<char> discoExterno, List<char> discoInterno, int ajuste, bool direccion)
        {
            char aux = ' ';
            if (direccion)
            {
                for (int x = 0; x < ajuste; x++)
                {
                    for (int posicion = 35; posicion > 0; posicion--)
                    {
                        if (posicion == 35)
                        {
                            aux = discoInterno[posicion];
                        }

                        discoInterno[posicion] = discoInterno[posicion - 1];
                    }
                    discoInterno[0] = aux;
                }
            }
            else
            {
                for (int x = 0; x < ajuste; x++)
                {
                    for (int posicion = 0; posicion < 35; posicion++)
                    {
                        if (posicion == 0)
                        {
                            aux = discoInterno[posicion];
                        }

                        discoInterno[posicion] = discoInterno[posicion + 1];
                    }
                    discoInterno[35] = aux;
                }
            }
        }
    }
}
