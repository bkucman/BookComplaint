using System;
using BookComplaint;
using BookComplaint.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestBoardGame
{
    [TestClass]
    public class ClientTest
    {
      
        string exmapleName = "Pablo";
        string exmapleSurname = "Kruzo";
        string exmapleNumber = "521445114";
        string exampleTopic = "Skarga!!!";
        public TestContext testContextInstance { get; set; }
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        public IComplaint MakeStubComplaint(string Topic)
        {
            var newComplaint = new StubIComplaint()
            {
                TopicGet = () =>  Topic 
                
            };
            return newComplaint;
        }

        [TestMethod]
        [TestCategory("Simple")]
        public void MakeClient_ReturnName_IfAddCorrect()
        {
            var c = new Client(exmapleName, exmapleSurname, exmapleNumber);
            var actual = c.Name;

            Assert.AreEqual(exmapleName, actual);
        }

        [TestMethod]
        [TestCategory("Simple")]
        public void CheckListComplaint_Return0_WhenListIsEmpty()
        {
            var c = new Client(exmapleName, exmapleSurname, exmapleNumber);
            var actual = c.CountComplaints();

            Assert.AreEqual(0, actual);

        }

        [TestMethod]
        [TestCategory("Stub")]
        public void AddComplaint_ReturnTableOfComplaints_CheckIfContains()
        {
            var c = new Client(exmapleName, exmapleSurname, exmapleNumber);

            c.AddedComplaints.Add(MakeStubComplaint(exampleTopic));

            var expected = "lp. 0 Topic: Skarga!!!";

            CollectionAssert.Contains(c.ShowComplainsTable(), expected);
        }


        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\AddComplaintClinet.csv", "AddComplaintClinet#csv",
            DataAccessMethod.Random),
            DeploymentItem("AddComplaintClinet.csv")]
        public void AddComplaintsFromDataSource_CheckIfListContainsOneOFThem()
        {
            string[] tabT = new string[4];
            tabT[0] = TestContext.DataRow["Topic1"].ToString();
            tabT[1] = TestContext.DataRow["Topic2"].ToString();
            tabT[2] = TestContext.DataRow["Topic3"].ToString();
            tabT[3] = TestContext.DataRow["Topic4"].ToString();
            string expected = TestContext.DataRow["Expected"].ToString();

            Client c = new Client(exmapleName, exmapleSurname, exmapleNumber);
            for (int i = 0; i < 4; i++)
            {
                c.AddedComplaints.Add(MakeStubComplaint(tabT[i]));
            }

            CollectionAssert.Contains(c.ShowComplainsTable(), expected);

        }

        [TestMethod]
        public void DropComplaintFromClient_ReturnSuccessMEssage_WhenClientExist()
        {
            Client c = new Client(exmapleName, exmapleSurname, exmapleNumber);

            c.AddedComplaints.Add(MakeStubComplaint(exampleTopic));
            var message = c.DropComplaintByID(0); // Drop complain "Skarga!!!"

            StringAssert.EndsWith(message, "successfully");

        }

        [TestMethod]
        public void DropComplainFromClient_ReturnTableWithoutDeleted()
        {
            Client c = new Client(exmapleName, exmapleSurname, exmapleNumber);

            c.AddedComplaints.Add(MakeStubComplaint(exampleTopic));
            c.AddedComplaints.Add(MakeStubComplaint("Beeee"));
            c.DropComplaintByID(0); // Drop complain "Skarga!!!"

            var s = "lp. 0 Topic: Skarga!!!";

            CollectionAssert.DoesNotContain(c.ShowComplainsTable(), s);

        }
        [TestMethod]
        public void ShowAcceptedComplaintClient_ReturnNotNullTable_WhenAddAcceptedComplaint()
        {
            var newComplain = new StubIComplaint()
            {
                TopicGet = () => { return exampleTopic; },
                AcceptGet = () => { return true; }
            };

            Client c = new Client(exmapleName, exmapleSurname, exmapleNumber);
            c.AddedComplaints.Add(newComplain);

            CollectionAssert.AllItemsAreNotNull(c.ShowComplaintsAcceptedTable());
           
        }

        [TestMethod]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void DropComplaint_ReturnException_WhenWrongIndex()
        {
            Client c = new Client(exmapleName, exmapleSurname, exmapleNumber);
            c.DropComplaintByID(0);
        }

        [TestMethod]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void TestDropComplainOnWrongIndexLowerThan0()
        {
            Client c = new Client(exmapleName, exmapleSurname, exmapleNumber);
            c.DropComplaintByID(-1);
        }

    }
}
