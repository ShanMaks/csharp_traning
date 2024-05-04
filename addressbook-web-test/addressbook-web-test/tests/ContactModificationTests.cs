using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests

{
    [TestFixture]
    public class AccountModifyTests : TestBase
    {
        [Test]
        public void AccountModifyTest()
        {
            ContactData newContact = new ContactData("Test1");
            newContact.LastName = "Test2";

            app.Contacts.Modify(1, newContact);

        }
    }
}
