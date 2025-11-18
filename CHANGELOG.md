# Changelog

All notable changes to Solution Opener will be documented in this file.

## [1.0.0] - 2025-01-XX

### ?? Initial Release

#### Added
- **Multi-Repository Management**
  - Add multiple repository paths
  - Remove repositories with confirmation dialog
  - Separate tab for each repository
  - Automatic folder name detection for tab labels

- **Solution Discovery**
  - Recursive scanning for .sln files
  - Async/background scanning with progress
  - Cached results for performance
  - Refresh on demand (F5)

- **Search & Filter**
  - Real-time search as you type
  - Multi-phrase search with AND logic
  - Search across solution names and paths
  - Case-insensitive searching
  - Clear search button

- **Favorites System**
  - Mark/unmark solutions as favorites
  - Dedicated Favorites tab
  - Star indicator (?) in solution list
  - Favorites persist across sessions

- **Recent Solutions**
  - Track last 20 opened solutions
  - Automatic tracking on solution open
  - Persisted in configuration

- **User Interface**
  - Clean, modern WPF interface
  - Tab-based repository navigation
  - Sortable DataGrid columns (Name, Path, Modified, Size)
  - Context menus on solutions and tabs
  - Loading overlay during scanning
  - Status bar with solution counts
  - Empty state guidance messages

- **Solution Operations**
  - Double-click to open solution
  - Enter key to open solution
  - Open in File Explorer
  - Copy path to clipboard
  - Default .sln file handler integration

- **Keyboard Shortcuts**
  - `Ctrl+F` - Focus search box
  - `Ctrl+N` - Add new repository
  - `F5` - Refresh current repository
  - `Enter` - Open selected solution
  - `Ctrl+Tab` / `Ctrl+Shift+Tab` - Switch tabs

- **Persistence**
  - JSON configuration in %APPDATA%
  - Window size, position, and state
  - Last selected tab remembered
  - Repository paths and solutions cached
  - Automatic backup (*.bak file)
  - Atomic file writes for reliability

- **Error Handling**
  - Graceful error messages
  - Configuration corruption recovery
  - Invalid path detection
  - Permission error handling

- **Documentation**
  - Comprehensive README.md
  - Detailed USER_GUIDE.md
  - POWER_USER_TIPS.md for advanced usage
  - PROJECT_PLAN.md with technical specification
  - IMPLEMENTATION_SUMMARY.md

#### Technical Details
- Built with .NET 10 WPF
- MVVM architecture using CommunityToolkit.Mvvm
- System.Text.Json for configuration
- Modern C# features (nullable reference types, pattern matching)
- Source generators for MVVM boilerplate

---

## Future Versions (Planned)

### [1.1.0] - Future
**Theme: Performance & UX Polish**
- Dark theme support
- Exclude patterns (ignore bin, obj, node_modules)
- Global hotkey to show/hide window
- System tray support
- Drag-and-drop repository adding
- Tab reordering (drag-and-drop)
- Recent solutions tab/menu
- Export/import configuration

### [1.2.0] - Future
**Theme: Advanced Features**
- Custom tags for solutions
- Solution groups/categories
- Advanced search operators (OR, NOT, wildcards)
- Search result highlighting
- Solution file preview (show projects)
- Multiple Visual Studio version selection
- Custom solution open handlers

### [2.0.0] - Future
**Theme: Integration & Intelligence**
- Git integration (branch, status, remote)
- Solution health indicators
- Build status integration
- Last opened timestamp
- Most frequently opened tracking
- Workspace/profile support
- Cloud sync (OneDrive, Google Drive)
- Team shared configurations

---

## Version History

| Version | Release Date | Highlights |
|---------|--------------|------------|
| 1.0.0   | 2025-01-XX   | Initial release with core features |

---

## Migration Guide

### Upgrading to Future Versions

Configuration will be automatically migrated. However, always backup your config before upgrading:

```powershell
copy %APPDATA%\SolutionOpener\config.json %APPDATA%\SolutionOpener\config.backup.json
```

### Breaking Changes

None in 1.0.0 (initial release).

---

## Credits

Developed for efficient solution management across multiple repository clones.

**Technologies:**
- .NET 10
- WPF
- CommunityToolkit.Mvvm
- System.Text.Json

**Testing:**
- Tested with repositories containing 1000+ solutions
- Tested across multiple monitor setups
- Tested with various Visual Studio versions

---

For more information, see:
- [README.md](README.md) - Overview and getting started
- [USER_GUIDE.md](USER_GUIDE.md) - Detailed usage instructions
- [POWER_USER_TIPS.md](POWER_USER_TIPS.md) - Advanced tips and tricks
