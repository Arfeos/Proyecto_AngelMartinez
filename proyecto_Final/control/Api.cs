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
                    MessageBox.Show("ha habido un error al leer los datos de la api por favor compruebe que todos los datos son correctos");
                    return "";
                }
            }
            string nombre;
            nombre = doc.RootElement.GetProperty("names")[0].ToString();
            return nombre;
        }
        public List<string> devolverlista()
        {

            return listaF;
        }

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
            listaventajas = "tendras que elegir " + doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("choose").ToString() + " de estas \n";
            for (int i = 0; i < doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("from").GetProperty("options").GetArrayLength(); i++)
            {

                listaventajas += doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("from").GetProperty("options")[i].GetProperty("item").GetProperty("name").ToString() + "\n";
            }
            listaventajas += "ademas se obtendran las siguientes ventajas \n";
            for (int i = 0; i < doc.RootElement.GetProperty("proficiencies").GetArrayLength(); i++)
            {

                listaventajas += doc.RootElement.GetProperty("proficiencies")[i].GetProperty("name").ToString() + "\n";
            }
            objetos = "estos objetos se obtienen de manera fija \n";
            for (int i = 0; i < doc.RootElement.GetProperty("starting_equipment").GetArrayLength(); i++)
            {
                objetos += doc.RootElement.GetProperty("starting_equipment")[i].GetProperty("equipment").GetProperty("name").ToString() + "x" + doc.RootElement.GetProperty("starting_equipment")[i].GetProperty("quantity").ToString() + "\n";
            }
            objetos += "de estos habra que elegir la a,b o c \n";
            for (int i = 0; i < doc.RootElement.GetProperty("starting_equipment_options").GetArrayLength(); i++)
            {
                objetos += doc.RootElement.GetProperty("starting_equipment_options")[i].GetProperty("desc").ToString() + "\n";
            }
            ;
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
        internal async Task llenarHechizos(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })

            {

                string consulta = "api/spells/";
                using (var response = await httpClient.GetAsync(consulta))
                {

                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            int longi = doc.RootElement.GetProperty("results").GetArrayLength();
            for (int i = 0; i < longi; i++)
            {
                db.llenarhechizos(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
            }

        }

        internal async Task llenarRasgos(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })

            {

                string consulta = "api/features";
                using (var response = await httpClient.GetAsync(consulta))
                {

                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }

            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {
                db.llenarrasgos(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
            }
        }
        public async Task llenarrazarasgo(BBDD db)
        {
            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpclient = new HttpClient { BaseAddress = direccion })

            {

                string consulta = "api/traits";
                using (var response = await httpclient.GetAsync(consulta))
                {

                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
            {
                db.llenarrasgos(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString());
            }
        }

        internal async Task<string> añadirdatosclaseporniv(string clase, string nombre, int niv)
        {
            try
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
                for (int i = 0; i < doc.RootElement.GetProperty("features").GetArrayLength(); i++)
                {
                    if (doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString() == "Ability Score Improvement")
                    {
                        MessageBox.Show(await devolverdesc(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString()));
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
                        await db.insertarrasgos(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString(), nombre);
                    }
                }
                return doc.RootElement.GetProperty("prof_bonus").ToString();
            }
            catch (KeyNotFoundException ex) { return doc.RootElement.GetProperty("prof_bonus").ToString(); }
        }

        internal async Task añadirdatossubclaseporniv(string subclase, string nombre, int niv)
        {
            try
            {
                var direccion = new Uri("https://www.dnd5eapi.co/");
                using (var httpclient = new HttpClient { BaseAddress = direccion })

                {

                    string consulta = db.devolverurlsubclase(subclase) + "/levels/" + niv;
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
                        await db.insertarrasgos(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString(), nombre);
                    }
                }
            }
            catch (KeyNotFoundException ex) { }
        }

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
                    MessageBox.Show("se debe eliminar lo aplicado acontinaucion:" + await devolverdesc(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString()));
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
                        MessageBox.Show("se debe eliminar lo aplicado acontinaucion:" + await devolverdesc(doc.RootElement.GetProperty("features")[i].GetProperty("name").ToString()));
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
                MessageBox.Show("marca el checkbox de salvacion : " + doc.RootElement.GetProperty("saving_throws")[i].GetProperty("name").ToString().Replace("Skill:", ""));
            }
        }

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
                MessageBox.Show("marca el checkbox de habilidad : " + doc.RootElement.GetProperty("starting_proficiencies")[i].GetProperty("name").ToString().Replace("Skill:", ""));
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

                MessageBox.Show("marca " + doc.RootElement.GetProperty("starting_proficiency_options").GetProperty("choose") + " de las siguientes habilidades:" + listAsString);
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
            MessageBox.Show("marca " + doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("choose") + " de las siguientes habilidades:" + cad);

        }

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
                MessageBox.Show("esta clase carece de hechizos");
                return null;
            };
        }
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

        //llenar tablas
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
