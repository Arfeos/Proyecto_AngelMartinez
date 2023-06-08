using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using proyecto_Final.Presentacion;

namespace proyecto_Final.control
{
    /// <summary>
    /// Clase para acceder a la API de D&D 5e y obtener información sobre clases y razas.
    /// </summary>
    internal class Api
    {
        BBDD db = new BBDD();
        private string respuesta;

        private JsonDocument doc;

        List<string> listaF = new List<string>();
        string listaventajas;
        string nombre, pgolpe, rasgo, objetos, hechizos, nivel, cad;

        int bono, valor;

        /// <summary>
        /// Realiza una llamada a la API para obtener la lista de clases.
        /// </summary>
        public async Task llamarClases()
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = "api/classes/";
                using (var response = await httpClient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            listaF.Clear();
            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {
                listaF.Add(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString());
            }
        }

        /// <summary>
        /// Devuelve el nombre de una clase específica.
        /// </summary>
        /// <param name="clase">Nombre de la clase.</param>
        /// <returns>Nombre de la clase.</returns>
        public async Task<string> devolvernombre(string clase)
        {
            var direccion = new Uri("https://muna.ironarachne.com/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = clase;
                using (var response = await httpClient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                try
                {
                    doc = JsonDocument.Parse(respuesta);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha habido un error al leer los datos de la API. Por favor, compruebe que todos los datos son correctos.");
                    return "";
                }
            }
            string nombre;
            nombre = doc.RootElement.GetProperty("names")[0].ToString();
            return nombre;
        }

        /// <summary>
        /// Devuelve la lista de clases obtenida de la llamada a la API.
        /// </summary>
        /// <returns>Lista de clases.</returns>
        public List<string> devolverlista()
        {
            return listaF;
        }

        /// <summary>
        /// Realiza una llamada a la API para obtener información detallada de una clase.
        /// </summary>
        /// <param name="e">Índice de la clase en la lista.</param>
        public async Task llamarClase(int e)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = db.devolverurlclase(listaF[e]);
                using (var response = await httpClient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            nombre = doc.RootElement.GetProperty("name").ToString();
            pgolpe = doc.RootElement.GetProperty("hit_die").ToString();
            listaventajas = "Tendrás que elegir " + doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("choose").ToString() + " de estas opciones:\n";
            for (int i = 0; i < doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("from").GetProperty("options").GetArrayLength(); i++)
            {
                listaventajas += doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("from").GetProperty("options")[i].GetProperty("item").GetProperty("name").ToString() + "\n";
            }
            listaventajas += "Además, se obtendrán las siguientes ventajas:\n";
            for (int i = 0; i < doc.RootElement.GetProperty("proficiencies").GetArrayLength(); i++)
            {
                listaventajas += doc.RootElement.GetProperty("proficiencies")[i].GetProperty("name").ToString() + "\n";
            }
            objetos = "Estos objetos se obtienen de manera fija:\n";
            for (int i = 0; i < doc.RootElement.GetProperty("starting_equipment").GetArrayLength(); i++)
            {
                objetos += doc.RootElement.GetProperty("starting_equipment")[i].GetProperty("equipment").GetProperty("name").ToString() + " x" + doc.RootElement.GetProperty("starting_equipment")[i].GetProperty("quantity").ToString() + "\n";
            }
            objetos += "De estos, habrá que elegir la opción A, B o C:\n";
            for (int i = 0; i < doc.RootElement.GetProperty("starting_equipment_options").GetArrayLength(); i++)
            {
                objetos += doc.RootElement.GetProperty("starting_equipment_options")[i].GetProperty("desc").ToString() + "\n";
            }
        }
        /// <summary>
        /// Obtiene el dato devuelta por la API.
        /// </summary>
        /// <returns>El nombre de la clase.</returns>
        public string devolvernombre()
        {
            return nombre;
        }
        /// <summary>
        /// Obtiene el dato devuelta por la API.
        /// </summary>
        /// <returns>El nombre de la clase.</returns>
        public string devolverpgolpe()
        {
            return pgolpe;
        }
        /// <summary>
        /// Obtiene el dato devuelta por la API.
        /// </summary>
        /// <returns>El nombre de la clase.</returns>
        public string devolverventajas()
        {
            return listaventajas;
        }
        /// <summary>
        /// Obtiene el dato devuelta por la API.
        /// </summary>
        /// <returns>El nombre de la clase.</returns>
        public string devolverobjetos()
        {
            return objetos;
        }
        /// <summary>
        /// Llama a la API para obtener una lista de las subclases disponibles.
        /// </summary>
        /// <returns>Una tarea que se puede esperar hasta que se complete la llamada.</returns>
        internal async Task llamarSubClases()
        {

            var direccion = new Uri("https://www.dnd5eapi.co");
            using (var httpClient = new HttpClient { BaseAddress = direccion })

            {

                string consulta = "api/subclasses/";
                using (var response = await httpClient.GetAsync(consulta))
                {

                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            listaF.Clear();
            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {

                listaF.Add(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString());
            }

        }
        /// <summary>
        /// Llama a la API para obtener información detallada sobre una subclase específica.
        /// </summary>
        /// <param name="e">El índice de la clase en la lista devuelta por la API.</param>
        /// <returns>Una tarea que se puede esperar hasta que se complete la llamada.</returns>
        internal async Task llamarsubClase(int e)
        {

            var direccion = new Uri("https://www.dnd5eapi.co");
            using (var httpClient = new HttpClient { BaseAddress = direccion })

            {
                string consulta = db.devolverurlsubclase(listaF[e]);
                using (var response = await httpClient.GetAsync(consulta))
                {

                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            nombre = doc.RootElement.GetProperty("class").GetProperty("name").ToString();
            pgolpe = doc.RootElement.GetProperty("desc")[0].ToString();

        }
        /// <summary>
        /// Llama a la API para obtener información detallada sobre una Raza específica.
        /// </summary>
        /// <param name="e">El índice de la clase en la lista devuelta por la API.</param>
        /// <returns>Una tarea que se puede esperar hasta que se complete la llamada.</returns>
        internal async Task llamarRaza(int e)
        {

            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })

            {
                string consulta = db.devolverurlraza(listaF[e]);
                using (var response = await httpClient.GetAsync(consulta))
                {

                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            listaventajas = "";
            for (int i = 0; i < doc.RootElement.GetProperty("ability_bonuses").GetArrayLength(); i++)
            {
                listaventajas += doc.RootElement.GetProperty("ability_bonuses")[i].GetProperty("ability_score").GetProperty("name").ToString() + " +" + doc.RootElement.GetProperty("ability_bonuses")[i].GetProperty("bonus").ToString() + "\n";
            }
            nombre = doc.RootElement.GetProperty("language_desc").ToString();
            objetos = "";
            for (int i = 0; i < doc.RootElement.GetProperty("traits").GetArrayLength(); i++)
            {
                objetos += doc.RootElement.GetProperty("traits")[i].GetProperty("name").ToString() + "\n";
            }
            pgolpe = doc.RootElement.GetProperty("size_description").ToString();

        }
        /// <summary>
        /// Llama a la API para obtener una lista de las Razas disponibles.
        /// </summary>
        /// <returns>Una tarea que se puede esperar hasta que se complete la llamada.</returns>
        internal async Task llamarRazas()
        {

            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })

            {

                string consulta = "api/races/";
                using (var response = await httpClient.GetAsync(consulta))
                {

                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            listaF.Clear();
            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {

                listaF.Add(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString());
            }

        }
        /// <summary>
        /// Llama a la API para obtener una lista de las subRazas disponibles.
        /// </summary>
        /// <returns>Una tarea que se puede esperar hasta que se complete la llamada.</returns>
        internal async Task llamarsubRazas()
        {

            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })

            {

                string consulta = "api/subraces/";
                using (var response = await httpClient.GetAsync(consulta))
                {

                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            listaF.Clear();
            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {

                listaF.Add(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString());
            }


        }
        /// <summary>
        /// Llama a la API para obtener información detallada sobre una subRaza específica.
        /// </summary>
        /// <param name="e">El índice de la clase en la lista devuelta por la API.</param>
        /// <returns>Una tarea que se puede esperar hasta que se complete la llamada.</returns>
        internal async Task llamarsubRaza(int e)
        {

            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })

            {
                string consulta = db.devolverurlsubraza(listaF[e]);
                using (var response = await httpClient.GetAsync(consulta))
                {

                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            nombre = doc.RootElement.GetProperty("race").GetProperty("name").ToString();
            pgolpe = doc.RootElement.GetProperty("desc").ToString();

        }

/// <summary>
/// Realiza una llamada a la API para obtener información detallada de una clase.
/// </summary>
/// <param name="v">URL de la clase.</param>
internal async Task llamarClaseurl(string v)
{

    var direccion = new Uri("https://www.dnd5eapi.co/");
    using (var httpClient = new HttpClient { BaseAddress = direccion })
    {
        string consulta = v;
        using (var response = await httpClient.GetAsync(consulta))
        {
            respuesta = await response.Content.ReadAsStringAsync();
        }
        doc = JsonDocument.Parse(respuesta);
    }

    pgolpe = doc.RootElement.GetProperty("hit_die").ToString();
}

/// <summary>
/// Devuelve la velocidad de una raza específica.
/// </summary>
/// <param name="v">Nombre de la raza.</param>
/// <returns>Velocidad de la raza.</returns>
internal async Task<string> devolvervelocidad(string v)
{

    var direccion = new Uri("https://www.dnd5eapi.co/");
    using (var httpClient = new HttpClient { BaseAddress = direccion })
    {
        string consulta = db.devolverurlraza(v);
        using (var response = await httpClient.GetAsync(consulta))
        {
            respuesta = await response.Content.ReadAsStringAsync();
        }
        doc = JsonDocument.Parse(respuesta);
    }

    return doc.RootElement.GetProperty("speed").ToString();
}

/// <summary>
/// Agrega los datos de una raza al personaje.
/// </summary>
/// <param name="raza">Nombre de la raza.</param>
/// <param name="nombre">Nombre del personaje.</param>
internal async Task añadirdatosraza(string raza, string nombre)
{
    var direccion = new Uri("https://www.dnd5eapi.co/");
    using (var httpClient = new HttpClient { BaseAddress = direccion })
    {
        string consulta = db.devolverurlraza(raza);
        using (var response = await httpClient.GetAsync(consulta))
        {
            respuesta = await response.Content.ReadAsStringAsync();
        }
        doc = JsonDocument.Parse(respuesta);
    }
    for (int i = 0; i < doc.RootElement.GetProperty("ability_bonuses").GetArrayLength(); i++)
    {
        rasgo = doc.RootElement.GetProperty("ability_bonuses")[i].GetProperty("ability_score").GetProperty("name").ToString();
        if (rasgo == "INT")
            rasgo = "INTE";
        bono = doc.RootElement.GetProperty("ability_bonuses")[i].GetProperty("bonus").GetInt32();
        valor = int.Parse(await db.devolvervalor(rasgo, nombre));
        if (valor == 0)
            MessageBox.Show("error al cargar el valor " + rasgo);
        else
        {
            valor = valor + bono;
            await db.modificarrasgo(rasgo, valor, nombre);
        }
    }
    for (int i = 0; i < doc.RootElement.GetProperty("traits").GetArrayLength(); i++)
    {
        rasgo = doc.RootElement.GetProperty("traits")[i].GetProperty("name").ToString();
        await db.insertarrasgos(rasgo, nombre);
    }
}

/// <summary>
/// Agrega los datos de una subraza al personaje.
/// </summary>
/// <param name="subraza">Nombre de la subraza.</param>
/// <param name="nombre">Nombre del personaje.</param>
internal async Task añadirdatossubraza(string subraza, string nombre)
{
    var direccion = new Uri("https://www.dnd5eapi.co/");
    using (var httpClient = new HttpClient { BaseAddress = direccion })
    {
        if (subraza == "None")
        {
            return;
        }
        string consulta = db.devolverurlsubraza(subraza);
        using (var response = await httpClient.GetAsync(consulta))
        {
            respuesta = await response.Content.ReadAsStringAsync();
        }
        doc = JsonDocument.Parse(respuesta);
    }
    for (int i = 0; i < doc.RootElement.GetProperty("ability_bonuses").GetArrayLength(); i++)
    {
        rasgo = doc.RootElement.GetProperty("ability_bonuses")[i].GetProperty("ability_score").GetProperty("name").ToString();
        if (rasgo == "INT")
            rasgo = "INTE";
        bono = doc.RootElement.GetProperty("ability_bonuses")[i].GetProperty("bonus").GetInt32();
        valor = int.Parse(await db.devolvervalor(rasgo, nombre));
        if (valor == 0)
            MessageBox.Show("error al cargar el valor " + rasgo);
        else
        {
            valor = valor + bono;
            await db.modificarrasgo(rasgo, valor, nombre);
        }
    }
    for (int i = 0; i < doc.RootElement.GetProperty("racial_traits").GetArrayLength(); i++)
    {
        rasgo = doc.RootElement.GetProperty("racial_traits")[i].GetProperty("name").ToString();
        await db.insertarrasgos(rasgo, nombre);
    }
}

/// <summary>
/// Agrega los datos de una clase al personaje.
/// </summary>
/// <param name="clase">Nombre de la clase.</param>
/// <param name="nombre">Nombre del personaje.</param>
internal async Task añadirdatosclase(string clase, string nombre)
{
    var direccion = new Uri("https://www.dnd5eapi.co/");
    using (var httpClient = new HttpClient { BaseAddress = direccion })
    {
        string consulta = db.devolverurlclase(clase) + "/spells";
        using (var response = await httpClient.GetAsync(consulta))
        {
            respuesta = await response.Content.ReadAsStringAsync();
        }
        doc = JsonDocument.Parse(respuesta);
    }
    int limite = doc.RootElement.GetProperty("results").GetArrayLength();

    for (int e = 0; e < limite; e++)
    {
        hechizos = doc.RootElement.GetProperty("results")[e].GetProperty("name").ToString();
        bono = await devolverlvlhechizo(doc.RootElement.GetProperty("results")[e].GetProperty("url").ToString());
        db.añadirfich_hech(nombre, hechizos, bono);
    }
}

/// <summary>
/// Agrega los datos de una subclase al personaje.
/// </summary>
/// <param name="subclase">Nombre de la subclase.</param>
/// <param name="nombre">Nombre del personaje.</param>
internal async Task añadirdatossubclase(string subclase, string nombre)
{
    var direccion = new Uri("https://www.dnd5eapi.co/");
    using (var httpClient = new HttpClient { BaseAddress = direccion })
    {
        string consulta = db.devolverurlsubclase(subclase);
        using (var response = await httpClient.GetAsync(consulta))
        {
            respuesta = await response.Content.ReadAsStringAsync();
        }
        doc = JsonDocument.Parse(respuesta);
    }
    int limite = doc.RootElement.GetProperty("spells").GetArrayLength();

    for (int e = 0; e < limite; e++)
    {
        hechizos = doc.RootElement.GetProperty("spells")[e].GetProperty("spell").GetProperty("name").ToString();
        bono = await devolverlvlhechizo(doc.RootElement.GetProperty("spells")[e].GetProperty("spell").GetProperty("url").ToString());
        db.añadirfich_hech(nombre, hechizos, bono);
    }
}

/// <summary>
/// Obtiene el nivel del hechizo a partir de su URL.
/// </summary>
/// <param name="v">URL del hechizo.</param>
/// <returns>Nivel del hechizo.</returns>
private async Task<int> devolverlvlhechizo(string v)
{
    var direccion = new Uri("https://www.dnd5eapi.co/");
    using (var httpClient = new HttpClient { BaseAddress = direccion })
    {
        string consulta = v;
        using (var response = await httpClient.GetAsync(consulta))
        {
            respuesta = await response.Content.ReadAsStringAsync();
        }
        return JsonDocument.Parse(respuesta).RootElement.GetProperty("level").GetInt32();
    }
}
        /// <summary>
        /// Llena los hechizos en la base de datos a partir de la API.
        /// </summary>
        /// <param name="db">Instancia de la base de datos.</param>
        internal async Task llenarHechizos(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = "api/spells/";

                // Realiza una consulta a la API para obtener los datos de los hechizos
                using (var response = await httpClient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }

            int longi = doc.RootElement.GetProperty("results").GetArrayLength();
            for (int i = 0; i < longi; i++)
            {
                // Agrega los hechizos a la base de datos
                db.llenarhechizos(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
            }
        }

        /// <summary>
        /// Llena los rasgos en la base de datos a partir de la API.
        /// </summary>
        /// <param name="db">Instancia de la base de datos.</param>
        internal async Task llenarRasgos(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = "api/features";

                // Realiza una consulta a la API para obtener los datos de los rasgos
                using (var response = await httpClient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }

            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {
                // Agrega los rasgos a la base de datos
                db.llenarrasgos(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
            }
        }

        /// <summary>
        /// Llena los rasgos de raza en la base de datos a partir de la API.
        /// </summary>
        /// <param name="db">Instancia de la base de datos.</param>
        public async Task llenarrazarasgo(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpclient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = "api/traits";

                // Realiza una consulta a la API para obtener los datos de los rasgos de raza
                using (var response = await httpclient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }

            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {
                // Agrega los rasgos de raza a la base de datos
                db.llenarrasgos(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString());
            }
        }

        /// <summary>
        /// Añade los datos de clase por nivel a partir de la API.
        /// </summary>
        /// <param name="clase">Nombre de la clase.</param>
        /// <param name="nombre">Nombre del personaje.</param>
        /// <param name="niv">Nivel del personaje.</param>
        /// <returns>La bonificación de competencia de la clase.</returns>
        internal async Task<string> añadirdatosclaseporniv(string clase, string nombre, int niv)
        {
            try
            {
                var direccion = new Uri("https://www.dnd5eapi.co/");
                using (var httpclient = new HttpClient { BaseAddress = direccion })
                {
                    string consulta = db.devolverurlclase(clase) + "/levels/" + niv;

                    // Realiza una consulta a la API para obtener los datos de la clase por nivel
                    using (var response = await httpclient.GetAsync(consulta))
                    {
                        respuesta = await response.Content.ReadAsStringAsync();
                    }
                    doc = JsonDocument.Parse(respuesta);
                }

                for (int i = 0; i < doc.RootElement.GetProperty("features").GetArrayLength(); i++)
                {
                    if (doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString() == "Ability Score Improvement")
                    {
                        MessageBox.Show(await devolverdesc(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString()));

                        // Vuelve a obtener los datos de la clase por nivel y realiza alguna acción específica
                        using (var httpclient = new HttpClient { BaseAddress = direccion })
                        {
                            string consulta = db.devolverurlclase(clase) + "/levels/" + niv;
                            using (var response = await httpclient.GetAsync(consulta))
                            {
                                respuesta = await response.Content.ReadAsStringAsync();
                            }
                            doc = JsonDocument.Parse(respuesta);
                        }
                    }
                    else
                    {
                        // Inserta los rasgos en la base de datos
                        await db.insertarrasgos(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString(), nombre);
                    }
                }
                return doc.RootElement.GetProperty("prof_bonus").ToString();
            }
            catch (KeyNotFoundException ex)
            {
                return doc.RootElement.GetProperty("prof_bonus").ToString();
            }
        }

        /// <summary>
        /// Añade los datos de subclase por nivel a partir de la API.
        /// </summary>
        /// <param name="subclase">Nombre de la subclase.</param>
        /// <param name="nombre">Nombre del personaje.</param>
        /// <param name="niv">Nivel del personaje.</param>
        internal async Task añadirdatossubclaseporniv(string subclase, string nombre, int niv)
        {
            try
            {
                var direccion = new Uri("https://www.dnd5eapi.co/");
                using (var httpclient = new HttpClient { BaseAddress = direccion })
                {
                    string consulta = db.devolverurlsubclase(subclase) + "/levels/" + niv;

                    // Realiza una consulta a la API para obtener los datos de la subclase por nivel
                    using (var response = await httpclient.GetAsync(consulta))
                    {
                        respuesta = await response.Content.ReadAsStringAsync();
                    }
                    doc = JsonDocument.Parse(respuesta);
                }

                for (int i = 0; i < doc.RootElement.GetProperty("features").GetArrayLength(); i++)
                {
                    if (doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString() == "Ability Score Improvement")
                    {
                        MessageBox.Show(await devolverdesc(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString()));

                        // Vuelve a obtener los datos de la subclase por nivel y realiza alguna acción específica
                        using (var httpclient = new HttpClient { BaseAddress = direccion })
                        {
                            string consulta = db.devolverurlsubclase(subclase) + "/levels/" + niv;
                            using (var response = await httpclient.GetAsync(consulta))
                            {
                                respuesta = await response.Content.ReadAsStringAsync();
                            }
                            doc = JsonDocument.Parse(respuesta);
                        }
                    }
                    else
                    {
                        // Inserta los rasgos en la base de datos
                        await db.insertarrasgos(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString(), nombre);
                    }
                }
            }
            catch (KeyNotFoundException ex)
            {
                // Manejar excepción
            }
        }

        /// <summary>
        /// Elimina los datos de clase por nivel a partir de la API.
        /// </summary>
        /// <param name="clase">Nombre de la clase.</param>
        /// <param name="nom">Nombre del personaje.</param>
        /// <param name="cont">Nivel del personaje.</param>
        /// <returns>La bonificación de competencia de la clase.</returns>
        internal async Task<object> elimardatosclaseporniv(string clase, string nom, int cont)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpclient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = db.devolverurlclase(clase) + "/levels/" + cont;
                using (var response = await httpclient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            for (int i = 0; i < doc.RootElement.GetProperty("features").GetArrayLength(); i++)
            {
                if (doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString() == "Ability Score Improvement")
                {
                    MessageBox.Show("se debe eliminar lo aplicado a continuación: " + await devolverdesc(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString()));
                    using (var httpclient = new HttpClient { BaseAddress = direccion })
                    {
                        string consulta = db.devolverurlclase(clase) + "/levels/" + cont;
                        using (var response = await httpclient.GetAsync(consulta))
                        {
                            respuesta = await response.Content.ReadAsStringAsync();
                        }
                        doc = JsonDocument.Parse(respuesta);
                    }
                }
                else
                {
                    await db.Borrarrasgos(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString(), nom);
                }
            }
            return doc.RootElement.GetProperty("prof_bonus").ToString();
        }

        /// <summary>
        /// Elimina los datos de subclase por nivel a partir de la API.
        /// </summary>
        /// <param name="subclase">Nombre de la subclase.</param>
        /// <param name="nom">Nombre del personaje.</param>
        /// <param name="cont">Nivel del personaje.</param>
        internal async Task eliminardatossubclaseporniv(string subclase, string nom, int cont)
        {
            try
            {
                var direccion = new Uri("https://www.dnd5eapi.co/");
                using (var httpclient = new HttpClient { BaseAddress = direccion })
                {
                    string consulta = db.devolverurlsubclase(subclase) + "/levels/" + cont;
                    using (var response = await httpclient.GetAsync(consulta))
                    {
                        respuesta = await response.Content.ReadAsStringAsync();
                    }
                    doc = JsonDocument.Parse(respuesta);
                }
                for (int i = 0; i < doc.RootElement.GetProperty("features").GetArrayLength(); i++)
                {
                    if (doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString() == "Ability Score Improvement")
                    {
                        MessageBox.Show("se debe eliminar lo aplicado a continuación: " + await devolverdesc(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString()));
                        using (var httpclient = new HttpClient { BaseAddress = direccion })
                        {
                            string consulta = db.devolverurlsubclase(subclase) + "/levels/" + cont;
                            using (var response = await httpclient.GetAsync(consulta))
                            {
                                respuesta = await response.Content.ReadAsStringAsync();
                            }
                            doc = JsonDocument.Parse(respuesta);
                        }
                    }
                    else
                    {
                        await db.Borrarrasgos(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString(), nom);
                    }
                }
            }
            catch (KeyNotFoundException ex) { }
        }

        /// <summary>
        /// Muestra un aviso de las salvaciones basado en la clase actual del personaje.
        /// </summary>
        /// <param name="db">Instancia de la base de datos.</param>
        internal async Task avisarsalvacion(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpclient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = db.devolverurlclase(db.devolverclase());
                using (var response = await httpclient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            for (int i = 0; i < doc.RootElement.GetProperty("saving_throws").GetArrayLength(); i++)
            {
                MessageBox.Show("Marca el checkbox de salvación: " + doc.RootElement.GetProperty("saving_throws")[i].GetProperty("name").ToString().Replace("Skill:", ""));
            }
        }

        /// <summary>
        /// Muestra un aviso de las competencias basado en la raza y clase actual del personaje.
        /// </summary>
        /// <param name="db">Instancia de la base de datos.</param>
        internal async Task avisarcompetencias(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpclient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = db.devolverurlraza(db.devolverRaza());
                using (var response = await httpclient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            for (int i = 0; i < doc.RootElement.GetProperty("starting_proficiencies").GetArrayLength(); i++)
            {
                MessageBox.Show("Marca el checkbox de habilidad: " + doc.RootElement.GetProperty("starting_proficiencies")[i].GetProperty("name").ToString().Replace("Skill:", ""));
            }
            try
            {
                listaF.Clear();
                for (int i = 0; i < doc.RootElement.GetProperty("starting_proficiency_options").GetProperty("from").GetProperty("options").GetArrayLength(); i++)
                {
                    string a = doc.RootElement.GetProperty("starting_proficiency_options").GetProperty("from").GetProperty("options")[i].GetProperty("item").GetProperty("name").ToString().Replace("Skill:", "");
                    listaF.Add(a);
                }
                string listAsString = string.Join(Environment.NewLine, listaF);

                MessageBox.Show("Marca " + doc.RootElement.GetProperty("starting_proficiency_options").GetProperty("choose") + " de las siguientes habilidades: " + listAsString);
            }
            catch (KeyNotFoundException ex) { }

            using (var httpclient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = db.devolverurlclase(db.devolverclase());
                using (var response = await httpclient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            listaF.Clear();

            for (int i = 0; i < doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("from").GetProperty("options").GetArrayLength(); i++)
            {
                string a = doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("from").GetProperty("options")[i].GetProperty("item").GetProperty("name").ToString().Replace("Skill:", "");
                listaF.Add(a);
            }
            cad = string.Join(Environment.NewLine, listaF);
            MessageBox.Show("Marca " + doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("choose") + " de las siguientes habilidades: " + cad);
        }

        /// <summary>
        /// Devuelve la descripción de un rasgo basado en su nombre a partir de la API.
        /// </summary>
        /// <param name="v">Nombre del rasgo.</param>
        /// <returns>La descripción del rasgo.</returns>
        internal async Task<string> devolverdesc(string v)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpclient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = db.devolverurlrasgo(v);
                using (var response = await httpclient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            return doc.RootElement.GetProperty("desc").ToString().Replace("[\"", "").Replace("\",\"-", "").Replace("\"]", "");
        }

        /// <summary>
        /// Obtiene la habilidad de lanzamiento de hechizos de una clase a partir de la API.
        /// </summary>
        /// <param name="clase">Nombre de la clase.</param>
        /// <returns>La habilidad de lanzamiento de hechizos de la clase.</returns>
        internal async Task<string> sacarbono(string clase)
        {
            try
            {
                var direccion = new Uri("https://www.dnd5eapi.co/");
                using (var httpclient = new HttpClient { BaseAddress = direccion })
                {
                    string consulta = db.devolverurlclase(clase);
                    using (var response = await httpclient.GetAsync(consulta))
                    {
                        respuesta = await response.Content.ReadAsStringAsync();
                    }
                    doc = JsonDocument.Parse(respuesta);
                }
                return doc.RootElement.GetProperty("spellcasting").GetProperty("spellcasting_ability").GetProperty("name").ToString();
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show("Esta clase no tiene habilidades de lanzamiento de hechizos.");
                return null;
            };
        }
        /// <summary>
        /// Obtiene los niveles de lanzamiento de hechizos de una clase en un nivel específico a partir de la API.
        /// </summary>
        /// <param name="clase">Nombre de la clase.</param>
        /// <param name="niv">Nivel del personaje.</param>
        /// <returns>Una lista de los niveles de lanzamiento de hechizos en el nivel específico.</returns>
        public async Task<List<string>> sacarniveles(string clase, string niv)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpclient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = db.devolverurlclase(clase) + "/levels/" + niv;
                using (var response = await httpclient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            listaF.Clear();
            try
            {
                string a = doc.RootElement.GetProperty("spellcasting").GetProperty("spell_slots_level_1").ToString();
                listaF.Add(a);
                a = doc.RootElement.GetProperty("spellcasting").GetProperty("spell_slots_level_2").ToString();
                listaF.Add(a);
                a = doc.RootElement.GetProperty("spellcasting").GetProperty("spell_slots_level_3").ToString();
                listaF.Add(a);
                a = doc.RootElement.GetProperty("spellcasting").GetProperty("spell_slots_level_4").ToString();
                listaF.Add(a);
                a = doc.RootElement.GetProperty("spellcasting").GetProperty("spell_slots_level_5").ToString();
                listaF.Add(a);
                a = doc.RootElement.GetProperty("spellcasting").GetProperty("spell_slots_level_6").ToString();
                listaF.Add(a);
                a = doc.RootElement.GetProperty("spellcasting").GetProperty("spell_slots_level_7").ToString();
                listaF.Add(a);
                a = doc.RootElement.GetProperty("spellcasting").GetProperty("spell_slots_level_8").ToString();
                listaF.Add(a);
                a = doc.RootElement.GetProperty("spellcasting").GetProperty("spell_slots_level_9").ToString();
                listaF.Add(a);
            }
            catch (KeyNotFoundException ex) { }
            return listaF;
        }

        /// <summary>
        /// Devuelve la descripción de un hechizo basado en su nombre a partir de la API.
        /// </summary>
        /// <param name="v">Nombre del hechizo.</param>
        /// <returns>La descripción del hechizo.</returns>
        internal async Task<string> devolverdeschech(string v)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpclient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = db.devolverurlhechizo(v);
                using (var response = await httpclient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            return doc.RootElement.GetProperty("desc").ToString().Replace("[\"", "").Replace("\",\"-", "").Replace("\"]", "");
        }

        /// <summary>
        /// Llena la tabla de clases en la base de datos a partir de la API.
        /// </summary>
        /// <param name="db">Instancia de la base de datos.</param>
        internal async Task llenarClase(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = "api/classes";
                using (var response = await httpClient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {
                db.llenarClases(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
            }
        }

        /// <summary>
        /// Llena la tabla de razas en la base de datos a partir de la API.
        /// </summary>
        /// <param name="db">Instancia de la base de datos.</param>
        internal async Task llenarRaza(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = "api/races";
                using (var response = await httpClient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {
                db.llenarRazas(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
            }
        }

        /// <summary>
        /// Llena la tabla de subrazas en la base de datos a partir de la API.
        /// </summary>
        /// <param name="db">Instancia de la base de datos.</param>
        internal async Task llenarsubrazas(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = "api/subraces";
                using (var response = await httpClient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {
                db.llenarsubrazas(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
            }
        }

        /// <summary>
        /// Llena la tabla de subclases en la base de datos a partir de la API.
        /// </summary>
        /// <param name="db">Instancia de la base de datos.</param>
        public async Task llenarsubclases(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = "api/subclasses";
                using (var response = await httpClient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {
                db.llenarsubclases(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
            }
        }

       
    }
}
