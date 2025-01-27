namespace DESAFIO_MOVISIS.DataStore;

public static class Constants
{
    public const string DatabaseFilename = "sbpn.db3";
    public const SQLiteOpenFlags Flags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;
    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}
