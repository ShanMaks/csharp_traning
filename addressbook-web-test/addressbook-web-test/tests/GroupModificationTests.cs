using System;
using System.Collections.Generic;
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

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData oldData = oldGroups[0];

            app.Groups.Modify(0, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData groups in newGroups)
            {
                if (groups.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, groups.Name);
                }
            }
        }
    }
}
