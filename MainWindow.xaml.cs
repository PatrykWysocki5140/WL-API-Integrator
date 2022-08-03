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
using Antheap.Model.Services;

namespace Antheap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WLwebAPI wlAPI = new WLwebAPI(new HttpClient());
        private List<Entity> wlItems = new();
        private List<Company> dbItems = new();

        DbServices db = new DbServices("datasource=127.0.0.1;port=3306;username=root;password=;database=antheap;");

        public MainWindow()
        {
            InitializeComponent();
            dbItems = db.SelectCompany(true);
            foreach(Company company in dbItems)
            {
                EntityItemsList.Items.Add(company.Nip);
            }
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
                Entity entity = new Entity();
                

                if (resultNip.Exception is null)
                {                  
                    ID.Text = resultNip.Result?.RequestId;
                    if (resultNip.Result?.Subject?.Name is not null)
                    {
                        EntityItemsList.Items.Add(resultNip.Result?.Subject.Nip);
                        entity = resultNip.Result?.Subject;
                        wlItems.Add(entity);
                        db.InsertCompany(entity);
                        LoadContractor(db.SelectCompany(entity.Nip));
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
        private void LoadContractor(Company entity)
        {
            ClearContractor();

            ContractorName.Text = entity.Name;
            NIP.Text = entity.Nip;
            REGON.Text = entity?.Regon;
            
            if (entity?.AccountNumbers is not null)
            {
                foreach (var item in entity?.AccountNumbers)
                {
                    AccountNumbers.AppendText("\r\n" + item);
                }
                AccountNumbers.ScrollToEnd();
            }
            KRS.Text = entity?.Krs;
            ResidenceAddress.Text = entity?.ResidenceAddress?.ToString();
            StatusVat.Text = entity?.StatusVat?.ToString();
            WorkingAddress.Text = entity?.WorkingAddress?.ToString();
            RegistrationLegalDate.Text = entity?.RegistrationLegalDate?.ToString();
            if (entity?.RegistrationLegalDate is not null)
            {
                foreach (var item in entity?.Representatives)
                {
                    Representatives.AppendText(item.FirstName + " " + item.LastName + " " + item.Pesel);
                }
                Representatives.ScrollToEnd();
            }
            if (entity?.Partners is not null)
            {
                foreach (var item in entity?.Partners)
                {
                    Partners.AppendText("\r\n" + item.FirstName + " " + item.LastName + " " + item.Pesel);
                }
                Partners.ScrollToEnd();
            }
            if (entity?.AuthorizedClerks is not null)
            {
                foreach (var item in entity?.AuthorizedClerks)
                {
                    AuthorizedClerks.AppendText("\r\n" + item.FirstName + " " + item.LastName + " " + item.Pesel);
                }
                AuthorizedClerks.ScrollToEnd();
            }
            RemovalDate.Text = entity?.RemovalDate?.ToString();
            RestorationDate.Text = entity?.RestorationDate?.ToString();
            RegistrationDenialDate.Text = entity?.RegistrationDenialDate?.ToString();
            RegistrationDenialBasis.Text = entity?.RegistrationDenialBasis?.ToString();
            RemovalBasis.Text = entity?.RemovalBasis?.ToString();
            RestorationBasis.Text = entity?.RestorationBasis?.ToString();
            HasVirtualAccounts.Text = entity?.HasVirtualAccounts?.ToString();
        }

        private void ClearContractor()
        {
            ContractorName.Text = String.Empty;
            NIP.Text = String.Empty;
            REGON.Text = String.Empty;
            AccountNumbers.Document.Blocks.Clear();         
            KRS.Text = String.Empty;
            ResidenceAddress.Text = String.Empty;
            StatusVat.Text = String.Empty;
            WorkingAddress.Text = String.Empty;
            RegistrationLegalDate.Text = String.Empty;
            Representatives.Document.Blocks.Clear();
            Partners.Document.Blocks.Clear();
            AuthorizedClerks.Document.Blocks.Clear();
            RemovalDate.Text = String.Empty;
            RestorationDate.Text = String.Empty;
            RegistrationDenialDate.Text = String.Empty;
            RegistrationDenialBasis.Text = String.Empty;
            RemovalBasis.Text = String.Empty;
            RestorationBasis.Text = String.Empty;
            HasVirtualAccounts.Text = String.Empty;
        }

        private void AccountNumbers_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void EntityItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadContractor(db.SelectCompany(EntityItemsList.SelectedItem.ToString()));
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
