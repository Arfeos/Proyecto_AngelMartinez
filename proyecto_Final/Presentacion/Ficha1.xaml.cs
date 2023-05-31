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

        public Ficha1()
        {
            ignorar = true;
            db = new BBDD();
            api = new Api();

            InitializeComponent();
        }

        public Ficha1(string nom)
        {
            ignorar = true;
            this.nom=nom;
            db= new BBDD();
            api= new Api();
            InitializeComponent();
            Nombre.Content = nom;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await cargarmetodosini();
            await ficheros.CargarDatosDesdeArchivo("../../../Ficheros/fich" + Nombre.Content.ToString() + ".txt", STR, DEX, CON, INT, WIS, CHA, Tras, niv, rtxtInv, rdFue, rdDes, rdCon, rdInt, rdSab, rdCar, rdCar, rdAcro, rdArca, rdAtle, rdEnga, rdHist, rdInte, rdInti, rdInve, rdInve, rdJuMa, rdMedi, rdNatu, rdPerc, rdPersp, rdPersu, rdReli, rdSigi, rdSupe, rdTrAn, rtxtRasPer, rtxtide, rtxtVin, rtxtDef, Ali);
            Fuebon.Content = sacarbono(STR.Text);
            Desbon.Content = sacarbono(DEX.Text);
            Conbon.Content = sacarbono(CON.Text);
            Intbon.Content = sacarbono(INT.Text);
            Sabbon.Content = sacarbono(WIS.Text);
            Carbon.Content = sacarbono(CHA.Text); 
            ignorar = false;
            
            
            
            actualizarsalvaciones();
            actualizarhablidades();
            list.ItemsSource = await db.devolverRasgos(nom);
        }

        private void actualizarhablidades()
        {
            if ((bool)rdAcro.IsChecked)
            {
                Acr.Content = Int32.Parse((string)Desbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Acr.Content = Desbon.Content;
            }

            if ((bool)rdArca.IsChecked)
            {
                Arc.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Arc.Content = Intbon.Content;
            }

            if ((bool)rdAtle.IsChecked)
            {
                Atl.Content = Int32.Parse((string)Fuebon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Atl.Content = Fuebon.Content;
            }

            if ((bool)rdEnga.IsChecked)
            {
                Eng.Content = Int32.Parse((string)Carbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Eng.Content = Carbon.Content;
            }

            if ((bool)rdHist.IsChecked)
            {
                His.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                His.Content = Intbon.Content;
            }

            if ((bool)rdInte.IsChecked)
            {
                Inte.Content = Int32.Parse((string)Carbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Inte.Content = Carbon.Content;
            }

            if ((bool)rdInti.IsChecked)
            {
                Inti.Content = Int32.Parse((string)Carbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Inti.Content = Carbon.Content;
            }

            if ((bool)rdInve.IsChecked)
            {
                Inv.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Inv.Content = Intbon.Content;
            }

            if ((bool)rdJuMa.IsChecked)
            {
                JuMa.Content = Int32.Parse((string)Desbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                JuMa.Content = Desbon.Content;
            }

            if ((bool)rdMedi.IsChecked)
            {
                Med.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Med.Content = Sabbon.Content;
            }

            if ((bool)rdNatu.IsChecked)
            {
                Nat.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Nat.Content = Intbon.Content;
            }

            if ((bool)rdPerc.IsChecked)
            {
                Perc.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Perc.Content = Sabbon.Content;
            }

            if ((bool)rdPersp.IsChecked)
            {
                Persp.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Persp.Content = Sabbon.Content;
            }
            if ((bool)rdPersu.IsChecked)
            {
                Persu.Content = Int32.Parse((string)Carbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Persu.Content = Carbon.Content;
            }

            if ((bool)rdReli.IsChecked)
            {
                Rel.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Rel.Content = Intbon.Content;
            }

            if ((bool)rdSigi.IsChecked)
            {
                Sig.Content = Int32.Parse((string)Desbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Sig.Content = Desbon.Content;
            }
            if ((bool)rdSupe.IsChecked)
            {
                Sup.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Sup.Content = Sabbon.Content;
            }

            if ((bool)rdTrAn.IsChecked)
            {
                AnHa.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                AnHa.Content = Sabbon.Content;
            }
        }

        private async Task cargarmetodosini()
        {
           
            db.cargarficha(Nombre.Content.ToString());
            STR.Text = db.devolverSTR();
            DEX.Text = db.devolverDEX();
            CON.Text = db.devolverCON();
            INT.Text = db.devolverINT();
            WIS.Text = db.devolverWIS();
            CHA.Text = db.devolverCAR();
            txtclase.Content = db.devolverclase()+" "+db.devolversubclase();
            due.Content=db.devolverdueño();
            velo.Content = (await api.devolvervelocidad(db.devolverRaza()));
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
            if ((bool)rdFue.IsChecked)
            {
                SalFue.Content = Int32.Parse((string)Fuebon.Content) + Int32.Parse((string)compmod.Content);
            }
            else {
                SalFue.Content = Fuebon.Content;
            }

            if ((bool)rdDes.IsChecked)
            {
                SalDes.Content = Int32.Parse((string)Desbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                SalDes.Content = Desbon.Content;
            }

            if ((bool)rdCon.IsChecked)
            {
                SalCon.Content = Int32.Parse((string)Conbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                SalCon.Content = Conbon.Content;
            }

            if ((bool)rdInt.IsChecked)
            {
                SalInt.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                SalInt.Content = Intbon.Content;
            }

            if ((bool)rdSab.IsChecked)
            {
                SalSab.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                SalSab.Content = Sabbon.Content;
            }

            if ((bool)rdCar.IsChecked)
            {
                SalCar.Content = Int32.Parse((string)Carbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                SalCar.Content = Carbon.Content;
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
                    case "STR":
                        Fuebon.Content = sacarbono(STR.Text);
                        break;
                    case "DES":
                        Desbon.Content = sacarbono(DEX.Text);
                        break;
                    case "CON":
                        Conbon.Content = sacarbono(CON.Text);
                        break;
                    case "INT":
                        Intbon.Content = sacarbono(INT.Text);
                        break;
                    case "WIS":
                        Sabbon.Content = sacarbono(WIS.Text);
                        break;
                    case "CHA":
                        Carbon.Content = sacarbono(CHA.Text);
                        break;
                    default:
                        break;
                }
                actualizarsalvaciones();
                actualizarhablidades();
            }
            
        }

        private void ch_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chckbox = sender as CheckBox;

            switch (chckbox.Name)
            {
                case "rdFue":
                case "rdDes":
                case "rdCon":
                case "rdInt":
                case "rdSab":
                case "rdCar":
                    actualizarsalvaciones();
                    break;
                case "rdAcro":
                case "rdArca":
                case "rdAtle":
                case "rdEnga":
                case "rdHist":
                case "rdInte":
                case "rdInti":
                case "rdInve":
                case "rdJuMa":
                case "rdMedi":
                case "rdNatu":
                case "rdPerc":
                case "rdPersp":
                case "rdPersu":
                case "rdReli":
                case "rdSigi":
                case "rdSupe":
                case "rdTrAn":
                    actualizarhablidades();
                    break;
                default:
                    break;
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            await ficheros.GuardarDatosEnArchivo("../../../Ficheros/fich" + Nombre.Content.ToString() + ".txt", STR, DEX, CON, INT, WIS, CHA, Tras, niv,rtxtInv,rdFue,rdDes,rdCon,rdInt,rdSab,rdCar,rdCar,rdAcro, rdArca, rdAtle, rdEnga, rdHist, rdInte, rdInti, rdInve, rdInve, rdJuMa, rdMedi, rdNatu, rdPerc, rdPersp, rdPersu, rdReli, rdSigi, rdSupe, rdTrAn,rtxtRasPer,rtxtide,rtxtVin,rtxtDef,Ali );
        }


    }
    }

       
    

    
