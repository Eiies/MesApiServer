using System.ComponentModel.DataAnnotations;

namespace MesApiServer.Models;

public class UserDto{
    [Key] 
    public int Id{ get; set; }

    [Required] 
    public required string Name{ get; set; }

    public int Age{ get; set; }
}