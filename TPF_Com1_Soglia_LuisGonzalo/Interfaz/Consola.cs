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
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Bienvenido a Simulador DNS");
            Console.WriteLine("MODULO DE ADMINISTRACION");
            Console.WriteLine("1. Agregar un nuevo dominio");
            Console.WriteLine("2. Eliminar un dominio");
            Console.WriteLine("MODULO DE CONSULTA");
            Console.WriteLine("3. Mostrar información de un equipo a partir de un dominio");
            Console.WriteLine("4. Mostrar todos los equipos a partir de un subdominio");
            Console.WriteLine("5. Contar los dominios, subdominios y equipos a partir de una profundidad dada");
            Console.WriteLine("0. Salir");
            Console.WriteLine("Inserte la opción deseada:");
        }
        public bool leer_Opcion(string s)
        {
            switch (s)
            {
                case "1":
                    opcion1_agregar_nuevo_dominio();
                    break;
                case "2":
                    opcion2_eliminar_un_dominio();
                    break; 
                case "3":
                    opcion3_mostrar_equipo();
                    break;
                case "4":
                    opcion4_mostrar_subdominios();
                    break;
                case "5":
                    opcion5_contar_profundidad();
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
            if (arr_dom.Count() < 2 || !validar_dominio(arr_dom))
            {
                Console.WriteLine("Dominio invalido. Ingrese uno válido");
                return;
            }
            Console.WriteLine("Ingrese protocolo: ");
            string protocolo = Console.ReadLine();
            Console.WriteLine("Ingrese IP: ");
            string ip = Console.ReadLine();
            Array.Reverse(arr_dom);
            try
            {
                this.arbol.agregarDominio(arr_dom, ip, protocolo);
           
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hubo un error en su dominio, verifique el formato indicado");
                return;
            }

            


        }
      public void opcion2_eliminar_un_dominio()
        {
            Console.WriteLine("Ingrese dominio del equipo (de la manera <nombre de equipo>.<subdominio_1>.<dominio>):");
            string dominio = Console.ReadLine();
            string[] arr_dom = dominio.Split(".");
            if (arr_dom.Count() < 2 || !validar_dominio(arr_dom))
            {
                Console.WriteLine("Dominio invalido. Ingrese uno válido");
                return;
            }
            Array.Reverse(arr_dom);
            if (arbol.eliminar_dominio(arr_dom) == true)
                Console.WriteLine("El dominio se elimino correctamente: ");
            else
                Console.WriteLine("El dominio ingresado no es valido, ingrese el correcto.");


        } 

        public void opcion3_mostrar_equipo()
        {
            Console.WriteLine("Ingrese dominio del equipo (de la manera <nombre de equipo>.<subdominio>.<dominio>):");
            string dominio = Console.ReadLine();
            string[] arr_dom = dominio.Split(".");
            if (arr_dom.Count() < 2 || !validar_dominio(arr_dom))
            {
                Console.WriteLine("Dominio invalido. Ingrese uno válido");
                return;
            }
            Array.Reverse(arr_dom);
            ArbolGeneral? hoja = this.arbol.buscar_hijo_todo_el_arbol(arr_dom);
            if (hoja == null)
            {
                Console.WriteLine("No existe el equipo colocado: ");
                return;
            }
            if (!hoja.getDatoRaiz().hoja)
            {
                Console.WriteLine("El dominio ingresado corresponde a un subdominio y no a un equipo");
                return;
            }
            Console.WriteLine("Nombre del equipo: " + hoja.getDatoRaiz().nombre);
            Console.WriteLine("IP del equipo: " + hoja.getDatoRaiz().ip);
            Console.WriteLine("Protocolo del equipo: " + hoja.getDatoRaiz().protocolo);



        }
        public void opcion4_mostrar_subdominios()
        {
            ArbolGeneral ? aux;
            Console.WriteLine("Ingrese dominio y subdominios de la siguiente manera (<subdominio1>.<subdominio2>....<dominio>): ");
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
            if (aux == null)
            {
                Console.WriteLine("No se encuentra la ruta...");
                return;
            }
            foreach (ArbolGeneral hijo in aux.getHijos())
            {
                imprimir_nodo(hijo, 0);
            }
            

        }

        public void opcion5_contar_profundidad()
        {
            Console.WriteLine("Ingrese la profundidad deseada: ");
            int profundidad = int.Parse(Console.ReadLine());
            Console.WriteLine("La cantidad de nodos en el nivel deseado es: " + arbol.contar_profundidad(0, profundidad));
        }

        public void imprimir_nodo(ArbolGeneral arbol, int nivel)
        {
            if (nivel > 0)
                Console.Write("╠");
            Console.Write(String.Concat(Enumerable.Repeat("═", nivel * 2)));
            Console.Write("╣ " + arbol.getDatoRaiz().nombre);
            if (arbol.getDatoRaiz().hoja)
            {
                Console.Write(" ip: " + arbol.getDatoRaiz().ip + " " + "protocolo: " + arbol.getDatoRaiz().protocolo);
                Console.WriteLine();
                return;
            }
            Console.WriteLine();

            foreach (ArbolGeneral hijo in arbol.getHijos())
            {
                imprimir_nodo(hijo, nivel + 1);
            }

        }
        public bool validar_dominio(string[] arr_dom)
        {
            foreach (string s in arr_dom) { 
                if (s =="")
                    return false;
            }
            return true;
        }

    }

}
