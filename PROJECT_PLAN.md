# Solution Opener - Project Plan

## Overview
A WPF desktop application for managing and quickly opening Visual Studio solutions across multiple repository clones. The app will discover, index, and provide fast search/filter capabilities for .sln files.

---

## Tech Stack
- **Framework**: .NET 10 WPF
- **Architecture**: MVVM (Model-View-ViewModel) for clean separation and maintainability
- **UI**: Standard WPF controls (simple and quick)
- **Data Persistence**: JSON file for storing configuration and repo paths
- **Libraries**:
  - `System.Text.Json` - Configuration persistence
  - `CommunityToolkit.Mvvm` - MVVM helpers (RelayCommand, ObservableObject)

---

## Core Features

### 1. Repository Management
- Add repository paths via UI
- Persist repository paths between sessions
- Remove repository paths
- Refresh/rescan repositories to update solution lists
- Display repo paths in tabs or easily switchable sections

### 2. Solution Discovery
- Recursively scan repository paths for `*.sln` files
- Cache discovered solutions for performance
- Background scanning to prevent UI blocking
- Show scan progress (optional)

### 3. Search & Filter
- Real-time search/filter as user types
- Search across:
  - Solution file name
  - Full file path
  - Parent folder names
- Multi-phrase search support (e.g., "cargo api" matches solutions containing both terms)
- Case-insensitive search
- Clear search button

### 4. Solution Display
- Tab-based UI - one tab per repository clone
- List view showing solutions with:
  - Solution name
  - Relative path from repo root
  - Last modified date
  - File size
- Sort capabilities (by name, date, path)
- Quick repo switching via tabs

### 5. Solution Opening
- Double-click or Enter key to open solution
- Opens with default `.sln` handler (Visual Studio)
- Context menu with additional options:
  - Open solution
  - Open solution folder in File Explorer
  - Copy solution path to clipboard

### 6. Additional Features
- **Favorites/Recent**: Track recently opened and favorite solutions
- **Keyboard Shortcuts**:
  - `Ctrl+F` - Focus search box
  - `Ctrl+O` - Open selected solution
  - `Ctrl+N` - Add new repository
  - `F5` - Refresh current repository
  - `Ctrl+Tab` / `Ctrl+Shift+Tab` - Switch between repo tabs
- **Persistence**: Remember window size, position, last selected tab
- **Status Bar**: Show count of solutions, current filter status

---

## Project Structure

```
Solution Opener/
??? Models/
?   ??? RepositoryInfo.cs          # Represents a repo clone
?   ??? SolutionInfo.cs            # Represents a .sln file
?   ??? AppConfiguration.cs        # App settings/config
??? ViewModels/
?   ??? MainWindowViewModel.cs     # Main window logic
?   ??? RepositoryTabViewModel.cs  # Individual repo tab logic
?   ??? SolutionItemViewModel.cs   # Individual solution item
??? Views/
?   ??? MainWindow.xaml[.cs]       # Main window UI
?   ??? AddRepositoryDialog.xaml[.cs] # Dialog to add repo path
??? Services/
?   ??? SolutionDiscoveryService.cs    # Scans for .sln files
?   ??? ConfigurationService.cs        # Load/save config
?   ??? RecentSolutionsService.cs      # Track recent/favorites
?   ??? SolutionLauncherService.cs     # Opens solutions/folders
??? Helpers/
?   ??? RelayCommand.cs (or use CommunityToolkit)
?   ??? FileSystemHelper.cs        # File system utilities
??? App.xaml[.cs]                  # Application entry point
```

---

## Data Models

### AppConfiguration
```
- List<RepositoryInfo> Repositories
- WindowSettings (width, height, position)
- string LastSelectedRepoPath
- List<string> RecentSolutionPaths (last 10-20)
- List<string> FavoriteSolutionPaths
```

### RepositoryInfo
```
- string Name (display name, defaults to folder name)
- string Path (full path to repo)
- DateTime LastScanned
- List<SolutionInfo> Solutions
```

### SolutionInfo
```
- string Name (file name without .sln)
- string FullPath
- string RelativePath (from repo root)
- DateTime LastModified
- long FileSize
- bool IsFavorite
```

---

## UI Layout

### Main Window
```
???????????????????????????????????????????????????????????????
? Solution Opener                                     [_][?][X]?
???????????????????????????????????????????????????????????????
? [+ Add Repository] [Refresh] [Settings]                      ?
???????????????????????????????????????????????????????????????
? Search: [___________________________] [Clear]                ?
???????????????????????????????????????????????????????????????
? ?? Repo Clone 1 ??? Repo Clone 2 ??? Favorites ??          ?
? ?                                                  ?          ?
? ?  ????????????????????????????????????????????  ?          ?
? ?  ? Name            | Path        | Modified ?  ?          ?
? ?  ????????????????????????????????????????????  ?          ?
? ?  ? CargoWise.sln   | /src        | 2h ago   ?  ?          ?
? ?  ? API.sln         | /src/api    | 1d ago   ?  ?          ?
? ?  ? Tests.sln       | /tests      | 3d ago   ?  ?          ?
? ?  ? ...                                       ?  ?          ?
? ?  ????????????????????????????????????????????  ?          ?
? ?                                                  ?          ?
? ????????????????????????????????????????????????????          ?
???????????????????????????????????????????????????????????????
? 127 solutions found | 12 filtered | Ready                    ?
???????????????????????????????????????????????????????????????
```

### Context Menu (Right-click on solution)
- Open Solution
- Open in File Explorer
- Copy Path
- Add to Favorites / Remove from Favorites
- Show in Tab (if filtered across tabs)

---

