

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wallet.Client.BL;
using Wallet.Model;

namespace Wallet.Desktop.ViewModels.PagesViewModels;

public class HistoryPageViewModel : ViewModelBase
{
    
    
    
    private readonly Service _service;

    public ObservableCollection<TransactionDto> Transactions { get; set; } = [];
    
    private IEnumerable<TransactionDto> BaseTransactions { get; set; } = [];
    public ObservableCollection<string> Accounts { get; set; } = [];
    public ObservableCollection<string> Types { get; set; } = [];
    
    [Reactive] public DateTimeOffset BeginDate { get; set; }
    [Reactive] public DateTimeOffset EndDate { get; set; }
    [Reactive] public string? Recipient { get; set; }
    [Reactive] public  string? Account { get; set; }
    [Reactive] public string? Type { get; set; }
    

    public HistoryPageViewModel()
    {
        
        _service = new Service(Cache.Requests.HttpRequests);
        
        RxApp.MainThreadScheduler.Schedule(StartingLoadTransactionsAsync);
        
        
        this.WhenValueChanged(vm => vm.BeginDate).Subscribe((_ => Search()));
        this.WhenValueChanged(vm => vm.EndDate).Subscribe((_ => Search()));
        this.WhenValueChanged(vm => vm.Recipient).Subscribe((_ => Search()));
        this.WhenValueChanged(vm => vm.Account).Subscribe((_ => Search()));
        this.WhenValueChanged(vm => vm.Type).Subscribe((_ => Search()));

    }

    private async void StartingLoadTransactionsAsync()
    {
        BaseTransactions = await _service.GetAllTransactionsAsync();

        foreach (var transaction in BaseTransactions)
        {
            Transactions.Add(transaction);
        }

        var bufer = new List<string>();
        
        foreach (var item in BaseTransactions)
        {
            bufer.Add($"{item.BankName}: *{item.AccountNumber.Substring(12,4)}");
        }

        var uniqAccounts = (from item in bufer select item).Distinct();

        Accounts.Add("Все");
        
        foreach (var item in uniqAccounts)
        {
            Accounts.Add(item);
        }

        Account = Accounts[0];

        var temp = (from transaction in BaseTransactions select transaction.TransactionType).Distinct();
        
        Types.Add("Все");

        foreach (var item in temp)
        {
            Types.Add(item);
        }

        Type = Types[0];
        
        DateTime firstMonthDay = DateTime.Now;
        BeginDate = firstMonthDay.AddDays(-firstMonthDay.Day+1);
        EndDate = DateTime.Now;
        
    }

    private void Search()
    {
        var temp = BaseTransactions.
            Where(x => x.Date.ToDateTime(TimeOnly.Parse("00:00:00"), DateTimeKind.Unspecified) >= BeginDate &&
                                   x.Date.ToDateTime(TimeOnly.Parse("00:00:00"), DateTimeKind.Unspecified) <= EndDate);

        if (Account != "Все")
        {
            temp = temp.Where(x => x.AccountNumber.Substring(12,4) == Account.Substring(Account.IndexOf('*')+1, 4) && x.BankName == Account.Substring(0,Account.IndexOf(':')));
        }

        if (!String.IsNullOrEmpty(Recipient))
        {
            temp = temp.Where(x => x.Recipient.Contains(Recipient, StringComparison.CurrentCultureIgnoreCase));
        }

        if (Type != "Все")
        {
            temp = temp.Where(x => x.TransactionType == Type);
        }
        
        Transactions.Clear();
        
        foreach (var item in temp)
        {
            Transactions.Add(item);
        }
    }

}