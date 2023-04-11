using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Final.Recursos
{
    /// <summary>
    /// Clase para representar una persona en el sistema.
    /// </summary>
    public class persona
    {
        public string Usuario { get; set; }
        public int EsAdmin { get; set; }

        /// <summary>
        /// Crea una nueva instancia de la clase persona.
        /// </summary>
        /// <param name="Usuario">El nombre de usuario de la persona.</param>
        /// <param name="EsAdmin">Indica si la persona es administrador (1) o no (0).</param>
        /// <param name="Clase">La clase del personaje de la persona.</param>
        /// <param name="Raza">La raza del personaje de la persona.</param>
        public persona(string usuario, int esAdmin)
        {
            this.Usuario = usuario;
            this.EsAdmin = esAdmin;

        }
        public override string ToString()
        {
            return "usuario=" + Usuario + " Administrador=" + EsAdmin;
        }
    }
}
