//using Antheap.Model.File;
using Antheap.Model.SRTFile;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Diagnostics;
using Antheap.Model.APIwl;
using System.Net.Http;
using Antheap.Model.APIwl.Data;

namespace Antheap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WLwebAPI wlAPI = new WLwebAPI(new HttpClient());
        private List<Entity> wlItems = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            string dirname = Directory.GetCurrentDirectory();

            FileData file = new(dirname + @"\napisy do filmu.srt");

            if (file.FileOperations())
            {
                MessageBox.Show("Zmodyfikowany plik");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(NIPTextBox.Text, "[^0-9]"))
            {
                if (NIPTextBox.Text != "")
                {
                    MessageBox.Show("Użyj cyfr w formacie : 0000000000");
                    NIPTextBox.Text = NIPTextBox.Text.Remove(NIPTextBox.Text.Length - 1);
                }
            }
        }
        private void ContractorSearch_Click(object sender, RoutedEventArgs e)
        {
            ContractorSearch_ClickAsync();
        }

        private async Task ContractorSearch_ClickAsync()
        {
            try
            {
                var resultNip = await wlAPI.GetDataFromNipAsync(NIPTextBox.Text, DateTime.Now);               
                if (resultNip.Exception is null)
                {                  
                    ID.Text = resultNip.Result?.RequestId;
                    if (resultNip.Result?.Subject?.Name is not null)
                    {
                        ContractorName.Text = resultNip.Result?.Subject?.Name;
                        NIP.Text = resultNip.Result?.Subject?.Nip;
                        REGON.Text = resultNip.Result?.Subject?.Regon;
                        if (resultNip.Result?.Subject?.AccountNumbers is not null)
                        {
                            foreach (var item in resultNip.Result?.Subject?.AccountNumbers)
                            {
                                AccountNumbers.AppendText("\r\n" + item);
                            }
                            AccountNumbers.ScrollToEnd();
                        }
                        KRS.Text = resultNip.Result?.Subject.Krs;
                        ResidenceAddress.Text = resultNip.Result?.Subject?.ResidenceAddress?.ToString();
                        StatusVat.Text = resultNip.Result?.Subject?.StatusVat.ToString();
                        WorkingAddress.Text = resultNip.Result?.Subject?.WorkingAddress.ToString();
                        RegistrationLegalDate.Text = resultNip.Result?.Subject?.RegistrationLegalDate.ToString();
                        if (resultNip.Result?.Subject?.RegistrationLegalDate is not null)
                        {
                            foreach (var item in resultNip.Result?.Subject?.Representatives)
                            {
                                Representatives.AppendText(item.FirstName + " " + item.LastName + " " + item.Pesel);
                            }
                            Representatives.ScrollToEnd();
                        }
                        if (resultNip.Result?.Subject?.Partners is not null)
                        {
                            foreach (var item in resultNip.Result?.Subject?.Partners)
                            {
                                Partners.AppendText("\r\n" + item.FirstName + " " + item.LastName + " " + item.Pesel);
                            }
                            Partners.ScrollToEnd();
                        }
                        if (resultNip.Result?.Subject?.AuthorizedClerks is not null)
                        {
                            foreach (var item in resultNip.Result?.Subject?.AuthorizedClerks)
                            {
                                AuthorizedClerks.AppendText("\r\n" + item.FirstName + " " + item.LastName + " " + item.Pesel);
                            }
                            AuthorizedClerks.ScrollToEnd();
                        }
                        RemovalDate.Text = resultNip.Result?.Subject?.RemovalDate?.ToString();
                        RestorationDate.Text = resultNip.Result?.Subject?.RestorationDate?.ToString();
                        RegistrationDenialDate.Text = resultNip.Result?.Subject?.RegistrationDenialDate.ToString();
                        RegistrationDenialBasis.Text = resultNip.Result?.Subject?.RegistrationDenialBasis?.ToString();
                        RemovalBasis.Text = resultNip.Result?.Subject?.RemovalBasis?.ToString();
                        RestorationBasis.Text = resultNip.Result?.Subject?.RestorationBasis?.ToString();
                        HasVirtualAccounts.Text = resultNip.Result?.Subject?.HasVirtualAccounts?.ToString();

                        wlItems.Add(resultNip.Result?.Subject);
                        EntityItemsList.Items.Add(resultNip.Result?.Subject.Nip);
                    }
                }
                else
                {
                    MessageBox.Show($"Wystąpił błąd podczas sprawdzania: Kod {resultNip.Exception.Code} | Komunikat: {resultNip.Exception.Message}");
                }       
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"[Błąd] {ex.Message}");
            }
            
        }

        private void AccountNumbers_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
