using CommunityToolkit.Mvvm.ComponentModel;
using Solution_Opener.Models;

namespace Solution_Opener.ViewModels;

public partial class SolutionItemViewModel : ObservableObject
{
    private readonly SolutionInfo _solutionInfo;

    [ObservableProperty]
    private bool _isFavorite;

    public SolutionItemViewModel(SolutionInfo solutionInfo, bool isFavorite)
    {
        _solutionInfo = solutionInfo;
        _isFavorite = isFavorite;
    }

    public string Name => _solutionInfo.Name;
    public string FullPath => _solutionInfo.FullPath;
    public string RelativePath => _solutionInfo.RelativePath;
    public DateTime LastModified => _solutionInfo.LastModified;
    public long FileSize => _solutionInfo.FileSize;

    public string LastModifiedDisplay => FormatTimeAgo(LastModified);
    public string FileSizeDisplay => FormatFileSize(FileSize);

    private static string FormatTimeAgo(DateTime dateTime)
    {
        var timeSpan = DateTime.Now - dateTime;

        if (timeSpan.TotalMinutes < 1)
            return "Just now";
        if (timeSpan.TotalHours < 1)
            return $"{(int)timeSpan.TotalMinutes}m ago";
        if (timeSpan.TotalDays < 1)
            return $"{(int)timeSpan.TotalHours}h ago";
        if (timeSpan.TotalDays < 30)
            return $"{(int)timeSpan.TotalDays}d ago";
        if (timeSpan.TotalDays < 365)
            return $"{(int)(timeSpan.TotalDays / 30)}mo ago";
        
        return dateTime.ToString("yyyy-MM-dd");
    }

    private static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        double len = bytes;
        int order = 0;
        
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }

        return $"{len:0.#} {sizes[order]}";
    }
}
