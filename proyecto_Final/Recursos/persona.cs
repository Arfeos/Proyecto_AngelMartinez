using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Final.Recursos
{
    /// <summary>
    /// Clase que representa a una persona.
    /// </summary>
    public class persona
    {
        /// <summary>
        /// Obtiene o establece el nombre de usuario de la persona.
        /// </summary>
        public string Usuario { get; set; }

        /// <summary>
        /// Obtiene o establece un valor que indica si la persona es administrador.
        /// </summary>
        public int EsAdmin { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase persona con el nombre de usuario y el indicador de administrador especificados.
        /// </summary>
        /// <param name="usuario">El nombre de usuario de la persona.</param>
        /// <param name="esAdmin">Un valor que indica si la persona es administrador.</param>
        public persona(string usuario, int esAdmin)
        {
            Usuario = usuario;
            EsAdmin = esAdmin;
        }

        /// <summary>
        /// Devuelve una representación en forma de cadena de la persona.
        /// </summary>
        /// <returns>La representación en forma de cadena de la persona.</returns>
        public override string ToString()
        {
            return "nombre:" + Usuario + " esAdmin:" + EsAdmin;
        }
    }
}