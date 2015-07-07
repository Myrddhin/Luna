using System.Data.Entity.ModelConfiguration;
using Luna.Model.CRM;

namespace Luna.Data.Local.SQLite.TablesConfiguration
{
    internal class ContactConfiguration : EntityTypeConfiguration<Contact>
    {
        public ContactConfiguration()
        {
            ToTable("Contacts");
            HasKey(x => x.Id);
            Property(x => x.Comments).HasColumnName("comment");
            Property(x => x.Description).HasColumnName("description");
            Property(x => x.Email).HasColumnName("email");
            Property(x => x.EmailSecondary).HasColumnName("email_second");
            Property(x => x.NoMail).HasColumnName("no_mail");
            Property(x => x.Facebook).HasColumnName("facebook");
            Property(x => x.Fax).HasColumnName("fax");
            Property(x => x.LinkedIn).HasColumnName("linkedin");
            Property(x => x.Mobile).HasColumnName("mobile");
            Property(x => x.MobileSecondary).HasColumnName("mobile_second");
            Property(x => x.Name).HasColumnName("name");
            Property(x => x.Phone).HasColumnName("phone");
            Property(x => x.PhoneSecondary).HasColumnName("phone_second");
            Property(x => x.Skype).HasColumnName("skype");
            Property(x => x.Source).HasColumnName("source");
            Property(x => x.Surname).HasColumnName("surname");
            Property(x => x.Title).HasColumnName("title");
            Property(x => x.Twitter).HasColumnName("twitter");

            HasOptional(x => x.Address).WithOptionalDependent().Map(m => m.MapKey("id_address"));

            HasMany(x => x.Tags).WithMany(x => x.Contacts).Map(m =>
            {
                m.ToTable("R_Contacts_Tags");
                m.MapLeftKey("id_contact");
                m.MapRightKey("id_tag");
            });
        }
    }
}