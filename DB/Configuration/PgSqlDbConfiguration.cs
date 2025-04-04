using System.ComponentModel.DataAnnotations;

namespace DB.Configuration;

public class PgSqlDbConfiguration
{
    public const string SectionName = nameof(PgSqlDbConfiguration);

    [Required]
    public string ConnectionString { get; set; }
}
