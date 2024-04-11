using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace post
{
    [Serializable]
    public class POPEmail
    {
        public POPEmail()
        {
            this.Attachments = new List<Attachments>();
        }
        public int MessageNumber { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime DateSent { get; set; }

        //public byte[] Image { get; set; }

        public List<Attachments> Attachments { get; set; }

        //public string Adress { get; set; }

        //public string bbody { get; set; }

        //public string ssubject { get; set; }
    }

    [Serializable]
    public class Attachments
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }



}
