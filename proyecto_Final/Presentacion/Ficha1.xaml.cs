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
    /// Lógica de interacción para Ficha1.xaml
    /// </summary>
    public partial class Ficha1 : Page
    {
        string nom;
        BBDD db;
        
        public Ficha1(string nom)
        {
            this.nom=nom;
            db= new BBDD();
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Nombre.Content = nom;
            db.cargarficha(nom);
            txtFue.Text = db.devolverfuerza();
            txtDes.Text = db.devolverdestreza();
            txtCon.Text = db.devolverconstitucion();
            txtInt.Text = db.devolverInteligencia();
            txtSab.Text = db.devolversabiduria();
            txtCar.Text = db.devolvercarisma();
            txtclase.Content=db.devolverclase();
            bonFue.Content = sacarbono((string)txtFue.Text);
            bonDes.Content = sacarbono((string)txtDes.Text);
            bonCon.Content = sacarbono((string)txtCon.Text);
            bonInt.Content = sacarbono((string)txtInt.Text);
            bonSab.Content = sacarbono((string)txtSab.Text);
            bonCar.Content = sacarbono((string)txtCar.Text);
        }

        private string sacarbono(string numero)
        {
           int i=Int32.Parse(numero);
            switch (i) {
                case 1:
                    return "-5";
                case int n when n<4 && n>1:
                    return "-4";
                case int n when n < 6 && n > 3:
                    return "-3";
                case int n when n < 8 && n > 5:
                    return "-2";
                case int n when n < 10 && n > 7:
                    return "-1";
                case int n when n < 12 && n > 9:
                    return "0";
                case int n when n < 14 && n > 11:
                    return "1";
                case int n when n < 16 && n > 13:
                    return "2";
                case int n when n < 18 && n > 15:
                    return "3";
                case int n when n < 20 && n > 17:
                    return "4";
                case int n when n < 22 && n > 19:
                    return "5";
                case int n when n < 24 && n > 21:
                    return "6";
                case int n when n < 26 && n > 23:
                    return "7";
                case int n when n < 28 && n > 25:
                    return "8";
                case int n when n < 30 && n > 27:
                    return "9";
                case 30:
                    return "10";
                default:
                    return "ERR";
            }
        }
    }
}
