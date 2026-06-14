namespace GP.Database;

public static class PathHelper
{
    public static string GetDatabasePath(string dbFileName)
    {
        string folderPath = FileSystem.Current.AppDataDirectory;
        string fullPath = Path.Combine(folderPath, dbFileName);
        
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        return fullPath;
    }
}