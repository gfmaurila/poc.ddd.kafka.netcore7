using Newtonsoft.Json;

namespace Application.DTOs;

public class WorkerUserDto
{
    public WorkerUserDto()
    {
    }

    public WorkerUserDto(int id)
    {
        Id = id;
    }

    [JsonConstructor]
    public WorkerUserDto(int id, int db, int tempo, string key, string fullName, string email, string phone, DateTime birthDate, DateTime modified, bool active, string role)
    {
        Id = id;
        Db = db;
        Tempo = tempo;
        Key = key;
        FullName = fullName;
        Email = email;
        Phone = phone;
        BirthDate = birthDate;
        Modified = modified;
        Active = active;
        Role = role;
    }

    public int Id { get; set; }
    public int Db { get; set; }
    public int Tempo { get; set; }
    public string Key { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime Modified { get; set; }
    public bool Active { get; set; }
    public string Role { get; set; }
}
