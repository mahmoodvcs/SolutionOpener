using System.IO;
using Solution_Opener.Models;

namespace Solution_Opener.Services;

public class DepthFirstDiscoveryEngine
{
    public List<SolutionInfo> Discover(
        string repositoryPath,
        IEnumerable<IDirectoryDiscoveryStrategy> strategies,
        IProgress<int>? progress = null)
    {
        var results = new List<SolutionInfo>();
        var directoriesToScan = new Stack<string>();

        directoriesToScan.Push(repositoryPath);

        while (directoriesToScan.Count > 0)
        {
            var directoryPath = directoriesToScan.Pop();

            try
            {
                foreach (var strategy in strategies)
                {
                    strategy.DiscoverInDirectory(repositoryPath, directoryPath, results);
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
        return results;
    }
}
