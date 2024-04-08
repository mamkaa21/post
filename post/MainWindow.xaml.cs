using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using System.Configuration;

namespace post
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

        private void Button_Enter(object sender, RoutedEventArgs e)
        {
            Enter enter = new Enter();
            enter.Show();
            this.Close();
        }

        private void Button_Registration(object sender, RoutedEventArgs e)
        {
           Registration registration = new Registration();
            registration.Show();
            this.Close();
        }
    }
}
