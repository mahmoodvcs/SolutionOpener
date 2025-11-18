namespace Solution_Opener.Models;

public class AppConfiguration
{
    public List<RepositoryInfo> Repositories { get; set; } = new();
    public WindowSettings Window { get; set; } = new();
    public string LastSelectedRepoPath { get; set; } = string.Empty;
    public List<string> RecentSolutionPaths { get; set; } = new();
    public List<string> FavoriteSolutionPaths { get; set; } = new();
}

public class WindowSettings
{
    public double Width { get; set; } = 1000;
    public double Height { get; set; } = 600;
    public double Left { get; set; } = 100;
    public double Top { get; set; } = 100;
    public bool IsMaximized { get; set; }
}
