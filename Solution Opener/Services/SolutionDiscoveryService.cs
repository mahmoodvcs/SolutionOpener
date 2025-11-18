using System.IO;
using Solution_Opener.Models;

namespace Solution_Opener.Services;

public class SolutionDiscoveryService
{
    public async Task<List<SolutionInfo>> DiscoverSolutionsAsync(string repositoryPath, IProgress<int>? progress = null)
    {
        return await Task.Run(() => DiscoverSolutions(repositoryPath, progress));
    }

    private List<SolutionInfo> DiscoverSolutions(string repositoryPath, IProgress<int>? progress = null)
    {
        var solutions = new List<SolutionInfo>();

        if (!Directory.Exists(repositoryPath))
        {
            return solutions;
        }

        try
        {
            var solutionFiles = Directory.GetFiles(repositoryPath, "*.sln", SearchOption.AllDirectories);
            var totalFiles = solutionFiles.Length;

            for (int i = 0; i < solutionFiles.Length; i++)
            {
                var filePath = solutionFiles[i];
                
                try
                {
                    var fileInfo = new FileInfo(filePath);
                    var relativePath = Path.GetRelativePath(repositoryPath, filePath);

                    solutions.Add(new SolutionInfo
                    {
                        Name = Path.GetFileNameWithoutExtension(filePath),
                        FullPath = filePath,
                        RelativePath = relativePath,
                        LastModified = fileInfo.LastWriteTime,
                        FileSize = fileInfo.Length,
                        IsFavorite = false
                    });

                    // Report progress
                    if (progress != null && totalFiles > 0)
                    {
                        var percentComplete = (int)((i + 1) * 100.0 / totalFiles);
                        progress.Report(percentComplete);
                    }
                }
                catch (Exception ex)
                {
                    // Skip files that can't be accessed
                    System.Diagnostics.Debug.WriteLine($"Error processing {filePath}: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error discovering solutions in {repositoryPath}: {ex.Message}");
        }

        return solutions;
    }
}
