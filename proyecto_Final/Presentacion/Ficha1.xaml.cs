using proyecto_Final.Recursos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
        bool ignorar;
        Api api;
        

        public Ficha1(string nom)
        {
            ignorar = true;
            this.nom=nom;
            db= new BBDD();
            api= new Api();
            InitializeComponent();
            

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await cargarmetodosini();
           
            Fuebon.Content = sacarbono(txtFue.Text);
            Desbon.Content = sacarbono(txtDes.Text);
            Conbon.Content = sacarbono(txtCon.Text);
            Intbon.Content = sacarbono(txtInt.Text);
            Sabbon.Content = sacarbono(txtSab.Text);
            Carbon.Content = sacarbono(txtCar.Text); 
            ignorar = false;
            
            
            
            actualizarsalvaciones();
            //actualizarhablidades();
        }

        private async Task cargarmetodosini()
        {
            Nombre.Content = nom;
            db.cargarficha(nom);
            txtFue.Text = db.devolverfuerza();
            txtDes.Text = db.devolverdestreza();
            txtCon.Text = db.devolverconstitucion();
            txtInt.Text = db.devolverInteligencia();
            txtSab.Text = db.devolversabiduria();
            txtCar.Text = db.devolvercarisma();
            txtclase.Content = db.devolverclase()+" "+db.devolversubclase();
            due.Content=db.devolverdueño();
            velo.Content = (await api.devolvervelocidad(db.devolverRaza())).ToString();
            niv.Text = db.devolvernivel().ToString();
            BitmapImage imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.UriSource = new Uri(db.devolverimagen(db.devolverclase()), UriKind.RelativeOrAbsolute);
            imageSource.EndInit();
            img.Source = imageSource;
            await api.llamarClaseurl(db.devolverurlclase(db.devolverclase()));
            dg.Content = api.devolverpgolpe();
            perpas.Content = (10 + Int32.Parse((string)Sabbon.Content)).ToString();
            inic.Content = Desbon.Content;
            if (db.devolversubraza().Equals("None"))
            {
                txtRaza.Content = db.devolverRaza();
            }
            else {
                txtRaza.Content = db.devolverRaza()+" "+db.devolversubraza();
            }
        }

        private void actualizarsalvaciones()
        {
            if ((bool)rdFue.IsChecked) {
               // SalFue.Content= bon
            }
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


        private void textbox_textchanged(object sender, TextChangedEventArgs e)
        {
            if (ignorar) { }
            else
            {
               
                TextBox textBox = sender as TextBox;

                switch (textBox.Name)
                {
                    case "txtFue":
                        Fuebon.Content = sacarbono(txtFue.Text);
                        break;
                    case "txtDes":
                        Desbon.Content = sacarbono(txtDes.Text);
                        break;
                    case "txtCon":
                        Conbon.Content = sacarbono(txtCon.Text);
                        break;
                    case "txtInt":
                        Intbon.Content = sacarbono(txtInt.Text);
                        break;
                    case "txtSab":
                        Sabbon.Content = sacarbono(txtSab.Text);
                        break;
                    case "txtCar":
                        Carbon.Content = sacarbono(txtCar.Text);
                        break;
                    default:
                        break;
                }
            }
        }


    }
    }

       
    

    
