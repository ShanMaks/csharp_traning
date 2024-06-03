using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests

{
    [TestFixture]
    public class ContactModifyTests : ContactTestBase
    {
        [Test]
        public void ContactModifyTest()
        {
            ContactData newContact = new ContactData("Name123", "LastName123");
            ContactData oldContact = new ContactData("TEST1", "LastName2");

            if (app.Contacts.NoContactToSelected() == false)
            {
                app.Contacts.CreateContact(oldContact);
            }

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeModify = oldContacts[0];

            app.Contacts.Modify(toBeModify, newContact);

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].FirstName = newContact.FirstName;
            oldContacts[0].LastName = newContact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}