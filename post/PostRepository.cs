using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
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
            using (var)
        }
    }

   
}
