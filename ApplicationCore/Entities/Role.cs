using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;
[Table("Roles")]
public class Role
{
    public int Id { get; set; }
    [MaxLength(20)]
    public string Name { get; set; }
    
    public ICollection<User> User { get; set; }
}