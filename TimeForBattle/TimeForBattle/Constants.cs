using CommunityToolkit.Maui.Views;

namespace TimeForBattle;

public static class Constants
{
    public const string DatabaseFileName = "Battle.db3";

    public const SQLite.SQLiteOpenFlags Flags =
    // open the database in read/write mode
    SQLite.SQLiteOpenFlags.ReadWrite |
    // create the database if it doesn't exist
    SQLite.SQLiteOpenFlags.Create |
    // enable multi-threaded database access
    SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    //public static string AndroidPath => Path.Combine(Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath, DatabaseFileName);

    public static int[] Integers { get => [1, 2, 3, 4, 5, 6];}
    public static string[] Attributes { get => ["STR", "DEX", "CON", "INT", "WIS", "CHA"]; }
    public static bool[] Bools { get => [false, true]; }
}
