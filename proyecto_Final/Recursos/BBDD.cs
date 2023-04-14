using proyecto_Final.Presentacion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
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
        private SQLiteConnection conexion;
        private SQLiteCommand comando;
        private SQLiteDataReader reader;
        int esAdministrador;
        private string us;
        List<persona> lista;
        string ruta = "C:\\Users\\angel\\curso22-23\\DI\\ejerciciosDI\\tema_3\\proyecto_Final\\proyecto_Final\\Resources\\db.db";
        //string strAppPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        //string strFilePath = Path.Combine(strAppPath, "Resources");
        //string strFullFilename = Path.Combine(strFilePath, "db.db");

        public BBDD()
        {
            conexion = new SQLiteConnection("Data Source=" + ruta + "; Version=3; New=false; Compress=True;");
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
            comando = new SQLiteCommand("SELECT * FROM usuarios WHERE nom_usuario=@usuario and contraseña=@contraseña", conexion);
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
        public bool insertar(string usuario, string contraseña, int administrador)
        {
            string con = pasarSHA256(contraseña);
            conexion.Open();
            SQLiteTransaction transaction = conexion.BeginTransaction();
            using (SQLiteCommand comando = new SQLiteCommand("INSERT INTO usuarios (nom_usuario, contraseña, EsAdmin) VALUES (@usuario, @contraseña,@administrador)", conexion))
            {
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@contraseña", con);
                comando.Parameters.AddWithValue("@administrador", administrador);

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
            comando = new SQLiteCommand("SELECT * FROM usuarios WHERE nom_usuario=@usuario", conexion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            reader = comando.ExecuteReader();
            if (reader.Read())
            {
                if (reader["EsAdmin"].ToString() =="1")
                    MessageBox.Show("no se puede eliminar a un administrador");
                else {
                    comando = new SQLiteCommand(query, conexion);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.ExecuteNonQuery();
                }    
            }
               
            conexion.Close();
            
        }
        public List<persona> recoger()
        {

                lista = new List<persona>();
                comando = new SQLiteCommand("SELECT * FROM usuarios", conexion);

            conexion.Open();
                reader = comando.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {

                        persona t = new persona(reader["nom_usuario"].ToString(), int.Parse(reader["EsAdmin"].ToString()));

                        lista.Add(t);
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

        internal void modificar(string nombre, int admin, string oldnom)
        {
            conexion.Open();
            string query = "update usuarios set nom_usuario=@nombre ,EsAdmin=@admin WHERE nom_usuario =@oldnom";
            comando = new SQLiteCommand(query, conexion);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@admin", admin);
            comando.Parameters.AddWithValue("@oldnom", oldnom);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        //llenar las bases de datos
        internal void llenarhechizos(string nombre,string indice) {
            using (SQLiteCommand comando = new SQLiteCommand("INSERT INTO Hechizos (nombre, indice) VALUES (@nombre, @indice)", conexion))
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
        internal void llenarRasgos(string nombre, string indice)
        {
            using (SQLiteCommand comando = new SQLiteCommand("INSERT INTO Rasgos (nombre, indice) VALUES (@nombre, @indice)", conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@indice", indice);

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

            using (SQLiteCommand comando = new SQLiteCommand("INSERT INTO Clase (nombre, indice) VALUES (@nombre, @indice)", conexion))
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
           
            using (SQLiteCommand comando = new SQLiteCommand("INSERT INTO Raza (nombre, indice) VALUES (@nombre, @indice)", conexion))
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
           
            using (SQLiteCommand comando = new SQLiteCommand("INSERT INTO subraza (nombre, indice) VALUES (@nombre, @indice)", conexion))
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
            using (SQLiteCommand comando = new SQLiteCommand("INSERT INTO subclase (nombre, indice) VALUES (@nombre, @indice)", conexion))
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
        internal void conectar() {
            conexion.Open();
        }
        internal void desconectar()
        {
            conexion.Close();
        }


    }
}

