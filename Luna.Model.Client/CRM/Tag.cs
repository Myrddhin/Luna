using System.Collections.Generic;

namespace Luna.Model.CRM
{
    public class Tag : Entity
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}