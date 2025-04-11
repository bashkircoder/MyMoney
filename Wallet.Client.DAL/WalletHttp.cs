using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Wallet.Client.Login;
using Wallet.Model;

namespace Wallet.Client.DAL;

public class WalletHttp
{
    private static readonly HttpClient Client = new();
    private readonly Uri GetAllTransactionsUri;
    private readonly Uri AddTransactionUri;
    private readonly Uri GetAllAccountsUri;
    private readonly Uri AddAccountUri;

    public WalletHttp(HttpRequestDto connections)
    {
        GetAllTransactionsUri = new Uri(connections.GetAllTransactions);
        AddTransactionUri = new Uri(connections.AddTransaction);
        GetAllAccountsUri = new Uri(connections.GetAllAccounts);
        AddAccountUri = new Uri(connections.AddAccount);
    }

    public async Task<IEnumerable<TransactionDto>?> GetAllTransactionsAsync() =>
        await Client.GetFromJsonAsync<List<TransactionDto>>(GetAllTransactionsUri);
    
    public async Task AddTransactionAsync(TransactionDto transaction) =>
        await Client.PutAsJsonAsync(AddTransactionUri, transaction);
    
    public async Task<IEnumerable<AccountDto>?> GetAllAccountsAsync() =>
        await Client.GetFromJsonAsync<IEnumerable<AccountDto>>(GetAllAccountsUri);
    
    public async Task AddAccountAsync(AccountDto account) =>
        await Client.PutAsJsonAsync(AddAccountUri, account);

}