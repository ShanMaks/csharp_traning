using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {

        [Test]
        public void TheContactCreationTestsTest()
        {
            ContactData contact = new ContactData("Name", "LastName");
            app.Contacts.CreateContact(contact);
        }
    }
}
