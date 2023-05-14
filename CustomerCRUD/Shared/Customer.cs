using FluentNHibernate.Mapping;

namespace CustomerCRUD.Shared
{
    public class Customer
    {
        public Customer()
        {
            BirthDate = DateTime.Now.Date;
            Gender = GenderEnum.Unspecified;
        }

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual GenderEnum Gender { get; set; }
        public enum GenderEnum
        {
            Male,
            Female,
            Unspecified
        }

        public virtual DateTime BirthDate { get; set; }

        public virtual string Address { get; set; }

        public virtual string Country { get; set; }

        public virtual int PostalCode { get; set; }

        public virtual string Email { get; set; }
    }

    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Surname);
            Map(x => x.Gender);
            Map(x => x.BirthDate);
            Map(x => x.Address);
            Map(x => x.Country);
            Map(x => x.PostalCode);
            Map(x => x.Email);
            Table("tblCustomer");
        }
    }
}