using Microsoft.EntityFrameworkCore;
using Npgsql;
using Wallet.Model;
using Wallet.Server.DAL;

namespace Wallet.Service;

public class WalletService
{
    private readonly Context _context;

    public WalletService()
    {
        var contextFactory = new ContextFactory();
        _context = contextFactory.CreateDbContext();
    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
    {
        return await _context.GetAllTransactions();
    }

    public async Task AddTransactionAsync(TransactionDto transaction)
    {
        await _context.AddTransaction(transaction);
    }

    public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
    {
        
        return await _context.GetAllAccounts();
    }

    public async Task AddAccountAsync(AccountDto account)
    {
        await _context.AddAccount(account);
    }

}