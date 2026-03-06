using System.IO;
using Solution_Opener.Models;

namespace Solution_Opener.Services;

public class SolutionDiscoveryService
{
    private readonly DepthFirstDiscoveryEngine _discoveryEngine;
    private readonly List<IDirectoryDiscoveryStrategy> _strategies;

    public SolutionDiscoveryService()
    {
        _discoveryEngine = new DepthFirstDiscoveryEngine();
        _strategies =
        [
            new SolutionFileDiscoveryStrategy(),
            new CodeWorkspaceDiscoveryStrategy(),
            new GitRepositoryDiscoveryStrategy()
        ];
    }

    public async Task<List<SolutionInfo>> DiscoverSolutionsAsync(string repositoryPath, IProgress<int>? progress = null)
    {
        return await Task.Run(() => DiscoverSolutions(repositoryPath, progress));
    }

    public List<SolutionInfo> DiscoverSolutions(string repositoryPath, IProgress<int>? progress = null)
    {
        if (!Directory.Exists(repositoryPath))
        {
            return new List<SolutionInfo>();
        }

        try
        {
            var solutions = _discoveryEngine.Discover(repositoryPath, _strategies, progress);

            return solutions
                .OrderBy(s => s.Type)
                .ThenBy(s => s.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error discovering solutions in {repositoryPath}: {ex.Message}");
            return new List<SolutionInfo>();
        }
    }
}
