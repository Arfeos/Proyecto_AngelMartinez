using proyecto_Final.Recursos;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace proyecto_Final.Presentacion
{
    
    /// <summary>
    /// Lógica de interacción para Administrador.xaml
    /// </summary>
    public partial class Administrador : Page
    {
        List<string> lista = new List<string>();
        BBDD bd= new BBDD();
        public Administrador()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            actualizar();
        }
        /// <summary>
        /// actualiza la lista de usuarios
        /// </summary>
        public void actualizar() {
            lista_bd.ItemsSource = "";
            lista = bd.recoger();
            lista_bd.ItemsSource =lista;
        }
        /// <summary>
        /// borra el usuario con ese nombre
        /// </summary>
        private void Borrar_btn_Click(object sender, RoutedEventArgs e)
        {
            bd.borrar(usuario.Text);
            actualizar();
            MessageBox.Show("se ha borrado exitosamente");
        }
    }
}
