namespace Matoapp.Health;

public static class HealthDbProperties
{
    public static string DbTablePrefix { get; set; } = "Health";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Health";
}
