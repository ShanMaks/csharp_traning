﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public ContactHelper CreateContact(ContactData contact)
        {
            manager.Navigator.GoToContactCreation();
            FillContactForm(contact);
            SubmitConctactCreation();
            ReturnToConctactPage();
            return this;
        }

        public ContactHelper Modify(int index, ContactData newContact)
        {
            manager.Navigator.GoToHomePage();
            InitContactModify(index);
            FillContactForm(newContact);
            SubmitConctactModify();
            return this;
        }

        public ContactHelper Modify(ContactData contact, ContactData newContact)
        {
            manager.Navigator.GoToHomePage();
            InitContactModify(contact.Id);
            FillContactForm(newContact);
            SubmitConctactModify();
            return this;
        }

        public ContactHelper Remove(int v)
        {
            SelectContact(v);
            SubmitContactRemove();
            contactCache = null;
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(contact.Id);
            SubmitContactRemove();
            contactCache = null;
            return this;
        }

        public ContactHelper FillContactInfo(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email_2);
            Type(By.Name("email3"), contact.Email_3);

            return this;
        }

        internal void RemoveContactFromGroup(ContactData contactsInGroups, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroupToRemove(group.Name);
            SelectIdContact(contactsInGroups.Id);
            CommitRemovingContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            return this;
        }

        public ContactHelper SubmitConctactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[20]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.XPath($"//input[@name='selected[]' and @value='{id}']")).Click();
            return this;
        }

        public ContactHelper ReturnToConctactPage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper InitContactModify(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[7].FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper InitContactModify(string id)
        {
            driver.FindElement(By.XPath($"//input[@name='selected[]' and @value='{id}']//..//../td[8]/a")).Click();
            return this;
        }
        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper SubmitConctactModify()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactRemove()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public bool NoContactToSelected()
        {
            return !IsElementPresent(By.XPath("img[@title = Edit]"));
        }

        public ContactHelper InitOpenProperties(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[6].FindElement(By.TagName("a")).Click();
            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    contactCache.Add(new ContactData(element.FindElement(By.CssSelector("td:nth-child(3)")).Text, element.FindElement(By.CssSelector("td:nth-child(2)")).Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            manager.Navigator.GoToHomePage();
            return driver.FindElements(By.Name("entry")).Count;
        }

        public ContactData GetContactInfomationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));

            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmail = allEmails,
            };
        }

        public ContactData GetContactInfomationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(0);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email_2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email_3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone, 
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
            };
        }

        public int GetNumberOfSearchResult()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

        public ContactData GetContactInformationFromPropertyPage(int index)
        {
            manager.Navigator.GoToHomePage();
            InitOpenProperties(index);

            string contactDetails = driver.FindElement(By.Id("content")).Text;

            return new ContactData
            {
                ContactDetails = contactDetails
            };
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectIdContact(contact.Id);
            SelectGroupToAdd(group.Name);
            ConmmitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void ConmmitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void SelectIdContact(string contactId)
        {
            //driver.FindElement(By.Id(contactId));
            driver.FindElement(By.XPath($"//input[@name='selected[]' and @value='{contactId}']")).Click();
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void SelectGroupToRemove(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        public void CommitRemovingContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void ContactNoToAdd()
        {
            List<ContactData> contacts = ContactData.GetAll();

            if (contacts.Count == 0)
            {
                ContactData contact = new ContactData("Name", "LastName");

                CreateContact(contact);
            }
        }

    }
}

