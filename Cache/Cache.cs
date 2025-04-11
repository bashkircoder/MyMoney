using System.Text.Json;

namespace Cache;

public static class Cache
{
    public static CacheModel? Model { get; set; } = new();
    
    public static CacheModel? GetUserCache()
    {
        const string path = @"D:\Wallet\Cache\usercache.json";
        var stream = File.ReadAllText(path);
        return JsonSerializer.Deserialize<CacheModel?>(stream);
    }

    public static void WriteUserCache (string? userName = null, string? password = null)
    {
        if (Model == null) return;
        Model.UserName = userName;
        Model.Password = password;
        const string path = @"D:\Wallet\Cache\usercache.json";
        using var stream = new FileStream(path, FileMode.Truncate, FileAccess.Write);
        JsonSerializer.Serialize(stream, Model);
    }
}