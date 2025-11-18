namespace Solution_Opener.Models;

public class RepositoryInfo
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public DateTime LastScanned { get; set; }
    public List<SolutionInfo> Solutions { get; set; } = new();
}
