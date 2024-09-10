using System.ComponentModel.DataAnnotations;

namespace MetaBank.Model.Base;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}