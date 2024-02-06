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


namespace wpf_whatifSample2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window1 window1;

        private bool firstchange = true;
        // private bool firstchange2 = true;
        public MainWindow()
        {
            InitializeComponent();
        }
        Class1 proper = new Class1();
        //   blob_properties12 blob = new blob_properties12();
        public void Next_button(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Subcri_text.Text))
            {
                MessageBox.Show("Please provide the subscription field");
            }
            else if (string.IsNullOrWhiteSpace(cli_id_text.Text))
            {
                MessageBox.Show("Please provide the client id field");
            }
            else if (string.IsNullOrWhiteSpace(Tnt_txt.Text))
            {
                MessageBox.Show("Please provide the tenant id field");
            }
            else if (string.IsNullOrWhiteSpace(cli_sec_txt.Text))
            {
                MessageBox.Show("Please provide the client secret field");
            }
            else
            {
                proper.sub_id = Subcri_text.Text;

                proper.cli_id = cli_id_text.Text;

                proper.cli_sect = cli_sec_txt.Text;
                //proper.dep_name = window1.txtName4.Text;

                proper.Ten_id = Tnt_txt.Text;
                //proper.dep_mode =window1.drop.SelectionBoxItem.ToString();

                if (window1 == null)
                {
                    window1 = new Window1(proper, this);
                }
                this.Hide();

                window1.Show();

                firstchange = false;
                // firstchange2 = false;
            }

        }
        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!firstchange)
            {
                window1.Validate_btn.IsEnabled = true;

                window1.Deploy_btn.IsEnabled = false;
                //  window1.btnAdd8.IsEnabled = true;

                window1.dataGrid1.Visibility = Visibility.Collapsed;

                firstchange = true;
            }
        }

    }
}