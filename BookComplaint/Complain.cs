using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookComplaint
{
    public interface IComplaint
    {
        IClient ClientShop { get; set; }
        string Topic { get; }
        string Body { get; }
        string FormOfCompensation { get; }
        DateTime DateAdd { get; }
        bool Accept { get; }
    }
    public class Complaint : IComplaint
    {
        public IClient ClientShop { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
        public string FormOfCompensation { get; set; }
        public DateTime DateAdd { get; set; }
        public bool Accept { get; set; }
        public Complaint(IClient Client, string Topic, string Body, string FormOfCompensation, DateTime DateAdd, bool Accept)
        {
            this.ClientShop = Client;
            this.Topic = Topic;
            this.Body = Body;
            this.FormOfCompensation = FormOfCompensation;
            this.DateAdd = DateAdd;
            this.Accept = Accept;
        }
    }


}
