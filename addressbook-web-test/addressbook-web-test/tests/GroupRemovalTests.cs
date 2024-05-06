using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            GroupData group = new GroupData("Тест1");
            group.Header = "Тест2";
            group.Footer = "Тест3";

            if (app.Groups.NoGroupToSelected())
            {
                app.Groups.Create(group);
            }
            app.Groups.Remove(1);
        }
    }
}
