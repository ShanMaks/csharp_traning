using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests

{
    [TestFixture]
    public class ContactRemovalTest : TestBase
    {
        [Test]
        public void AccountModifyTest()
        {
            app.Groups.Remove(1);
            app.Auth.Logout();
        }
    }
}
