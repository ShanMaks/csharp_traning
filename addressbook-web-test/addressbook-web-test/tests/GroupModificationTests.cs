using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests

{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
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

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeModify = oldGroups[0];

            app.Groups.Modify(toBeModify, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData groups in newGroups)
            {
                if (groups.Id == toBeModify.Id)
                {
                    Assert.AreEqual(newData.Name, groups.Name);
                }
            }
        }
    }
}
