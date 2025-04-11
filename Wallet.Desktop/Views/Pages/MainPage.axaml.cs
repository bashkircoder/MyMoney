using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ReactiveUI;
using Wallet.Client.BL;
using Wallet.Desktop.Views.UserControls;
using Wallet.Model;

namespace Wallet.Desktop.Views.Pages;

public partial class MainPage : UserControl
{
    private Dictionary<string, Bitmap> _images = new Dictionary<string, Bitmap>();
    private IEnumerable<AccountDto> Accounts { get; set; }
    private Service _service;
    public MainPage()
    {
        InitializeComponent();
        _images.Add("Сбербанк", new Bitmap("D:\\Wallet\\Wallet.Desktop\\Assets\\sber.png"));
        _images.Add("Долинск", new Bitmap("D:\\Wallet\\Wallet.Desktop\\Assets\\dolinsk.jpg"));
        _service = new Service(Cache.Requests.HttpRequests);
        
        RxApp.MainThreadScheduler.Schedule(LoadAccountsAsync);
        
        
        
    }

    private async void LoadAccountsAsync()
    {
        
    }
}