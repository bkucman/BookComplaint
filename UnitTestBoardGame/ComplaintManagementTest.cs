using System;
using BookComplaint;
using BookComplaint.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestBoardGame
{
    [TestClass]
    public class ComplaintManagementTest
    {

        string exmapleName = "Pablo";
        string exmapleSurname = "Kruzo";
        string exmapleNumber = "521445114";
        string exampleTopic = "Skarga!!!";
        string exmapleBody = "Bardzo do kitu..";
        string exmapleFormOfCompensation = "Money";

        public TestContext testContextInstance { get; set; }
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        public StubIComplaint MakeStubComplaint()
        {
            var newComplaint = new StubIComplaint()
            {
                TopicGet = () => exampleTopic,
                BodyGet = () => exmapleBody,
                AcceptGet = () => false,
                DateAddGet = () => DateTime.Now,
                FormOfCompensationGet = () => exmapleFormOfCompensation

            };
            return newComplaint;
        }

        public StubIClient MakeStubClient()
        {
            var newClient = new StubIClient()
            {
                NameGet = () =>  exmapleName,
                SurnameGet = () =>  exmapleSurname,
                NumberTelephoneGet = () => exmapleNumber,
                AddedComplaintsGet = () => new System.Collections.Generic.List<IComplaint>() 

            };
            return newClient;
        }

        public StubIClient MakeStubClient(string n,string s,string num)
        {
            var newClient = new StubIClient()
            {
                NameGet = () => n,
                SurnameGet = () => s,
                NumberTelephoneGet = () => num,
                AddedComplaintsGet = () => new System.Collections.Generic.List<IComplaint>()

            };
            return newClient;
        }

        [TestMethod]
        public void MakeNewObjectComplaintManagement_Return0_WhenListClientsIsEmpty()
        {
            var CM = new ComplaintManagement();

            Assert.AreEqual(0, CM.Clients.Count);
        }
        [TestMethod]
        public void MakeNewObjectComplaintManagement_Return0_WhenListComplaintsIsEmpty()
        {
            var CM = new ComplaintManagement();

            Assert.AreEqual(0, CM.Complaints.Count);
        }

        [TestMethod]
        public void AddComplaint_ReturnErrorMessage_WhenNullClient()
        {
            Client c = null;
            var CM = new ComplaintManagement();
            var result = CM.AddComplaint(c, exampleTopic, exmapleBody, exmapleFormOfCompensation);

            Assert.AreEqual("Client required", result);
        }

        [TestMethod]
        public void AddComplaint_ReturnErrorMessage_WhenEmptyTopic()
        {
            var c = MakeStubClient();
            var CM = new ComplaintManagement();
            var result = CM.AddComplaint(c, "", exmapleBody, exmapleFormOfCompensation);

            Assert.AreEqual("Topic and Body required", result);
        }

        [TestMethod]
        public void AddComplaint_ReturnErrorMessage_WhenNullBody()
        {
            var c = MakeStubClient();
            var CM = new ComplaintManagement();
            var result = CM.AddComplaint(c,exampleTopic, null, exmapleFormOfCompensation);

            Assert.AreEqual("Topic and Body required", result);
        }

        [TestMethod]

        public void AddNewClient_Return1_WhenIsAddOneClient()
        {
           
            var CM = new ComplaintManagement();

            CM.NewClient(exmapleName,exmapleSurname,exmapleNumber);

            Assert.AreEqual(CM.Clients.Count, 1);

        }
        [TestMethod]
        public void AddNewClientAndFind_ReturnClient_WhenExist()
        {
           
            var CM = new ComplaintManagement();

            CM.NewClient(exmapleName, exmapleSurname, exmapleNumber);

            Assert.IsNotNull(CM.FindClient(exmapleName, exmapleSurname, exmapleNumber));
        }

        [TestMethod]
        public void AddClient_ReturnFlase_WhenClienExist()
        {
            var CM = new ComplaintManagement();
            CM.NewClient(exmapleName, exmapleSurname, exmapleNumber);

            Assert.IsFalse(CM.NewClient(exmapleName, exmapleSurname, exmapleNumber));
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void AddClient_RetrunExceptions_WhenSomeValueIsNull()
        {
            var CM = new ComplaintManagement();
            CM.NewClient(exmapleName,null, exmapleNumber);

        }

        [TestMethod]
        public void FindClient_ReturnNull_WhenEmptyListClients()
        {
            var CM = new ComplaintManagement();

            Assert.IsNull(CM.FindClient(exmapleName, exmapleSurname, exmapleNumber)); 

        }
        [TestMethod]
        public void DropClient_ReturnFalse_ObjectNull()
        {
            var CM = new ComplaintManagement();
            Client c = null;

            CM.NewClient(exmapleName, exmapleSurname, exmapleNumber);

            var result = CM.DropClient(c);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DropClient_RetrunTrue_WhenClientExist()
        {
            var CM = new ComplaintManagement();
            var c = MakeStubClient();

            CM.NewClient(exmapleName, exmapleSurname, exmapleNumber);

            var result = CM.DropClient(c);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DropClient_ReturnFalse_WhenOneValueIsDiferent()
        {
            var CM = new ComplaintManagement();
            var c = MakeStubClient();

            CM.NewClient(exmapleName, exmapleSurname, "513888789");

            var result = CM.DropClient(c);

            Assert.IsFalse(result);
        }

        /// Drop with string
        [TestMethod]
        public void DropUClientWithStrings_ReturnTrue_WhenStringsIsCorrect()
        {
            var CM = new ComplaintManagement();

            CM.NewClient(exmapleName, exmapleSurname, exmapleNumber);

            var result = CM.DropClient(exmapleName,exmapleSurname,exmapleNumber);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void DropClient_ReturnFalase_WhenOneValueIsNull()
        {
            var CM = new ComplaintManagement();

            CM.NewClient(exmapleName, exmapleSurname, exmapleNumber);

            var result = CM.DropClient(exmapleName, exmapleSurname, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DropClientStrings_ReturnFalase_WhenClientNotExist()
        {
            var CM = new ComplaintManagement();

            CM.NewClient(exmapleName, exmapleSurname, exmapleNumber);

            var result = CM.DropClient(exmapleName, exmapleSurname, "588214521");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddComplaint_ReturnSuccessMessage_WhenCorrect()
        {
            var CM = new ComplaintManagement();
            var c = MakeStubClient();

            var result = CM.AddComplaint(c, exampleTopic, exmapleBody, exmapleFormOfCompensation);

            StringAssert.Equals("Added Succesfully", result);
        }

        [TestMethod]
        public void AddComplaintObjects_ReturnSuccessMessage_WhenObjectIsCorrect()
        {
            var c = MakeStubClient();
            var complaint = MakeStubComplaint();
            var CM = new ComplaintManagement();

            var result = CM.AddComplaint(c, complaint);

            Assert.AreEqual("Added Succesfully", result);

        }
        [TestMethod]
        public void AddComplaintObjects_ReturnTopic_WhenAdded()
        {
            var c = MakeStubClient();
            var complaint = MakeStubComplaint();
            var CM = new ComplaintManagement();

            var result = CM.AddComplaint(c, complaint);

            Assert.AreEqual(exampleTopic,CM.Complaints[0].Topic);

        }

        [TestMethod]
        public void AddThreeClients_ReturnListClients_WhenAllAddSuccessfully()
        {
            var CM = new ComplaintManagement();
            IClient[] cc = new StubIClient[3];
            for (int i = 0; i < 3; i++)
            {
                cc[i] = MakeStubClient(i.ToString(), exmapleSurname, exmapleNumber);
                CM.Clients.Add(cc[i]);
            }

            CollectionAssert.Contains(CM.Clients, cc[1]);

        }

        [TestMethod]
        public void AddThreeClients_ReturnListClients_CheckifFourtContains_ExpectedNot()
        {
            var CM = new ComplaintManagement();
            
            for (int i = 0; i < 3; i++)
            {
                CM.Clients.Add(MakeStubClient(i.ToString(), exmapleSurname, exmapleNumber));
            }
            var notAdded = MakeStubClient(exmapleName, exmapleSurname, exmapleNumber);

            CollectionAssert.DoesNotContain(CM.Clients, notAdded);

        }

        [TestMethod]
        public void AddThreeClients_ReturnCount4_WhenAllIsAdded()
        {
            var CM = new ComplaintManagement();
            IClient[] cc = new StubIClient[4];
            for (int i = 0; i < 4; i++)
            {
                cc[i] = MakeStubClient(i.ToString(), exmapleSurname, exmapleNumber);
                CM.Clients.Add(cc[i]);
            }

            Assert.AreEqual(CM.Clients.Count, 4);

        }

    }
}
