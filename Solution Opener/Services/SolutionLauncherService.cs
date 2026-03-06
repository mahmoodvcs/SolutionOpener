using System.Diagnostics;
using System.IO;
using Solution_Opener.Models;

namespace Solution_Opener.Services;

public class SolutionLauncherService
{
    public void OpenWithDefault(SolutionInfo project)
    {
        switch (project.Type)
        {
            case ProjectType.Solution:
                OpenSolution(project.FullPath);
                break;
            case ProjectType.GitRepository:
                OpenVsCode(project.FullPath);
                break;
            default:
                throw new InvalidOperationException($"Unknown project type: {project.Type}");
        }
    }

    public void OpenSolution(string solutionPath)
    {
        if (!File.Exists(solutionPath))
        {
            throw new FileNotFoundException("Solution file not found", solutionPath);
        }

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = solutionPath,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to open solution: {ex.Message}", ex);
        }
    }

    public void OpenVsCode(string repositoryPath)
    {
        if (!Directory.Exists(repositoryPath))
        {
            throw new DirectoryNotFoundException("Repository directory not found", new DirectoryNotFoundException(repositoryPath));
        }

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "code",
                Arguments = $"\"{repositoryPath}\"",
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to open VS Code: {ex.Message}. Make sure VS Code is installed and added to PATH.", ex);
        }
    }

    public void OpenInExplorer(string path)
    {
        try
        {
            string argument;
            
            if (File.Exists(path))
            {
                // If it's a file, select it in Explorer
                argument = $"/select,\"{path}\"";
            }
            else if (Directory.Exists(path))
            {
                // If it's a directory, just open it
                argument = $"\"{path}\"";
            }
            else
            {
                throw new FileNotFoundException("Path not found", path);
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = argument,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to open Explorer: {ex.Message}", ex);
        }
    }

    public void CopyPathToClipboard(string path)
    {
        try
        {
            System.Windows.Clipboard.SetText(path);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to copy to clipboard: {ex.Message}", ex);
        }
    }
}
