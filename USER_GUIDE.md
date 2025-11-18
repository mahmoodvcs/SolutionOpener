# Solution Opener - User Guide

## Quick Start Guide

### 1. Adding Your First Repository

When you first launch Solution Opener, you'll see an empty interface with a message prompting you to add a repository.

**Steps:**
1. Click the **"? Add Repository"** button in the toolbar
2. Browse to your repository folder (e.g., `C:\git\GitHub\WiseTechGlobal\CargoWise`)
3. Click "Select Folder"
4. Wait for the scan to complete (progress shown in status bar)
5. Your solutions will appear in a new tab

### 2. Understanding the Interface

```
???????????????????????????????????????????????????????
? [? Add Repository] [?? Refresh]                     ? ? Toolbar
???????????????????????????????????????????????????????
? Search: [Type to search...] [Clear]                 ? ? Search Bar
???????????????????????????????????????????????????????
? ?? Repo 1 ??? Repo 2 ??? ? Favorites ??            ? ? Repository Tabs
? ?  Solutions List                        ?            ?
? ??????????????????????????????????????????            ?
???????????????????????????????????????????????????????
? 127 solutions total | Repo 1: 45 solutions          ? ? Status Bar
???????????????????????????????????????????????????????
```

### 3. Searching for Solutions

The search feature is powerful and flexible:

**Simple Search:**
- Type `cargo` ? finds all solutions with "cargo" in name or path
- Type `api` ? finds all solutions with "api" in name or path

**Multi-Phrase Search:**
- Type `cargo api` ? finds solutions containing BOTH "cargo" AND "api"
- Type `test integration` ? finds solutions with both words
- Search is case-insensitive

**Clear Search:**
- Click the "Clear" button
- Or press `Ctrl+A` then `Delete` in the search box

### 4. Opening Solutions

**Method 1: Double-Click**
- Simply double-click any solution in the list

**Method 2: Enter Key**
- Select a solution with arrow keys or mouse
- Press `Enter`

**Method 3: Context Menu**
- Right-click on a solution
- Choose "Open Solution"

### 5. Using Favorites

**Add to Favorites:**
1. Right-click a solution
2. Select "Add to Favorites"
3. A star (?) appears in the first column

**View Favorites:**
- Click the "? Favorites" tab
- All your favorite solutions are shown together, regardless of which repository they're from

**Remove from Favorites:**
1. Right-click a favorite solution
2. Select "Remove from Favorites"

### 6. Managing Repositories

**Refresh a Repository:**
- Select the repository tab you want to refresh
- Click the "?? Refresh" button or press `F5`
- This rescans the folder for new or deleted solutions

**Switch Between Repositories:**
- Click the tab for the repository you want to view
- Or use `Ctrl+Tab` to cycle through tabs

**Remove a Repository:**
- Right-click on the repository tab
- Select "Remove Repository"
- Confirm the removal
- Note: This only removes it from Solution Opener, it doesn't delete any files

## Advanced Features

### Context Menu Options

Right-click any solution to access:

1. **Open Solution** - Opens in Visual Studio (default .sln handler)
2. **Open in Explorer** - Opens the folder containing the solution in Windows Explorer
3. **Copy Path** - Copies the full file path to clipboard (useful for sharing or command-line work)
4. **Add to Favorites / Remove from Favorites** - Toggle favorite status

### Keyboard Shortcuts Reference

| Shortcut | Action |
|----------|--------|
| `Ctrl+F` | Focus search box and select all text |
| `Ctrl+N` | Add new repository |
| `F5` | Refresh current repository (rescan for solutions) |
| `Enter` | Open selected solution |
| `?` `?` | Navigate solution list |
| `Ctrl+Tab` | Switch to next tab |
| `Ctrl+Shift+Tab` | Switch to previous tab |

### Sorting Solutions

