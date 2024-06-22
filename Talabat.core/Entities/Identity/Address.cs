namespace Talabat.core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string street { get; set; }
        /// <summary>
        ///  - AppUserId (string) : [Identity User]
        /// </summary>
        public string AppUserId { get; set; }//Foreign Key
        public AppUser User { get; set; } //Navigational Property [one] [one] untill Now 
    }
}