using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests

{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            ContactData newData = new ContactData("TestName123", "TestLastName123");
            
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            if (app.Contacts.NoContactToSelected())
            {
                app.Contacts.CreateContact(newData);
            }
            app.Contacts.Remove(0);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);
        }
    }
}
