# Solution Opener

**Instantly find and open Visual Studio solutions across multiple repository clones.**

A productivity-focused WPF application that eliminates the time wasted navigating folder structures. Search across thousands of solutions in milliseconds, manage multiple repository clones simultaneously, and get back to coding in under 5 seconds.

## 🚀 Why Solution Opener?

**The Problem:**
- Wasting 30+ seconds browsing nested folders to find solutions
- Managing multiple repository clones for different branches
- Forgetting where specific solutions are located
- No quick way to search across all your repositories

**The Solution:**
- ⚡ **Instant Search**: Find any solution across all repositories in < 1 second
- 🎯 **Multi-Repository Management**: View and search multiple repo clones simultaneously
- ⭐ **Smart Favorites**: Quick access to frequently-used solutions
- ⌨️ **Keyboard-First**: Complete keyboard control for maximum efficiency
- 💾 **Persistent Cache**: Solutions remembered between sessions, no re-scanning needed

**Modern, polished interface** designed for professional developers who value speed and efficiency.

---

## 📋 Table of Contents

- [Features](#-features)
- [Quick Start](#-quick-start)
- [Interface Overview](#-interface-overview)
- [Keyboard Shortcuts](#-keyboard-shortcuts)
- [Search & Filter](#-search--filter)
- [Favorites System](#-favorites-system)
- [Context Menu Actions](#-context-menu-actions)
- [Configuration](#-configuration)
- [Common Workflows](#-common-workflows)
- [Power User Tips](#-power-user-tips)
- [Troubleshooting](#-troubleshooting)
- [FAQ](#-faq)
- [Technical Details](#-technical-details)

---

## 🚀 Features

### 🎯 Core Capabilities
- **Multi-Repository Support**: Add multiple repository paths and manage them in separate tabs
- **Recursive Solution Discovery**: Automatically finds all .sln files in repository folders and subfolders
- **Fast Search & Filter**: Real-time multi-phrase search across solution names and paths
- **Persistent Configuration**: Remembers repositories, window size/position, and settings between sessions
- **Favorites System**: Mark frequently used solutions as favorites for quick access
- **Recent Solutions**: Tracks recently opened solutions (last 20)
- **Powerful Keyboard Shortcuts**: Navigate and control everything with keyboard shortcuts for maximum productivity

### 🖥️ User Interface
- **Tab-Based Navigation**: Each repository gets its own tab, plus a dedicated Favorites tab
- **Smart Search**: Search for solutions using multiple terms (e.g., "cargo api" finds solutions with both words)
- **Solution Details**: View solution name, path, last modified date, and file size
- **Sortable Columns**: Click column headers to sort by name, path, date, or size
- **Empty State Guidance**: Helpful messages when no solutions are found
- **Clean, Modern Design**: Polished interface with intuitive layout

---

## ⚡ Quick Start

### First Launch

When you first launch Solution Opener, you'll see an empty interface with a message prompting you to add a repository.

**Steps:**
1. Click the **"➕ Add Repository"** button in the toolbar or press `Ctrl+N`
2. Browse to your repository folder (e.g., `C:\git\MyProject`)
3. Click "Select Folder"
4. Wait for the scan to complete (progress shown in status bar)
5. Your solutions will appear in a new tab

### Daily Usage

1. Launch the app (your repositories are automatically loaded)
2. Press `Ctrl+F` to focus search
3. Type a few letters of your solution name
4. Press `Enter` to open
5. Visual Studio launches with your solution

**⏱️ Time saved**: ~30 seconds vs. browsing folders manually

---

## 📐 Interface Overview

```
┌─────────────────────────────────────────────────────────┐
│ [➕ Add Repository] [🔄 Refresh]                        │ ← Toolbar
├─────────────────────────────────────────────────────────┤
│ Search: [Type to search...] [Clear]                    │ ← Search Bar
├─────────────────────────────────────────────────────────┤
│ 📁 Repo 1 │ 📁 Repo 2 │ ⭐ Favorites │                 │ ← Repository Tabs
│ ┌───────────────────────────────────────────┐          │
│ │  Solutions List                            │          │
│ └───────────────────────────────────────────┘          │
├─────────────────────────────────────────────────────────┤
│ 127 solutions total | Repo 1: 45 solutions             │ ← Status Bar
└─────────────────────────────────────────────────────────┘
```

### Solution Columns

| Column | Description | Example |
|--------|-------------|---------|
| ⭐ | Favorite indicator | ⭐ or empty |
| Name | Solution filename (without .sln) | CargoWise.API |
| Path | Relative path from repository root | src/api/CargoWise.API.sln |
| Modified | Time since last modification | 2h ago, 3d ago, 2024-01-15 |
| Size | File size in appropriate units | 45 KB, 1.2 MB |

---

## ⌨️ Keyboard Shortcuts

| Shortcut | Action |
|----------|--------|
| `Ctrl+F` | Focus search box and select all text |
| `Ctrl+N` | Add new repository |
| `F5` | Refresh current repository (rescan for solutions) |
| `Enter` | Open selected solution |
| `Esc` | Clear search (when search box is focused) |
| `↑` `↓` | Navigate solution list |
| `Ctrl+Tab` | Switch to next tab |
| `Ctrl+Shift+Tab` | Switch to previous tab |
| `Ctrl+1` to `Ctrl+9` | Switch to tab 1-9 directly |

### 🥷 Keyboard Ninja Mode

Minimal mouse usage for maximum productivity:
1. `Ctrl+F` → focus search
2. Type search terms
3. `↑` or `↓` → select solution
4. `Enter` → open solution
5. `Ctrl+Tab` → switch repo tabs
6. `F5` → refresh when needed

---

## 🔍 Search & Filter

The search feature is powerful and flexible with multi-phrase AND logic.

### Simple Search

- Type `cargo` → finds all solutions with "cargo" in name or path
- Type `api` → finds all solutions with "api" in name or path
- Search is **case-insensitive**: `CARGO` = `cargo` = `Cargo`

### Multi-Phrase Search

- Type `cargo api` → finds solutions containing BOTH "cargo" AND "api"
- Type `test integration` → finds solutions with both words
- Type `cargo integration test` → finds solutions with all three words

### Clear Search

- Click the "Clear" button
- Press `Esc` in the search box
- Or press `Ctrl+A` then `Delete`

### Advanced Filtering Tips

**Combine Terms Strategically:**


❌ Bad: `cargowise` (too broad, 100+ results)  
✅ Good: `cargowise api client` (precise, 2-3 results)

❌ Bad: `test` (finds everything)  
✅ Good: `integration test payment` (specific area)

**Use Path Hints:**
- `\samples\` → only samples folder
- `\v2\` → version 2 projects
- `src\` → source code (not tests/samples)

### 📊 Sorting Solutions

Click any column header to sort:
- **⭐** - Sort by favorite status
- **Name** - Sort alphabetically by solution name
- **Path** - Sort by relative path
- **Modified** - Sort by last modified date
- **Size** - Sort by file size

Click the same header again to reverse the sort order.

---

## ⭐ Favorites System

### Add to Favorites

1. Right-click a solution
2. Select "Add to Favorites"
3. A star (⭐) appears in the first column

### View Favorites

- Click the "⭐ Favorites" tab
- All your favorite solutions are shown together, regardless of which repository they're from

### Remove from Favorites

1. Right-click a favorite solution
2. Select "Remove from Favorites"

### 💡 Favorites Strategy

**Star These:**
- Solutions you open daily
- Main entry-point solutions
- Frequently debugged solutions
- Solutions you demo to others

**Don't Star:**
- One-off utility solutions
- Rarely-touched test solutions
- Automatically generated solutions

---

## 🖱️ Context Menu Actions

### Right-Click on Solutions

1. **Open Solution** - Opens in Visual Studio (default .sln handler)
2. **Open in Explorer** - Opens the folder containing the solution in Windows Explorer
3. **Copy Path** - Copies the full file path to clipboard (useful for sharing or command-line work)
4. **Add to Favorites / Remove from Favorites** - Toggle favorite status

### Right-Click on Repository Tabs

1. **Refresh Repository** - Rescan the repository for solutions (same as F5)
2. **Remove Repository** - Remove the repository from Solution Opener (doesn't delete files)

### Time-Saving Context Menu Tricks

**Copy Path:**
- Right-click → Copy Path
- Paste into:
  - Command line (`dotnet build "path"`)
  - Slack/Teams (share with colleagues)
  - Documentation
  - Scripts

**Open in Explorer:**
- Right-click → Open in Explorer
- Quick access to:
  - Solution folder
  - Related files
  - Build outputs
  - Configuration files

---

## ⚙️ Configuration

Configuration is automatically saved to:
```
%APPDATA%\SolutionOpener\config.json
```

### What's Saved

- Repository paths and discovered solutions (cached)
- Window size, position, and maximized state
- Last selected tab
- Favorite solution paths
- Recent solution paths (last 20)

### Automatic Backup

Every time configuration is saved, the previous version is backed up to:
```
%APPDATA%\SolutionOpener\config.json.bak
```

### Manual Backup

**Backup:**
```powershell
copy %APPDATA%\SolutionOpener\config.json D:\Backups\
```

**Restore:**
```powershell
copy D:\Backups\config.json %APPDATA%\SolutionOpener\
```

### Reset to Defaults

1. Close Solution Opener
2. Delete `%APPDATA%\SolutionOpener\config.json`
3. Restart the application
4. Re-add your repositories

### Advanced Configuration

**Manual Edits (Advanced):**
```json
{
  "Repositories": [
    {
      "Name": "Main (Production)",  // 💡 Customize tab names
      "Path": "C:\\git\\MyProject",
      // ...
    }
  ],
  "FavoriteSolutionPaths": [
    // Manually add paths if needed
  ]
}
```

---

## 📝 Common Workflows

### Daily Development Workflow

1. Launch Solution Opener
2. Press `Ctrl+F` to focus search
3. Type a few letters of your solution name
4. Press `Enter` to open
5. Visual Studio launches with your solution

**⏱️ Time saved**: ~30 seconds vs. browsing folders manually

### Working with Multiple Clones

If you have multiple clones of the same repo (e.g., for different branches):

**Folder Naming:**
```
C:\git\MyProject-Main\
C:\git\MyProject-Release\
C:\git\MyProject-Feature-ABC\
```

**In Solution Opener:**
1. Add each clone as a separate repository
2. Name the folders descriptively (the folder name becomes the tab name)
3. Each gets its own tab
4. Quickly switch between branches without git checkout
5. Work on multiple versions simultaneously

### Finding Solutions Across All Repositories

1. Go to the "⭐ Favorites" tab
2. Click the "Name" column header to sort alphabetically
3. Or use Search to find across all repos simultaneously

### Cleaning Up Old Solutions

1. Delete solutions from disk (outside this app)
2. Press `F5` in Solution Opener to refresh
3. Deleted solutions will disappear from the list

### Opening Multiple Solutions

**Need to Open Multiple Solutions?**
1. Search for common term
2. Double-click first solution
3. `Alt+Tab` back to Solution Opener
4. Double-click next solution
5. Repeat

**Better Approach:**
- Star all related solutions
- Go to Favorites tab
- Quick access anytime

---

## 🔥 Power User Tips

### 🚀 Lightning-Fast Workflow

1. Launch app (pin to taskbar for quick access)
2. `Ctrl+F` to focus search
3. Type 2-3 letters of solution name
4. `Enter` to open
5. Back to coding in < 5 seconds!

### 📁 Repository Organization

**Optimal Setup:**
- Add your main working clone
- Add release/stable clone (for quick reference)
- Add feature branch clone (current work)
- Don't add too many clones (3-5 max for clarity)

### ⚡ Performance Optimization

**Large Repositories (1000+ solutions):**
1. First scan will take 30-60 seconds (one-time cost)
2. Results are cached forever
3. Only refresh when you pull new code
4. Search is instant even with 1000s of solutions

**Multiple Large Repositories:**
- Add them all once
- Let them scan overnight if needed
- Cache persists between sessions
- No re-scanning on app restart

### 🎯 Expert Scenarios

#### Scenario 1: Finding That One Solution
*"I know it's something about shipping labels..."*

1. `Ctrl+F`
2. Type `ship label`
3. If too many results, add more: `ship label print`
4. Found in 3 seconds vs. 5 minutes of folder browsing

#### Scenario 2: Demonstrating Features
*"Let me show you how our API works..."*

1. Open Solution Opener
2. Click Favorites tab
3. All demo solutions starred and ready
4. Double-click API solution
5. Professional demo starts immediately

#### Scenario 3: Branch Switching Without Git
*"Need to check how this worked in release version"*


1. Click "MyProject-Release" tab
2. Search for solution
3. Open it
4. No git checkout, no IDE restart
5. Both versions open simultaneously

#### Scenario 4: Team Knowledge Sharing
*"Where's that integration test solution?"*

1. Search `integration test payment`
2. Right-click → Copy Path
3. Paste in Slack: `\\server\git\MyProject\tests\integration\Payment.IntegrationTests.sln`
4. Colleague finds it instantly

#### Scenario 5: Spring Cleaning
*"So many old solutions..."*

1. Refresh each repository (`F5`)
2. Deleted solutions disappear automatically
3. Remove old repo clones (right-click tab)
4. Clean workspace in minutes

### 💡 Did You Know?

- Search is case-insensitive: `CARGO` = `cargo` = `Cargo`
- You can sort by any column (click header)
- Modified time shows relative time (2h ago) for recent files
- File size auto-scales (B, KB, MB, GB)
- Configuration is auto-saved after every change
- Window position is remembered per monitor
- Favorites persist even if you remove and re-add a repo
- Tab order is customizable (drag-drop coming soon?)

### ✅ Power User Checklist

- [ ] Application pinned to taskbar
- [ ] All active repo clones added
- [ ] Top 10 solutions starred as favorites
- [ ] Keyboard shortcuts memorized
- [ ] Config backed up
- [ ] Old/inactive repos removed
- [ ] Window sized and positioned optimally
- [ ] Taught colleagues about the tool!

### 🎓 Pro Tips from the Developer

1. **Keep it Clean**: Only add repositories you actively work on
2. **Use Favorites Liberally**: Star anything you open weekly
3. **Search Smart**: More terms = better results (AND logic)
4. **Refresh Rarely**: Only after major pulls (it's cached!)
5. **Backup Config**: Especially if you have 100+ favorites
6. **Share the Tool**: Your team will thank you!

---

## 🔧 Troubleshooting

### Repository Not Showing Solutions

**Problem**: Repository scanned but no solutions appear

**Solutions**:
- Verify .sln files exist in the folder
- Check folder permissions
- Click the Refresh button or press `F5`

### Solutions Not Opening

**Problem**: Solution won't open when double-clicked

**Solutions**:
- Verify Visual Studio is installed and .sln files are associated with it
- Try right-click → "Open in Explorer" to verify the file exists
- Check .sln file association, reinstall Visual Studio file handlers

### Window Position Issues

**Problem**: Window opens off-screen

**Solutions**:
- Configuration auto-resets if window is outside screen bounds
- Or delete config file to reset manually

### Search Issues

**Problem**: Search not working as expected

**Solution**:
- Remember all search terms must match (AND logic), not OR
- Check spelling
- Try fewer terms if no results

### Configuration Problems

**Problem**: Lost configuration or corrupt config

**Solutions**:
- Check `%APPDATA%\SolutionOpener\config.json.bak` for automatic backup
- Delete `%APPDATA%\SolutionOpener\config.json` to reset
- A backup is kept at `config.json.bak` if the main file becomes corrupted

### Performance Issues

**Problem**: Slow scanning

**Solution**:
- Normal for first time with large repositories (1000+ solutions)
- Exclude node_modules/bin folders (future feature)
- Consider breaking up very large repositories

**Problem**: Too many search results

**Solution**:
- Add more search terms (be specific)

**Problem**: No search results

**Solution**:
- Check spelling
- Try fewer terms
- Check if repo is refreshed

---

## ❓ FAQ

**Q: Can I add the same repository twice?**  
A: No, the app prevents duplicate repositories.

**Q: What happens if I move a repository folder?**  
A: The app will show an error when trying to refresh. Remove the old path and add the new one.

**Q: Can I edit the configuration file manually?**  
A: Yes, it's JSON format. Close the app first, edit, then reopen.

**Q: Does this work with Rider or VS Code?**  
A: It opens .sln files with the default handler, so if Rider/Code is your default, yes!

**Q: Can I search by solution contents (projects inside)?**  
A: Not in the current version. Search only covers file names and paths.

**Q: How do I uninstall?**  
A: Delete the executable and `%APPDATA%\SolutionOpener` folder.

**Q: Can I use this on macOS or Linux?**  
A: No, this is a Windows-only WPF application.

**Q: How many repositories can I add?**  
A: No hard limit, but 3-5 repositories is recommended for optimal usability.

**Q: What's the maximum number of solutions it can handle?**  
A: Tested with 1000+ solutions per repository with instant search performance.

---

## 🛠️ Technical Details

### Built With

- **.NET 10** - Windows Desktop Runtime
- **WPF** - Windows Presentation Foundation
- **MVVM Pattern** - Clean architecture using CommunityToolkit.Mvvm
- **System.Text.Json** - Configuration persistence

### Architecture

```
Solution Opener/
├── Models/              # Data models
├── ViewModels/          # MVVM view models
├── Services/            # Business logic
├── Converters/          # XAML value converters
└── Views/               # XAML views
```

### Performance Considerations

- **Async Scanning**: Repository scanning happens in the background without blocking the UI
- **Cached Results**: Solutions are cached and only rescanned when explicitly refreshed
- **Efficient Search**: Uses optimized string matching for fast filtering
- **Lazy Loading**: UI elements are loaded on-demand for better responsiveness

---

## 🔮 Future Enhancements

Potential features for future versions:

- Dark theme support
- Custom tags for solutions
- Export/import configuration
- Integration with Git (show branch info)
- Solution health checks (build status)
- Multiple Visual Studio version selection
- Solution file preview (show contained projects)
- Command-line interface for automation
- Exclude patterns (node_modules, bin, obj)
- Solution grouping and categories

---

## 📞 Getting Help

If you encounter issues:

1. Check the [Troubleshooting](#-troubleshooting) section above
2. Review the [FAQ](#-faq)
3. Try resetting configuration
4. Contact your development team
5. Check the application logs (coming in future versions)
6. Create an issue on [GitHub](https://github.com/mahmoodvcs/SolutionOpener)

---

## 📄 License

This is a personal utility application. Feel free to modify and extend as needed.

---

## 📊 Project Info

**Version**: 1.0  
**Last Updated**: 2025  
**Platform**: Windows 10/11, .NET 10  
**Repository**: https://github.com/mahmoodvcs/SolutionOpener

---

**Happy solution hunting! 🚀🔍**

**Remember**: The goal is to find and open solutions in < 5 seconds. If it takes longer, you're doing it wrong! 😉
