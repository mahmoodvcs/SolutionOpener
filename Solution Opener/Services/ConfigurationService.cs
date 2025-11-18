using System.IO;
using System.Text.Json;
using Solution_Opener.Models;

namespace Solution_Opener.Services;

public class ConfigurationService
{
    private static readonly string AppDataFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "SolutionOpener");
    
    private static readonly string ConfigFilePath = Path.Combine(AppDataFolder, "config.json");
    private static readonly string BackupFilePath = Path.Combine(AppDataFolder, "config.json.bak");

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public AppConfiguration LoadConfiguration()
    {
        try
        {
            if (!File.Exists(ConfigFilePath))
            {
                return new AppConfiguration();
            }

            var json = File.ReadAllText(ConfigFilePath);
            return JsonSerializer.Deserialize<AppConfiguration>(json) ?? new AppConfiguration();
        }
        catch (Exception ex)
        {
            // If config is corrupted, try backup
            if (File.Exists(BackupFilePath))
            {
                try
                {
                    var json = File.ReadAllText(BackupFilePath);
                    return JsonSerializer.Deserialize<AppConfiguration>(json) ?? new AppConfiguration();
                }
                catch
                {
                    // Backup also corrupted, return defaults
                }
            }
            
            // Log the error (for now just to debug output)
            System.Diagnostics.Debug.WriteLine($"Error loading configuration: {ex.Message}");
            return new AppConfiguration();
        }
    }

    public void SaveConfiguration(AppConfiguration config)
    {
        try
        {
            // Ensure directory exists
            Directory.CreateDirectory(AppDataFolder);

            // Create backup of existing config
            if (File.Exists(ConfigFilePath))
            {
                File.Copy(ConfigFilePath, BackupFilePath, overwrite: true);
            }

            // Write to temp file first
            var tempFile = ConfigFilePath + ".tmp";
            var json = JsonSerializer.Serialize(config, JsonOptions);
            File.WriteAllText(tempFile, json);

            // Atomic rename
            File.Move(tempFile, ConfigFilePath, overwrite: true);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error saving configuration: {ex.Message}");
            throw;
        }
    }
}
