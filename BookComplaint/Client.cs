using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookComplaint
{
    public interface IClient
    {
        string Name { get; }
        string Surname { get; }
        string NumberTelephone { get; }
        List<IComplaint> AddedComplaints { get; }

        string[] ShowComplainsTable();
        string DropComplaintByID(int id);
        string[] ShowComplaintsAcceptedTable();

        int CountComplaints();

        

    }
    public class Client : IClient
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NumberTelephone { get; set; }
        public List<IComplaint> AddedComplaints { get; set; }

        public Client(string Name,string Surname,string NumberTelephone)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.NumberTelephone = NumberTelephone;
            this.AddedComplaints = new List<IComplaint>();

        }
        public string[] ShowComplainsTable()
        {
            string[] tab = new string[AddedComplaints.Count];
            for(int i = 0; i < AddedComplaints.Count; i++)
            {
                var s = "lp. " + i + " Topic: " + AddedComplaints[i].Topic;
                tab[i] = s;
            }
            return tab;
        }

        public string DropComplaintByID(int id)
        {
            if(id >=0 && id < AddedComplaints.Count)
            {
                AddedComplaints.RemoveAt(id);
                return "Drop successfully";
            }
            else
            {
                var mess = "Incorrect index, shuld be between 0 and " + (AddedComplaints.Count - 1).ToString() ;
                throw new IndexOutOfRangeException(mess);
            }
        }

        public string[] ShowComplaintsAcceptedTable()
        {
            string[] tab = null;
            if (AddedComplaints.Count > 0)
            {
                 tab = new string[AddedComplaints.Count];
            }
            
            for (int i = 0; i < AddedComplaints.Count; i++)
            {
                if(AddedComplaints[i].Accept == true)
                    tab[i] = "lp. " + i + "Topic: " + AddedComplaints[i].Topic;
            }
            return tab;
        }

        public int CountComplaints()
        {
            return AddedComplaints.Count;
        }

    }
}
