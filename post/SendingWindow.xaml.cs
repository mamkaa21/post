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
using Microsoft.Win32;
using System.IO;
using System.Net.Mime;
using OpenPop.Mime;
using System.Collections;
using System.Windows.Interop;
using System.Xml.Linq;

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
            message.Image = iimage;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.beget.com";
            smtpClient.Port = 25;
            smtpClient.EnableSsl = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(fromAdress.Address, "D35de%TJ");


//            Directory.GetFiles(Image, "*.txt").ToList().ForEach(
//    name => message.Attachments.Add(new Attachment(name, MediaTypeNames.Text.Plain))
//);
            //message.Attachments.Add(new Attachment("C://IMG_20231112_171210_408.jpg"));
            smtpClient.Send(message); 
            this.Close();
        }
        public string Adress { get; set; }
        public string bbody { get; set; }
        public string ssubject { get; set; }
        public byte[] iimage { get; set; }
      


        //private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        string selectedImagePath = openFileDialog.FileName;
        //        byte[] imageData = File.ReadAllBytes(selectedImagePath);

        //        BitmapImage bitmapImage = new BitmapImage();
        //        bitmapImage.BeginInit();
        //        bitmapImage.StreamSource = new MemoryStream(imageData);
        //        bitmapImage.EndInit();

        //        SelectedImage.Source = bitmapImage;
        //    }
        //}

      
    }
}
