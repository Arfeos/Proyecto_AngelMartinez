using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Controls;
using proyecto_Final.Presentacion;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Documents;

namespace proyecto_Final.control
{
    /// <summary>
    /// Clase que proporciona métodos para guardar los datos de los controles en un archivo y cargar datos desde un archivo a los controles.
    /// </summary>
    internal class ficheros
    {
        /// <summary>
        /// Guarda los datos de los controles en un archivo y los guarda en la base de datos.
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo a guardar.</param>
        /// <param name="nombre">Nombre del archivo.</param>
        /// <param name="controles">Controles cuyos datos se guardarán.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public static async Task GuardarDatos(string rutaArchivo, string nombre, params Control[] controles)
        {
            BBDD db = new BBDD();
            using (StreamWriter writer = new StreamWriter(rutaArchivo))
            {
                foreach (Control control in controles)
                {
                    if (control is TextBox textBox)
                    {
                        writer.WriteLine($"TextBox:{textBox.Name}:{textBox.Text}");
                    }
                    else if (control is CheckBox checkBox)
                    {
                        writer.WriteLine($"CheckBox:{checkBox.Name}:{checkBox.IsChecked}");
                    }
                    else if (control is RichTextBox richTextBox)
                    {
                        TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                        string contenido = textRange.Text;
                        writer.Write($"RichTextBox:{richTextBox.Name}:{contenido}");
                    }
                    else if (control is ComboBox comboBox)
                    {
                        writer.WriteLine($"ComboBox:{comboBox.Name}:{comboBox.SelectedIndex}");
                    }
                }

                writer.Close();

                await db.guardardoc(File.ReadAllText(rutaArchivo), rutaArchivo.Replace(".txt", ""), nombre);
                File.Delete(rutaArchivo);
            }
        }

        /// <summary>
        /// Carga los datos desde un archivo y los asigna a los controles correspondientes.
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo a cargar.</param>
        /// <param name="nombre">Nombre del archivo.</param>
        /// <param name="controles">Controles a los que se asignarán los datos cargados.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public static async Task CargarDatosDesdeArchivo(string rutaArchivo, string nombre, params Control[] controles)
        {
            BBDD db = new BBDD();

            if (db.comprobardocumento(rutaArchivo.Replace(".txt", "")))
            {
                string[] lineas = await db.cargarfichabd(rutaArchivo.Replace(".txt", ""));

                foreach (string linea in lineas)
                {
                    try
                    {
                        string[] partes = linea.Split(':');
                        string tipoControl = partes[0];
                        string nombreControl = partes[1];
                        string valorControl = partes[2];

                        Control control = controles.FirstOrDefault(c => c.Name == nombreControl);

                        if (control != null)
                        {
                            if (tipoControl == "TextBox" && control is TextBox textBox)
                            {
                                textBox.Text = valorControl;
                            }
                            else if (tipoControl == "CheckBox" && control is CheckBox checkBox)
                            {
                                bool estado = bool.Parse(valorControl);
                                checkBox.IsChecked = estado;
                            }
                            else if (tipoControl == "RichTextBox" && control is RichTextBox richTextBox)
                            {
                                richTextBox.Document.Blocks.Clear();
                                richTextBox.AppendText(valorControl);
                            }
                            else if (tipoControl == "ComboBox" && control is ComboBox comboBox)
                            {
                                int indice;
                                if (int.TryParse(valorControl, out indice) && indice >= 0 && indice < comboBox.Items.Count)
                                {
                                    comboBox.SelectedIndex = indice;
                                }
                            }
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return;
                    }
                }
            }
        }
    }

}

