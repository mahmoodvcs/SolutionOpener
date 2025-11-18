# Solution Opener

**Instantly find and open Visual Studio solutions across multiple repository clones.**

A productivity-focused WPF application that eliminates time wasted navigating folder structures. Search thousands of solutions in milliseconds and get back to coding in under 5 seconds.

## 🚀 Why Solution Opener?

**The Problem:**
- Wasting 30+ seconds browsing nested folders to find solutions
- Managing multiple repository clones for different branches
- Forgetting where specific solutions are located
- No quick way to search across all repositories

**The Solution:**
- ⚡ **Instant Search**: Find any solution in < 1 second
- 🎯 **Multi-Repository Management**: View and search multiple repos simultaneously
- ⭐ **Smart Favorites**: Quick access to frequently-used solutions
- ⌨️ **Keyboard-First**: Complete keyboard control for maximum efficiency
- 💾 **Persistent Cache**: No re-scanning needed between sessions

---

## 📋 Table of Contents

- [Quick Start](#-quick-start)
- [Keyboard Shortcuts](#-keyboard-shortcuts)
- [Search & Filter](#-search--filter)
- [Favorites](#-favorites)
- [Configuration](#-configuration)
- [Power User Tips](#-power-user-tips)
- [Troubleshooting](#-troubleshooting)
- [FAQ](#-faq)

---

## ⚡ Quick Start

### First Launch
1. Click **"➕ Add Repository"** or press `Ctrl+N`
2. Browse to your repository folder
3. Wait for the scan to complete
4. Your solutions appear in a new tab

### Daily Usage
1. Press `Ctrl+F` to focus search
2. Type a few letters of your solution name
3. Press `Enter` to open
4. Visual Studio launches with your solution

**⏱️ Time saved**: ~30 seconds per launch

---

## ⌨️ Keyboard Shortcuts

| Shortcut | Action |
|----------|--------|
| `Ctrl+F` | Focus search box |
| `Ctrl+N` | Add new repository |
| `F5` | Refresh current repository |
| `Enter` | Open selected solution |
| `Esc` | Clear search |
| `↑` `↓` | Navigate solution list |
| `Ctrl+Tab` | Switch to next tab |
| `Ctrl+1-9` | Switch to tab 1-9 directly |

---

## 🔍 Search & Filter

### Multi-Phrase Search (AND logic)
- `api web` → finds solutions containing BOTH "api" AND "web"
- `test integration payment` → finds solutions with all three words
- Search is **case-insensitive**

### Tips
**Combine terms strategically:**
- ❌ Bad: `test` (too broad)
- ✅ Good: `integration test payment` (specific)

**Use path hints:**
- `src core` → core libraries in src folder
- `\samples\` → only samples folder

### Sorting
Click any column header to sort by:
- ⭐ Favorite status
- Name, Path, Modified date, or Size

---

## ⭐ Favorites

**Add to Favorites:**
1. Right-click a solution
2. Select "Add to Favorites"
3. A star (⭐) appears

**View Favorites:**
- Click the "⭐ Favorites" tab
- All favorites shown together, regardless of repository

**What to favorite:**
- Solutions you open daily
- Main entry-point solutions
- Frequently debugged solutions

---

## 🖱️ Context Menu

**Right-click on Solutions:**
- **Open Solution** - Opens in Visual Studio
- **Open in Explorer** - Opens the folder
- **Copy Path** - Copies full path to clipboard
- **Add/Remove Favorites** - Toggle favorite status

**Right-click on Repository Tabs:**
- **Refresh Repository** - Rescan for solutions (F5)
- **Remove Repository** - Remove from app (doesn't delete files)

---

## ⚙️ Configuration

**Location:** `%APPDATA%\SolutionOpener\config.json`

**What's saved:**
- Repository paths and discovered solutions (cached)
- Window size, position, maximized state
- Favorite and recent solution paths (last 20)

**Automatic Backup:** `config.json.bak` created on every save

**Reset to Defaults:**
1. Close app
2. Delete `config.json`
3. Restart and re-add repositories

---

## 📝 Common Workflows

### Working with Multiple Clones
Add each clone as a separate repository:
```
C:\git\MyProject-Main\
C:\git\MyProject-Release\
C:\git\MyProject-Feature\
```
Each gets its own tab. Switch between branches without git checkout.

### Daily Development
1. `Ctrl+F` → Type 2-3 letters → `Enter`
2. Back to coding in < 5 seconds!

---

## 🔥 Power User Tips

### Performance
- **Large Repositories (1000+ solutions):** First scan takes 30-60s (one-time). Search is instant after caching.
- **Only refresh after pulling new code** - results are cached between sessions

### Repository Organization
- Add 3-5 repos max for clarity
- Name folders descriptively (becomes tab name)
- Use Favorites for frequently-accessed solutions

---

## 🔧 Troubleshooting

**No solutions found:**
- Verify .sln files exist
- Check folder permissions
- Press `F5` to refresh

**Solutions won't open:**
- Verify Visual Studio is installed
- Check .sln file association

**Lost configuration:**
- Check `config.json.bak` for backup
- Delete `config.json` to reset

**Too many/no search results:**
- Add more specific terms (AND logic)
- Check spelling
- Try fewer terms

---

## ❓ FAQ

**Q: Can I add the same repository twice?**  
A: No, duplicates are prevented.

**Q: What happens if I move a repository folder?**  
A: Remove old path and add new one.

**Q: Does this work with Rider or VS Code?**  
A: Yes, uses the default .sln handler.

**Q: How many solutions can it handle?**  
A: Tested with 1000+ solutions per repository with instant search.

**Q: Can I use this on macOS or Linux?**  
A: No, Windows-only WPF application.

---

## 🛠️ Technical Details

**Built With:**
- **.NET 10** - Windows Desktop Runtime
- **WPF** - Windows Presentation Foundation
- **MVVM Pattern** - CommunityToolkit.Mvvm
- **System.Text.Json** - Configuration persistence

**Performance:**
- Async scanning (non-blocking UI)
- Cached results (no re-scanning)
- Optimized string matching
- Lazy loading

---

## 📊 Project Info

**Version:** 1.0  
**Platform:** Windows 10/11, .NET 10  
**Repository:** https://github.com/mahmoodvcs/SolutionOpener  
**License:** Personal utility - modify freely

---

**Happy solution hunting! 🚀**

*Remember: Find and open solutions in < 5 seconds. If it takes longer, you're doing it wrong! 😉*
