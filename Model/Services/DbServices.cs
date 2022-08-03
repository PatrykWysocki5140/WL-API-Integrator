using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Antheap.Model.APIwl.Data;
using MySql.Data.MySqlClient;



namespace Antheap.Model.Services
{
    //string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=antheap;";
    //status 1 = Representatives
    //status 2 = Partners
    //status 3 = AuthorizedClerks
    internal class DbServices
    {
        public string SqlServerConnectionString { get; set; }

        public DbServices(string sqlServerConnectionString)
        {
            SqlServerConnectionString = sqlServerConnectionString;
        }

        public void InsertCompany(Entity company)
        {
            bool checkCompany = false;
            string query;
            if (!CheckCompany(company.Nip))
            {
                checkCompany = true;
                query = $"INSERT INTO `company`( `Nip`, `StatusVat`, `Regon`, `Pesel`, `Krs`, `ResidenceAddress`, `WorkingAddress`, `Name`, `RegistrationLegalDate`, `RegistrationDenialDate`, `RegistrationDenialBasis`, `RestorationDate`, `RestorationBasis`, `RemovalDate`, `RemovalBasis`, `HasVirtualAccounts`)  VALUES ( '{company.Nip}','{company.StatusVat}', '{company.Regon}', '{company.Pesel}', '{company.Krs}','{company.ResidenceAddress}','{company.WorkingAddress}','{company.Name}','{company.RegistrationLegalDate}','{company.RegistrationDenialDate}','{company.RegistrationDenialBasis}','{company.RestorationDate}','{company.RestorationBasis}','{company.RemovalDate}', '{company.RemovalBasis}','{company.HasVirtualAccounts}') ";
            }
            else
            {
                query = $"UPDATE  `company` SET `StatusVat`='{company.StatusVat}', `Regon`='{company.Regon}', `Pesel`='{company.Pesel}', `Krs`='{company.Krs}', `ResidenceAddress`='{company.ResidenceAddress}', `WorkingAddress`='{company.WorkingAddress}', `Name`='{company.Name}', `RegistrationLegalDate`='{company.RegistrationLegalDate}', `RegistrationDenialDate`='{company.RegistrationDenialDate}', `RegistrationDenialBasis`='{company.RegistrationDenialBasis}', `RestorationDate`='{company.RestorationDate}', `RestorationBasis`='{company.RestorationBasis}', `RemovalDate`='{company.RemovalDate}', `RemovalBasis`='{company.RemovalBasis}', `HasVirtualAccounts`='{company.HasVirtualAccounts}' WHERE Nip = {company.Nip}";
            }
            MySqlConnection databaseConnection = new MySqlConnection(SqlServerConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;
           
            try
            {
                
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                List<string> accountlist = company.AccountNumbers;
                foreach (string account in accountlist)
                {
                    SetAccountNo(account, company.Nip);
                }

                foreach (EntityPerson obj in company.Representatives)
                {
                    SetPerson(obj, company.Nip, "1");
                }
                foreach (EntityPerson obj in company.Partners)
                {
                    SetPerson(obj, company.Nip,"2");
                }
                foreach (EntityPerson obj in company.AuthorizedClerks)
                {
                    SetPerson(obj, company.Nip, "3");
                }
                if (checkCompany)
                {
                    MessageBox.Show("Dodano firmę.");
                }
                else
                {
                    MessageBox.Show("Zaktualizowano firmę.");
                }

                databaseConnection.Close();
            }
            catch (System.Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }


        }

        private bool CheckCompany(string nip)
        {
            string query = $"SELECT Nip FROM `company` WHERE Nip = '{nip}'";

            MySqlConnection databaseConnection = new MySqlConnection(SqlServerConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;

            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(0).ToString() == nip) 
                            return true;
                    }
                }
                databaseConnection.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        public void DeleteCompany(Entity company)
        {
            string query = $"DELETE  FROM `company` WHERE Nip = {company.Nip}";

            MySqlConnection databaseConnection = new MySqlConnection(SqlServerConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                databaseConnection.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public Company SelectCompany(string nip)
        {
            Company company = new Company();

            string query = $"SELECT * FROM `company` WHERE Nip = {nip}";

            MySqlConnection databaseConnection = new MySqlConnection(SqlServerConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {                        
                        company.Nip = reader.GetString(0).ToString();
                        company.StatusVat = reader.GetString(1).ToString();
                        company.Regon = reader.GetString(2).ToString();
                        company.Pesel = reader.GetString(3).ToString();
                        company.Krs = reader.GetString(4).ToString();
                        company.ResidenceAddress = reader.GetString(5).ToString();
                        company.WorkingAddress = reader.GetString(6).ToString();
                        company.Name = reader.GetString(7).ToString();
                        company.RegistrationLegalDate = reader.GetString(8);
                        company.RegistrationDenialDate = reader.GetString(9);
                        company.RegistrationDenialBasis = reader.GetString(10).ToString();
                        company.RestorationDate = reader.GetString(11);
                        company.RestorationBasis = reader.GetString(12).ToString();
                        company.RemovalDate = reader.GetString(13);
                        company.RemovalBasis = reader.GetString(14).ToString();
                        company.HasVirtualAccounts = Convert.ToBoolean(reader.GetString(15));

                        company.AccountNumbers = GetAccountNo(company.Nip);
                        company.Representatives = GetPerson(company.Nip,"1");
                        company.Partners = GetPerson(company.Nip,"2");
                        company.AuthorizedClerks = GetPerson(company.Nip,"3");

                        reader.Close();                       
                        return company;
                    }
                }
                else
                {
                    Console.WriteLine("Nie znaleziono firmy.");
                }

                databaseConnection.Close();
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SelectCompany " + ex.Message);
            }
            return company;
        }
        public List<Company> SelectCompany(bool selectall)
        {
            
            List<Company> list = new();
            string query;

            if (selectall)
            {
                query = $"SELECT * FROM `company`";
            }
            else
            {
                query = $"SELECT * FROM `company` WHERE Nip = 1111111111";
            }


            MySqlConnection databaseConnection = new MySqlConnection(SqlServerConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Company company = new Company();
                        company.Nip = reader.GetString(0).ToString();
                        company.StatusVat = reader.GetString(1).ToString();
                        company.Regon = reader.GetString(2).ToString();
                        company.Pesel = reader.GetString(3).ToString();
                        company.Krs = reader.GetString(4).ToString();
                        company.ResidenceAddress = reader.GetString(5).ToString();
                        company.WorkingAddress = reader.GetString(6).ToString();
                        company.Name = reader.GetString(7).ToString();
                        company.RegistrationLegalDate = reader.GetString(8);
                        company.RegistrationDenialDate = reader.GetString(9);
                        company.RegistrationDenialBasis = reader.GetString(10).ToString();
                        company.RestorationDate = reader.GetString(11);
                        company.RestorationBasis = reader.GetString(12).ToString();
                        company.RemovalDate = reader.GetString(13);
                        company.RemovalBasis = reader.GetString(14).ToString();
                        company.HasVirtualAccounts = Convert.ToBoolean(reader.GetString(15));

                        company.AccountNumbers = GetAccountNo(company.Nip);
                        company.Representatives = GetPerson(company.Nip, "1");
                        company.Partners = GetPerson(company.Nip, "2");
                        company.AuthorizedClerks = GetPerson(company.Nip, "3");
                        
                        list.Add(company);
                    }
                    reader.Close();
                }
                else
                {
                    Console.WriteLine("Nie znaleziono firmy.");
                }

                databaseConnection.Close();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SelectCompany " + ex.Message);
            }
            return list;
        }
        private void SetAccountNo(string accountNo,string nip)
        {
            string query = $"INSERT INTO `accountnumbers`(`Nip`, `AccountNumber`) VALUES ('{nip}','{accountNo}')";

            MySqlConnection databaseConnection = new MySqlConnection(SqlServerConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<string> GetAccountNo(string nip)
        {
            List<string> accountNo = new List<string>();

            string query =$"SELECT AccountNumber FROM `accountnumbers` WHERE Nip = '{nip}'";

            MySqlConnection databaseConnection = new MySqlConnection(SqlServerConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;

            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {                     
                        accountNo.Add(reader.GetString(0));
                    }
                }
                databaseConnection.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return accountNo;
        }

        private void SetPerson(EntityPerson person, string nip, string status)
        {
            string query = $"INSERT INTO `person`(`FirstName`, `LastName`, `Pesel`, `Nip`, `Status`) VALUES ('{person.FirstName}','{person.LastName}','{person.Pesel}','{nip}','{status}')";

            MySqlConnection databaseConnection = new MySqlConnection(SqlServerConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private List<Person> GetPerson(string nip, string status)
        {
            List<Person> personList = new List<Person>();
            Person person = new ();

            string query = $"SELECT* FROM `person` WHERE Nip = '{nip}' AND Status = '{status}'";

            MySqlConnection databaseConnection = new MySqlConnection(SqlServerConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;

            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {                    
                        person.FirstName = reader.GetString(0);
                        person.LastName = reader.GetString(1);
                        person.Pesel = reader.GetString(2);

                        personList.Add(person);
                    }
                }
                databaseConnection.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return personList;
        }

    }
}
