using Wallet.Client.DAL;
using Wallet.Client.Login;
using Wallet.Model;

namespace Wallet.Client.BL;

public class Service
{
    private readonly WalletHttp _walletHttp;

    public Service(HttpRequestDto connections)
    {
        _walletHttp = new WalletHttp(connections);
    }
    
    public async Task<IEnumerable<TransactionDto>?> GetAllTransactionsAsync() => 
        await _walletHttp.GetAllTransactionsAsync();
    
    public async Task AddTransactionAsync(TransactionDto transaction) =>
        await _walletHttp.AddTransactionAsync(transaction);
    
    public async Task<IEnumerable<AccountDto>?> GetAllAccountsAsync() =>
        await _walletHttp.GetAllAccountsAsync();
    
    public async Task AddAccountAsync(AccountDto account) =>
        await _walletHttp.AddAccountAsync(account);
    
}