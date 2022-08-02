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
    internal class DbServices
    {
        public string SqlServerConnectionString { get; set; }

        public DbServices(string sqlServerConnectionString)
        {
            SqlServerConnectionString = sqlServerConnectionString;
        }

        public void InsertCompany(Entity company)
        {
            string query = $"INSERT INTO `company`( `Nip`, `StatusVat`, `Regon`, `Pesel`, `Krs`, `ResidenceAddress`, `WorkingAddress`, `Name`, `RegistrationLegalDate`, `RegistrationDenialDate`, `RegistrationDenialBasis`, `RestorationDate`, `RestorationBasis`, `RemovalDate`, `RemovalBasis`, `HasVirtualAccounts`)  VALUES ( '{company.Nip}','{company.StatusVat}', '{company.Regon}', '{company.Pesel}', '{company.Krs}','{company.ResidenceAddress}','{company.WorkingAddress}','{company.Name}','{company.RegistrationLegalDate}','{company.RegistrationDenialDate}','{company.RegistrationDenialBasis}','{company.RestorationDate}','{company.RestorationBasis}','{company.RemovalDate}', '{company.RemovalBasis}','{company.HasVirtualAccounts}') ";

            MySqlConnection databaseConnection = new MySqlConnection(SqlServerConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;

            try
            {
                
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Dodano firmę.");

                databaseConnection.Close();
            }
            catch (System.Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }


        }
        public void UpdateCompany(Entity company)
        {
            string query = $"UPDATE  `company` SET `StatusVat`='{company.StatusVat}', `Regon`='{company.Regon}', `Pesel`='{company.Pesel}', `Krs`='{company.Krs}', `ResidenceAddress`='{company.ResidenceAddress}', `WorkingAddress`='{company.WorkingAddress}', `Name`='{company.Name}', `RegistrationLegalDate`='{company.RegistrationLegalDate}', `RegistrationDenialDate`='{company.RegistrationDenialDate}', `RegistrationDenialBasis`='{company.RegistrationDenialBasis}', `RestorationDate`='{company.RestorationDate}', `RestorationBasis`='{company.RestorationBasis}', `RemovalDate`='{company.RemovalDate}', `RemovalBasis`='{company.RemovalBasis}', `HasVirtualAccounts`='{company.HasVirtualAccounts}' WHERE Nip = {company.Nip}";

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
        public Entity SelectCompany()
        {
            Entity company = new Entity();

            string query = "SELECT * FROM `company`";

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
                        company.Nip = reader.GetString(1);
                        company.StatusVat = reader.GetString(2);
                        company.Regon = reader.GetString(3);
                        company.Pesel = (Pesel)reader.GetString(4);
                        company.Krs = reader.GetString(5);
                        company.ResidenceAddress = reader.GetString(6);
                        company.WorkingAddress = reader.GetString(7);
                        company.Name = reader.GetString(8);
                        company.RegistrationLegalDate = DateTime.Parse(reader.GetString(9));
                        company.RegistrationDenialDate = DateTime.Parse(reader.GetString(10));
                        company.RegistrationDenialBasis = reader.GetString(11);
                        company.RestorationDate = DateTime.Parse(reader.GetString(12));
                        company.RestorationBasis = reader.GetString(13);
                        company.RemovalDate = DateTime.Parse(reader.GetString(14));
                        company.RemovalBasis = reader.GetString(15);
                        company.HasVirtualAccounts = Convert.ToBoolean(reader.GetString(16));
                        
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
                MessageBox.Show(ex.Message);
            }

            return company;
        }

    }
}
