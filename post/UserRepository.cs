﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace post
{
    public class UserRepository
    {
        private UserRepository() { }

        static UserRepository instance;
        public static UserRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserRepository();
                return instance;
            }
        }

        internal User GetUserByLoginPassword(string login, string password)
        {
            User result = new User();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            string sql = "SELECT u.ID, u.NickName, u.Login, ab.Email, ab.Title, ab.ID AS idAddress FROM User u, AdressBook ab WHERE u.Login = @login AND u.Password = @password AND ab.ID_User = u.ID";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("login", login));
                mc.Parameters.Add(new MySqlParameter("password", password));
                using (var reader = mc.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result.ID = reader.GetInt32("id");
                        result.NickName = reader.GetString("NickName");
                        result.Email = reader.GetString("Email");
                        result.EmailTitle = reader.GetString("Title");
                        result.Login = reader.GetString("Login");
                        result.IDAddress = reader.GetInt32("idAddress");
                    }
                }
                return result;
            }
        }

        internal User AddUserByLoginPassword(string nickname,string login, string password)
        {
            User result = new User();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null) 
                return result;
            int id = MySqlDB.Instance.GetAutoID("User");
            string sql = "INSERT INTO User VALUES (0, @nickname, @login, @password, null)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("nickname", nickname));
                mc.Parameters.Add(new MySqlParameter("login", login));
                mc.Parameters.Add(new MySqlParameter("password", password));
                mc.ExecuteNonQuery(); 
                result = new User { NickName = nickname, Login = login, Password = password };
                if (mc.ExecuteNonQuery() > 0)
                {
                    sql = "";
                    foreach (var ab in result.AdressBooks)
                    sql += "INSERT INTO AdressBook VALUES (" + id + "," + ab.ID + "," + ab.Email + "," + ab.Title + "," + ab.ID_User + ");";
                    using (var mcCross = new MySqlCommand(sql, connect))
                    mcCross.ExecuteNonQuery();
                }
            }          
            return result; 
           
        }
    }
}
