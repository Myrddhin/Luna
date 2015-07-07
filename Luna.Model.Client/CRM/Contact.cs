using System.Collections.Generic;

namespace Luna.Model.CRM
{
    public class Contact : Entity
    {
        public static string DefaultName = "Nouveau contact";

        public Contact()
        {
            Name = DefaultName;
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        public Address Address { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string PhoneSecondary { get; set; }

        public string MobileSecondary { get; set; }

        public string EmailSecondary { get; set; }

        public bool NoMail { get; set; }

        public string Skype { get; set; }

        public string LinkedIn { get; set; }

        public string Twitter { get; set; }

        public string Facebook { get; set; }

        public string Description { get; set; }

        public string Comments { get; set; }

        public string Source { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}