using Newtonsoft.Json;

namespace Application.DTOs;

public class UserDto
{
    public UserDto()
    {
    }

    public UserDto(int id)
    {
        Id = id;
    }

    [JsonConstructor]
    public UserDto(int id, string fullName, string email, string phone, DateTime birthDate, DateTime modified, bool active, string password, string role)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Phone = phone;
        BirthDate = birthDate;
        Modified = modified;
        Active = active;
        Password = password;
        Role = role;
    }

    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime Modified { get; set; }
    public bool Active { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
