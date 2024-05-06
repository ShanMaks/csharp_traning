using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [SetUpFixture]
    public class TestSuiteFixture
    {
        public static ApplicationManager app;
        [SetUp]
        public void InitApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

        [TearDown]
        public void StopApplicationManager()
        {
            app.Stop();
        }
    }
}