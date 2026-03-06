using System.IO;
using Solution_Opener.Models;

namespace Solution_Opener.Services;

public class SolutionDiscoveryService
{
    public async Task<List<SolutionInfo>> DiscoverSolutionsAsync(string repositoryPath, IProgress<int>? progress = null)
    {
        return await Task.Run(() => DiscoverSolutions(repositoryPath, progress));
    }

    public List<SolutionInfo> DiscoverSolutions(string repositoryPath, IProgress<int>? progress = null)
    {
        var solutions = new List<SolutionInfo>();
        var discoveredGitRepositories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var directoriesToScan = new Stack<string>();

        if (!Directory.Exists(repositoryPath))
        {
            return solutions;
        }

        directoriesToScan.Push(repositoryPath);

        try
        {
            while (directoriesToScan.Count > 0)
            {
                var directoryPath = directoriesToScan.Pop();

                try
                {
                    var gitMarkerPath = Path.Combine(directoryPath, ".git");
                    var hasGitMarker = Directory.Exists(gitMarkerPath) || File.Exists(gitMarkerPath);

                    if (hasGitMarker && discoveredGitRepositories.Add(directoryPath))
                    {
                        var directoryInfo = new DirectoryInfo(directoryPath);
                        var relativePath = Path.GetRelativePath(repositoryPath, directoryPath);

                        solutions.Add(new SolutionInfo
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

                    foreach (var filePath in Directory.GetFiles(directoryPath, "*.sln", SearchOption.TopDirectoryOnly))
                    {
                        var fileInfo = new FileInfo(filePath);
                        var relativePath = Path.GetRelativePath(repositoryPath, filePath);

                        solutions.Add(new SolutionInfo
                        {
                            Name = Path.GetFileNameWithoutExtension(filePath),
                            FullPath = filePath,
                            RelativePath = relativePath,
                            Type = ProjectType.Solution,
                            LastModified = fileInfo.LastWriteTime,
                            FileSize = fileInfo.Length,
                            IsFavorite = false
                        });
                    }

                    foreach (var childDirectory in Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly))
                    {
                        if (Path.GetFileName(childDirectory).Equals(".git", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        directoriesToScan.Push(childDirectory);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error processing {directoryPath}: {ex.Message}");
                }
            }

            progress?.Report(100);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error discovering solutions in {repositoryPath}: {ex.Message}");
        }

        return solutions
            .OrderBy(s => s.Type)
            .ThenBy(s => s.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}
