using System.Collections.Generic;

using Luna.Model.Storage;

namespace Luna.Model.CRM
{
    public class Tag : ClientEntity
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}