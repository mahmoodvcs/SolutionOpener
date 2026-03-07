using Solution_Opener.Models;

namespace Solution_Opener.Services;

public interface IDirectoryDiscoveryStrategy
{
    void DiscoverInDirectory(string repositoryPath, string directoryPath, List<SolutionInfo> results);
}
