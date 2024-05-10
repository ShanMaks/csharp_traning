using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ContactHelper Modify(int p, ContactData newContact)
        {
            manager.Navigator.GoToHomePage();
            InitContactModify(p);
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

        public ContactHelper ReturnToConctactPage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper InitContactModify(int p)
        {
            p++;
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + p + "]/td[8]/a/img")).Click();
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
    }
}

