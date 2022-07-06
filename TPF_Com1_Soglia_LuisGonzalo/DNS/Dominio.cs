using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPF_Com1_Soglia_LuisGonzalo.DNS
{
    public class Dominio
    {
        public string nombre { get; set;}
        public bool hoja { get; set; }
        public string ? ip { get; set; }
        public string ? protocolo { get; set; }

       
        public Dominio(string nombre)
        {
            this.nombre = nombre;
            this.hoja = false;
        }

        public Dominio(string nombre, string ip, string protocolo)
        {
            this.nombre = nombre;
            this.hoja = true;
            this.ip = ip;
            this.protocolo = protocolo;
        }


    }
}
