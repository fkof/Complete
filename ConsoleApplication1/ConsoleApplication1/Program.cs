using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int nclientes, i, accidentes;
		string nombre, clientes;
		nclientes = 0;
		nombre = "";
		i = 0;
		clientes = "";
		accidentes = 0;
		Console.WriteLine("Teclee la cantidad de clientes");
		nclientes = int.Parse(Console.ReadLine());

		while (i <= nclientes) 

			{	Console.WriteLine("Teclee el nombre del cliente");
				nombre = Console.ReadLine();
				Console.WriteLine("Cuantos accidentes a tenido el cliente");
				accidentes = int.Parse(Console.ReadLine());
				if (accidentes >= 2 && accidentes <= 4)
				{
					clientes = clientes + nombre;

				}
					i = i + 1;
				
           	}
        Console.WriteLine("Los clientes entre 2 y 4 accidentes son: " + clientes);
        }
    }
}
