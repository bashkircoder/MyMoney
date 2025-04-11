using System;
using System.Net.Mime;
using System.Reactive;
using System.Runtime.InteropServices.ObjectiveC;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wallet.Client.Login;
using Wallet.Desktop.Views;
using Wallet.Desktop.Views.Pages;
using System.Windows;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.LogicalTree;
using Avalonia.Platform;
using Avalonia.VisualTree;
using DynamicData.Binding;

namespace Wallet.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private AppWindow? _appWindow;
    [Reactive] public string? UserName { get; set; }
    [Reactive] public string? Password { get; set; }
    
    [Reactive] public bool IsChecked  { get; set; } =  true;
    public ReactiveCommand<Unit, Unit> CommandClear { get; set; }
    
    public ReactiveCommand<Unit, Unit> CommandEnter { get; set; }

    private Authorisation _authorisation;

    public MainWindowViewModel()
    {
        var canExecCommandClear = this.WhenAnyValue(vm => vm.UserName, vm => vm.Password,
            (UserName, Password) => !string.IsNullOrWhiteSpace(UserName) || !string.IsNullOrWhiteSpace(Password));
        var canExecCommandEnter = this.WhenAnyValue(vm => vm.UserName, vm => vm.Password,
            (UserName, Password) => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password));
        CommandClear = ReactiveCommand.Create(Clear, canExecCommandClear);
        CommandEnter = ReactiveCommand.Create(Login, canExecCommandEnter);
        var cache = Cache.Cache.GetUserCache();
        UserName = cache?.UserName;
        Password = cache?.Password;
    }
    
    private void Login()
    {
        if (UserName != null)
        {
            if (Password != null)
            {
                _authorisation = new Authorisation(UserName, Password);

                Cache.Requests.HttpRequests = _authorisation.Login();
            }
        }

        if (Cache.Requests.HttpRequests?.GetAllAccounts == "")
        {
            return;
        }
        
        switch (IsChecked)
        {
            case true:
                Cache.Cache.WriteUserCache(UserName, Password);
                break;
            case false:
                Cache.Cache.WriteUserCache();
                break;
        }

        _appWindow = new AppWindow();
        _appWindow.Show();
        (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow.Close();
    }

    private void Clear()
    {
        UserName = String.Empty;
        Password = String.Empty;
    }
    
    
}