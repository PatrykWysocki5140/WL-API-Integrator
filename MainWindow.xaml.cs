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

namespace Antheap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            /* //Test czytanych linii
            List<Component> components = new List<Component>();
            components = File.GetComponents();

            foreach(var obj in components)
            {
                Debug.WriteLine(
                    obj.Count.ToString () + "\n"+
                    obj.TextLineOne.ToString () + "\n"+
                    obj.TextLineTwo.ToString () + "\n"+
                    obj.StartTime.ToString () + "\n"+
                    obj.EndTime.ToString () + "\n" + "\n"
                    );
            }              
            */
        }
    }
}
