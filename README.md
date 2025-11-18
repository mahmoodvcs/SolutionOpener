# Solution Opener

A **beautiful**, modern WPF desktop application for managing and quickly opening Visual Studio solutions across multiple repository clones.

## ? Beautiful Modern Design

Solution Opener features a completely custom-designed interface with:
- ?? Modern blue color scheme
- ? Smooth animations and transitions  
- ?? Polished, professional appearance
- ?? Intuitive, clean layout
- ??? Rich interactive feedback

See [DESIGN_GUIDE.md](DESIGN_GUIDE.md) for complete design documentation.

## Features

### ? Core Features
- **Multi-Repository Support**: Add multiple repository paths and manage them in separate tabs
- **Recursive Solution Discovery**: Automatically finds all .sln files in repository folders and subfolders
- **Fast Search & Filter**: Real-time multi-phrase search across solution names and paths
- **Persistent Configuration**: Remembers repositories, window size/position, and settings between sessions
- **Favorites System**: Mark frequently used solutions as favorites for quick access
- **Recent Solutions**: Tracks recently opened solutions

### ?? User Interface
- **Tab-Based Navigation**: Each repository gets its own tab, plus a dedicated Favorites tab
- **Smart Search**: Search for solutions using multiple terms (e.g., "cargo api" finds solutions with both words)
- **Solution Details**: View solution name, path, last modified date, and file size
- **Sortable Columns**: Click column headers to sort by name, path, date, or size
- **Empty State Guidance**: Helpful messages when no solutions are found

### ?? Keyboard Shortcuts
- `Ctrl+F` - Focus search box
- `Ctrl+N` - Add new repository
- `F5` - Refresh current repository
- `Enter` - Open selected solution
- `Esc` - Clear search (when search box is focused)

### ??? Context Menu Actions

**On Solutions:**
- **Open Solution** - Launch solution in Visual Studio
- **Open in Explorer** - Open solution folder in Windows Explorer
- **Copy Path** - Copy full solution path to clipboard
- **Add/Remove Favorites** - Toggle favorite status

**On Repository Tabs:**
- **Refresh Repository** - Rescan the repository for solutions (same as F5)
- **Remove Repository** - Remove the repository from Solution Opener (doesn't delete files)

## Getting Started

### First Launch
1. Launch the application
2. Click **"Add Repository"** button or press `Ctrl+N`
3. Browse to your repository folder (e.g., `C:\git\GitHub\WiseTechGlobal\CargoWise`)
4. The app will scan for all .sln files recursively
5. Solutions will appear in a new tab

### Adding Multiple Repository Clones
Repeat the steps above for each repository clone you want to track. Each will get its own tab.

### Daily Usage
1. Launch the app (your repositories are automatically loaded)
2. Use tabs to switch between different repository clones
3. Type in the search box to filter solutions (e.g., "api test")
4. Double-click a solution or press Enter to open it in Visual Studio
5. Right-click for additional options

## Configuration

Configuration is automatically saved to:
```
%APPDATA%\SolutionOpener\config.json
```

The configuration includes:
- Repository paths and discovered solutions
- Window size, position, and state
- Favorite solutions
- Recent solutions (last 20)
- Last selected tab

### Manual Configuration Reset
To reset the application to defaults, delete the configuration file while the app is closed.

## Technical Details

### Built With
- **.NET 10** - Windows Desktop Runtime
- **WPF** - Windows Presentation Foundation
- **MVVM Pattern** - Clean architecture using CommunityToolkit.Mvvm
- **System.Text.Json** - Configuration persistence

### Architecture
```
Solution Opener/
??? Models/              # Data models
??? ViewModels/          # MVVM view models
??? Services/            # Business logic
??? Converters/          # XAML value converters
??? Views/              # XAML views
```

### Performance Considerations
- **Async Scanning**: Repository scanning happens in the background without blocking the UI
- **Cached Results**: Solutions are cached and only rescanned when explicitly refreshed
- **Efficient Search**: Uses optimized string matching for fast filtering

## Troubleshooting

### Repository Not Showing Solutions
- Ensure the path contains .sln files
- Click the Refresh button or press F5
- Check that you have read permissions for the folder

### Solutions Not Opening
- Verify Visual Studio is installed and .sln files are associated with it
- Try right-click ? "Open in Explorer" to verify the file exists

### Configuration Issues
- Delete `%APPDATA%\SolutionOpener\config.json` to reset
- A backup is kept at `config.json.bak` if the main file becomes corrupted

## Future Enhancements

Potential features for future versions:
- Dark theme support
- Custom tags for solutions
- Export/import configuration
- Integration with Git (show branch info)
- Solution health checks (build status)
- Multiple Visual Studio version selection
- Solution file preview (show contained projects)

## License

This is a personal utility application. Feel free to modify and extend as needed.

## Support

For issues or feature requests, please contact the development team or create an issue in your internal repository.

---

**Version**: 1.0  
**Last Updated**: 2025  
**Platform**: Windows 10/11, .NET 10
