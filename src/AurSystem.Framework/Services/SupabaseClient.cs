using AurSystem.Framework.Models.Options;
using Microsoft.Extensions.Options;
using Supabase;

namespace AurSystem.Framework.Services;

public class SupabaseClient
{
    private readonly SupabaseConfig _supabaseConfig;
    private readonly ILogger<SupabaseClient> _logger;
    private Client? _client;

    public SupabaseClient(IOptions<SupabaseConfig> config, ILogger<SupabaseClient> logger)
    {
        _logger = logger;
        _supabaseConfig = config.Value;
    }

    public async Task<Client> GetClient()
    {
        if (_client is not null) return _client;
        _logger.LogInformation("Config: {Url} - {Key}", _supabaseConfig.Url, _supabaseConfig.Key);        
        await Supabase.Client.InitializeAsync(_supabaseConfig.Url, _supabaseConfig.Key);
        _client = Supabase.Client.Instance;
        return _client;
    }
}
