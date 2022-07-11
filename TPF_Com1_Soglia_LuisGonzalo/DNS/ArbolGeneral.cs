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

		public void eliminar_dominio(string [] dominio)
        {
			ArbolGeneral pos = buscar_hijo_todo_el_arbol(dominio);
			if (pos == null)
			{
				Console.WriteLine("No se encontro el dominio");
				return;
			}

			Cola<T> c = new Cola<T>();
			
				






        }

		//public void eliminar_nodo(ArbolGeneral nodo )
       // {
		//	foreach (ArbolGeneral hijo in nodo.getHijos())
		//	{
		//		hijo.eliminar_nodo(hijo);
				
      //  }

	
		//public void eliminarHijo(ArbolGeneral hijo) {
		//	this.getHijos().Remove(hijo);
		//}
	
		public bool esHoja() {
			return this.getHijos().Count == 0;
		}
	
		public int altura() {
			if (this.esHoja())
            {
				return 0;
            }
			else
            {
				int alt_maxima=0;
				foreach (var nodos in this.getHijos())
				{
					int altura_actual = nodos.altura();
					if (alt_maxima < altura_actual) { 
						alt_maxima = altura_actual;
					}
				}
				return alt_maxima + 1;
            }
		}

		

	
		
		public int include(Dominio dato) {
           if(!getDatoRaiz().Equals(dato))
            {
                foreach (var nodo in this.getHijos())
                {
					int suma_nivel = nodo.include(dato);
					if (suma_nivel != -1)
                    {
						return suma_nivel + 1;
                    }
				}
				return -1;
            }
            else
            {
                return 1;
            }

        }

		public int nivel (Dominio dato)
        {
			if (getDatoRaiz().Equals(dato))
            {
				return 0;
            }
			return this.include(dato);
		
        }

		public void Preorder()
        {
			Console.WriteLine(this.getDatoRaiz());
			foreach (ArbolGeneral hijo in this.getHijos()) 
			{
				hijo.Preorder();
            }
		}
		public void Postorder()
		{
			foreach (ArbolGeneral hijo in this.getHijos())
			{
				hijo.Postorder();
			}
			Console.WriteLine(this.getDatoRaiz());
		}

		public int caudal (int caud)
        {
			int min = caud;
			List<ArbolGeneral> hijos_aux = this.getHijos();
			if (hijos_aux.Count > 0)
			{
				int caudal_hijo = caud / hijos_aux.Count;
				int min_aux;
				foreach (ArbolGeneral arbolh in hijos_aux)
				{
					min_aux = arbolh.caudal(caudal_hijo);
					if (min_aux < min)
					{
						min = min_aux;
					}
				}
			}
			return min;
        }

		public void InOrder()
        {
			List<ArbolGeneral> hijos = this.getHijos();
			if (hijos.Count>0)
            {
				hijos[0].InOrder();
				hijos.RemoveAt(0);
            }
			Console.WriteLine(this.dato);
			foreach (ArbolGeneral hijo in hijos)
            {
				hijo.InOrder();
            }
        }
	}
}
