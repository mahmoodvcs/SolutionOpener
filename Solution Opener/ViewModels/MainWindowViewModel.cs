using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Solution_Opener.Models;
using Solution_Opener.Services;

namespace Solution_Opener.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ConfigurationService _configService;
    private readonly SolutionDiscoveryService _discoveryService;
    private readonly SolutionLauncherService _launcherService;
    private readonly RecentSolutionsService _recentService;

    [ObservableProperty]
    private ObservableCollection<RepositoryTabViewModel> _repositoryTabs;

    [ObservableProperty]
    private RepositoryTabViewModel? _selectedTab;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _statusBarText = "Ready";

    [ObservableProperty]
    private bool _isScanning;

    private AppConfiguration _configuration;

    public MainWindowViewModel()
    {
        _configService = new ConfigurationService();
        _discoveryService = new SolutionDiscoveryService();
        _launcherService = new SolutionLauncherService();
        _recentService = new RecentSolutionsService(_configService);

        _repositoryTabs = new ObservableCollection<RepositoryTabViewModel>();
        _configuration = new AppConfiguration();
    }

    public async Task InitializeAsync()
    {
        _configuration = _configService.LoadConfiguration();

        // Load repositories
        foreach (var repo in _configuration.Repositories)
        {
            var tabVm = new RepositoryTabViewModel(repo);
            RepositoryTabs.Add(tabVm);
            tabVm.UpdateSolutions(repo.Solutions, _configuration.FavoriteSolutionPaths);
        }

        // Add Favorites tab
        AddFavoritesTab();

        // Select last selected tab
        if (!string.IsNullOrEmpty(_configuration.LastSelectedRepoPath))
        {
            SelectedTab = RepositoryTabs.FirstOrDefault(t => t.Path == _configuration.LastSelectedRepoPath);
        }

        if (SelectedTab == null && RepositoryTabs.Count > 0)
        {
            SelectedTab = RepositoryTabs[0];
        }

        UpdateStatusBar();
    }

    private void AddFavoritesTab()
    {
        var quickAccessRepo = new RepositoryInfo { Name = "Quick Access", Path = "quick-access" };
        var quickAccessTab = new RepositoryTabViewModel(quickAccessRepo);

        RefreshQuickAccessTab(quickAccessTab);
        RepositoryTabs.Add(quickAccessTab);
    }

    [RelayCommand]
    private async Task AddRepositoryAsync()
    {
        var dialog = new Microsoft.Win32.OpenFolderDialog
        {
            Title = "Select Repository Folder"
        };

        if (dialog.ShowDialog() == true)
        {
            var path = dialog.FolderName;
            
            // Check if already added
            if (_configuration.Repositories.Any(r => r.Path.Equals(path, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("This repository is already added.", "Duplicate Repository", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            await ScanRepositoryAsync(path);
        }
    }

    private async Task ScanRepositoryAsync(string path)
    {
        IsScanning = true;
        StatusBarText = $"Scanning {path}...";

        try
        {
            var progress = new Progress<int>(percent => 
            {
                StatusBarText = $"Scanning {path}... {percent}%";
            });

            var solutions = await _discoveryService.DiscoverSolutionsAsync(path, progress);

            var repo = new RepositoryInfo
            {
                Name = Path.GetFileName(path),
                Path = path,
                LastScanned = DateTime.Now,
                Solutions = solutions
            };

            _configuration.Repositories.Add(repo);
            _configService.SaveConfiguration(_configuration);

            var tabVm = new RepositoryTabViewModel(repo);
            tabVm.UpdateSolutions(solutions, _configuration.FavoriteSolutionPaths);

            // Insert before Quick Access tab
            var favTabIndex = RepositoryTabs.Count - 1;
            RepositoryTabs.Insert(favTabIndex, tabVm);

            SelectedTab = tabVm;
            StatusBarText = $"Found {solutions.Count} solutions in {repo.Name}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error scanning repository: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            StatusBarText = "Error scanning repository";
        }
        finally
        {
            IsScanning = false;
        }
    }

    [RelayCommand]
    private async Task RefreshCurrentRepositoryAsync()
    {
        if (SelectedTab == null || SelectedTab.Path == "quick-access")
            return;

        var repo = _configuration.Repositories.FirstOrDefault(r => r.Path == SelectedTab.Path);
        if (repo == null)
            return;

        IsScanning = true;
        StatusBarText = $"Refreshing {repo.Name}...";

        try
        {
            var solutions = await _discoveryService.DiscoverSolutionsAsync(repo.Path);
            repo.Solutions = solutions;
            repo.LastScanned = DateTime.Now;

            _configService.SaveConfiguration(_configuration);

            SelectedTab.UpdateSolutions(solutions, _configuration.FavoriteSolutionPaths);
            SelectedTab.ApplyFilter(SearchText);

            // Refresh favorites tab
            RefreshFavoritesTab();

            StatusBarText = $"Refreshed {repo.Name} - {solutions.Count} solutions";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error refreshing repository: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            StatusBarText = "Error refreshing repository";
        }
        finally
        {
            IsScanning = false;
        }
    }

    [RelayCommand]
    private void OpenSolution(SolutionItemViewModel? solution)
    {
        if (solution == null)
            return;

        try
        {
            _launcherService.OpenSolution(solution.FullPath);
            _recentService.AddRecentSolution(solution.FullPath, _configuration);
            
            // Refresh Quick Access tab to show the newly opened solution
            RefreshQuickAccessTab();
            
            StatusBarText = $"Opened {solution.Name}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error opening solution: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void OpenInExplorer(SolutionItemViewModel? solution)
    {
        if (solution == null)
            return;

        try
        {
            _launcherService.OpenInExplorer(solution.FullPath);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error opening Explorer: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void CopyPath(SolutionItemViewModel? solution)
    {
        if (solution == null)
            return;

        try
        {
            _launcherService.CopyPathToClipboard(solution.FullPath);
            StatusBarText = "Path copied to clipboard";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error copying path: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void ToggleFavorite(SolutionItemViewModel? solution)
    {
        if (solution == null)
            return;

        _recentService.ToggleFavorite(solution.FullPath, _configuration);
        solution.IsFavorite = !solution.IsFavorite;

        RefreshQuickAccessTab();
        StatusBarText = solution.IsFavorite ? "Added to favorites" : "Removed from favorites";
    }

    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = string.Empty;
    }

    [RelayCommand]
    private void RemoveRepository(RepositoryTabViewModel? tab)
    {
        if (tab == null || tab.Path == "quick-access")
            return;

        var result = MessageBox.Show(
            $"Are you sure you want to remove repository '{tab.Name}'?\n\nThis will not delete any files, only remove it from Solution Opener.",
            "Remove Repository",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            // Remove from configuration
            var repo = _configuration.Repositories.FirstOrDefault(r => r.Path == tab.Path);
            if (repo != null)
            {
                _configuration.Repositories.Remove(repo);
                _configService.SaveConfiguration(_configuration);
            }

            // Remove tab
            RepositoryTabs.Remove(tab);

            // Select another tab
            if (RepositoryTabs.Count > 0)
            {
                SelectedTab = RepositoryTabs[0];
            }

            StatusBarText = $"Removed repository {tab.Name}";
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        // Apply filter to all tabs
        foreach (var tab in RepositoryTabs)
        {
            tab.ApplyFilter(value);
        }

        UpdateStatusBar();
    }

    partial void OnSelectedTabChanged(RepositoryTabViewModel? value)
    {
        if (value != null)
        {
            _configuration.LastSelectedRepoPath = value.Path;
            _configService.SaveConfiguration(_configuration);
        }

        UpdateStatusBar();
    }

    private void RefreshFavoritesTab()
    {
        RefreshQuickAccessTab();
    }

    private void RefreshQuickAccessTab(RepositoryTabViewModel? quickAccessTab = null)
    {
        quickAccessTab ??= RepositoryTabs.LastOrDefault();
        
        if (quickAccessTab?.Path == "quick-access")
        {
            // Create a dictionary to lookup repository name by solution path
            var solutionToRepo = new Dictionary<string, string>();
            foreach (var repo in _configuration.Repositories)
            {
                foreach (var solution in repo.Solutions)
                {
                    solutionToRepo[solution.FullPath] = repo.Name;
                }
            }

            // Get recent solutions (up to 10 most recent) with repository names
            var recentSolutions = _configuration.RecentSolutionPaths
                .Take(10)
                .Select(path =>
                {
                    var solution = _configuration.Repositories
                        .SelectMany(r => r.Solutions)
                        .FirstOrDefault(s => s.FullPath == path);
                    
                    if (solution != null && solutionToRepo.TryGetValue(path, out var repoName))
                    {
                        return (solution, repoName);
                    }
                    return (solution: null, repoName: string.Empty);
                })
                .Where(x => x.solution != null)
                .ToList();

            // Get favorite solutions with repository names
            var favoriteSolutions = _configuration.Repositories
                .SelectMany(r => r.Solutions
                    .Where(s => _configuration.FavoriteSolutionPaths.Contains(s.FullPath))
                    .Select(s => (solution: s, repoName: r.Name)))
                .ToList();

            quickAccessTab.UpdateQuickAccessSolutions(
                recentSolutions.Select(x => (x.solution!, x.repoName)).ToList(),
                favoriteSolutions,
                _configuration.FavoriteSolutionPaths);
            quickAccessTab.ApplyFilter(SearchText);
        }
    }

    private void UpdateStatusBar()
    {
        if (IsScanning)
            return;

        if (SelectedTab == null)
        {
            StatusBarText = "No repository selected";
            return;
        }

        var totalSolutions = RepositoryTabs.Where(t => t.Path != "quick-access")
            .Sum(t => t.Solutions.Count);

        var filteredCount = SelectedTab.FilteredSolutions.Count;
        var tabTotal = SelectedTab.Solutions.Count;

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            StatusBarText = $"{totalSolutions} solutions total | {SelectedTab.Name}: {tabTotal} solutions";
        }
        else
        {
            StatusBarText = $"{totalSolutions} solutions total | {SelectedTab.Name}: {filteredCount} of {tabTotal} solutions";
        }
    }

    public WindowSettings GetWindowSettings()
    {
        return _configuration.Window;
    }

    public void SaveWindowSettings(WindowSettings settings)
    {
        _configuration.Window = settings;
        _configService.SaveConfiguration(_configuration);
    }
}
