namespace Talabat.core.Entities.OrderAggregate
{
    public class Addrress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public Addrress()
        {
        }
        public Addrress(string firstName, string lastName, string country, string city, string street)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Country = country;
            this.City = city;
            this.Street = street;
        }

    }
}
