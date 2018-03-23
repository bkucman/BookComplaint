using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookComplaint
{
   /* public interface IComplaintManagement
    {
        List<IComplaint> Complaints { get; }
        List<IClient> Clients { get; }
        IClient FindClient(string Name, string Surname, string Number);
        bool NewClient(string Name, string Surname, string Number);
        bool DropClient(string Name, string Surname, string Number);
        bool DropClient(IClient Client);
        string AddComplaint(IClient Client, string Topic, string Body, string FormOfCompensation);

    }*/
    public class ComplaintManagement //: IComplaintManagement
    {
        public List<IComplaint> Complaints { get; set; }
        public List<IClient> Clients { get; set; }

        public ComplaintManagement()
        {
            Complaints = new List<IComplaint>();
            Clients = new List<IClient>();
        }
        
        public IClient FindClient(string Name,string Surname,string Number)
        {
            return Clients.Find(c => c.Name == Name && c.Surname == Surname && c.NumberTelephone == Number);
        }

        public bool NewClient(string Name,string Surname,string Number)
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Surname) || String.IsNullOrEmpty(Number))
                throw new ArgumentNullException();
            if(FindClient(Name,Surname,Number) != null)
            {
                return false;
            }
            else
            {
                Clients.Add(new Client(Name,Surname,Number));
                return true;
            }
        }

        public bool DropClient(string Name, string Surname, string Number)
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Surname) || String.IsNullOrEmpty(Number))
                throw new ArgumentNullException();
            if (FindClient(Name, Surname, Number) == null)
            {
                return false;
            }else
            {
                Clients.Remove(FindClient(Name, Surname, Number));
                return true;
            }
        }
        public bool DropClient(IClient Client)
        {
            if( Client == null)
            {
                return false;
            }
            if (FindClient(Client.Name, Client.Surname, Client.NumberTelephone) == null)
            {
                return false;
            }
            else
            {
                Clients.Remove(Client);
                return true;
            }
        }

        public string AddComplaint(IClient Client,string Topic,string Body, string FormOfCompensation)
        {
            if(Client == null)
            {
                return "Client required";
            }
            if(String.IsNullOrEmpty(Topic) || String.IsNullOrEmpty(Body)){
                return "Topic and Body required";
            }

            var complaintPrepare = new Complaint(Client, Topic, Body, FormOfCompensation, DateTime.Now, false);
            Client.AddedComplaints.Add(complaintPrepare);
            Complaints.Add(complaintPrepare);
            return "Added Succesfully";
        }

        public string AddComplaint(IClient Client,IComplaint Complaint)
        {
            if (Client == null)
            {
                return "Client required";
            }
            if (Complaint == null)
            {
                return "Complaint required";
            }
            Complaint.ClientShop = Client;
            //var complainPrepare = new Complaint(Client, Topic, Body, FormOfCompensation, DateTime.Now, false);
            Client.AddedComplaints.Add(Complaint);
            Complaints.Add(Complaint);
            return "Added Succesfully";
        }
    }
}
