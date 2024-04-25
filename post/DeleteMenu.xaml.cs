using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;

namespace post
{
    /// <summary>
    /// Логика взаимодействия для DeleteMenu.xaml
    /// </summary>
    public partial class DeleteMenu : Window
    {
        Pop3Client pop3Client;
        public DeleteMenu()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void But_Back(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            Close();
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
        public POPEmail POPEmail
        {
            get => email;
            set
            {
                email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(POPEmail)));
            }
        }

       

        private void But_DelPost(object sender, EventArgs e)
        {
            if (POPEmail == null)
            {
                MessageBox.Show("Не выбран обьект");
                return;
            }
            try
            {
                pop3Client = ConnectMail();
                pop3Client.DeleteMessage(POPEmail.MessageNumber);
                pop3Client.Disconnect();
                var index = POPEmail.MessageNumber;
                Email.Remove(POPEmail);
                var sort = Email.ToArray();
                Array.Sort(sort, (x, y) => y.DateSent.CompareTo(x.DateSent));
                for (int i = 0; i < sort.Length; i++)
                    sort[i].MessageNumber = i + 1;
            }
            catch { }
        }
    }
}

