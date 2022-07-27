using Antheap.Model.File;
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
            MessageBox.Show(dirname);
            FileData File = new(dirname + @"\napisy do filmu.srt");

            Component item = new();
            item = File.GetComponent(1);
            
            MessageBox.Show(item.EndTime);

            List<Component> components = new List<Component>();
            components = File.GetComponents();

            foreach(var obj in components)
            {
                MessageBox.Show(
                    obj.Count.ToString () + "\n"+
                    obj.TextLineOne.ToString () + "\n"+
                    //obj.TextLineTwo.ToString () + "\\n"+
                    obj.StartTime.ToString () + "\n"+
                    obj.EndTime.ToString () + "\n"
                    );
            }

        }
    }
}
