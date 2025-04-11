

using System.Collections;
using System.Globalization;
using System.Net.Http.Json;
using Wallet.Client.BL;
using Wallet.Client.Login;
using Wallet.Model;

var login = new Authorisation("a.astahov","usr737AS");

var connections = login.Login();

var service = new Service(connections);

var result = await service.GetAllTransactionsAsync();

foreach (var VARIABLE in result)
{ 
    Console.WriteLine($"{VARIABLE.Date}|{VARIABLE.BankName}-{VARIABLE.AccountNumber}|{VARIABLE.TransactionType}|{VARIABLE.Amount}");
}

