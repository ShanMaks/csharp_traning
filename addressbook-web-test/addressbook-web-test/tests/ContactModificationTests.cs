using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests

{
    [TestFixture]
    public class ContactModifyTests : AuthTestBase
    {
        [Test]
        public void ContactModifyTest()
        {
            ContactData newContact = new ContactData("Name123", "LastName123");
            if (app.Contacts.NoContactToSelected())
            {
                app.Contacts.CreateContact(newContact);
            }
            ContactData contact = new ContactData("TEST1", "LastName2");
            app.Contacts.Modify(1, newContact, contact);

        }
    }
}
