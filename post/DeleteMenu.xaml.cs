using OpenPop.Mime;
using OpenPop.Pop3;
using post;
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

namespace post
{
    /// <summary>
    /// Логика взаимодействия для DeleteMenu.xaml
    /// </summary>
    public partial class DeleteMenu : Window
    {
        public DeleteMenu()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        public ObservableCollection<POPEmail> Email { get; set; } = new();


     
    }
}


//        pop3Client = ConnectMail();
//        int count = 0;
//             try
//             {
//                count = pop3Client.GetMessageCount();
//             }
//            catch { return; }

//var lastCountFor = lastCount;
//lastCount = count;

//int counter = 0;
//Message message;
//for (int i = count; i > lastCountFor; i--)
//{
//    try
//    {
//        message = pop3Client.GetMessage(i);
//    }
//    catch (Exception e)
//    {
//        MessageBox.Show(e.Message);
//        continue;
//    }
//    POPEmail email = new POPEmail()
//    {
//        MessageNumber = i,
//        Subject = message.Headers.Subject,
//        DateSent = message.Headers.DateSent,
//        From = message.Headers.From.Address,
//    };

//    MessagePart body = message.FindFirstHtmlVersion();
//    if (body != null)
//    {
//        email.Body = HttpUtility.HtmlDecode(Regex.Replace(body.GetBodyAsText(), "<(.|\n)*?>", ""));
//    }
//    else
//    {
//        body = message.FindFirstPlainTextVersion();
//        if (body != null)
//        {
//            email.Body = body.GetBodyAsText();
//        }
//    }
//    List<MessagePart> attachments = message.FindAllAttachments();

//    foreach (MessagePart part in attachments)
//    {
//        email.Attachments.Add(new Attachments
//        {
//            FileName = part.FileName,
//            ContentType = part.ContentType.MediaType,
//            Content = part.Body
//        });
//    }
//    this.Dispatcher.Invoke(() =>
//    {
//        if (first)
//            Email.Add(email);
//        else
//            Email.Insert(0, email);
//    });
//    counter++;
//}
//first = false;

//try
//{
//    pop3Client.Disconnect();
//}
//catch { }
//          }
//    }
//}
