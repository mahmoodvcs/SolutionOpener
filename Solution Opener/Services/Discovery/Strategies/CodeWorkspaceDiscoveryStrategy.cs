using System.IO;
using Solution_Opener.Models;

namespace Solution_Opener.Services;

public class CodeWorkspaceDiscoveryStrategy : IDirectoryDiscoveryStrategy
{
    public void DiscoverInDirectory(string repositoryPath, string directoryPath, List<SolutionInfo> results)
    {
        foreach (var filePath in Directory.GetFiles(directoryPath, "*.code-workspace", SearchOption.TopDirectoryOnly))
        {
            var fileInfo = new FileInfo(filePath);
            var relativePath = Path.GetRelativePath(repositoryPath, filePath);

            results.Add(new SolutionInfo
            {
                Name = Path.GetFileNameWithoutExtension(filePath),
                FullPath = filePath,
                RelativePath = relativePath,
                Type = ProjectType.CodeWorkspace,
                LastModified = fileInfo.LastWriteTime,
                FileSize = fileInfo.Length,
                IsFavorite = false
            });
        }
    }
}