## Implementation Phases

### Phase 1: Core Infrastructure (Foundation)
1. Set up MVVM structure with CommunityToolkit.Mvvm
2. Create data models (RepositoryInfo, SolutionInfo, AppConfiguration)
3. Implement ConfigurationService (load/save JSON)
4. Basic MainWindow UI with TabControl

### Phase 2: Repository & Solution Discovery
1. Implement SolutionDiscoveryService
   - Recursive directory scanning
   - Parse .sln files
   - Background worker/async scanning
2. Add Repository dialog
3. Display repositories in tabs
4. Show solutions in ListView/DataGrid

### Phase 3: Search & Filter
1. Implement search logic in ViewModel
2. Real-time filtering as user types
3. Multi-phrase search support
4. Highlight search terms (optional)
5. Clear search functionality

### Phase 4: Solution Opening
1. Implement SolutionLauncherService
2. Double-click to open solution
3. Context menu items:
   - Open solution
   - Open in Explorer
   - Copy path
4. Keyboard shortcuts

### Phase 5: Enhanced Features
1. Recent solutions tracking
2. Favorites system
3. Favorites tab
4. Refresh repository functionality
5. Remove repository functionality
6. Sort by column headers

### Phase 6: Polish & UX
1. Keyboard shortcuts (Ctrl+F, Ctrl+O, etc.)
2. Window state persistence (size, position, last tab)
3. Loading indicators for long scans
4. Status bar with counts and status
5. Error handling and user feedback
6. Icons and visual improvements (optional)

### Phase 7: Testing & Optimization
1. Test with large repositories (1000+ solutions)
2. Performance optimization for search
3. Test persistence across sessions
4. Edge case handling (deleted repos, invalid paths)

---

## Configuration File Location
- **Path**: `%APPDATA%/SolutionOpener/config.json`
- **Format**: JSON
- **Auto-created** on first run

---

## Key Algorithms

### Multi-Phrase Search
```
Input: "cargo api" 
Logic: Split on spaces, check if ALL phrases exist in (name OR path)
Example matches:
  - "CargoWise.API.sln"
  - "api/CargoCore.sln"
  - Path contains "cargo" AND "api"
```

### Solution Discovery
```
1. Get repository path
2. Recursively enumerate *.sln files
3. For each .sln:
   - Extract file info (name, size, modified)
   - Calculate relative path from repo root
   - Create SolutionInfo object
4. Store in RepositoryInfo.Solutions
5. Save to config
```

---

## User Workflow

### Initial Setup
1. Launch app (first time)
2. Click "Add Repository"
3. Browse to `C:\git\GitHub\WiseTechGlobal\CargoWise`
4. App scans recursively for .sln files
5. Tab created showing all solutions
6. Repeat for additional repo clones

### Daily Use
1. Launch app (loads previous repos)
2. Click appropriate repo tab or search across all
3. Type search term (e.g., "cargo api")
4. Double-click to open solution in Visual Studio
5. Solution added to recent list

### Managing Repositories
1. Add new repo: Click "Add Repository" button
2. Refresh repo: F5 or Refresh button (rescans for new/deleted solutions)
3. Remove repo: Right-click tab ? Remove (with confirmation)

---

## Technical Considerations

### Performance
- **Async scanning**: Don't block UI during solution discovery
- **Cached results**: Only rescan on explicit refresh
- **Lazy loading**: Load solutions into UI as needed for very large repos
- **Indexed search**: Use efficient string searching (contains/indexOf)

### Data Persistence
- **Auto-save**: Save config after any change (add repo, favorite, etc.)
- **Atomic writes**: Write to temp file, then rename to prevent corruption
- **Backup**: Keep previous config as `.config.json.bak`

### Error Handling
- Invalid repository paths ? Show error message, don't add
- Deleted repositories ? Show warning, offer to remove
- Corrupted config ? Fall back to defaults, backup old config
- Solution file deleted ? Grey out in list or remove on refresh

### UX Considerations
- **First run**: Show welcome message explaining how to add repositories
- **Empty state**: Show helpful message when no repos or solutions
- **Progress**: Show scanning progress for large repositories
- **Responsiveness**: Keep UI responsive during long operations

---

## Future Enhancements (Out of Scope for MVP)
- Solution file preview (show projects in solution)
- Open specific Visual Studio version
- Custom tags for solutions
- Export/import configuration
- Solution templates/quick create
- Recent projects within solutions
- Dark theme support
- Solution health checks (build status, missing projects)
- Integration with Git (show branch, status)

---

## Development Estimates

| Phase | Tasks | Estimated Time |
|-------|-------|----------------|
| Phase 1 | Core Infrastructure | 2-3 hours |
| Phase 2 | Repository & Discovery | 3-4 hours |
| Phase 3 | Search & Filter | 2 hours |
| Phase 4 | Solution Opening | 2 hours |
| Phase 5 | Enhanced Features | 3-4 hours |
| Phase 6 | Polish & UX | 2-3 hours |
| Phase 7 | Testing | 1-2 hours |
| **Total** | | **15-20 hours** |

---

## Success Criteria
? Can add multiple repository paths  
? Discovers all .sln files recursively  
? Persists configuration between sessions  
? Fast search/filter across all solutions  
? Easy repo switching via tabs  
? Double-click opens solution in Visual Studio  
? Tracks recent and favorite solutions  
? Keyboard shortcuts work  
? Context menu provides useful actions  
? Handles 1000+ solutions without performance issues  

---

## Getting Started

Once approved, implementation will begin with Phase 1, establishing the MVVM foundation and basic UI structure. Each phase will be completed and tested before moving to the next.

**Ready to proceed?**
