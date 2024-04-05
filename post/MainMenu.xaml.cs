using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using OpenPop.Mime;
using OpenPop.Pop3;

namespace post
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Button_Send_Window(object sender, EventArgs e)
        {
            SendingWindow sendingWindow = new SendingWindow();
            sendingWindow.Show();
        }

        //    private void Button_Sending(object sender, EventArgs e)
        //{
        //    MailAddress fromAdress = new MailAddress("alina1125@suz-ppk.ru", "mmn");
        //    MailAddress toAdress = new MailAddress("kim1evda@gmail.com", "mmn");
        //    MailMessage message = new MailMessage(fromAdress, toAdress);
        //    message.Body = "Вы отчислены";
        //    message.Subject = "Вам бан";

        //    SmtpClient smtpClient = new SmtpClient();
        //    smtpClient.Host = "smtp.beget.com";
        //    smtpClient.Port = 25;
        //    smtpClient.EnableSsl = false;
        //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpClient.Credentials = new NetworkCredential(fromAdress.Address, "D35de%TJ");
        //    smtpClient.Send(message);
        //}

        private void Button_Upgrate(object sender, RoutedEventArgs e)
        {
            Pop3Client pop3Client = new Pop3Client();

            var username = "alina1125@suz-ppk.ru";
            var password = "D35de%TJ";

            pop3Client.Connect("pop3.beget.com", 110, false);
            pop3Client.Authenticate(username, password, AuthenticationMethod.UsernameAndPassword);
            int count = pop3Client.GetMessageCount();
            var emails = new List<POPEmail>();

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

            test.ItemsSource = emails;

        }

    }
}
