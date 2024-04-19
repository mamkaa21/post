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
    /// Логика взаимодействия для Enter.xaml
    /// </summary>
    public partial class Enter : Window
    {
        public  string Login {  get; set; }

        public Enter()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            var user = PostRepository.Instance.GetUserByLoginPassword(Login, passwordBox.Password);
            if (user.ID != 0)
            {
                ActiveUser.Instance.SetUser(user);
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
                this.Close();
            }
            else MessageBox.Show("Введите логин и пароль");
        }
    }
}
