using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web;
using System.Security.Policy;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmail;
        private string contactDetails;
        private string detailsName;
        private string detailsPhone;

        public ContactData(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
            Address = "";
            MobilePhone = "";
            WorkPhone = "";
            HomePhone = "";
            Email = "";
            Email_2 = "";
            Email_3 = "";
        }

        public ContactData()
            {
            }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return (FirstName + LastName).GetHashCode();
        }

        public override string ToString()
        {
            return "firstname=" + FirstName + "\nlastname" + LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (this.FirstName != other.FirstName)
            {
                return FirstName.CompareTo(other.FirstName);
            }
            if (this.LastName != other.LastName)
            {
                return LastName.CompareTo(other.LastName);
            }
            return 0;
        }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "firstname")]
        public string FirstName { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email_2 { get; set; }

        [Column(Name = "email3")]
        public string Email_3 { get; set; }


        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        public string AllEmail
        {
            get
            {
                if (allEmail != null)
                {
                    return allEmail;
                }
                else
                {
                    return (NextLine(Email) + NextLine(Email_2) + NextLine(Email_3)).Trim();
                }
            }
            set
            {
                allEmail = value;
            }
        }

        public string DetailsPhone
        {
            get
            {
                if (detailsPhone != null)
                {
                    return detailsPhone;
                }
                else
                {
                    return (NextLinePhone(HomePhone, "H: ") + NextLinePhone(MobilePhone, "M: ") + NextLinePhone(WorkPhone, "W: ")).Trim(); ;
                }
            }
            set
            {
                detailsPhone = value;
            }
        }

        public string DetailsName
        {
            get
            {
                if (detailsName != null)
                {
                    return detailsName;
                }
                else
                {
                    return NextLine(FirstName + " " + LastName).Trim();
                }
            }
            set
            {
                detailsName = value;
            }
        }

        public string ContactDetails
        {
            get
            {
                if (contactDetails != null)
                {
                    return contactDetails;
                }
                else
                {
                    if (Address == null || Address == "")
                    {
                        return (NextLineDouble(DetailsName) + NextLineDouble(DetailsPhone) + AllEmail).Trim();

                    }
                    else
                    {
                        return (NextLine(DetailsName) + NextLineDouble(Address) + NextLineDouble(DetailsPhone) + AllEmail).Trim();
                    }

                }
            }
            set
            {
                contactDetails = value;
            }
        }

        private string NextLine(string detail)
        {
            if (detail == null || detail == "")
            {
                return "";
            }
            return detail + "\r\n";
        }

        private string NextLineDouble(string detail)
        {
                if (detail == null || detail == "")
                {
                    return "";
                }
            return detail + "\r\n\r\n";
        }

        private string NextLinePhone(string phone, string letter)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return letter + phone + "\r\n";
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Accounts select c).ToList();
            }
        }
    }
}