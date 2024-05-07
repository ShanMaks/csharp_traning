using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
            ContactData contact = new ContactData("TEST1", "LastName2");

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            if (app.Contacts.NoContactToSelected())
            {
                app.Contacts.CreateContact(newContact);
            }

            app.Contacts.Modify(1, newContact, contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].FirstName = contact.FirstName;
            oldContacts[0].LastName = contact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
