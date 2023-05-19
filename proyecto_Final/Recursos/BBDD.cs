using MySql.Data.MySqlClient;
using proyecto_Final.Presentacion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace proyecto_Final.Recursos
{
    /// <summary>
    /// Clase que define los métodos de la base de datos.
    /// </summary>
    public class BBDD
    {
        private MySqlConnection conexion;
        private MySqlCommand comando;
        private MySqlDataReader reader;
        int esAdministrador;
        private string us,fuerza,destreza,constitucion,inteligencia,sabiduria,carisma;
        List<persona> listaper;
        List<string> listastr;
        //string ruta = "C:\\Users\\angel\\curso22-23\\DI\\ejerciciosDI\\tema_3\\proyecto_Final\\proyecto_Final\\Resources\\db.db";
        //string strAppPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        //string strFilePath = Path.Combine(strAppPath, "Resources");
        //string strFullFilename = Path.Combine(strFilePath, "db.db");

        public BBDD()
        {
            conexion = new MySqlConnection("Server=127.0.0.1;port=3306;user=root;password=CIFP1;database=rol");
        }
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
        /// Método para conectar a la base de datos.
        /// </summary>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <param name="contraseña">Contraseña del usuario.</param>
        /// <returns>True si la conexión fue exitosa, False si hubo un error.</returns>
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
                        esAdministrador = Int32.Parse(reader["EsAdmin"].ToString());
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
        /// Devuelve el valor de la variable esAdministrador.
        /// </summary>
        /// <returns>Valor de la variable esAdministrador.</returns>
        public int EsAdministrador()
        {
            return esAdministrador;
        }

        /// <summary>
        /// Devuelve el valor de la variable us.
        /// </summary>
        /// <returns>Valor de la variable us.</returns>
        public string EsUsuario()
        {
            return us;
        }

        /// <summary>
        /// Método para insertar usuarios en la base de datos.
        /// </summary>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <param name="contraseña">Contraseña del usuario.</param>
        /// <returns>True si la operación fue exitosa, False si hubo un error.</returns>
        public bool insertarusuarios(string usuario, string contraseña,String correo,  int administrador)
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
        /// Método para borrar usuarios en la base de datos.
        /// </summary>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <returns>True si la operación fue exitosa, False si hubo un error.</returns>
        public void borrar(string usuario)
        {
            conexion.Open();
            string query = "DELETE FROM usuarios WHERE nom_usuario =@usuario";
            comando = new MySqlCommand("SELECT * FROM usuarios WHERE nom_usuario=@usuario", conexion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            reader = comando.ExecuteReader();
            if (reader.Read())
            {
                if (reader["EsAdmin"].ToString() == "1")
                    MessageBox.Show("no se puede eliminar a un administrador");
                else
                {
                    comando = new MySqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.ExecuteNonQuery();
                }
            }

            conexion.Close();

        }
        public List<persona> recoger()
        {

            listaper = new List<persona>();
            comando = new MySqlCommand("SELECT * FROM usuarios", conexion);

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

        internal void modificar(string nombre, int admin, string oldnom)
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

        internal void borrarficha(string v)
        {
            conexion.Open();
            string query = "DELETE FROM ficha WHERE Nombre=@nombre";
            comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
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
        internal IEnumerable devolversubraza(string v)
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
        internal void insertarficha(string clase, string subclase, string raza, string subraza, string nombre, string usuario, int fuerza, int destreza, int constitucion, int sabiduria, int inteligencia, int carisma)
        {
            conexion.Open();
            MySqlTransaction transaction = conexion.BeginTransaction();
            using (MySqlCommand comando = new MySqlCommand("INSERT INTO ficha (Nombre,nom_usuario,Clase,Subclase,Raza,subraza,Fuerza,Destreza,Constitucion,Sabiduria,Inteligencia,Carisma) VALUES (@nombre, @usuario, @Clase, @subclase, @Raza, @subraza, @Fuerza, @Destreza, @Constitucion, @Sabiduria, @Inteligencia, @Carisma)", conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@Clase", clase);
                comando.Parameters.AddWithValue("@subclase", subclase);
                comando.Parameters.AddWithValue("@Raza", raza);
                comando.Parameters.AddWithValue("@subraza", subraza);
                comando.Parameters.AddWithValue("@Fuerza", fuerza);
                comando.Parameters.AddWithValue("@Destreza", destreza);
                comando.Parameters.AddWithValue("@Constitucion", constitucion);
                comando.Parameters.AddWithValue("@Sabiduria", sabiduria);
                comando.Parameters.AddWithValue("@Inteligencia", inteligencia);
                comando.Parameters.AddWithValue("@Carisma", carisma);
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
        internal string devolverurlclase(string v)
        {

            comando = new MySqlCommand("SELECT indice from Clase where Nombre=@nombre", conexion);
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
        internal void cargarficha(string v)
        {
            comando = new MySqlCommand("SELECT * FROM ficha WHERE  Nombre=@nombre", conexion);
            comando.Parameters.AddWithValue("@nombre", v);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                reader.Read();
                fuerza = reader["Fuerza"].ToString();
                destreza = reader["Destreza"].ToString();
                constitucion = reader["Constitucion"].ToString();
                inteligencia = reader["Inteligencia"].ToString();
                sabiduria = reader["Sabiduria"].ToString();
                carisma = reader["Carisma"].ToString();
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
        public string devolverfuerza()
        {
            return fuerza;
        }
        public string devolverdestreza()
        {
            return destreza;
        }
        public string devolverconstitucion()
        {
            return constitucion;
        }
        public string devolverInteligencia()
        {
            return inteligencia;
        }
        public string devolversabiduria()
        {
            return sabiduria;
        }
        public string devolvercarisma()
        {
            return carisma;
        }
        //llenar las bases de datos
        //internal void llenarhechizos(string nombre, string indice)
        //{
        //    using (MySqlCommand comando = new MySqlCommand("INSERT INTO Hechizos (nombre, indice) VALUES (@nombre, @indice)", conexion))
        //    {
        //        comando.Parameters.AddWithValue("@nombre", nombre);
        //        comando.Parameters.AddWithValue("@indice", indice);

        //        try
        //        {
        //            comando.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            conexion.Close();
        //        }
        //    }
        //}
        //internal void llenarRasgos(string nombre, string indice)
        //{
        //    using (MySqlCommand comando = new MySqlCommand("INSERT INTO Rasgos (nombre, indice) VALUES (@nombre, @indice)", conexion))
        //    {
        //        comando.Parameters.AddWithValue("@indice", indice);
        //        comando.Parameters.AddWithValue("@nombre", nombre);
        //        try
        //        {

        //            comando.ExecuteNonQuery();
        //            ;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            conexion.Close();
        //        }
        //    }
        //}
        //internal void llenarClases(string nombre, string indice)
        //{

        //    using (MySqlCommand comando = new MySqlCommand("INSERT INTO Clase (nombre, indice) VALUES (@nombre, @indice)", conexion))
        //    {
        //        comando.Parameters.AddWithValue("@nombre", nombre);
        //        comando.Parameters.AddWithValue("@indice", indice);

        //        try
        //        {
        //            comando.ExecuteNonQuery();

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            conexion.Close();
        //        }
        //    }
        //}
        //internal void llenarRazas(string nombre, string indice)
        //{

        //    using (MySqlCommand comando = new MySqlCommand("INSERT INTO Raza (nombre, indice) VALUES (@nombre, @indice)", conexion))
        //    {
        //        comando.Parameters.AddWithValue("@nombre", nombre);
        //        comando.Parameters.AddWithValue("@indice", indice);

        //        try
        //        {
        //            comando.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            conexion.Close();
        //        }
        //    }
        //}
        //internal void llenarsubrazas(string nombre, string indice)
        //{

        //    using (MySqlCommand comando = new MySqlCommand("INSERT INTO subraza (nombre, indice) VALUES (@nombre, @indice)", conexion))
        //    {
        //        comando.Parameters.AddWithValue("@nombre", nombre);
        //        comando.Parameters.AddWithValue("@indice", indice);

        //        try
        //        {
        //            comando.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            conexion.Close();
        //        }
        //    }
        //}
        //internal void llenarsubclases(string nombre, string indice)
        //{
        //    using (MySqlCommand comando = new MySqlCommand("INSERT INTO subclase (nombre, indice) VALUES (@nombre, @indice)", conexion))
        //    {
        //        comando.Parameters.AddWithValue("@nombre", nombre);
        //        comando.Parameters.AddWithValue("@indice", indice);


        //        try
        //        {
        //            comando.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            conexion.Close();
        //        }
        //    }
        //}
        //internal void llenarRazarasgo(string nombre, string indice)
        //{
        //    using (MySqlCommand comando = new MySqlCommand("INSERT INTO Rasgos (Nombre, indice) VALUES (@nombre, @indice)", conexion))

        //    {
        //        comando.Parameters.AddWithValue("@nombre", nombre);
        //        comando.Parameters.AddWithValue("@indice", indice);
        //        try
        //        {
        //            comando.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            conexion.Close();
        //        }
        //    }
        //}
        internal void conectar()
        {
            conexion.Open();
        }
        internal void desconectar()
        {
            conexion.Close();
        }

        
    }
}

