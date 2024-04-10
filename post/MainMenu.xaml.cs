using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OpenPop.Mime;
using OpenPop.Pop3;
using System.Windows.Threading;

namespace post
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window, INotifyPropertyChanged
    {
        Pop3Client pop3Client;
        public MainMenu()
        {
            InitializeComponent();
            pop3Client = ConnectMail();
            DataContext = this;
        }

        //timer = new DispatcherTimer();
        //timer.Tick += new EventHandler(timer_Tick);
        //timer.Interval = new TimeSpan(0,0,1);
        //timer.Start();

        private void Button_Send_Window(object sender, EventArgs e)
        {
            SendingWindow sendingWindow = new SendingWindow();
            sendingWindow.Show();
        }

        private void Button_Upgrate(object sender, RoutedEventArgs e)
        {
            if (pop3Client.Connected)
                pop3Client.Disconnect();
            pop3Client = ConnectMail();
            int count = pop3Client.GetMessageCount();
            var emails = new ObservableCollection<POPEmail>();

            int counter = 0;
            for (int i = count; i >= 1; i--)
            {
                Message message = pop3Client.GetMessage(i);

                POPEmail email = new POPEmail()
                {
                    MessageNumber = i,
                    Subject = message.Headers.Subject,
                    DateSent = message.Headers.DateSent,
                    From = message.Headers.From.Address,
                };

                MessagePart body = message.FindFirstHtmlVersion();
                if (body != null)
                {
                    email.Body = body.GetBodyAsText();
                }
                else
                {
                    body = message.FindFirstPlainTextVersion();
                    if (body != null)
                    {
                        email.Body = body.GetBodyAsText();
                    }
                }
                List<MessagePart> attachments = message.FindAllAttachments();

                foreach (MessagePart part in attachments)
                {
                    email.Attachments.Add(new Attachments
                    {
                        FileName = part.FileName,
                        ContentType = part.ContentType.MediaType,
                        Content = part.Body
                    });
                }
                emails.Add(email);
                counter++;
            }

            Email = emails;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
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
        public ObservableCollection<POPEmail> Email { get; set; }
        
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
        
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (POPEmail != null)
                MessageBox.Show("Выбран обьект");
            pop3Client.DeleteMessage(POPEmail.MessageNumber);
            Email.Remove(POPEmail);



        }

    }
}
