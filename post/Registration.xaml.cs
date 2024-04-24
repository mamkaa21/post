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
using System.Windows.Shapes;

namespace post
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public string Login { get; set; }
        public string NickName { get; set; }
        public Registration()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            var user = UserRepository.Instance.AddUserByLoginPassword(NickName, Login, passwbox.Password);
            ActiveUser.Instance.SetUser(user);
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
                this.Close();            
        }
    }
}
