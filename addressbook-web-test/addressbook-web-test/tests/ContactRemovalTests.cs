using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests

{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            ContactData newData = new ContactData("TestName123", "TestLastName123");

            if (app.Contacts.NoContactToSelected() == false)
            {
                app.Contacts.CreateContact(newData);
            }

            List<ContactData> oldContacts = ContactData.GetAll();

            app.Contacts.Remove(0);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];
            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
