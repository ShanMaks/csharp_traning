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
            ContactData contact = new ContactData("Tom");
            contact.LastName = "Spot";
            app.Contacts.CreateContact(contact);
            app.Auth.Logout();
        }
    }
}
