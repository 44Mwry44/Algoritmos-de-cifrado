# Algoritmos de Cifrado -- V 0.1.7

Este programa cuenta con 3 clases principales.

- Algoritmos.
- Memoria.
- Configuracion.

# Clase Algoritmos

| Método | Resultado |
|--------|-----------|
| Alberti | Devuelve una **cadena** con el mensaje cifrado o descifrado por el algoritmo del mismo nombre. |
| VigènereM1 | Devuelve una **cadena** con el mensaje cifrado o descifrado por el algoritmo del mismo nombre por el método de mátriz. |
| VigènereM2 | Devuelve una **cadena** con el mensaje cifrado o descifrado por el algoritmo del mismo nombre por el método del modulo de 26 (# de caracteres del alfabeto inglés). |
| Beufort | Devuelve una **cadena** con el mensaje cifrado o descifrado por el algoritmo del mismo nombre. |
| Vernam | Devuelve una **cadena** con el mensaje cifrado o descifrado por el algoritmo del mismo nombre. |

---

## Alberti

Este algoritmo utiliza dos discos que contienen el alfabeto que permite cifrar. El externo que correspondera al mensaje y el interno que será el criptograma.

| D | i | s | c | o | | E | x | t | e | r | n | o | | | | | | | | | | | | | | 
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
|A|O| D| X| 1| C| G| K| B| 8| M| 4| R| F| U| I| Y| 2| L| 6| Ñ| S| H| Q| 5| W| 7| E| V| 9| N| J| P| Z| T| 3|


| D | i | s | c | o | | I | n | t | e | r | n | o | | | | | | | | | | | | | |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
|a| !| x| 0| j| t| @| $| g| z| w| &| e| o| p| h| %| ñ| d| r| =| c| s| ?| m| #| n| f| k| u| l| i| b| y| q| v|

*Estos valores son los utilizados por default en el programa*

```C#
    Algoritmos.Alberti(List<char> discoExterno, List<char> discoInterno, string mensaje, string llave, int caracteresPorRotacion, int rotacion, bool direccion, bool cifrar, memoria memoria)
```

| Parametro | Tipo | funcion |
| --------- | ---- | ------- |
| DiscoExterno | List\<char> | Elementro principal del algoritmo. |
| DiscoInterno | List\<char> | Elementro principal del algoritmo. |
| mensaje | string | Mensaje en claro o criptograma. Es aquello que se sometera al algoritmo para devolver una nueva cadena con el resultado dependiendo de lo que se pida. |
| Llave | string | Cadena de longitud 2. Reprensenta el ajuste inicial al que se someteran los discos para así obtener el criptograma o mensaje. |
| caracteresPorRotacion | int | Representa la cantidad de caracteres que se someteran al algotirmo antes de reajustar la posicion del disco interno. |
| rotacion | int | Representa la cantidad de caracteres que se movera el disco interno en cada reajuste. |
| direccion | bool | false = Izquierda \| true = Derecha. <br> Direccion a la cual girará el disco interno en cada reajuste. |
| cifrar | bool | false = Descifrar \| true = Cifrar |
| memoria | memoria | Objeto que almacenará los ajustes al disco interno. |

---

## VigènereM1

El algoritmo forma una matriz con el alfabeto y la llave. El mensaje se cifrara o descifrara por medio de la matriz.

Las coordenadas se obtienen de la siguiente forma:  
| Fila                                | Columna               | Celda                 |   |   |
|-------------------------------------|-----------------------|-----------------------|---|---|
| Elementos del mensaje / criptograma | Elementos de la llave | Criptograma / Mensaje |   |   |

```C#
    Algoritmos.VigènereM1(string mensaje, string llave, bool cifrar, memoria memoria)
```

| Parametro | Tipo | funcion |
| --------- | ---- | ------- |
| mensaje | string | Mensaje en claro o criptograma. Es aquello que se sometera al algoritmo para devolver una nueva cadena con el resultado dependiendo de lo que se pida. <br> Se utliza para obtener la fila de la celda a buscar. |
| Llave | string | Cadena de longitud menor o mayor al mensaje. Si es menor, esta se repetira tantas veces sea necesario para completar la longitud del mensaje. Se utliza para obtener la columna de la celda a buscar. |
| cifrar | bool | false = Descifrar \| true = Cifrar |
| memoria | memoria | Objeto que almacenará las coordenadas obtenidas. |

---

## VigènereM2

Es el segundo metodo de cifrado de Vigènere. Este algoritmo en vez de emplear una mátriz, realiza una operacion de suma o resta (dependiendo de si se busca encriptar o desencriptar) junto al modulo de la cantidad de caracteres del alfabeto a emplear.

```C#
    Algoritmos.VigènereM2(string mensaje, string llave, bool cifrar, memoria memoria)
```

| Parametro | Tipo | funcion |
| --------- | ---- | ------- |
| mensaje | string | Mensaje en claro o criptograma. Es aquello que se sometera al algoritmo para devolver una nueva cadena con el resultado dependiendo de lo que se pida. |
| Llave | string | Cadena de longitud menor o mayor al mensaje. Si es menor, esta se repetira tantas veces sea necesario para completar la longitud del mensaje. |
| cifrar | bool | false = Descifrar \| true = Cifrar |
| memoria | memoria | Objeto que almacenará las coordenadas obtenidas. |

---

## Beufort

El algoritmo Beufort es una variante del algoritmo de VigènereM2. Este algoritmo en vez de emplear una mátriz, realiza una operacion de resta o suma (dependiendo de si se busca encriptar o desencriptar) junto al modulo de la cantidad de caracteres del alfabeto a emplear.

```C#
    Algoritmos.Beufort(string mensaje, string llave, bool cifrar, memoria memoria)
```

| Parametro | Tipo | funcion |
| --------- | ---- | ------- |
| mensaje | string | Mensaje en claro o criptograma. Es aquello que se sometera al algoritmo para devolver una nueva cadena con el resultado dependiendo de lo que se pida. |
| Llave | string | Cadena de longitud menor o mayor al mensaje. Si es menor, esta se repetira tantas veces sea necesario para completar la longitud del mensaje. |
| cifrar | bool | false = Descifrar \| true = Cifrar |
| memoria | memoria | Objeto que almacenará las coordenadas obtenidas. |

---

## Vernam

También conocido como "Mascara deshechable" emplea la representación ASCII de los caracteres para realizar una operación XOR entre el mensaje y la llave.  
  
*La llave debe ser de la misma longitud que el mensaje.*

Este algoritmo tiene la particularidad de ser reversible por si mismo, unicamente volviendo a someter el criptograma al mismo algoritmo con la misma llave.

```C#
    Algoritmos.Vernam(string mensaje, string llave)
```

| Parametro | Tipo | funcion |
| --------- | ---- | ------- |
| mensaje | string | Mensaje en claro o criptograma. Es aquello que se sometera al algoritmo para devolver una nueva cadena con el resultado dependiendo de lo que se pida. |
| Llave | string | Cadena de longitud igual al mensaje. |