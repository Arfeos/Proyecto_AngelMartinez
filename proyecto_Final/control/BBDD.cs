using Google.Protobuf.Collections;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using proyecto_Final.Presentacion;
using proyecto_Final.Recursos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace proyecto_Final.control
{
    /// <summary>
    /// Clase que define los métodos de la base de datos.
    /// </summary>
    public class BBDD
    {
        private MySqlConnection conexion;
        private MySqlCommand comando;
        private MySqlDataReader reader;
        int esAdministrador, nivel;
        private string us, STR, DEX, CON, INT, WIS, carisma, clase, raza, subclase, subraza, dueño;
        List<persona> listaper;
        List<string> listastr;
        /// <summary>
        /// Constructor de la clase BBDD. Inicializa una nueva instancia de la clase MySqlConnection.
        /// </summary>
        public BBDD()
        {
            conexion = new MySqlConnection("Server=127.0.0.1;port=3306;user=root;password=CIFP1;database=rol");
        }

        /// <summary>
        /// Método estático que convierte una cadena en su hash SHA256 correspondiente.
        /// </summary>
        /// <param name="con">Cadena a convertir en hash.</param>
        /// <returns>Hash SHA256 de la cadena.</returns>
        public static string pasarSHA256(string con)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(con));
            for (int i = 0; i < stream.Length; i++)
                sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        /// <summary>
        /// Conecta a la base de datos con el usuario y contraseña proporcionados.
        /// </summary>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <param name="contraseña">Contraseña del usuario.</param>
        /// <returns>True si la conexión es exitosa, False en caso contrario.</returns>
        public bool Conectar(string usuario, string contraseña)
        {
            string con = pasarSHA256(contraseña);
            comando = new MySqlCommand("SELECT * FROM usuarios WHERE nom_usuario=@usuario and contraseña=@contraseña and Activo=1", conexion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@contraseña", con);
            try
            {
                conexion.Open();
                reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["EsAdmin"].ToString() != null)
                        esAdministrador = int.Parse(reader["EsAdmin"].ToString());
                    if (reader["nom_usuario"].ToString() != null)
                        us = reader["nom_usuario"].ToString();

                    reader.Close();
                    conexion.Close();
                    return true;
                }
                reader.Close();
                conexion.Close();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Devuelve el valor del indicador de administrador.
        /// </summary>
        /// <returns>Valor del indicador de administrador.</returns>
        public int EsAdministrador()
        {
            return esAdministrador;
        }

        /// <summary>
        /// Devuelve el nombre de usuario actual.
        /// </summary>
        /// <returns>Nombre de usuario actual.</returns>
        public string EsUsuario()
        {
            return us;
        }
        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">El nombre de usuario.</param>
        /// <param name="contraseña">La contraseña del usuario.</param>
        /// <param name="correo">El correo electrónico del usuario.</param>
        /// <param name="administrador">Indica si el usuario es administrador (1) o no (0).</param>
        /// <returns>True si el usuario se insertó correctamente, False si ocurrió un error.</returns>
        public async Task<bool> insertarusuarios(string usuario, string contraseña, string correo, int administrador)
        {
            string con = pasarSHA256(contraseña);
            conexion.Open();
            MySqlTransaction transaction = conexion.BeginTransaction();
            using (MySqlCommand comando = new MySqlCommand("INSERT INTO usuarios (nom_usuario, contraseña, EsAdmin ,Correo) VALUES (@usuario, @contraseña,@administrador, @Correo)", conexion))
            {
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@contraseña", con);
                comando.Parameters.AddWithValue("@administrador", administrador);
                comando.Parameters.AddWithValue("@Correo", correo);

                try
                {
                    comando.ExecuteNonQuery();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    conexion.Close();
                    return false;
                }
            }

        }
        /// <summary>
        /// Borra un usuario de la base de datos.
        /// </summary>
        /// <param name="usuario">El nombre de usuario a borrar.</param>
        public void borrar(string usuario)
        {
            bool borrar = true;
            conexion.Open();
            string query = "UPDATE usuarios SET Activo=0 WHERE nom_usuario =@usuario";
            comando = new MySqlCommand("SELECT * FROM usuarios WHERE nom_usuario=@usuario", conexion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            reader = comando.ExecuteReader();
            if (reader.Read())
            {
                if (reader["EsAdmin"].ToString() == "1")
                {
                    MessageBox.Show("no se puede eliminar a un administrador");
                    borrar = false;
                }

            }
            reader.Close();
            if (borrar)
            {
                comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.ExecuteNonQuery();
            }
            conexion.Close();

        }
        /// <summary>
        /// Recupera una lista de objetos persona de la base de datos.
        /// </summary>
        /// <returns>La lista de objetos persona o null en caso de error.</returns>
        public List<persona> recoger()
        {

            listaper = new List<persona>();
            comando = new MySqlCommand("SELECT * FROM usuarios where Activo=1", conexion);

            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                while (reader.Read())
                {

                    persona t = new persona(reader["nom_usuario"].ToString(), int.Parse(reader["EsAdmin"].ToString()));

                    listaper.Add(t);
                }
                reader.Close();
                conexion.Close();
                return listaper;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                return null;
            }

        }
        /// <summary>
        /// modifica un usuario en la base de datos
        /// </summary>
        /// <param name="nombre">El nuevo nombre de usuario.</param>
        /// <param name="oldnom">El nombre anterior del usuario.</param>
        /// <param name="admin">Indica si el usuario es administrador (1) o no (0).</param>
        internal async Task modificar(string nombre, int admin, string oldnom)
        {
            conexion.Open();
            string query = "update usuarios set nom_usuario=@nombre ,EsAdmin=@admin WHERE nom_usuario =@oldnom";
            comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@admin", admin);
            comando.Parameters.AddWithValue("@oldnom", oldnom);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        /// <summary>
        /// Devuelve una lista de nombres de fichas asociadas a un usuario.
        /// </summary>
        /// <param name="nom">El nombre de usuario.</param>
        /// <returns>La lista de nombres de fichas o null en caso de error.</returns>
        internal List<string> devolverfichas(string nom)
        {
            listastr = new List<string>();
            comando = new MySqlCommand("SELECT Nombre from ficha where nom_usuario=@nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", nom);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    string a = reader["Nombre"].ToString();
                    listastr.Add(a);
                }
                reader.Close();
                conexion.Close();
                return listastr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }
        }

        /// <summary>
        /// Borra una ficha y sus datos relacionados de la base de datos.
        /// </summary>
        /// <param name="v">El nombre de la ficha a borrar.</param>
        internal void borrarficha(string v)
        {
            conexion.Open();
            string query = "DELETE FROM ficha_rasgo WHERE nombre_personaje=@nombre";
            comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            comando.ExecuteNonQuery();
            query = "DELETE FROM ficha_hechizo WHERE nom_per=@nombre";
            comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            comando.ExecuteNonQuery();
            query = "DELETE FROM ficha WHERE Nombre=@nombre";
            comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        /// <summary>
        /// Devuelve una lista de nombres de las clases.
        /// </summary>
        /// <returns>La lista con las clases o null en caso de error.</returns>
        internal List<string> devolverClases()
        {
            listastr = new List<string>();
            comando = new MySqlCommand("SELECT Nombre from Clase", conexion);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    string a = reader["Nombre"].ToString();
                    listastr.Add(a);
                }
                reader.Close();
                conexion.Close();
                return listastr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }

        }
        /// <summary>
        /// Devuelve una lista de nombres de las raza.
        /// </summary>
        /// <returns>La lista de nombres de razas o null en caso de error.</returns>
        internal List<string> devolverRazas()
        {
            listastr = new List<string>();
            comando = new MySqlCommand("SELECT Nombre from Raza", conexion);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    string a = reader["Nombre"].ToString();
                    listastr.Add(a);
                }
                reader.Close();
                conexion.Close();
                return listastr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }
        }
        /// <summary>
        /// Devuelve una lista de nombres de las subclases de cierta clase.
        /// </summary>
        /// /// <param name="v">clase a comprobar.</param>
        /// <returns>La lista de nombres de subclase o null en caso de error.</returns>
        internal List<string> devolversubclase(string v)
        {
            listastr = new List<string>();
            comando = new MySqlCommand("SELECT Nombre from subclase where clase_padre=@nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    string a = reader["Nombre"].ToString();
                    listastr.Add(a);
                }
                reader.Close();
                conexion.Close();
                return listastr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }
        }
        /// <summary>
        /// Devuelve una lista de nombres de las subrazas de cierta raza.
        /// </summary>
        /// /// <param name="v">raza a comprobar.</param>
        /// <returns>La lista de nombres de raza o null en caso de error.</returns>
        internal IEnumerable devolversubrazas(string v)
        {
            listastr = new List<string>();
            comando = new MySqlCommand("SELECT nombre from subraza where raza_padre=@nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                while (reader.Read())
                {

                    string a = reader["Nombre"].ToString();

                    listastr.Add(a);
                }
                reader.Close();
                conexion.Close();
                return listastr;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }
        }
        /// <summary>
        /// Inserta una nueva ficha en la base de datos.
        /// </summary>
        /// <param name="clase">La clase de la ficha.</param>
        /// <param name="subclase">La subclase de la ficha.</param>
        /// <param name="raza">La raza de la ficha.</param>
        /// <param name="subraza">La subraza de la ficha.</param>
        /// <param name="nombre">El nombre de la ficha.</param>
        /// <param name="usuario">El nombre de usuario asociado a la ficha.</param>
        /// <param name="STR">El valor de la estadística de Fuerza (STR).</param>
        /// <param name="DEX">El valor de la estadística de Destreza (DEX).</param>
        /// <param name="CON">El valor de la estadística de Constitución (CON).</param>
        /// <param name="WIS">El valor de la estadística de Sabiduría (WIS).</param>
        /// <param name="INT">El valor de la estadística de Inteligencia (INT).</param>
        /// <param name="CAR">El valor de la estadística de Carisma (CAR).</param>
        internal void insertarficha(string clase, string subclase, string raza, string subraza, string nombre, string usuario, int STR, int DEX, int CON, int WIS, int INT, int CAR)
        {
            conexion.Open();
            MySqlTransaction transaction = conexion.BeginTransaction();
            using (MySqlCommand comando = new MySqlCommand("INSERT INTO ficha (Nombre,nom_usuario,Clase,Subclase,Raza,subraza,STR,DEX,CON,WIS,INTE,CHA) VALUES (@nombre, @usuario, @Clase, @subclase, @Raza, @subraza, @STR, @DEX, @CON, @WIS, @INT, @CAR)", conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@Clase", clase);
                comando.Parameters.AddWithValue("@subclase", subclase);
                comando.Parameters.AddWithValue("@Raza", raza);
                comando.Parameters.AddWithValue("@subraza", subraza);
                comando.Parameters.AddWithValue("@STR", STR);
                comando.Parameters.AddWithValue("@DEX", DEX);
                comando.Parameters.AddWithValue("@CON", CON);
                comando.Parameters.AddWithValue("@WIS", WIS);
                comando.Parameters.AddWithValue("@INT", INT);
                comando.Parameters.AddWithValue("@CAR", CAR);
                try
                {
                    comando.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ha ocurrido un error al ejecutar la creacion");
                    conexion.Close();
                }
            }
        }
        /// <summary>
        /// Inserta una nueva datos en la base de datos de ficha_hechizos.
        /// </summary>
        /// <param name="nombre">nombre de la ficha que se le va a agregar el hechizo.</param>
        /// <param name="hechizos">hechizo que se va a agregar.</param>
        /// <param name="nivel">nivel del hechizo.</param>

        internal void añadirfich_hech(string nombre, string hechizos, int nivel)
        {
            conexion.Open();
            MySqlTransaction transaction = conexion.BeginTransaction();
            using (MySqlCommand comando = new MySqlCommand("INSERT INTO ficha_hechizo  VALUES (@nombre,@hechizo,@niv)", conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@hechizo", hechizos);
                comando.Parameters.AddWithValue("@niv", nivel);

                try
                {
                    comando.ExecuteNonQuery();
                    transaction.Commit();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ha ocurrido un error al ejecutar la creacion");
                    conexion.Close();
                }
            }
        }
        /// <summary>
        /// Devuelve la url de la clase
        /// </summary>
        /// <param name="v">clase que se desea la url.</param>
        /// <returns>la url de la clase</returns>
        internal string devolverurlclase(string v)
        {
            conexion.Open();

            comando = new MySqlCommand("SELECT indice from Clase where Nombre=@nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", v);

            reader = comando.ExecuteReader();
            try
            {

                reader.Read();
                string a = reader["indice"].ToString();
                reader.Close();
                conexion.Close();
                return a;




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }
        }
        /// <summary>
        /// Devuelve la url del rasgo
        /// </summary>
        /// <param name="v">rasgo que se desea la url.</param>
        /// <returns>la url del rasgo</returns>
        internal string devolverurlrasgo(string v)
        {
            conexion.Open();

            comando = new MySqlCommand("SELECT indice from rasgos where Nombre=@nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", v);

            reader = comando.ExecuteReader();
            try
            {

                reader.Read();
                string a = reader["indice"].ToString();
                reader.Close();
                conexion.Close();
                return a;




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }
        }
        /// <summary>
        /// Devuelve la url de la subclase
        /// </summary>
        /// <param name="v">subclase que se desea la url.</param>
        /// <returns>la url de la subclase</returns>
        internal string devolverurlsubclase(string v)
        {

            comando = new MySqlCommand("SELECT indice from subclase where Nombre=@nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {

                reader.Read();
                string a = reader["indice"].ToString();
                reader.Close();
                conexion.Close();
                return a;




            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }
        }
        /// <summary>
        /// Devuelve la url de la raza
        /// </summary>
        /// <param name="v">raza que se desea la url.</param>
        /// <returns>la url de la raza</returns>
        internal string devolverurlraza(string v)
        {

            comando = new MySqlCommand("SELECT indice from raza where Nombre=@nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {

                reader.Read();
                string a = reader["indice"].ToString();
                reader.Close();
                conexion.Close();
                return a;




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }
        }
        /// <summary>
        /// Devuelve la url de la subraza
        /// </summary>
        /// <param name="v">subraza que se desea la url.</param>
        /// <returns>la url de la subraza</returns>
        internal string devolverurlsubraza(string v)
        {

            comando = new MySqlCommand("SELECT indice from subraza where Nombre=@nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {

                reader.Read();
                string a = reader["indice"].ToString();
                reader.Close();
                conexion.Close();
                return a;




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }
        }
        /// <summary>
        /// carga los datos de una ficha
        /// </summary>
        /// <param name="v">nombre de la ficha.</param>

        internal async Task cargarficha(string v)
        {
            comando = new MySqlCommand("SELECT * FROM ficha WHERE  Nombre=@nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                reader.Read();
                clase = reader["Clase"].ToString();
                raza = reader["Raza"].ToString();
                subclase = reader["subclase"].ToString();
                subraza = reader["subraza"].ToString();
                STR = reader["STR"].ToString();
                DEX = reader["DEX"].ToString();
                CON = reader["CON"].ToString();
                INT = reader["INTE"].ToString();
                WIS = reader["WIS"].ToString();
                carisma = reader["CHA"].ToString();
                dueño = reader["nom_usuario"].ToString();
                nivel = int.Parse(reader["nivel"].ToString());
                reader.Close();
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
            }
        }
        /// <summary>
        /// Devuelve el valor de la estadística de Fuerza (STR).
        /// </summary>
        /// <returns>El valor de la estadística de Fuerza.</returns>
        internal string devolverSTR()
        {
            return STR;
        }

        /// <summary>
        /// Devuelve el valor de la estadística de Destreza (DEX).
        /// </summary>
        /// <returns>El valor de la estadística de Destreza.</returns>
        internal string devolverDEX()
        {
            return DEX;
        }

        /// <summary>
        /// Devuelve el valor de la estadística de Constitución (CON).
        /// </summary>
        /// <returns>El valor de la estadística de Constitución.</returns>
        internal string devolverCON()
        {
            return CON;
        }

        /// <summary>
        /// Devuelve el valor de la estadística de Inteligencia (INT).
        /// </summary>
        /// <returns>El valor de la estadística de Inteligencia.</returns>
        internal string devolverINT()
        {
            return INT;
        }

        /// <summary>
        /// Devuelve el valor de la estadística de Sabiduría (WIS).
        /// </summary>
        /// <returns>El valor de la estadística de Sabiduría.</returns>
        internal string devolverWIS()
        {
            return WIS;
        }

        /// <summary>
        /// Devuelve el valor de la estadística de Carisma (CAR).
        /// </summary>
        /// <returns>El valor de la estadística de Carisma.</returns>
        internal string devolverCAR()
        {
            return carisma;
        }

        /// <summary>
        /// Devuelve la raza del personaje.
        /// </summary>
        /// <returns>La raza del personaje.</returns>
        internal string devolverRaza()
        {
            return raza;
        }

        /// <summary>
        /// Devuelve la subraza del personaje.
        /// </summary>
        /// <returns>La subraza del personaje.</returns>
        internal string devolversubraza()
        {
            return subraza;
        }

        /// <summary>
        /// Devuelve la clase del personaje.
        /// </summary>
        /// <returns>La clase del personaje.</returns>
        internal string devolverclase()
        {
            return clase;
        }

        /// <summary>
        /// Devuelve la subclase del personaje.
        /// </summary>
        /// <returns>La subclase del personaje.</returns>
        internal string devolversubclase()
        {
            return subclase;
        }

        /// <summary>
        /// Devuelve el dueño de la ficha.
        /// </summary>
        /// <returns>El dueño de la ficha.</returns>
        internal string devolverdueño()
        {
            return dueño;
        }

        /// <summary>
        /// Devuelve el nivel del personaje.
        /// </summary>
        /// <returns>El nivel del personaje.</returns>
        internal int devolvernivel()
        {
            return nivel;
        }

        /// <summary>
        /// Devuelve la URL de la imagen asociada a una clase.
        /// </summary>
        /// <param name="clase">La clase del personaje.</param>
        /// <returns>La URL de la imagen asociada a la clase.</returns>
        internal string devolverimagen(string clase)
        {
            comando = new MySqlCommand("SELECT url FROM imagenes WHERE Clase=@Clase", conexion);
            comando.Parameters.AddWithValue("@Clase", clase);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                reader.Read();
                string st = reader["url"].ToString();
                conexion.Close();
                reader.Close();
                return st;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }
        }
        //llenar las bases de datos
        internal void llenarhechizos(string nombre, string indice)
        {
            using (MySqlCommand comando = new MySqlCommand("insert into hechizos (nombre, indice) values (@nombre, @indice)", conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@indice", indice);

                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    conexion.Close();
                }
            }
        }
        internal void llenarrasgos(string nombre, string indice)
        {
            using (MySqlCommand comando = new MySqlCommand("insert into rasgos (nombre, indice) values (@nombre, @indice)", conexion))
            {
                comando.Parameters.AddWithValue("@indice", indice);
                comando.Parameters.AddWithValue("@nombre", nombre);
                try
                {

                    comando.ExecuteNonQuery();
                    ;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    conexion.Close();
                }
            }
        }
        internal void llenarClases(string nombre, string indice)
        {

            using (comando = new MySqlCommand("insert into clase (nombre, indice) values (@nombre, @indice)", conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@indice", indice);

                try
                {
                    comando.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    conexion.Close();
                }
            }
        }
        internal void llenarRazas(string nombre, string indice)
        {

            using (comando = new MySqlCommand("insert into raza (nombre, indice) values (@nombre, @indice)", conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@indice", indice);

                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    conexion.Close();
                }
            }
        }
        internal void llenarsubrazas(string nombre, string indice)
        {

            using (MySqlCommand comando = new MySqlCommand("insert into subraza (nombre, indice) values (@nombre, @indice)", conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@indice", indice);

                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    conexion.Close();
                }
            }
        }
        internal void llenarsubclases(string nombre, string indice)
        {
            using (MySqlCommand comando = new MySqlCommand("insert into subclase (nombre, indice) values (@nombre, @indice)", conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@indice", indice);


                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    conexion.Close();
                }
            }
        }

        internal void conectar()
        {
            conexion.Open();
        }
        internal void desconectar()
        {
            conexion.Close();
        }

        internal async Task<string> devolvervalor(string rasgo, string nombre)
        {

            comando = new MySqlCommand("SELECT * FROM ficha WHERE  Nombre=@nombre", conexion);
            comando.Parameters.AddWithValue("@rasgo", rasgo);
            comando.Parameters.AddWithValue("@nombre", nombre);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                string st = null;
                reader.Read();
                if (rasgo == "STR")
                {

                    st = reader["STR"].ToString();
                }
                if (rasgo == "DEX")
                {

                    st = reader["DEX"].ToString();
                }
                if (rasgo == "CON")
                {

                    st = reader["CON"].ToString();
                }
                if (rasgo == "INTE")
                {

                    st = reader["INTE"].ToString();
                }
                if (rasgo == "WIS")
                {

                    st = reader["WIS"].ToString();
                }
                if (rasgo == "CHA")
                {
                    st = reader["CHA"].ToString();
                }
                conexion.Close();

                reader.Close();
                return st;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }

        }

        internal async Task modificarrasgo(string rasgo, int valor, string nombre)
        {
            conexion.Open();
            string query = "update ficha set " + rasgo + "=@valor WHERE Nombre ='" + nombre + "'";
            comando = new MySqlCommand(query, conexion);


            comando.Parameters.AddWithValue("@valor", valor);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        internal async Task insertarrasgos(string rasgo, string nombre)
        {
            if (!consultarrasgo(nombre, rasgo))
            {
                conexion.Open();
                string query = "insert into ficha_rasgo values(@nombre, @rasgo )";
                comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@rasgo", rasgo);
                comando.ExecuteNonQuery();
                conexion.Close();
            }
        }

        internal async Task<List<string>> devolverRasgos(string nom)
        {
            listastr = new List<string>();
            comando = new MySqlCommand("SELECT * FROM ficha_rasgo WHERE  nombre_personaje=@nom", conexion);
            comando.Parameters.AddWithValue("@nom", nom);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {

                while (reader.Read())
                {

                    string a = reader["indice_rasgo"].ToString();

                    listastr.Add(a);
                }
                reader.Close();
                conexion.Close();
                return listastr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                conexion.Close();
                return null;
            }

        }

        internal async Task guardardoc(string contenido, string ficha, string nombre)
        {
            if (comprobardocumento(ficha))
            {
                borrardocumento(ficha);
            }
            comando = new MySqlCommand("insert into documentos values(@nombre,@contenido,@ficha)", conexion);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@contenido", contenido);
            comando.Parameters.AddWithValue("@ficha", ficha);
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        internal void borrardocumento(string ficha)
        {
            conexion.Open();
            string query = "DELETE FROM documentos WHERE nom_doc=@nombre";
            comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nombre", ficha);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        internal bool comprobardocumento(string nombre)
        {
            string query = "SELECT COUNT(*) FROM documentos WHERE nom_doc = @nombre";

            using (comando = new MySqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);

                conexion.Open();
                long count = (long)comando.ExecuteScalar();
                conexion.Close();
                return count > 0;
            }
        }

        internal async Task<string[]> cargarfichabd(string nombre)
        {
            comando = new MySqlCommand("select * from documentos WHERE nom_doc = @nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", nombre);
            conexion.Open();
            using (MySqlDataReader reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    string cad = reader["Contenido"].ToString();
                    string[] lista = cad.Replace("/r", "").Split("\n");
                    conexion.Close();
                    reader.Close();
                    return lista;
                }
                else
                {
                    conexion.Close();
                    reader.Close();
                    return null;
                }

            }
        }

        internal bool consultarrasgo(string nombre, string rasgo)
        {
            string query = "SELECT COUNT(*) FROM ficha_rasgo WHERE nombre_personaje = @nombre and indice_rasgo=@rasgo ";

            using (comando = new MySqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@rasgo", rasgo);

                conexion.Open();
                long count = (long)comando.ExecuteScalar();
                conexion.Close();
                return count > 0;
            }
        }

        internal async Task modificarficha(string nombre, string STR, string DEX, string CON, string INT, string WIS, string CHA, string niv)
        {
            conexion.Open();
            string query = "update ficha set STR=@STR, DEX=@DEX, CON=@CON, INTE=@INT, WIS=@WIS, CHA=@CHA, nivel=@nivel where Nombre=@nombre  ";
            comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@STR", STR);
            comando.Parameters.AddWithValue("@DEX", DEX);
            comando.Parameters.AddWithValue("@CON", CON);
            comando.Parameters.AddWithValue("@INT", INT);
            comando.Parameters.AddWithValue("@WIS", WIS);
            comando.Parameters.AddWithValue("@CHA", CHA);
            comando.Parameters.AddWithValue("@nivel", niv);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        internal async Task Borrarrasgos(string rasgo, string nombre)
        {
            conexion.Open();
            string query = "DELETE FROM ficha_rasgo WHERE nombre_personaje=@nombre and indice_rasgo=@rasgo";
            comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@rasgo", rasgo);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        internal async Task<List<string>> devolverhechizos(string nombre, int niv)
        {
            List<string> lista = new List<string> { };
            comando = new MySqlCommand("select * from ficha_hechizo WHERE nom_per ='" + nombre + "' and niv='" + niv + "'", conexion);

            conexion.Open();
            using (MySqlDataReader reader = comando.ExecuteReader())
            {

                while (reader.Read())
                {
                    string cad = reader["nom_hechizo"].ToString();
                    lista.Add(cad);
                }
                conexion.Close();
                reader.Close();
                return lista;
            }



        }

        internal string devolverurlhechizo(string v)
        {
            comando = new MySqlCommand("select * from hechizos WHERE Nombre=@nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            conexion.Open();
            using (MySqlDataReader reader = comando.ExecuteReader())
            {

                if (reader.Read())
                {
                    string cad = reader["indice"].ToString();
                    conexion.Close();
                    reader.Close();
                    return cad;
                }
                else
                {
                    conexion.Close();
                    reader.Close();
                    return null;
                }

            }


        }

        internal List<string> recogerusuariosinactivos()
        {
           List<string> lista = new List<string>();
            comando = new MySqlCommand("SELECT * FROM usuarios where Activo=0", conexion);

            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                while (reader.Read())
                {

                    string a= reader["nom_usuario"].ToString();

                    lista.Add(a);
                }
                reader.Close();
                conexion.Close();
                return lista;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reader.Close();
                return null;
            }
        }

        internal void reactivar(string usuario)
        {
            conexion.Open();
            string query = "update usuarios set Activo=1 where nom_usuario=@usu  ";
            comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@usu", usuario);

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
}

