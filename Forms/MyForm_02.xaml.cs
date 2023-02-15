using Microsoft.Win32;
using System;
using System.Collections.Generic;
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


namespace RAA_Level2
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MyForm : Window
    {
        public MyForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //load csv file
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.RestoreDirectory= true;
            openFile.Filter = "csv files(*.csv)|*.csv";

            if (openFile.ShowDialog() == true)
            {
                inputBox.Text = openFile.FileName;

            }
            else
            {
                inputBox.Text = "";
            }

        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();   
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        //parameters
        public string GetTextBoxValue()
        {
            return inputBox.Text;
        }

        public bool GetLevelBox() 
        {
            if (level_box.IsChecked == true)
                return true;
            else
                return false;
            
        }
        public bool GetceilingBox()
        {
            if(ceiling_box.IsChecked == true) 
                return true;
            else
                return false;
        }
    }
}
