using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            configuracion miConfiguracion = new configuracion();

            while (!salir)
            {   
                Console.Clear();
                memoria miMemoria = new memoria();

                Console.WriteLine("Datos---------------------------------");
                Console.WriteLine("Mensaje: \t" + miConfiguracion.Mensaje);
                Console.WriteLine("Llave: \t\t" + miConfiguracion.Llave);
                Console.WriteLine("Modo: \t\t" + (miConfiguracion.Cifrar ? "Cifrar" : "Descifrar"));
                Console.WriteLine("Memoria: \t" + (miConfiguracion.Memoria ? "Activada" : "Apagada"));
                Console.WriteLine("--------------------------------------\n");

                Console.WriteLine("Ingrese una opcion para continuar.\n");
                Console.WriteLine("1.- Modificar parametros.");
                Console.WriteLine("2.- Alberti.");
                Console.WriteLine("3.- Vigenere - Por Tabla.");
                Console.WriteLine("4.- Vigenere - Por modulo de 26.");
                Console.WriteLine("5.- Beufort");
                Console.WriteLine("6.- Vernam");
                Console.WriteLine("0.- Salir.");
                opcion = Console.ReadKey().KeyChar - 48;

                switch (opcion)
                {
                    case 0:
                        {
                            salir = true;
                            break;
                        }
                    case 1:
                        {
                            ModificarConfiguracion(ref miConfiguracion);
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();

                            Console.WriteLine("Desea utilizar esta configuracion?");
                            Console.WriteLine("Caracter por rotacion: " + miConfiguracion.CaracteresPorRotacion.ToString());
                            Console.WriteLine("Rotacion: " + miConfiguracion.Rotacion.ToString());
                            Console.WriteLine("Direccion: " + (miConfiguracion.Direccion ? "Derecha" : "Izquierda"));
                            Console.WriteLine("(Y/N)");

                            ConsoleKeyInfo aux = Console.ReadKey();
                            
                            if(aux.Key == ConsoleKey.N)
                            {
                                ModificarConfiguracion(ref miConfiguracion);
                            }

                            Console.Clear();
                            
                            try
                            {
                                Console.WriteLine("Mensaje: " + miConfiguracion.Mensaje);
                                Console.WriteLine("Llave: " + miConfiguracion.Llave);
                                Console.WriteLine("Criptograma: " + Algoritmos.Alberti(miConfiguracion.DiscoExterno, miConfiguracion.DiscoInterno, miConfiguracion.Mensaje, miConfiguracion.Llave, miConfiguracion.CaracteresPorRotacion, miConfiguracion.Rotacion, miConfiguracion.Direccion, miConfiguracion.Cifrar, (miConfiguracion.Memoria ? miMemoria : null)));
                            }
                            catch(Exception error)
                            {
                                Console.WriteLine("Error: " + error.Message);
                            }

                            if (miConfiguracion.Memoria)
                            {
                                foreach (Proceso proceso in miMemoria.Procesos)
                                {
                                    Console.WriteLine(proceso.ToString());
                                }
                            }

                            Console.ReadKey();

                            break;
                        }
                    case 3:
                        {
                            Console.Clear();

                            Console.WriteLine("Desea utilizar esta configuracion?");
                            Console.WriteLine("Llave: " + miConfiguracion.Llave);
                            Console.WriteLine("(Y/N)");

                            ConsoleKeyInfo aux = Console.ReadKey();

                            if (aux.Key == ConsoleKey.N)
                            {
                                ModificarConfiguracion(ref miConfiguracion);
                            }

                            Console.Clear();

                            try
                            {
                                Console.WriteLine("Mensaje: " + miConfiguracion.Mensaje);
                                Console.WriteLine("Llave: " + miConfiguracion.Llave);
                                Console.WriteLine("Criptograma: " + Algoritmos.VigenereM1(miConfiguracion.Mensaje, miConfiguracion.Llave, miConfiguracion.Cifrar, (miConfiguracion.Memoria ? miMemoria : null)));
                            }
                            catch (Exception error)
                            {
                                Console.WriteLine("Error: " + error.Message);
                            }

                            if (miConfiguracion.Memoria)
                            {
                                foreach (Proceso proceso in miMemoria.Procesos)
                                {
                                    Console.WriteLine(proceso.ToString());
                                }
                            }

                            Console.ReadKey();

                            break;
                        }
                    case 4:
                        {
                            Console.Clear();

                            Console.WriteLine("Desea utilizar esta configuracion?");
                            Console.WriteLine("Llave: " + miConfiguracion.Llave);
                            Console.WriteLine("(Y/N)");

                            ConsoleKeyInfo aux = Console.ReadKey();

                            if (aux.Key == ConsoleKey.N)
                            {
                                ModificarConfiguracion(ref miConfiguracion);
                            }

                            try
                            {
                                Console.WriteLine("Mensaje: " + miConfiguracion.Mensaje);
                                Console.WriteLine("Llave: " + miConfiguracion.Llave);
                                Console.WriteLine("Criptograma: " + Algoritmos.VigenereM2(miConfiguracion.Mensaje, miConfiguracion.Llave, miConfiguracion.Cifrar, (miConfiguracion.Memoria ? miMemoria : null)));
                            }
                            catch (Exception error)
                            {
                                Console.WriteLine("Error: " + error.Message);
                            }

                            if (miConfiguracion.Memoria)
                            {
                                foreach (Proceso proceso in miMemoria.Procesos)
                                {
                                    Console.WriteLine(proceso.ToString());
                                }
                            }

                            Console.ReadKey();
                            
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();

                            Console.WriteLine("Desea utilizar esta configuracion?");
                            Console.WriteLine("Llave: " + miConfiguracion.Llave);
                            Console.WriteLine("(Y/N)");

                            ConsoleKeyInfo aux = Console.ReadKey();

                            if (aux.Key == ConsoleKey.N)
                            {
                                ModificarConfiguracion(ref miConfiguracion);
                            }

                            Console.Clear();

                            try
                            {
                                Console.WriteLine("Mensaje: " + miConfiguracion.Mensaje);
                                Console.WriteLine("Llave: " + miConfiguracion.Llave);
                                Console.WriteLine("Criptograma: " + Algoritmos.Beufort(miConfiguracion.Mensaje, miConfiguracion.Llave, miConfiguracion.Cifrar, (miConfiguracion.Memoria ? miMemoria : null)));
                            }
                            catch (Exception error)
                            {
                                Console.WriteLine("Error: " + error.Message);
                            }

                            if (miConfiguracion.Memoria)
                            {
                                foreach (Proceso proceso in miMemoria.Procesos)
                                {
                                    Console.WriteLine(proceso.ToString());
                                }
                            }

                            Console.ReadKey();

                            break;
                        }
                    case 6:
                        {
                            Console.Clear();

                            Console.WriteLine("ESTE MÉTODO ES INDIFERENTE DEL MODO | CIFRA O DECIFRA DEPENDIENDO DEL MENSAJE");
                            Console.WriteLine("-----------------------------------------------------------------------------");
                            Console.WriteLine("Desea utilizar esta configuracion?");
                            Console.WriteLine("Mensaje: " + miConfiguracion.Mensaje);
                            Console.WriteLine("Llave: " + miConfiguracion.Llave);
                            Console.WriteLine("(Y/N)");

                            ConsoleKeyInfo aux = Console.ReadKey();

                            if (aux.Key == ConsoleKey.N)
                            {
                                ModificarConfiguracion(ref miConfiguracion);
                            }

                            Console.Clear();

                            try
                            {
                                Console.WriteLine("Mensaje: " + miConfiguracion.Mensaje);
                                Console.WriteLine("Llave: " + miConfiguracion.Llave);
                                Console.WriteLine("Criptograma: " + Algoritmos.Vernam(miConfiguracion.Mensaje, miConfiguracion.Llave, (miConfiguracion.Memoria ? miMemoria : null)));
                            }
                            catch (Exception error)
                            {
                                Console.WriteLine("Error: " + error.Message);
                            }
                            

                            Console.ReadKey();
                            
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
        static void ModificarConfiguracion(ref configuracion miConfiguracion)
        {
            ConsoleKeyInfo opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("Seleccione la configuracion a modificar");
                Console.WriteLine("1.- Mensaje");
                Console.WriteLine("2.- Llave");
                Console.WriteLine("3.- Caracteres por rotacion (Alberti)");
                Console.WriteLine("4.- Rotacion (Alberti)");
                Console.WriteLine("5.- Direccion");
                Console.WriteLine("6.- Cifrar/Descifrar");
                Console.WriteLine("7.- Memoria");
                Console.WriteLine("--Presione Esc para salir.");
                opcion = Console.ReadKey();

                if (opcion.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    Console.WriteLine("Ingrese el mensaje a cifrar");
                    miConfiguracion.Mensaje = Console.ReadLine();
                }

                if (opcion.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    Console.WriteLine("Ingrese la llave");
                    miConfiguracion.Llave = Console.ReadLine();
                }

                if(opcion.Key == ConsoleKey.D3)
                {
                    Console.Clear();
                    Console.WriteLine("Ingrese los caracteres por rotacion - Cuantos caracteres se Cifraran/Descifraran antes de rotar");
                    miConfiguracion.CaracteresPorRotacion = int.Parse(Console.ReadLine());
                }

                if (opcion.Key == ConsoleKey.D4)
                {
                    Console.Clear();
                    Console.WriteLine("Ingrese la cantidad de caracteres a rotar");
                    miConfiguracion.Rotacion = int.Parse(Console.ReadLine());
                }

                if (opcion.Key == ConsoleKey.D5)
                {
                    Console.Clear();
                    Console.WriteLine("Rotar izquierda o derecha? - (I)zquierda | (D)erecha");
                    ConsoleKeyInfo aux;
                    do
                    {
                        aux = Console.ReadKey();
                        //Console.WriteLine("aux = " + aux);

                        if (aux.Key == ConsoleKey.I)
                        {
                            miConfiguracion.Direccion = false;
                            break;
                        }

                        if (aux.Key == ConsoleKey.D)
                        {
                            miConfiguracion.Direccion = true;
                            break;
                        }

                        Console.WriteLine("Ingrese una direccion valida");

                    } while (aux.Key != ConsoleKey.I || aux.Key != ConsoleKey.D);
                }

                if (opcion.Key == ConsoleKey.D6)
                {
                    Console.Clear();
                    Console.WriteLine("Cifrar o Descifrar? - (C)ifrar | (D)escifrar");
                    ConsoleKeyInfo aux;
                    do
                    {
                        aux = Console.ReadKey();

                        if (aux.Key == ConsoleKey.C)
                        {
                            miConfiguracion.Cifrar = true;
                            break;
                        }

                        if (aux.Key == ConsoleKey.D)
                        {
                            miConfiguracion.Cifrar = false;
                            break;
                        }

                        Console.WriteLine("Ingrese una opcion valida");

                    } while (aux.Key != ConsoleKey.C || aux.Key != ConsoleKey.D);
                }

                if (opcion.Key == ConsoleKey.D7)
                {
                    Console.Clear();
                    Console.WriteLine(miConfiguracion.Memoria ? "La Memoria se encuentra encendida" : "La memoria se encuentra apagada");
                    Console.WriteLine("Desea encender o apagar la memoria? - (E)ncender | (A)pagar");
                    ConsoleKeyInfo aux;
                    do
                    {
                        aux = Console.ReadKey();

                        if (aux.Key == ConsoleKey.E)
                        {
                            miConfiguracion.Memoria = true;
                            break;
                        }

                        if (aux.Key == ConsoleKey.A)
                        {
                            miConfiguracion.Memoria = false;
                            break;
                        }

                        Console.WriteLine("Ingrese una opcion valida");

                    } while (aux.Key != ConsoleKey.A || aux.Key != ConsoleKey.E);
                }

            } while (opcion.Key != ConsoleKey.Escape);
        }

    }
}
