namespace Wallet.Client.Login;

public class HttpRequestDto : IEquatable<HttpRequestDto>
{
    
    public string? GetAllTransactions { get; set; }
    public string? AddTransaction { get; set; }
    public string? GetAllAccounts { get; set; }
    public string? AddAccount { get; set; }


    public bool Equals(HttpRequestDto? other)
    {
        throw new NotImplementedException();
    }
}