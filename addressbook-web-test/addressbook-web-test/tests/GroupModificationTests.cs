using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests

{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("yyy");
            newData.Header = null;
            newData.Footer = null;

            GroupData group = new GroupData("test1");
            group.Header = "test2";
            group.Footer = "test3";

            if (app.Groups.NoGroupsToSelected())
            {
                app.Groups.Create(group);
            }

            app.Groups.Modify(1, newData);
        }
    }
}
