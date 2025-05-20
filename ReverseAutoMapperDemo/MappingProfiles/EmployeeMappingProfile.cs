using AutoMapper;
using ReverseAutoMapperDemo.DTOs;
using ReverseAutoMapperDemo.Models;

namespace ReverseAutoMapperDemo.MappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile() {

            // Define two-way mapping between Employee and EmployeeDTO.
            // This maps Employee.Id to EmployeeDTO.EmployeeId, combines FirstName and LastName into FullName,
            // and maps the Address properties to their corresponding DTO fields.

            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                // Enable reverse mapping: this automatically creates the mapping from EmployeeDTO to Employee
                .ReverseMap()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => GetFirstName(src.FullName)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => GetFirstName(src.FullName)));
        }

        // Extracts the first name from FullName.
        private static string GetFirstName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return string.Empty;

            var names = fullName.Split(' ');
            return names.FirstOrDefault() ?? string.Empty;

        }

        // Extracts the last name from FullName.
        public static string GetLastName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return string.Empty;

            var names = fullName.Split(' ');

            // If there is more than one word, combine the rest as the last name
            return names.Length > 1 ? string.Join(" ", names.Skip(1)) : string.Empty;
        }
    }
}
