namespace ReverseAutoMapperDemo.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; } // Mapped from Employee.Id

        public string FullName { get; set; } // Combined first and last names

        public string Email { get; set; }    // Employee's email address

        public string Gender { get; set; }   // Employee's gender

        // Address properties extracted from the Address model
        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

    }
}
