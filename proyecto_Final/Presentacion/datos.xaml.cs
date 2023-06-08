using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace proyecto_Final.Presentacion
{
    /// <summary>
    /// Lógica de interacción para datos.xaml
    /// </summary>
    public partial class datos : Window
    {
        string nom, des;
        /// Clase parcial que representa la ventana de visualización de datos.
        /// </summary>
        public datos(string nomb, string descr)
        {
            this.nom = nomb;
            this.des = descr;
            InitializeComponent();
        }

        /// <summary>
        /// Manejador del evento "Loaded" del Grid en la ventana de visualización de datos.
        /// Actualiza los elementos visuales con los datos proporcionados.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            nombre.Content = nom;
            desc.Text = des;
        }
    }
}
