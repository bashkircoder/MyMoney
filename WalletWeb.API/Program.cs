using Wallet.Model;
using Wallet.Service;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();

var service = new WalletService();

app.MapGet("/transactions", async () => await service.GetAllTransactionsAsync());
app.MapGet("/accounts/", async () => await service.GetAllAccountsAsync());
app.MapPost("/transactions", async (TransactionDto transaction) => await service.AddTransactionAsync(transaction));
app.MapPost("/accounts", async (AccountDto account) => await service.AddAccountAsync(account));


app.Run();