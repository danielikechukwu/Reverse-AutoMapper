namespace ReverseAutoMapperDemo.Models
{
    public class Address
    {
        public int Id { get; set; } // Unique identifier for the address

        public string City { get; set; } // City of the address

        public string State { get; set; } // State of the address

        public string Country { get; set; } // Country of the address

        public int EmployeeId { get; set; } // Foreign key to Employee

        public Employee Employee { get; set; } // Navigation property for Employee

    }
}
