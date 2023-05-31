using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
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
using System.Windows.Media;

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
        int esAdministrador,nivel;
        private string us,STR,DEX,CON,INT,WIS,carisma,clase,raza,subclase, subraza, dueño;
        List<persona> listaper;
        List<string> listastr;


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
                if (reader["EsAdmin"].ToString() == "1") { 
                    MessageBox.Show("no se puede eliminar a un administrador");
                    borrar = false;
                }
               
            }
            reader.Close();
             if(borrar)
            {           
                    comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.ExecuteNonQuery();
            }
            conexion.Close();

        }
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
                nivel =Int32.Parse( reader["nivel"].ToString());
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
        internal string devolverSTR()
        {
            return STR;
        }
        internal string devolverDEX()
        {
            return DEX;
        }
        internal string devolverCON()
        {
            return CON;
        }
        internal string devolverINT()
        {
            return INT;
        }
        internal string devolverWIS()
        {
            return WIS;
        }
        internal string devolverCAR()
        {
            return carisma;
        }
        internal string devolverRaza()
        {
            return raza;
        }
        internal string devolversubraza()
        {
            return subraza;
        }
        internal string devolverclase()
        {
            return clase;
        }
        internal string devolversubclase()
        {
            return subclase;
        }
        internal string devolverdueño()
        {
            return dueño;
        }
        internal int devolvernivel()
        {
            return nivel;
        }
        internal string devolverimagen(String clase)
        {
            comando = new MySqlCommand("SELECT url FROM imagenes WHERE  Clase=@Clase", conexion);
            comando.Parameters.AddWithValue("@Clase", clase);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                reader.Read();
                string st = reader["url"].ToString();
                conexion.Close();
                
                    reader.Close();
                return st ;
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
        //internal void llenarclases(string nombre, string indice)
        //{

        //    using (MySqlCommand comando = new MySqlCommand("insert into clase (nombre, indice) values (@nombre, @indice)", conexion))
        //    {
        //        comando.parameters.addwithvalue("@nombre", nombre);
        //        comando.parameters.addwithvalue("@indice", indice);

        //        try
        //        {
        //            comando.executenonquery();

        //        }
        //        catch (exception ex)
        //        {
        //            console.writeline(ex.message);
        //            conexion.close();
        //        }
        //    }
        //}
        //internal void llenarrazas(string nombre, string indice)
        //{

        //    using (mysqlcommand comando = new mysqlcommand("insert into raza (nombre, indice) values (@nombre, @indice)", conexion))
        //    {
        //        comando.parameters.addwithvalue("@nombre", nombre);
        //        comando.parameters.addwithvalue("@indice", indice);

        //        try
        //        {
        //            comando.executenonquery();
        //        }
        //        catch (exception ex)
        //        {
        //            console.writeline(ex.message);
        //            conexion.close();
        //        }
        //    }
        //}
        //internal void llenarsubrazas(string nombre, string indice)
        //{

        //    using (mysqlcommand comando = new mysqlcommand("insert into subraza (nombre, indice) values (@nombre, @indice)", conexion))
        //    {
        //        comando.parameters.addwithvalue("@nombre", nombre);
        //        comando.parameters.addwithvalue("@indice", indice);

        //        try
        //        {
        //            comando.executenonquery();
        //        }
        //        catch (exception ex)
        //        {
        //            console.writeline(ex.message);
        //            conexion.close();
        //        }
        //    }
        //}
        //internal void llenarsubclases(string nombre, string indice)
        //{
        //    using (mysqlcommand comando = new mysqlcommand("insert into subclase (nombre, indice) values (@nombre, @indice)", conexion))
        //    {
        //        comando.parameters.addwithvalue("@nombre", nombre);
        //        comando.parameters.addwithvalue("@indice", indice);


        //        try
        //        {
        //            comando.executenonquery();
        //        }
        //        catch (exception ex)
        //        {
        //            console.writeline(ex.message);
        //            conexion.close();
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

        internal async Task<string> devolvervalor(string rasgo,string nombre)
        {
            comando = new MySqlCommand("SELECT * FROM ficha WHERE  Nombre=@nombre", conexion);
            comando.Parameters.AddWithValue("@rasgo", rasgo);
            comando.Parameters.AddWithValue("@nombre", nombre);
            conexion.Open();
            reader = comando.ExecuteReader();
            try
            {
                String st=null;
                reader.Read();
                if (rasgo=="STR") {
                    
                   st = reader["STR"].ToString(); 
                }
                if (rasgo == "DEX")
                {

                    st = reader["DEX"].ToString();
                }
                if (rasgo == "CON")
                {

                     st =reader["CON"].ToString();
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
            string query = "update ficha set "+rasgo+"=@valor WHERE Nombre ='"+nombre+"'";
            comando = new MySqlCommand(query, conexion);


            comando.Parameters.AddWithValue("@valor", valor);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        internal async Task insertarrasgos(string rasgo, string nombre)
        {
           conexion.Open();
            string query = "insert into ficha_rasgo values(@nombre, @rasgo )";
            comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@rasgo", rasgo);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        internal async Task<List<string>> devolverRasgos(string nom)
        {
            listastr= new List<string> ();  
            comando = new MySqlCommand("SELECT * FROM rasgos WHERE  indice in (select indice_rasgo from ficha_rasgo where nombre_personaje=@nom)", conexion);
            comando.Parameters.AddWithValue("@nom",nom);
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
    }
}

