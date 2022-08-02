using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antheap.Model.APIwl.Data
{
    internal class Entity
    {
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Nip
        /// </summary>
        public string Nip { get; set; }

        /// <summary>
        /// Status podatnika VAT.
        /// </summary>
        /// <value>Status podatnika VAT. </value>
        public string StatusVat { get; set; }

        /// <summary>
        /// Numer identyfikacyjny REGON
        /// </summary>
        /// <value>Numer identyfikacyjny REGON </value>
        public string Regon { get; set; }

        /// <summary>
        /// Gets or Sets Pesel
        /// </summary>
        public Pesel Pesel { get; set; }

        /// <summary>
        /// numer KRS jeżeli został nadany
        /// </summary>
        /// <value>numer KRS jeżeli został nadany </value>
        public string Krs { get; set; }

        /// <summary>
        /// Adres siedziby
        /// </summary>
        /// <value>Adres siedziby </value>
        public string ResidenceAddress { get; set; }

        /// <summary>
        /// Adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania w przypadku braku adresu stałego miejsca prowadzenia działalności
        /// </summary>
        /// <value>Adres stałego miejsca prowadzenia działalności lub adres miejsca zamieszkania w przypadku braku adresu stałego miejsca prowadzenia działalności </value>
        public string WorkingAddress { get; set; }

        /// <summary>
        /// Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i/lub PESEL
        /// </summary>
        /// <value>Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i/lub PESEL </value>
        public List<EntityPerson> Representatives { get; set; }

        /// <summary>
        /// Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL
        /// </summary>
        /// <value>Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL </value>
        public List<EntityPerson> AuthorizedClerks { get; set; }

        /// <summary>
        /// Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numeryNIP i/lub PESEL
        /// </summary>
        /// <value>Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numeryNIP i/lub PESEL </value>
        public List<EntityPerson> Partners { get; set; }

        /// <summary>
        /// Data rejestracji jako podatnika VAT
        /// </summary>
        /// <value>Data rejestracji jako podatnika VAT </value>
        public DateTime? RegistrationLegalDate { get; set; }

        /// <summary>
        /// Data odmowy rejestracji jako podatnika VAT
        /// </summary>
        /// <value>Data odmowy rejestracji jako podatnika VAT </value>
        public DateTime? RegistrationDenialDate { get; set; }

        /// <summary>
        /// Podstawa prawna odmowy rejestracji
        /// </summary>
        /// <value>Podstawa prawna odmowy rejestracji </value>
        public string RegistrationDenialBasis { get; set; }

        /// <summary>
        /// Data przywrócenia jako podatnika VAT
        /// </summary>
        /// <value>Data przywrócenia jako podatnika VAT </value>
        public DateTime? RestorationDate { get; set; }

        /// <summary>
        /// Podstawa prawna przywrócenia jako podatnika VAT
        /// </summary>
        /// <value>Podstawa prawna przywrócenia jako podatnika VAT </value>
        public string RestorationBasis { get; set; }

        /// <summary>
        /// Data wykreślenia odmowy rejestracji jako podatnika VAT
        /// </summary>
        /// <value>Data wykreślenia odmowy rejestracji jako podatnika VAT </value>
        public DateTime? RemovalDate { get; set; }

        /// <summary>
        /// Podstawa prawna wykreślenia odmowy rejestracji jako podatnika VAT
        /// </summary>
        /// <value>Podstawa prawna wykreślenia odmowy rejestracji jako podatnika VAT </value>
        public string RemovalBasis { get; set; }

        /// <summary>
        /// Gets or Sets AccountNumbers
        /// </summary>
        public List<string> AccountNumbers { get; set; }

        /// <summary>
        /// Podmiot posiada maski kont wirtualnych
        /// </summary>
        /// <value>Podmiot posiada maski kont wirtualnych </value>
        public bool? HasVirtualAccounts { get; set; }
    }
}
