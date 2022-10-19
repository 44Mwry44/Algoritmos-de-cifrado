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
        public static string Alberti(List<char> discoExterno, List<char> discoInterno, string mensaje, string llave, int caracteresPorRotacion, int rotacion, bool direccion, bool cifrar, memoria memoria)
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

        public static string VigenereM1(string mensaje, string llave, bool cifrar, memoria memoria)
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

        public static string VigenereM2(string mensaje, string llave, bool cifrar, memoria memoria)
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

        public static string Beufort(string mensaje, string llave, bool cifrar, memoria memoria)
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

            List<int> tempCoordenadas;

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
    }
}
