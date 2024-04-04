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

        private void Button_Click(object sender, EventArgs e)
        {
            MailAddress fromAdress = new MailAddress("alina1125@suz-ppk.ru", "mmn");
            MailAddress toAdress = new MailAddress("kim1evda@gmail.com", "mmn");
            MailMessage message = new MailMessage(fromAdress, toAdress);
            message.Body = "AAAAAAAAAA";
            message.Subject = "hjdjvsd";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.beget.com";
            smtpClient.Port = 25;
            smtpClient.EnableSsl = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(fromAdress.Address, "D35de%TJ");
            smtpClient.Send(message);


        }

      
    }
}
