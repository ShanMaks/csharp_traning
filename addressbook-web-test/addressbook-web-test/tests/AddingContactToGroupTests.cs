using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]

        public void TestAddingContactToGroup()
        {

            app.Contacts.ContactNoToAdd();
            app.Groups.GroupNoToAddAccounts();


            GroupData group = GroupData.GetAll()[0];
            ContactData contact = ContactData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            List<ContactData> contacts = ContactData.GetAll();
            if (oldList.Count == contacts.Count)
            {
                app.Contacts.CreateContact(new ContactData("TestName111", "TestLastName222"));
                contact = ContactData.GetAll().First(i => i.Id == ContactData.MaxContactId());
            }
            else
            {
                contact = ContactData.GetAll().Except(oldList).First();
            }

            //actions
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);

        }
    }
}