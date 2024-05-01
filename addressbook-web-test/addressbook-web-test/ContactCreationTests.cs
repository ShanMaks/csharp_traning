using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {

        [Test]
        public void TheContactCreationTestsTest()
        {
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            GoToContactCreation();
            ContactData contact = new ContactData("Tom");
            contact.LastName = "Spot";
            FillConctactForm(contact);
            SubmitConctactCreation();
            ReturnToConctactPage();
            Logout();
        }
    }
}
