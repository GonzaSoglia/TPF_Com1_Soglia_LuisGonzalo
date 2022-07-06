// See https://aka.ms/new-console-template for more information

using TPF_Com1_Soglia_LuisGonzalo;
using TPF_Com1_Soglia_LuisGonzalo.DNS;
using TPF_Com1_Soglia_LuisGonzalo.Interfaz;

ArbolGeneral arbol = new ArbolGeneral(new Dominio(""));
Consola c = new Consola(arbol);
do
{
    c.mostrar_Menu();
}
while (c.leer_Opcion(Console.ReadLine()));


