using System.IO;
using Solution_Opener.Models;

namespace Solution_Opener.Services;

public class GitRepositoryDiscoveryStrategy : IDirectoryDiscoveryStrategy
{
    public void DiscoverInDirectory(string repositoryPath, string directoryPath, List<SolutionInfo> results)
    {
        var gitMarkerPath = Path.Combine(directoryPath, ".git");
        var hasGitMarker = Directory.Exists(gitMarkerPath) || File.Exists(gitMarkerPath);

        if (!hasGitMarker)
        {
            return;
        }

        var directoryInfo = new DirectoryInfo(directoryPath);
        var relativePath = Path.GetRelativePath(repositoryPath, directoryPath);

        results.Add(new SolutionInfo
        {
            Name = directoryInfo.Name,
            FullPath = directoryPath,
            RelativePath = relativePath,
            Type = ProjectType.GitRepository,
            LastModified = directoryInfo.LastWriteTime,
            FileSize = 0,
            IsFavorite = false
        });
    }
}
