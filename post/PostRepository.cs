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

        internal IEnumerable<POPEmail> GetAllPOPEmails(string sql)
        {
            var result = new ObservableCollection<POPEmail>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null) 
            return result;
            using (var mc = new MySqlCommand(sql,connect))
            using (var reader = mc.ExecuteReader()) 
            {
                POPEmail pOPEmail = new POPEmail();
                int id;
                while (reader.Read()) 
                {
                    id = reader.GetInt32("id");
                    if (pOPEmail.ID != id)
                    {
                        pOPEmail = new POPEmail();
                        pOPEmail.ID = id;
                        pOPEmail.ID_AdressFrom = reader.GetInt32("ID_AdressFrom");
                        pOPEmail.ID_AdressTo = reader.GetInt32("ID_AdressTo");
                        pOPEmail.Subject = reader.GetString("Subject");
                        pOPEmail.Body = reader.GetString("Body");
                        pOPEmail.DateSent = reader.GetDateTime("DateSent");
                    }
                    AdressBook adressBook = new AdressBook
                    {
                        ID = reader.GetInt32("adressbookId"),
                        Email = reader.GetString("adressbookEmail"),
                        Title = reader.GetString("Title"),
                    };
                    pOPEmail.AdressBooks.Add(adressBook);
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
                //if (mc.ExecuteNonQuery() > 0)
                //{
                //    sql = "";
                //    foreach (var adressbook in pOPEmail.AdressBooks)
                //        sql "
                //}
            }
        }

        internal void Remove(POPEmail pOPEmail) 
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null) return;
            string sql = "DELETE FROM Email WHERE id = ' " + pOPEmail.ID + "';";
            using (var mc = new MySqlCommand(sql,connect))
                mc.ExecuteNonQuery();
        }
        //internal IEnumerable<POPEmail> Search(string searchText, AdressBook selectedAdressBook)
        //{
        //    string sql = 
        //}

    }

   
}
