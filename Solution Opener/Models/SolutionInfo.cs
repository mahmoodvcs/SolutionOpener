namespace Solution_Opener.Models;

public class SolutionInfo
{
    public string Name { get; set; } = string.Empty;
    public string FullPath { get; set; } = string.Empty;
    public string RelativePath { get; set; } = string.Empty;
    public ProjectType Type { get; set; } = ProjectType.Solution;
    public DateTime LastModified { get; set; }
    public long FileSize { get; set; }
    public bool IsFavorite { get; set; }
}
