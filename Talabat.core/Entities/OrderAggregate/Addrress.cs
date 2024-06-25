namespace Talabat.core.Entities.OrderAggregate
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public Address()
        {
        }
        public Address(string firstName, string lastName, string country, string city, string street)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Country = country;
            this.City = city;
            this.Street = street;
        }

    }
}
