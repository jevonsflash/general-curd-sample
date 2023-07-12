namespace Matoapp.Common;

public static class CommonDbProperties
{
    public static string DbTablePrefix { get; set; } = "Common";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Common";
}
