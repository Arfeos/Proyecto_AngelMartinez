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
    internal class ficheros
    {

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
                    else if (control is RichTextBox richtextbox)
                    {
                        TextRange textRange = new TextRange(richtextbox.Document.ContentStart, richtextbox.Document.ContentEnd);
                        string contenido = textRange.Text;
                        writer.Write($"RichTextBox:{richtextbox.Name}:{contenido}");
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
                    catch (IndexOutOfRangeException) { return; }


                }

            }
        }
    }
}

