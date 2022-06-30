using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPF_Com1_Soglia_LuisGonzalo;

namespace TPF_Com1_Soglia_LuisGonzalo.Interfaz
{
    internal class Consola
    {
        public ArbolGeneral arbol;
       public Consola(ArbolGeneral arbol)
        {
            this.arbol = arbol;
        }
       public void mostrar_Menu()
        {
            Console.WriteLine("Bienvenido a Simulador DNS");
            Console.WriteLine("MODULO DE ADMINISTRACION");
            Console.WriteLine("1. Agregar un nuevo dominio");
            Console.WriteLine("2. Eliminar un dominio");
            Console.WriteLine("MODULO DE CONSULTA");
            Console.WriteLine("3. Mostrar información de un equipo a partir de un dominio");
            Console.WriteLine("4. Mostrar todos los equipos a partir de un subdominio");
            Console.WriteLine("5. Mostrar todos los dominios, subdominios y equipos a partir de una profundidad dada");
            Console.WriteLine("0. Salir");
            Console.WriteLine("Inserte la opción deseada:");
        }
        public bool leer_Opcion(string s)
        {
            switch (s) {
                case "1":
                    opcion1_agregar_nuevo_dominio();
                    break;
                case "4":
                    opcion4_mostrar_subdominios();
                    break;
                case "0":
                    return false;
                default:
                    return true;
            }
            return true;
        }

        public void opcion1_agregar_nuevo_dominio()
        {
            Console.WriteLine("Ingrese dominio:");
            string dominio = Console.ReadLine();
            string[] arr_dom = dominio.Split(".");
            if (arr_dom.Count() < 2)
            {
                Console.WriteLine("Dominio invalido. Ingrese uno válido");
                return;
            }
            Array.Reverse(arr_dom);
            this.arbol.agregarDominio(arr_dom);
            Console.WriteLine(arbol);
        }
        public void opcion4_mostrar_subdominios()
        {
            ArbolGeneral aux;
            Console.WriteLine("Ingrese dominio (vacio desde la raiz): ");
            string dominio = Console.ReadLine();
            if (dominio == "")
            {
                aux = this.arbol;
            }
            else
            { 
                string[] arr_dom = dominio.Split(".");
                if (arr_dom.Count() < 2)
                {
                    Console.WriteLine("Dominio invalido. Ingrese uno válido");
                    return;
                }
                Array.Reverse(arr_dom);
                aux = this.arbol.buscar_hijo_todo_el_arbol(arr_dom);

            }
            foreach (ArbolGeneral hijo in aux.getHijos())
            {
                imprimir_nodo(hijo,0);
            }

        }

        public void imprimir_nodo(ArbolGeneral arbol, int nivel)
        {
            if (nivel > 0)
                Console.Write("╠");
            Console.Write(String.Concat(Enumerable.Repeat("═", nivel * 2)));
            Console.WriteLine("╣ " + arbol.getDatoRaiz().nombre);
            foreach (ArbolGeneral hijo in arbol.getHijos())
            {
                imprimir_nodo(hijo, nivel + 1);
            }

        }
    }
}
