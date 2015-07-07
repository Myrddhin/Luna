namespace Luna.Model.CRM
{
    public class Address : Entity
    {
        public string Name { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}