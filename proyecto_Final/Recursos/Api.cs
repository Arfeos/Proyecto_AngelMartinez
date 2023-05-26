﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
namespace proyecto_Final.Recursos
{
    /// <summary>
    /// Clase para acceder a la API de D&D 5e y obtener información sobre clases y razas.
    /// </summary>
    internal class Api
    {
        BBDD db = new BBDD();
        private string respuesta;

        private JsonDocument doc;
        
        List<String> listaF = new List<string>();
        string listaventajas;
        string objetos;
        string nombre, pgolpe;
       
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
        public async Task<string> devolvernombre(string clase) {
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
                }catch (Exception ex) {
                    MessageBox.Show("ha habido un error al leer los datos de la api por favor compruebe que todos los datos son correctos");
                    return"";
                }
            }
            String nombre;
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
                string consulta =db.devolverurlclase(listaF[e]) ;
                using (var response = await httpClient.GetAsync(consulta))
                {

                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }
            nombre = doc.RootElement.GetProperty("name").ToString();
            pgolpe = doc.RootElement.GetProperty("hit_die").ToString();
            listaventajas = ("tendras que elegir " + doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("choose").ToString() + " de estas \n");
            for (int i = 0; i < doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("from").GetProperty("options").GetArrayLength(); i++)
            {

                listaventajas += doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("from").GetProperty("options")[i].GetProperty("item").GetProperty("url").ToString() + "\n";
            }
            listaventajas += "ademas se obtendran las siguientes ventajas \n";
            for (int i = 0; i < doc.RootElement.GetProperty("proficiencies").GetArrayLength(); i++)
            {

                listaventajas += (doc.RootElement.GetProperty("proficiencies")[i].GetProperty("url").ToString() + "\n");
            }
            objetos = ("estos objetos se obtienen de manera fija \n");
            for (int i = 0; i < doc.RootElement.GetProperty("starting_equipment").GetArrayLength(); i++)
            {
                objetos += (doc.RootElement.GetProperty("starting_equipment")[i].GetProperty("equipment").GetProperty("url").ToString() + "x" + doc.RootElement.GetProperty("starting_equipment")[i].GetProperty("quantity").ToString() + "\n");
            }
            objetos += ("de estos habra que elegir la a,b o c \n");
            for (int i = 0; i < doc.RootElement.GetProperty("starting_equipment_options").GetArrayLength(); i++)
            {
                objetos += (doc.RootElement.GetProperty("starting_equipment_options")[i].GetProperty("desc").ToString() + "\n");
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
                string consulta =db.devolverurlsubclase(listaF[e]);
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
            MessageBox.Show(doc.RootElement.GetProperty("proficiency_choices")[0].GetProperty("desc").ToString());
            pgolpe = doc.RootElement.GetProperty("hit_die").ToString();
        }

        internal async Task<string> devolvervelocidad(string v)
        {

            var direccion = new Uri("https://www.dnd5eapi.co/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })

            {
                string consulta =db.devolverurlraza(v);
                using (var response = await httpClient.GetAsync(consulta))
                {

                    respuesta = await response.Content.ReadAsStringAsync();
                }
                doc = JsonDocument.Parse(respuesta);
            }

            return doc.RootElement.GetProperty("speed").ToString();
        }
        //llenar tablas
        //internal async Task llenarClase(BBDD db)
        //{
        //    var direccion = new Uri("https://www.dnd5eapi.co/");
        //    using (var httpClient = new HttpClient { BaseAddress = direccion })
        //    {

        //        string consulta = "api/classes";
        //        using (var response = await httpClient.GetAsync(consulta))

        //        {
        //            respuesta = await response.Content.ReadAsStringAsync();
        //        }
        //        doc = JsonDocument.Parse(respuesta);
        //    }
        //    for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
        //    {
        //        db.llenarClases(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
        //    }
        //}
        //internal async Task llenarRaza(BBDD db)
        //{
        //    var direccion = new Uri("https://www.dnd5eapi.co/");
        //    using (var httpClient = new HttpClient { BaseAddress = direccion })

        //    {

        //        string consulta = "api/races";
        //        using (var response = await httpClient.GetAsync(consulta))
        //        {

        //            respuesta = await response.Content.ReadAsStringAsync();
        //        }
        //        doc = JsonDocument.Parse(respuesta);
        //    }
        //    for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
        //    {
        //        db.llenarRazas(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
        //    }
        //}
        //internal async Task llenarHechizos(BBDD db)
        //{
        //    var direccion = new Uri("https://www.dnd5eapi.co/");
        //    using (var httpClient = new HttpClient { BaseAddress = direccion })

        //    {

        //        string consulta = "api/spells/";
        //        using (var response = await httpClient.GetAsync(consulta))
        //        {

        //            respuesta = await response.Content.ReadAsStringAsync();
        //        }
        //        doc = JsonDocument.Parse(respuesta);
        //    }
        //    int longi = doc.RootElement.GetProperty("results").GetArrayLength();
        //    for (int i = 0; i < longi; i++)
        //    {
        //        db.llenarhechizos(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
        //    }

        //}

        //internal async Task llenarRasgos(BBDD db)
        //{
        //    var direccion = new Uri("https://www.dnd5eapi.co/");
        //    using (var httpClient = new HttpClient { BaseAddress = direccion })

        //    {

        //        string consulta = "api/features";
        //        using (var response = await httpClient.GetAsync(consulta))
        //        {

        //            respuesta = await response.Content.ReadAsStringAsync();
        //        }
        //        doc = JsonDocument.Parse(respuesta);
        //    }

        //    for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
        //    {
        //        db.llenarRasgos(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
        //    }
        //}
        //internal async Task llenarsubrazas(BBDD db)
        //{
        //    var direccion = new Uri("https://www.dnd5eapi.co/");
        //    using (var httpClient = new HttpClient { BaseAddress = direccion })
        //    {

        //        string consulta = "api/subraces";
        //        using (var response = await httpClient.GetAsync(consulta))
        //        {

        //            respuesta = await response.Content.ReadAsStringAsync();
        //        }
        //        doc = JsonDocument.Parse(respuesta);
        //    }
        //    for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
        //    {
        //        db.llenarsubrazas(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
        //    }
        //}

        //public async Task llenarRazarasgo(BBDD db)
        //{
        //    var direccion = new Uri("https://www.dnd5eapi.co/");
        //    using (var httpClient = new HttpClient { BaseAddress = direccion })

        //    {

        //        string consulta = "api/traits";
        //        using (var response = await httpClient.GetAsync(consulta))
        //        {

        //            respuesta = await response.Content.ReadAsStringAsync();
        //        }
        //        doc = JsonDocument.Parse(respuesta);
        //    }
        //    for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
        //    {
        //        db.llenarRazarasgo(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
        //    }
        //}

        //public async Task llenarsubclases(BBDD db)
        //{
        //    var direccion = new Uri("https://www.dnd5eapi.co/");
        //    using (var httpClient = new HttpClient { BaseAddress = direccion })

        //    {

        //        string consulta = "api/subclasses";
        //        using (var response = await httpClient.GetAsync(consulta))
        //        {

        //            respuesta = await response.Content.ReadAsStringAsync();
        //        }
        //        doc = JsonDocument.Parse(respuesta);
        //    }
        //    for (int i = 0; i < doc.RootElement.GetProperty("results").GetArrayLength(); i++)
        //    {
        //        db.llenarsubclases(doc.RootElement.GetProperty("results")[i].GetProperty("name").ToString(), doc.RootElement.GetProperty("results")[i].GetProperty("url").ToString());
        //    }
        //}
    } 
}
