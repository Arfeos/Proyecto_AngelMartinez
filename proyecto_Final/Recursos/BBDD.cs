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
        string raza;
        string clase;
        int esAdministrador;
        private string us;
        List<string> lista=new List<string>();
        public static string pasarSHA256(string con) {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb= new StringBuilder();
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
            string strAppPath = Path.GetDirectoryName(Environment.ProcessPath);
            string strFilePath = Path.Combine(strAppPath, "Resources");
            string strFullFilename = Path.Combine(strFilePath, "db.db");
            conexion = new SQLiteConnection("Data Source=" + strFullFilename + "; Version=3; New=false; Compress=True;");
            
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
        /// Devuelve el valor de la variable clase.
        /// </summary>
        /// <returns>Valor de la variable clase.</returns>
        public string EsClase()
        {
            return clase;
        }

        /// <summary>
        /// Devuelve el valor de la variable raza.
        /// </summary>
        /// <returns>Valor de la variable raza.</returns>
        public string EsRaza()
        {
            return raza;
        }

        /// <summary>
        /// Método para insertar usuarios en la base de datos.
        /// </summary>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <param name="contraseña">Contraseña del usuario.</param>
        /// <returns>True si la operación fue exitosa, False si hubo un error.</returns>
        public bool insertar(string usuario, string contraseña)
        {
            string strAppPath = Path.GetDirectoryName(Environment.ProcessPath);
            string strFilePath = Path.Combine(strAppPath, "Resources");
            string strFullFilename = Path.Combine(strFilePath, "db.db");

            using (SQLiteConnection conexion = new SQLiteConnection("Data Source=" + strFullFilename + "; Version=3; New=false; Compress=True;"))
            {
                string con=pasarSHA256(contraseña);
                conexion.Open();
                SQLiteTransaction transaction = conexion.BeginTransaction();
                using (SQLiteCommand comando = new SQLiteCommand("INSERT INTO usuarios (nom_usuario, contraseña, EsAdmin) VALUES (@usuario, @contraseña, 0)", conexion))
                {
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@contraseña", con);

                    try
                    {
                        comando.ExecuteNonQuery();
                        transaction.Commit();
                        conexion.Close();
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
        }
        /// <summary>
        /// Método para borrar usuarios en la base de datos.
        /// </summary>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <returns>True si la operación fue exitosa, False si hubo un error.</returns>
        public void borrar(string usuario)
        {
            string strAppPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string strFilePath = Path.Combine(strAppPath, "Resources");
            string strFullFilename = Path.Combine(strFilePath, "db.db");
            SQLiteConnection conexion = new SQLiteConnection("Data Source=" + strFullFilename + "; Version=3; New=false; Compress=True;");
                conexion.Open();          
            string query = "DELETE FROM usuarios WHERE nom_usuario ='"+usuario+"'";
            comando = new SQLiteCommand(query, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public List<string> recoger() {
            lista=new List<string>();
            string strAppPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string strFilePath = Path.Combine(strAppPath, "Resources");
            string strFullFilename = Path.Combine(strFilePath, "db.db");

            using (SQLiteConnection conexion = new SQLiteConnection("Data Source=" + strFullFilename + "; Version=3; New=false; Compress=True;"))
            {
                comando = new SQLiteCommand("SELECT * FROM usuarios", conexion);
                conexion.Open();
                
                reader = comando.ExecuteReader();
                try
                {
                    while (reader.Read()){
                    
                        persona t = new persona(reader["nom_usuario"].ToString(), int.Parse(reader["EsAdmin"].ToString()));

                        lista.Add(t.ToString());               
                    }
                    return lista;
                    reader.Close();
                    conexion.Close();
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    reader.Close();
                    return null;
                }
            }
        }
    }
}
