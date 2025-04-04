using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Entities;

public class SomethingEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("code")]
    public int Code { get; set; }

    [Column("value")]
    public string? Value { get; set; }
}
