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
using System.Windows.Shapes;

namespace post
{
    /// <summary>
    /// Логика взаимодействия для SendingWindow.xaml
    /// </summary>
    public partial class SendingWindow : Window
    {
        public SendingWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Button_Sending(object sender, EventArgs e)
        {            
            MailAddress fromAdress = new MailAddress("alina1125@suz-ppk.ru", "mmn");
            MailAddress toAdress = new MailAddress(Adress);
            MailMessage message = new MailMessage(fromAdress, toAdress);
            message.Body = bbody;
            message.Subject = ssubject;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.beget.com";
            smtpClient.Port = 25;
            smtpClient.EnableSsl = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(fromAdress.Address, "D35de%TJ");
            smtpClient.Send(message);

            this.Close();
        }

       
        public string Adress { get; set; }
        public string bbody { get; set; }
        public string ssubject { get; set; }
    }
}
