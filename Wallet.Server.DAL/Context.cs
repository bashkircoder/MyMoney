using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Wallet.Model;

namespace Wallet.Server.DAL;

public class Context : DbContext
{
    private readonly string _connectionString;
    
    public Context(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransactions()
    {
        var connection = new NpgsqlConnection(_connectionString);
        const string sql = "SELECT * FROM view_transactions";
        await using var command = new NpgsqlCommand(sql, connection);
        await connection.OpenAsync();
        
        var result = await command.ExecuteReaderAsync();
        
        var transactions = new List<TransactionDto>();
        
        while (await result.ReadAsync())
        {
            var transaction = new TransactionDto
            {
                Id = result.GetInt32(0),
                Date = DateOnly.FromDateTime(result.GetDateTime(1)),
                Direction = result.GetString(2),
                BankName = result.GetString(3),
                AccountNumber = result.GetString(4),
                Recipient = result.GetString(5),
                TransactionType = result.GetString(6),
                Amount = result.GetDouble(7)
            };

            transactions.Add(transaction);
        }
        await connection.CloseAsync();
        return transactions;
    }
    
    public async Task AddTransaction(TransactionDto transaction)
    {
        var in_date = transaction.Date.ToString("o", CultureInfo.InvariantCulture);
        DateOnly date = DateOnly.Parse(in_date);
        
        var connection = new NpgsqlConnection(_connectionString);
        string sql = "CALL procedure_insert_transaction(@date,@direction,@bank,@account,@recipient,@type,@amount);";
        await using var command = new NpgsqlCommand(sql, connection);
        
        command.Parameters.AddWithValue("@date", date);
        command.Parameters.AddWithValue("@direction", transaction.Direction);
        command.Parameters.AddWithValue("@bank", transaction.BankName);
        command.Parameters.AddWithValue("@account", transaction.AccountNumber);
        command.Parameters.AddWithValue("@recipient", transaction.Recipient);
        command.Parameters.AddWithValue("@type", transaction.TransactionType);
        command.Parameters.AddWithValue("@amount", transaction.Amount);
        await connection.OpenAsync();
        
        await command.ExecuteScalarAsync();
        
        await connection.CloseAsync();
    }
    
    public async Task<IEnumerable<AccountDto>> GetAllAccounts()
    {
        var connection = new NpgsqlConnection(_connectionString);
        string sql = "SELECT * FROM view_accounts;";
        
        await using var command = new NpgsqlCommand(sql, connection);
        
        await connection.OpenAsync();
       
        var result = (await command.ExecuteReaderAsync());
        
        var accounts = new List<AccountDto>();

        while (await result.ReadAsync())
        {
            var account = new AccountDto();
            account.Id = result.GetInt32(0);
            account.BankName = result.GetString(1);
            account.AccountNumber = result.GetString(2);
            account.Balance = result.GetDouble(3);
            
            accounts.Add(account);
        }

        await connection.CloseAsync();
        
        return accounts;
    }
    
    public async Task AddAccount(AccountDto account)
    {
        var connection = new NpgsqlConnection(_connectionString);
        string sql = "CALL procedure_insert_account(@bank,@account_number,@balance);";
        await using var command = new NpgsqlCommand(sql, connection);
        
        command.Parameters.AddWithValue("@bank", account.BankName);
        command.Parameters.AddWithValue("@account_number", account.AccountNumber);
        command.Parameters.AddWithValue("@balance", account.Balance);
        
        await connection.OpenAsync();
        
        await command.ExecuteScalarAsync();
        
        await connection.CloseAsync();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
}