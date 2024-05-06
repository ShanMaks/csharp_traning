using System;
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

            if (app.Contacts.NoContactToSelected())
            {
                app.Contacts.CreateContact(newData);
            }
            app.Contacts.Remove(1);
        }
    }
}
