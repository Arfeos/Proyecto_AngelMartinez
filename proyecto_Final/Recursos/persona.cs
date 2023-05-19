using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Final.Recursos
{
    public class persona
    {
        public string Usuario { get; set; }
        public int EsAdmin { get; set; }
        public persona(string usuario, int esAdmin)
        {
            Usuario = usuario;
            EsAdmin = esAdmin;
        }
        public override string ToString()
        {
            return "nombre:"+Usuario+" esAdmin:"+EsAdmin;
        }
    }
}
