using Solution_Opener.Models;

namespace Solution_Opener.Services;

public class RecentSolutionsService
{
    private const int MaxRecentSolutions = 20;
    private readonly ConfigurationService _configService;

    public RecentSolutionsService(ConfigurationService configService)
    {
        _configService = configService;
    }

    public void AddRecentSolution(string solutionPath, AppConfiguration config)
    {
        // Remove if already exists
        config.RecentSolutionPaths.Remove(solutionPath);
        
        // Add to beginning
        config.RecentSolutionPaths.Insert(0, solutionPath);
        
        // Keep only the most recent
        if (config.RecentSolutionPaths.Count > MaxRecentSolutions)
        {
            config.RecentSolutionPaths.RemoveRange(MaxRecentSolutions, 
                config.RecentSolutionPaths.Count - MaxRecentSolutions);
        }
        
        _configService.SaveConfiguration(config);
    }

    public void ToggleFavorite(string solutionPath, AppConfiguration config)
    {
        if (config.FavoriteSolutionPaths.Contains(solutionPath))
        {
            config.FavoriteSolutionPaths.Remove(solutionPath);
        }
        else
        {
            config.FavoriteSolutionPaths.Add(solutionPath);
        }
        
        _configService.SaveConfiguration(config);
    }

    public bool IsFavorite(string solutionPath, AppConfiguration config)
    {
        return config.FavoriteSolutionPaths.Contains(solutionPath);
    }
}
