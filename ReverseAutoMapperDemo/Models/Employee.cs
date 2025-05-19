namespace ReverseAutoMapperDemo.Models
{
    public class Employee
    {
        public int Id { get; set; } // Primary key for the employee

        public string FirstName { get; set; } // Employee's first name

        public string LastName { get; set; } // Employee's last name

        public string Email { get; set; } // Employee's email address

        public string Gender { get; set; } // Employee's gender

        public Address Address { get; set; } // Navigation property to the Address entity

    }
}
