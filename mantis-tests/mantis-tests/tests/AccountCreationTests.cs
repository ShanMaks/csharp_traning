using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit.Framework;
using System.IO;
using System.Collections.Generic;


namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]

        public void setUpConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {

            List<AccountData> accounts = app.Admin.GetAllAccounts();
            AccountData existingAccount = accounts.Find(x => x.Name == "testuser1");
            if (existingAccount != null)
            {
            app.Admin.DeleteAccount(existingAccount);
        }

            //app.Registration.Register(account);
        }

    [OneTimeTearDown]
    public void restoreConfig()
    {
        app.Ftp.RestoreBackupFile("/config_inc.php");
    }
}
}