Click any column header to sort:
- **?** - Sort by favorite status
- **Name** - Sort alphabetically by solution name
- **Path** - Sort by relative path
- **Modified** - Sort by last modified date
- **Size** - Sort by file size

Click the same header again to reverse the sort order.

### Understanding Solution Information

| Column | Description | Example |
|--------|-------------|---------|
| ? | Favorite indicator | ? or empty |
| Name | Solution filename (without .sln) | CargoWise.API |
| Path | Relative path from repository root | src/api/CargoWise.API.sln |
| Modified | Time since last modification | 2h ago, 3d ago, 2024-01-15 |
| Size | File size in appropriate units | 45 KB, 1.2 MB |

## Tips & Tricks

### ?? Search Tips

1. **Use Multiple Words**: `cargo integration test` finds solutions with all three words
2. **Search Paths Too**: Searching for `src api` will find solutions in the `src/api` folder
3. **Quick Filter**: Type a few letters to quickly narrow down hundreds of solutions

### ?? Organization Tips

1. **Use Favorites**: Star your most-used solutions for quick access
2. **Descriptive Repository Names**: When you have multiple clones, the folder name becomes the tab name
3. **Regular Refreshes**: Press F5 after pulling latest code to update the solution list

### ? Performance Tips

1. **Large Repositories**: Initial scan may take a minute for repos with 1000+ solutions
2. **Search is Instant**: Once scanned, search and filter are instantaneous
3. **Cached Results**: Solutions are cached; only refresh when needed

### ?? Troubleshooting Tips

**Problem**: Repository scanned but no solutions appear
- **Solution**: Verify .sln files exist, check folder permissions, try F5 to refresh

**Problem**: Solution won't open
- **Solution**: Verify Visual Studio is installed and .sln association is set up

**Problem**: Window opens off-screen
- **Solution**: Configuration auto-resets if window is outside screen bounds, or delete config file

**Problem**: Search not working as expected
- **Solution**: Remember all search terms must match (AND logic), not OR

## Configuration & Data

### Where is my data stored?

**Configuration File:**
```
%APPDATA%\SolutionOpener\config.json
```

**What's saved:**
- Repository paths
- Discovered solutions (cached)
- Window size, position, maximized state
- Last selected tab
- Favorite solution paths
- Recent solution paths (last 20)

### Backup Configuration

**Automatic Backup:**
- Every time configuration is saved, the previous version is backed up to `config.json.bak`

**Manual Backup:**
- Copy `%APPDATA%\SolutionOpener\config.json` to a safe location

**Restore from Backup:**
- Copy your backup to `%APPDATA%\SolutionOpener\config.json`

### Reset to Defaults

1. Close Solution Opener
2. Delete `%APPDATA%\SolutionOpener\config.json`
3. Restart the application
4. Re-add your repositories

## Common Workflows

### Daily Development Workflow

1. Launch Solution Opener
2. Press `Ctrl+F` to focus search
3. Type a few letters of your solution name
4. Press `Enter` to open
5. Visual Studio launches with your solution

**Time saved**: ~30 seconds vs. browsing folders manually

### Working with Multiple Clones

If you have multiple clones of the same repo (e.g., for different branches):

1. Add each clone as a separate repository
2. Name the folders descriptively (e.g., `CargoWise-Main`, `CargoWise-Feature`)
3. Each gets its own tab
4. Switch between tabs to work on different versions

### Finding Solutions Across All Repositories

1. Go to the "? Favorites" tab
2. Click the "Name" column header to sort alphabetically
3. Or use Search to find across all repos simultaneously

### Cleaning Up Old Solutions

1. Delete solutions from disk (outside this app)
2. Press `F5` in Solution Opener to refresh
3. Deleted solutions will disappear from the list

## FAQ

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

## Getting Help

If you encounter issues:

1. Check the Troubleshooting section above
2. Try resetting configuration
3. Contact your development team
4. Check the application logs (coming in future versions)

---

**Happy solution hunting! ??**
