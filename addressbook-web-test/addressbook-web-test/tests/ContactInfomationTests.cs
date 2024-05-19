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
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void ContactInformationTest()
        {
            ContactData fromTable = app.Contacts.GetContactInfomationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInfomationFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]

        public void ContactDetailsInformationTest()
        {
            ContactData fromPropertyPage = app.Contacts.GetContactInformationFromPropertyPage(0);
            ContactData fromForm = app.Contacts.GetContactInfomationFromEditForm(0);

            Assert.AreEqual(fromPropertyPage.ContactDetails, fromForm.ContactDetails);
        }
    }
}
