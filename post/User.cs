﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace post
{
   public class User
   {
        public int ID { get; set; }
        public string NickName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public byte[] Image { get; set; }
        public string Email { get; internal set; }
        public string EmailTitle { get; internal set; }
        public int IDAddress { get; internal set; }

        public ObservableCollection<AdressBook> AdressBooks { get; set; } = new();
        public string EmailFrom { get; internal set; }
        public string TitleFrom { get; internal set; }

    }
}
