using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для WindowSendPost.xaml
    /// </summary>
    public partial class WindowSendPost : Window
    {
        public WindowSendPost()
        {
            InitializeComponent();
            DataContext = this;
        }
        private static Pop3Client ConnectMail()
        {
            Pop3Client pop3Client = new Pop3Client();

            var username = "alina1125@suz-ppk.ru";
            var password = "D35de%TJ";

            pop3Client.Connect("pop3.beget.com", 110, false);
            pop3Client.Authenticate(username, password, AuthenticationMethod.UsernameAndPassword);
            return pop3Client;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<POPEmail> Email { get; set; } = new();

        private POPEmail email;
        public POPEmail pOPEmail
        {
            get => email;
            set
            {
                email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(POPEmail)));
            }
        }
        private void But_Back(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            Close();
        }

    }
}
