using System;
using System.Collections.Generic;
using System.Windows;

namespace Financiero
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Random r = new Random();
        string numVerif;
        long numtarjeta;
        List<int> listaVerif = new List<int> { };
        //int[] listaVerif = new int[16];

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                listaVerif.Clear();

                numtarjeta = r.Next(000000000, 999999999);

                List<int> lista = new List<int> { 1, 2, 4, 8, 5, 10, 9, 7, 3, 6 };


                string campoNumBanco = campoCoEntidad.Text;
                string campoOficina = campoCoSucursal.Text;
                string campoCuenta = campoCoCuenta.Text;
                string campoDc = campoCoDc.Text;

                int digIban = 0;
                int prefTarjeta = 0;

                string enlace;
                int resMul1 = 0;

                enlace = "00" + campoNumBanco + campoOficina;
                //int en = enlace.ToString("D10");

                for (int i = 0; i <= lista[6]; i++)
                {
                    int num = (int)Char.GetNumericValue(enlace[i]);
                    resMul1 = resMul1 + (num * lista[i]);
                }

                resMul1 = resMul1 % 11;
                int digControl1 = 11 - resMul1;

                int resMul2 = 0;
                for (int y = 0; y <= lista[6]; y++)
                {
                    int num = (int)Char.GetNumericValue(campoCuenta[y]);
                    resMul2 = resMul2 + num * lista[y];
                }
                resMul2 = resMul2 % 11;
                int digControl2 = 11 - resMul2;

                if (digControl1 == 10)
                {
                    digControl1 = 1;
                }
                if (digControl1 == 11)
                {
                    digControl1 = 0;
                }
                if (digControl2 == 10)
                {
                    digControl2 = 1;
                }
                if (digControl2 == 11)
                {
                    digControl2 = 0;
                }

                string numDc = Convert.ToString(digControl1) + Convert.ToString(digControl2);
                campoCoDc.Text = numDc;

                string union = campoNumBanco + campoOficina + campoCoDc.Text + campoCuenta + "14" + "18" + "00";

                for (int i = 0; i < union.Length; i++)
                {
                    int num = (int)Char.GetNumericValue(union[i]);
                    digIban = (digIban * 10 + num) % 97;
                }

                digIban = 98 - digIban;

                if (digIban >= 0 & digIban <= 9)
                {
                    CampoIbanEntidad.Text = "ES0" + digIban;
                }
                if (digIban > 9)
                {
                    CampoIbanEntidad.Text = "ES" + digIban;
                }

                campoIban.Text = campoNumBanco + " " + campoOficina + " " + numDc + " " + campoCuenta;

                if (radio_visa.IsChecked == true)
                {
                    prefTarjeta = r.Next(400000, 499999);
                }

                if (radio_mastercard.IsChecked == true)
                {
                    prefTarjeta = r.Next(510000, 559999);
                }

                if (radio_aexpress.IsChecked == true)
                {
                    prefTarjeta = r.Next(340000, 379999);
                }

                string numUnion = prefTarjeta.ToString("D6") + numtarjeta.ToString("D9") + "0";


                int total = 0;
                int numFor = 0;

                for (int i = numUnion.Length; i >= 1; i--)
                {
                    if (i != 16)
                    {
                        if (i != 0)
                        {
                            numFor = (int)Char.GetNumericValue(numUnion[i - 1]);
                        }


                        if (i % 2 != 0)
                        {
                            int num2 = 2 * numFor;

                            if (num2 > 9)
                            {
                                num2 = num2 - 9;
                                listaVerif.Add(num2);
                            }
                            else
                            {
                                listaVerif.Add(num2);
                            }
                        }
                        else
                        {
                            listaVerif.Add(numFor);
                        }
                    }

                }

                for (int y = 0; y < listaVerif.Count; y++)
                {
                    total = total + listaVerif[y];


                }
                total = total * 9;

                numVerif = total.ToString();
                numVerif = numVerif.Substring(numVerif.Length - 1, 1);

                numUnion = prefTarjeta.ToString("D6") + numtarjeta.ToString("D9") + numVerif;

                campoTarjetaPago.Text = numUnion;
                label_error.Content = "";

              
            } catch (Exception ex)
            {
                label_error.Content = "Formato Incorrecto";
            }
        }

        private void radio_visa_Checked(object sender, RoutedEventArgs e)
        {
            imagen_mastercard.Visibility = Visibility.Hidden;
            imagen_aexpress.Visibility = Visibility.Hidden;
            imagen_visa.Visibility = Visibility.Visible;
        }

        private void radio_mastercard_Checked(object sender, RoutedEventArgs e)
        {
            imagen_visa.Visibility = Visibility.Hidden;
            imagen_aexpress.Visibility = Visibility.Hidden;
            imagen_mastercard.Visibility = Visibility.Visible;
        }

        private void radio_aexpress_Checked(object sender, RoutedEventArgs e)
        {
            imagen_visa.Visibility = Visibility.Hidden;
            imagen_mastercard.Visibility = Visibility.Hidden;
            imagen_aexpress.Visibility = Visibility.Visible;
        }

        private void combo_banco_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (combo_banco.SelectedIndex == 0)
            {
                fotoSantander.Visibility = Visibility.Visible;
                fotoBbva.Visibility = Visibility.Hidden;
                fotoCaixa.Visibility = Visibility.Hidden;
                fotoBankia.Visibility = Visibility.Hidden;
                fotoSabadell.Visibility = Visibility.Hidden;
            }
            if (combo_banco.SelectedIndex == 1)
            {
                fotoSantander.Visibility = Visibility.Hidden;
                fotoBbva.Visibility = Visibility.Visible;
                fotoCaixa.Visibility = Visibility.Hidden;
                fotoBankia.Visibility = Visibility.Hidden;
                fotoSabadell.Visibility = Visibility.Hidden;
            }
            if (combo_banco.SelectedIndex == 2)
            {
                fotoSantander.Visibility = Visibility.Hidden;
                fotoBbva.Visibility = Visibility.Hidden;
                fotoCaixa.Visibility = Visibility.Visible;
                fotoBankia.Visibility = Visibility.Hidden;
                fotoSabadell.Visibility = Visibility.Hidden; 
            }
            if (combo_banco.SelectedIndex == 3)
            {
                fotoSantander.Visibility = Visibility.Hidden;
                fotoBbva.Visibility = Visibility.Hidden;
                fotoCaixa.Visibility = Visibility.Hidden;
                fotoBankia.Visibility = Visibility.Visible;
                fotoSabadell.Visibility = Visibility.Hidden;
            }
            if (combo_banco.SelectedIndex == 4)
            {
                fotoSantander.Visibility = Visibility.Hidden;
                fotoBbva.Visibility = Visibility.Hidden;
                fotoCaixa.Visibility = Visibility.Hidden;
                fotoBankia.Visibility = Visibility.Hidden;
                fotoSabadell.Visibility = Visibility.Visible;
            }
        }

        private void campoCoEntidad_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(campoCoEntidad.Text, "[^0-9]"))
            {
                MessageBox.Show("Solo se aceptan números");
                campoCoEntidad.Text = campoCoEntidad.Text.Remove(campoCoEntidad.Text.Length - 1);
            }
        }

        private void campoCoSucursal_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(campoCoSucursal.Text, "[^0-9]"))
            {
                MessageBox.Show("Solo se aceptan números");
                campoCoSucursal.Text = campoCoSucursal.Text.Remove(campoCoSucursal.Text.Length - 1);
            }
        }

        private void campoCoCuenta_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(campoCoCuenta.Text, "[^0-9]"))
            {
                MessageBox.Show("Solo se aceptan números");
                campoCoCuenta.Text = campoCoCuenta.Text.Remove(campoCoCuenta.Text.Length - 1);
            }
        }
    }
}
