using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Solution_Opener.Models;

namespace Solution_Opener.ViewModels;

public partial class RepositoryTabViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _path;

    [ObservableProperty]
    private ObservableCollection<SolutionItemViewModel> _solutions;

    [ObservableProperty]
    private ObservableCollection<SolutionItemViewModel> _filteredSolutions;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _statusText;

    private string _currentSearchText = string.Empty;

    public RepositoryTabViewModel(RepositoryInfo repositoryInfo)
    {
        _name = repositoryInfo.Name;
        _path = repositoryInfo.Path;
        _solutions = new ObservableCollection<SolutionItemViewModel>();
        _filteredSolutions = new ObservableCollection<SolutionItemViewModel>();
        _statusText = "Ready";
    }

    public void UpdateSolutions(List<SolutionInfo> solutions, List<string> favoritePaths)
    {
        Solutions.Clear();
        
        foreach (var solution in solutions)
        {
            var isFavorite = favoritePaths.Contains(solution.FullPath);
            Solutions.Add(new SolutionItemViewModel(solution, isFavorite));
        }

        ApplyFilter(_currentSearchText);
        UpdateStatus();
    }

    public void ApplyFilter(string searchText)
    {
        _currentSearchText = searchText;
        FilteredSolutions.Clear();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            foreach (var solution in Solutions)
            {
                FilteredSolutions.Add(solution);
            }
        }
        else
        {
            // Multi-phrase search: all phrases must match
            var phrases = searchText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var solution in Solutions)
            {
                var searchableText = $"{solution.Name} {solution.RelativePath}".ToLowerInvariant();
                
                if (phrases.All(phrase => searchableText.Contains(phrase.ToLowerInvariant())))
                {
                    FilteredSolutions.Add(solution);
                }
            }
        }

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        if (string.IsNullOrWhiteSpace(_currentSearchText))
        {
            StatusText = $"{Solutions.Count} solutions";
        }
        else
        {
            StatusText = $"{FilteredSolutions.Count} of {Solutions.Count} solutions";
        }
    }
}
