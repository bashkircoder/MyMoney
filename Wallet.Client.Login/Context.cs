using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Wallet.Client.Login;

public class Context(string connectionString) : DbContext
{
    public HttpRequestDto? GetConnection(string user, string password)
    {
        var connection = new NpgsqlConnection(connectionString);
        const string sql = "SELECT function_get_connection(@1,@2);";
        using var command = new NpgsqlCommand(sql, connection);
        
        command.Parameters.AddWithValue("@1", user);
        command.Parameters.AddWithValue("@2", password);
        
        connection.Open();
        
        var result = command.ExecuteReader();
        
        if (!result.HasRows) return null;

        HttpRequestDto? connections = new HttpRequestDto();
        
        while (result.Read())
        {
          
          return connections = JsonSerializer.Deserialize<HttpRequestDto>(result.GetString(0));
            
        }
        connection.CloseAsync();

        return null;
    }
    
    
}