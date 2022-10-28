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
        public static string Alberti(List<char> discoExterno, List<char> discoInterno, string mensaje, string llave, int caracteresPorRotacion, int rotacion, bool direccion, bool cifrar, memoria memoria = null)
        {
            int aux = 1;
            int numRotacion = 1;
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

            if (memoria != null)
            {
                List<int> tempRotacion = new List<int>();

                foreach (char caracter in discoInterno)
                {
                    tempRotacion.Add(caracter);
                }

                Proceso miProceso = new Proceso("Alberti", "Ajuste inicial", tempRotacion, true);

                memoria.Procesos.Add(miProceso);
            }

            if (cifrar)
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

                    if(memoria != null)
                    {
                        List<int> tempRotacion = new List<int>();
                        
                        foreach(char caracter in discoInterno)
                        {
                            tempRotacion.Add(caracter);
                        }

                        Proceso miProceso = new Proceso("Alberti", "Rotacion #" + numRotacion.ToString(), tempRotacion, true);
                        memoria.Procesos.Add(miProceso);
                        numRotacion++;
                    }

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

        public static string VigenereM1(string mensaje, string llave, bool cifrar, memoria memoria = null)
        {
            //Matriz en castellano
            //char[,] matriz = new char[,]
            //        { { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'},
            //          { 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A'},
            //          { 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B'},
            //          { 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C'},
            //          { 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D'},
            //          { 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E'},
            //          { 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F'},
            //          { 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G'},
            //          { 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'},
            //          { 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I'},
            //          { 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'},
            //          { 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K'},
            //          { 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L'},
            //          { 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M'},
            //          { 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N'},
            //          { 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ'},
            //          { 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O'},
            //          { 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P'},
            //          { 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q'},
            //          { 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R'},
            //          { 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S'},
            //          { 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T'},
            //          { 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U'},
            //          { 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V'},
            //          { 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W'},
            //          { 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X'},
            //          { 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y'} };

            //Alfabeto inglés
            char[,] matriz = new char[,]
                    { { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'},
                      { 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A'},
                      { 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B'},
                      { 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C'},
                      { 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D'},
                      { 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E'},
                      { 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F'},
                      { 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G'},
                      { 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'},
                      { 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I'},
                      { 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'},
                      { 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K'},
                      { 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L'},
                      { 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M'},
                      { 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N'},
                      { 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O'},
                      { 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P'},
                      { 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q'},
                      { 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R'},
                      { 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S'},
                      { 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T'},
                      { 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U'},
                      { 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V'},
                      { 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W'},
                      { 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X'},
                      { 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y'} };

            int posicionLlave = 0;

            string criptograma = "";

            mensaje = mensaje.ToUpper();
            llave = llave.ToUpper();

            List<int> tempCoordenadas;

            for (int x = 0; x < mensaje.Length; x++)
            {
                if(posicionLlave == llave.Length)
                {
                    posicionLlave = 0;
                }
                
                if(cifrar)
                {
                    criptograma += matriz[mensaje[x] - 65, llave[posicionLlave] - 65];

                    if (memoria != null)
                    {
                        tempCoordenadas = new List<int>();
                        tempCoordenadas.Add(mensaje[x] - 65);
                        tempCoordenadas.Add(llave[posicionLlave] - 65);
                        Proceso miProceso = new Proceso("Vigenere", "Coordenada # " + (x + 1).ToString(), tempCoordenadas, false);
                        memoria.Procesos.Add(miProceso);
                    }
                }
                else
                {
                    int row = 0;
                    for (row = 0; row < 26; row++)
                    {
                        if (matriz[row, llave[posicionLlave] - 65] == mensaje[x])
                        {
                            break;
                        }
                    }
                    criptograma += matriz[row, 0];

                    if (memoria != null)
                    {
                        tempCoordenadas = new List<int>();
                        tempCoordenadas.Add(row);
                        tempCoordenadas.Add(0);
                        Proceso miProceso = new Proceso("VigenereM1", "Coordenada # " + (x + 1).ToString(), tempCoordenadas, false);
                        memoria.Procesos.Add(miProceso);
                    }
                }

                posicionLlave++;
            }


            return criptograma;
        }

        public static string VigenereM2(string mensaje, string llave, bool cifrar, memoria memoria = null)
        {
            int posicionLlave = 0;
            string cifrado = "";

            List<int> tempMensaje = new List<int>();
            List<int> tempLlave = new List<int>();
            List<int> tempSuma = new List<int>();
            List<int> tempMod = new List<int>();

            mensaje = mensaje.ToUpper();
            llave = llave.ToUpper();

            

            for (int x = 0; x < mensaje.Length; x++)
            {
                int auxMensaje = 0;
                int auxLlave = 0;


                if (posicionLlave == llave.Length)
                {
                    posicionLlave = 0;
                }

                auxMensaje = mensaje[x] - 65;
                auxLlave = llave[posicionLlave] - 65;

                if (cifrar)
                {
                    cifrado += (char)((auxMensaje + auxLlave) % 26 + 65);

                    tempMensaje.Add(mensaje[x] - 65);
                    tempLlave.Add(llave[posicionLlave] - 65);
                    tempSuma.Add(auxMensaje + auxLlave);
                    tempMod.Add(cifrado[x] - 65);
                }
                else
                {

                    if (auxMensaje - auxLlave < 0)
                    {
                        cifrado += (char)((auxMensaje - auxLlave + 26) % 26 + 65);
                        tempMensaje.Add(auxMensaje);
                        tempLlave.Add(auxLlave);
                        tempMod.Add(cifrado[x] - 65);
                        posicionLlave++;
                        continue;
                    }

                    cifrado += (char)((auxMensaje - auxLlave) % 26  + 65);
                    tempMensaje.Add(auxMensaje);
                    tempLlave.Add(auxLlave);
                    tempMod.Add(cifrado[x] - 65);
                }

                posicionLlave++;
            }

            Proceso guardarLlave = new Proceso("VigenereM2", "guardarLlave", tempLlave, true);
            Proceso guardarMensaje = new Proceso("VigenereM2", "guardarMensaje", tempMensaje, true);
            Proceso guardarSuma = new Proceso("VigenereM2", "guardarSuma", tempSuma, false);
            Proceso guardarMod = new Proceso("VigenereM2", "guardarMod", tempMod, false);

            memoria.Procesos.Add(guardarLlave);
            memoria.Procesos.Add(guardarMensaje);
            memoria.Procesos.Add(guardarSuma);
            memoria.Procesos.Add(guardarMod);

            return cifrado;
        }

        public static string Beufort(string mensaje, string llave, bool cifrar, memoria memoria = null)
        {
            char[,] matriz = new char[,]
            {
                {'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B'},
                {'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C'},
                {'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D'},
                {'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E'},
                {'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F'},
                {'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G'},
                {'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H'},
                {'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I'},
                {'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J'},
                {'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K'},
                {'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L'},
                {'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M'},
                {'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N'},
                {'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O'},
                {'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P'},
                {'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q'},
                {'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R'},
                {'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S'},
                {'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U', 'T'},
                {'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V', 'U'},
                {'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W', 'V'},
                {'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X', 'W'},
                {'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y', 'X'},
                {'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z', 'Y'},
                {'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'Z'},
                {'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A'}
            };

            int posicionLlave = 0;
            string cifrado = "";

            List<int> tempMensaje = new List<int>();
            List<int> tempLlave = new List<int>();
            List<int> tempSuma = new List<int>();
            List<int> tempMod = new List<int>();

            mensaje = mensaje.ToUpper();
            llave = llave.ToUpper();

            for (int x = 0; x < mensaje.Length; x++)
            {
                int auxMensaje = 0;
                int auxLlave = 0;


                if (posicionLlave == llave.Length)
                {
                    posicionLlave = 0;
                }

                auxMensaje = mensaje[x] - 65;
                auxLlave = llave[posicionLlave] - 65;

                if (cifrar)
                {
                    cifrado += (char)((auxMensaje - auxLlave + 26) % 26 + 65);

                    tempMensaje.Add(mensaje[x] - 65);
                    tempLlave.Add(llave[posicionLlave] - 65);
                    tempSuma.Add(auxMensaje + auxLlave);
                    tempMod.Add(cifrado[x] - 65);
                }
                else
                {

                    if (auxMensaje - auxLlave < 0)
                    {
                        cifrado += (char)((auxMensaje + auxLlave + 26) % 26 + 65);
                        tempMensaje.Add(auxMensaje);
                        tempLlave.Add(auxLlave);
                        tempMod.Add(cifrado[x] - 65);
                        posicionLlave++;
                        continue;
                    }

                    cifrado += (char)((auxMensaje - auxLlave) % 26 + 65);
                    tempMensaje.Add(auxMensaje);
                    tempLlave.Add(auxLlave);
                    tempMod.Add(cifrado[x] - 65);
                }

                posicionLlave++;
            }

            Proceso guardarLlave = new Proceso("Beufort", "guardarLlave", tempLlave, true);
            Proceso guardarMensaje = new Proceso("Beufort", "guardarMensaje", tempMensaje, true);
            Proceso guardarSuma = new Proceso("Beufort", "guardarSuma", tempSuma, false);
            Proceso guardarMod = new Proceso("Beufort", "guardarMod", tempMod, false);

            memoria.Procesos.Add(guardarLlave);
            memoria.Procesos.Add(guardarMensaje);
            memoria.Procesos.Add(guardarSuma);
            memoria.Procesos.Add(guardarMod);

            return cifrado;
        }

        public static string Vernam(string mensaje, string llave, memoria memoria = null)
        {
            string criptograma = "";

            mensaje = mensaje.ToUpper();
            llave = llave.ToLower();

            List<string> byteMensaje = new List<string>();
            List<string> byteLlave = new List<string>();
            List<string> byteCripto = new List<string>();

            //Obtengo el byte de cada caracter
            for(int x = 0; x < mensaje.Length; x++)
            {
                byteMensaje.Add(DecimalAByte(mensaje[x]));
                byteLlave.Add(DecimalAByte(llave[x]));
            }

            //Ejecuto la operacion XOR en cada bit para obtener el nuevo byte
            for (int x = 0; x < byteMensaje.Count; x++)
            {
                string auxCripto = "";
                
                for (int y = 0; y < byteMensaje.ElementAt(x).Length; y++)
                {
                    bool aux1 = byteMensaje.ElementAt(x)[y] == '1' ? true : false;
                    bool aux2 = byteLlave.ElementAt(x)[y] == '1' ? true : false;

                    if (aux1 ^ aux2)
                    {
                        auxCripto += '1';
                    }
                    else
                    {
                        auxCripto += '0';
                    }

                }

                byteCripto.Add(auxCripto);
            }

            for(int x = 0; x < byteCripto.Count; x++)
            {
                criptograma += (char)Convert.ToInt32(byteCripto.ElementAt(x), 2);
            }

            return criptograma;
        }

        static string DecimalAByte(int numero)
        {
            string binario = "", aux = "";
            char[] auxArray;

            //Console.WriteLine("Decimal a convertir: " + numero.ToString());

            if (numero == 0)
            {
                return "00000000";
            }

            if (numero < 128)
            {
                binario += '0';
            }

            if (numero < 64)
            {
                binario += '0';
            }

            if (numero < 32)
            {
                binario += '0';
            }

            if (numero < 16)
            {
                binario += '0';
            }

            if (numero < 8)
            {
                binario += '0';
            }

            if (numero < 4)
            {
                binario += '0';
            }

            if (numero < 2)
            {
                binario += '0';
            }

            if (numero < 1)
            {
                binario += '0';
                return binario;
            }

            while(numero > 0)
            {
                aux += numero % 2;
                numero = numero / 2;
            }

            auxArray = aux.ToCharArray();

            foreach(char bit in auxArray.Reverse())
            {
                binario += bit;
            }

            //Console.WriteLine("Byte: " + binario);

            return binario;
        }

        public static string Inversa(string mensaje)
        {
            string criptograma = "";
            char[] aux;
            aux = mensaje.ToCharArray();
            
            foreach(char caracter in aux.Reverse())
            {
                criptograma += caracter;
            }

            return criptograma;
        }

        public static string Simple(string mensaje)
        {
            string criptograma = "";

            mensaje.Trim(' ');

            if(mensaje.Length % 2 != 0)
            {
                mensaje += 'x';
            }

            string aux1 = "", aux2 = "";

            for(int x = 0; x < mensaje.Length; x++)
            {
                if(x % 2 == 0)
                {
                    aux1 += mensaje[x];
                }
                else
                {
                    aux2 += mensaje[x];
                }
            }

            criptograma = aux1 + aux2;

            return criptograma;
        }

        public static string Doble(string mensaje, bool iterarHastaDesencriptar = false)
        {
            string criptograma = "";

            criptograma = Simple(mensaje);

            criptograma = Simple(criptograma);

            int iteraciones = 1;

            if (iterarHastaDesencriptar)
            {
                do
                {
                    criptograma = Simple(criptograma);
                    iteraciones++;
                } while (criptograma != mensaje);

                criptograma += " " + iteraciones.ToString();
            }

            return criptograma;
        }

        public static string PorGrupos(string mensaje, string llave)
        {

            if(mensaje.Length % llave.Length != 0)
            {
                throw new Exception("El mensaje no corresponde a la longitud de la llave");
            }


            string criptograma = "";

            List<string> subCadenas = new List<string>();

            int aux = 0;

            string subString = "";

            for(int x = 0; x < mensaje.Length; x++)
            {

                if (aux == llave.Length)
                {
                    aux = 1;
                    subCadenas.Add(subString);
                    subString = "";
                    subString += mensaje[x];
                    continue;
                }

                if (aux < llave.Length)
                {
                    subString += mensaje[x];
                }

                aux++;
            }

            subCadenas.Add(subString);

            foreach (string cadena in subCadenas)
            {
                Console.WriteLine(cadena);
                foreach(char numero in llave)
                {
                    criptograma += cadena[int.Parse(numero.ToString()) - 1];
                }
            }

            return criptograma;
        }

        public static string PorSeries(string mensaje, List<Regla> reglas, bool cifrar)
        {
            string criptograma = "";
            List<List<int>> posiciones = new List<List<int>>();

            mensaje = mensaje.ToUpper();

            //Crea las listas de posiciones segun cada regla
            foreach(Regla regla in reglas)
            {
                List<int> auxPosiciones = new List<int>();
                
                if(regla.NumerosNaturales)
                {
                    for (int x = 1; x < mensaje.Length; x++)
                    {
                        auxPosiciones.Add(x);
                    }

                    if(posiciones.Count == 0)
                    {
                        break;
                    }

                    posiciones.Add(auxPosiciones);

                    continue;
                }

                if(regla.NumerosPrimos)
                {
                    for (int x = 2; x < mensaje.Length; x++)
                    {
                        bool primo = true;
                        
                        if (x == 2)
                        {
                            auxPosiciones.Add(x);
                            continue;

                        }

                        if (x % 2 == 0) 
                        {
                            primo = false;
                            continue;
                        }

                        var boundary = (int)Math.Floor(Math.Sqrt(x));

                        for (int i = 3; i <= boundary; i += 2)
                        {
                            if (x % i == 0)
                            {
                                primo = false;
                                break;
                            }
                            primo = true;
                        }

                        if(primo)
                        {
                            auxPosiciones.Add(x);
                        }
                    }

                    posiciones.Add(auxPosiciones);

                    continue;
                }

                if(regla.NumerosPares)
                {
                    for (int x = 2; x < mensaje.Length; x += 2)
                    {
                        auxPosiciones.Add(x);
                    }

                    posiciones.Add(auxPosiciones);

                    continue;
                }

                if(regla.NumerosImpares)
                {
                    for (int x = 1; x < mensaje.Length; x += 2)
                    {
                        auxPosiciones.Add(x);
                    }

                    posiciones.Add(auxPosiciones);

                    continue;
                }

                if(regla.Multiplo)
                {
                    for (int x = regla.MultiploDe; x <= mensaje.Length; x += regla.MultiploDe)
                    {
                        auxPosiciones.Add(x);
                    }

                    posiciones.Add(auxPosiciones);

                    continue;
                }
            }

            List<int> posicionesCripto = new List<int>();
            
            //Ciclo en cada lista de posiciones (de acuerdo a las reglas)
            foreach(List<int> posicionesReglas in posiciones)
            {
                //Ciclo por cada posicion de cada regla
                foreach(int posicion in posicionesReglas)
                {
                    //Si la posicion no existe en la lista de orden del cripto, la agrega
                    if(!posicionesCripto.Contains(posicion))
                    {
                        posicionesCripto.Add(posicion);
                    }
                }
            }

            if(cifrar)
            {
                //Reacomoda el mensaje de acuerdo a las posiciones finales obtenidas
                foreach (int posicion in posicionesCripto)
                {
                    //Console.Write(posicion.ToString() + ' ');
                    criptograma += mensaje[posicion - 1];
                }
            }
            else
            {
                //Arreglo que contiene las posiciones de cada letra del mensaje
                char[] aux = new char[mensaje.Length];

                //Indice de posición en el mensaje.
                int posicionAux = 0;

                foreach (int posicion in posicionesCripto)
                {
                    //Console.Write(posicion.ToString() + ' ');
                    //Coloco en el arreglo, segun la posición del mensaje original, la letra del mensaje cifrado
                    aux[posicion - 1] = mensaje[posicionAux];
                    posicionAux++;
                }

                foreach(char caracter in aux)
                {
                    criptograma += caracter;
                }
            }

            return criptograma;
        }
    }
}
