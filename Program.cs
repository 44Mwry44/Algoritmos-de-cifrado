using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmos_de_cifrado
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            int opcion;

            List<char> discoExterno = new List<char> { 'A', '0', 'D', 'X', '1', 'C', 'G', 'K', 'B', '8', 'M', '4', 'R', 'F', 'U', 'I', 'Y', '2', 'L', '6', 'Ñ', 'S', 'H', 'Q', '5', 'W', '7', 'E', 'V', '9', 'N', 'J', 'P', 'Z', 'T', '3'};
            List<char> discoInterno = new List<char> { 'a', '!', 'x', '0', 'j', 't', '@', '$', 'g', 'z', 'w', '&', 'e', 'o', 'p', 'h', '%', 'ñ', 'd', 'r', '=', 'c', 's', '?', 'm', '#', 'n', 'f', 'k', 'u', 'l', 'i', 'b', 'y', 'q', 'v'};

            while(!salir)
            {
                Console.Clear();
                Console.WriteLine("Ingrese una opcion para continuar.\n");
                Console.WriteLine("0.- Salir.");
                opcion = int.Parse(Console.ReadLine());

                switch(opcion)
                {
                    case 0:
                        {
                            salir = true;
                            break;
                        }
                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine("Ingrese el mensaje en claro");
                            string mensaje = Console.ReadLine();

                            Console.WriteLine("Ingrese la llave");
                            string llave = Console.ReadLine();

                            Console.WriteLine("Ingrese los caracteres por rotacion - Cuantos caracteres se Cifraran/Descifraran antes de rotar");
                            int caracteresPorRotacion = int.Parse(Console.ReadLine());

                            Console.WriteLine("Ingrese la cantidad de caracteres a rotar");
                            int rotacion = int.Parse(Console.ReadLine());

                            Console.WriteLine("Rotar izquierda o derecha? - (I)zquierda | (D)erecha");
                            string aux;
                            bool direccion = true;
                            do
                            {
                                 aux = Console.ReadLine();
                                Console.WriteLine("aux = " + aux);
                                switch (aux)
                                {
                                    case "i":
                                        {
                                            direccion = false;
                                            break;
                                        }
                                    case "d":
                                        {
                                            direccion = true;
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("Ingrese una direcion valida");
                                            break;
                                        }
                                }
                            } while (aux != "i\n" || aux != "d\n");

                            Console.Clear();
                            Console.WriteLine("Mensaje: " + mensaje);
                            Console.WriteLine("Llave: " + llave);
                            Console.WriteLine("Criptograma: " + Alberti(discoExterno, discoInterno, mensaje, llave, caracteresPorRotacion, rotacion, direccion));

                            break;
                        } 
                    default:
                        {
                            Console.WriteLine("Ingrese una opcion valida - Presione cualquier tecla para continuar");
                            Console.ReadKey();
                            break;
                        }
                }
            }
        }

        //direccion = false | izquierda
        //direccion = true  | derecha
        static string Alberti(List<char> discoExterno, List<char> discoInterno, string mensaje, string llave, int caracteresPorGiro, int rotacion, bool direccion)
        {
            int aux = 1;
            string criptograma = "";
            for(int posicion = 0; posicion < mensaje.Length; posicion++)
            {
                if (aux == caracteresPorGiro)
                {
                    Alberti_AjustarMatriz(discoExterno, discoInterno, rotacion, direccion);
                    aux = 1;
                }
                int charIndex = discoExterno.FindIndex(item => mensaje[posicion] == item);
                criptograma += discoInterno.ElementAt(charIndex);
                aux++;
            }

            return criptograma;
        }

        static void Alberti_AjustarMatriz(List<char> discoExterno, List<char> discoInterno, string llave)
        {
            int posicionLlave1 = discoExterno.FindIndex(item => llave[0] == item);
            Console.WriteLine("Posicion 1: " + posicionLlave1.ToString());
            
            int posicionLlave2 = discoInterno.FindIndex(item => llave[1] == item);
            Console.WriteLine("Posicion 2: " + posicionLlave2.ToString());

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

        static void Alberti_AjustarMatriz(List<char> discoExterno, List<char> discoInterno, int ajuste, bool direccion)
        {
            char aux = ' ';
            if(direccion)
            {
                for (int x = 0; x < ajuste; x++)
                {
                    for (int posicion = 35; posicion > 0; posicion--)
                    {
                        if(posicion == 35)
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
                        if(posicion == 0)
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
