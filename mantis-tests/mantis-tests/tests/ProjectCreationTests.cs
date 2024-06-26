﻿using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : TestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            List<ProjectData> oldProjects = app.API.GetProjects(account);
            ProjectData newProject = new ProjectData("Project" + TestBase.GenerateRandomSting(5));

            app.Project.Create(newProject);

            List<ProjectData> newProjects = app.API.GetProjects(account);
            oldProjects.Add(newProject);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects.Count, newProjects.Count);
        }
    }
}