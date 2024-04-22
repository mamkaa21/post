using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace post
{
    public class PostRepository
    {
        private PostRepository() { }

        static PostRepository instance;
        public static PostRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new PostRepository();
                return instance;
            }
        }

        //internal User GetUserByLoginPassword(string login, string password)
        //{
        //    User result = new User();
        //    var connect = MySqlDB.Instance.GetConnection();
        //    if (connect == null)
        //        return result;
        //    string sql = "SELECT u.ID, u.NickName, u.Login, ab.Email, ab.Title, ab.ID AS idAddress FROM User u, AdressBook ab WHERE u.Login= @login AND u.Password = @password AND ab.ID_User = u.ID";
        //    using (var mc = new MySqlCommand(sql, connect))
        //    {
        //        mc.Parameters.Add(new MySqlParameter("login", login));
        //        mc.Parameters.Add(new MySqlParameter("password", password));
        //        using (var reader = mc.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                result.ID = reader.GetInt32("id");
        //                result.NickName = reader.GetString("NickName");
        //                result.Email = reader.GetString("Email");
        //                result.EmailTitle = reader.GetString("Title");
        //                result.Login = reader.GetString("Login");
        //                result.IDAddress = reader.GetInt32("idAddress");
        //            }
        //        }
        //        return result;
        //    }
        //}

        internal IEnumerable<POPEmail> GetAllPOPEmails()
        {
            ObservableCollection<POPEmail> result = new ObservableCollection<POPEmail>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            string sql = "SELECT Email.ID, ID_AdressFrom, Subject,Body, DateSend, AdressBook.Email, AdressBook.Title FROM Email, AdressBook where ID_AdressTo = " + ActiveUser.Instance.GetUser().IDAddress + " AND ID_AdressFrom = AdressBook.ID";
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                while (reader.Read())
                {
                    var pOPEmail = new POPEmail();
                    pOPEmail.ID = reader.GetInt32("id");
                    pOPEmail.ID_AdressFrom = reader.GetInt32("ID_AdressFrom");
                    //pOPEmail.ID_AdressTo = reader.GetInt32("ID_AdressTo");
                    pOPEmail.Subject = reader.GetString("Subject");
                    pOPEmail.Body = reader.GetString("Body");
                    pOPEmail.DateSent = reader.GetDateTime("DateSend");
                    pOPEmail.EmailFrom = reader.GetString("Email");
                    pOPEmail.TitleFrom = reader.GetString("Title");
                    result.Add(pOPEmail);   
                }
            }
            return result;
        }

        internal void AddPOPEmail(POPEmail pOPEmail)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null) return;
            int id = MySqlDB.Instance.GetAutoID("Email");
            string sql = "INSERT INTO Email VALUES (0, @id_AdressFrom, @id_AdressTo, @subject, @body, @datesent)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("subject", pOPEmail.Subject));
                mc.Parameters.Add(new MySqlParameter("body", pOPEmail.Body));
                mc.Parameters.Add(new MySqlParameter("datesent", pOPEmail.DateSent));
            }
        }

        internal void RemovePOPEmail(POPEmail pOPEmail)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null) return;
            string sql = "DELETE FROM Email WHERE id = ' " + pOPEmail.ID + "';";
            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }

        internal IEnumerable<POPEmail> Search(string searchText, AdressBook selectedAdressBook)
        {
            string sql = "select  e.ID_AdressFrom, e.Subject, e.Body, e.DateSend, ab.Email, ab.Title from Email e, AdressBook ab";
            if (selectedAdressBook.ID != 0)
            {
                var result = GetAllPOPEmails().Where(s => s.AdressBooks.FirstOrDefault(s => s.ID == selectedAdressBook.ID) != null);
                return result;
            }
            return GetAllPOPEmails();
        }

        internal void UpdatePOPEmail(POPEmail pOPEmail)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null) return;
            string sql = "delete from Email";
            using (var mc = new MySqlCommand(sql,connect))
                mc.ExecuteNonQuery();
            sql = "update Email set ID_AdressFrom = @id_AdressFrom, ID_AdressTo = @id_AdressTo, Subject = @subject, Body =  @body, DateSent =  @datesent where ID = " + pOPEmail.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("subject", pOPEmail.Subject));
                mc.Parameters.Add(new MySqlParameter("body", pOPEmail.Body));
                mc.Parameters.Add(new MySqlParameter("datesent", pOPEmail.DateSent));
                mc.ExecuteNonQuery();
            }
        }

    }


}
