using LinqToDB.Mapping;

namespace DB.Entities;

[Table(Name = "somethings")]
public class Something
{
    [Column("id", IsIdentity = true, IsPrimaryKey = true)]
    public long Id { get; init; }

    [Column("code"), NotNull]
    public int Code { get; set; }

    [Column("value")]
    public string? Value { get; set; }
}
