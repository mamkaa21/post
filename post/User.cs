﻿using System;
using System.Collections.Generic;
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
        
   }
}