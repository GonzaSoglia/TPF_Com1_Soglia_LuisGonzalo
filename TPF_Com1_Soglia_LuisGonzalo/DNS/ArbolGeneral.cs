using System;
using System.Collections.Generic;
using TPF_Com1_Soglia_LuisGonzalo.DNS;


namespace TPF_Com1_Soglia_LuisGonzalo
{
	public class ArbolGeneral
	{
		
		private Dominio dato;
		private List<ArbolGeneral> hijos = new List<ArbolGeneral>();

		public ArbolGeneral(Dominio dato) {
			this.dato = dato;
		}
	
		public Dominio getDatoRaiz() {
			return this.dato;
		}
	
		public List<ArbolGeneral> getHijos() {
			return hijos;
		}
	
		public void agregarHijo(ArbolGeneral hijo) {
			
			this.getHijos().Add(hijo);
		}

		public void agregarDominio(string[] dominio, string ip, string protocolo)
		{
			ArbolGeneral pos = this;
			ArbolGeneral aux;
			string nombre_equipo = dominio [dominio.Length - 1];
			dominio = dominio.SkipLast(1).ToArray();
			foreach(string nombre in dominio)
            {
				aux= pos.buscar_Hijo_Nombre(nombre);
				if (aux == null)
                {
					Dominio nuevo_dominio = new Dominio(nombre);
					ArbolGeneral nodo = new ArbolGeneral(nuevo_dominio);
					pos.agregarHijo(nodo);
					pos = nodo;

                }
				
				else
                {
					if (aux.getDatoRaiz().hoja)
                    {
						Console.WriteLine("No se puede convertir un equipo en un subdominio, ingrese otro");
						return;
                    }
					pos = aux;
                }
            }
			aux = pos.buscar_Hijo_Nombre(nombre_equipo);

			if (aux == null)
			{
				Dominio nuevo_dominio = new Dominio(nombre_equipo, ip, protocolo);
				ArbolGeneral nodo = new ArbolGeneral(nuevo_dominio);
				pos.agregarHijo(nodo);
				Console.WriteLine("Dominio ingresado correctamente");
			}
			else
				Console.WriteLine("Nombre de equipo existente");

		} 

		public ArbolGeneral buscar_Hijo_Nombre(string nombre)
        {
			foreach  (ArbolGeneral hijo in this.getHijos())
            {
				if (hijo.getDatoRaiz().nombre == nombre)
                {
					return hijo;
                }
            }
			return null;
        }

		public ArbolGeneral buscar_hijo_todo_el_arbol(string[] dominio)
        {
			ArbolGeneral pos = this;
			ArbolGeneral aux;

			foreach (string nombre in dominio)
			{
				aux = pos.buscar_Hijo_Nombre(nombre);
				if (aux == null)
					return null;
				pos = aux;

			}
			return pos;

		}

		public bool eliminar_dominio(string [] dominio)
        {
			ArbolGeneral aux = buscar_hijo_todo_el_arbol(dominio);
			if (aux == null)
            {
				return false;
            }

			ArbolGeneral aux_padre;
			string [] dominio_aux= dominio.SkipLast(1).ToArray();

			for (int i= dominio.Length-1; i >= 0; -- i)
            {
				aux_padre = buscar_hijo_todo_el_arbol(dominio_aux);
				aux_padre.hijos.Remove(aux);
				if (aux_padre.hijos.Count() > 0)
                {
					return true;
                }
				dominio_aux = dominio_aux.SkipLast(1).ToArray();
				aux = aux_padre;
				
			}
			return true;


		}

		public int contar_profundidad(int lvl, int p)
        {
			if (lvl == p)
				return 1;
			else
			{
				int acum = 0;
				foreach (ArbolGeneral hijo in hijos)
				{
					acum += hijo.contar_profundidad(lvl + 1, p);


				}
				return acum;
            }


        }

	}
}
