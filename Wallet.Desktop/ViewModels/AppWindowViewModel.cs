using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wallet.Desktop.ViewModels.PagesViewModels;
using Wallet.Desktop.Views.Pages;

namespace Wallet.Desktop.ViewModels;

public class AppWindowViewModel : ViewModelBase
{
    
    private UserControl MainPage;
    private UserControl HistoryPage;
    private UserControl PaymentPage;
    private UserControl SupportPage;
    [Reactive] public UserControl? SelectedPage { get; set; }
    [Reactive] public Bitmap SelectedBitmapMainPage { get; set; }
    [Reactive] public Bitmap SelectedBitmapHistoryPage { get; set; }
    [Reactive] public Bitmap SelectedBitmapPaymentPage { get; set; }
    [Reactive] public Bitmap SelectedBitmapSupportPage { get; set; }

    public ReactiveCommand<Unit, Unit> CommandMainPage { get; }
    public ReactiveCommand<Unit, Unit> CommandHistoryPage { get; }
    public ReactiveCommand<Unit, Unit> CommandPaymentPage { get; }
    public ReactiveCommand<Unit, Unit> CommandSupportPage { get; }
  public AppWindowViewModel()
    {
        MainPage = new MainPage();
        HistoryPage = new HistoryPage();
        PaymentPage = new PaymentPage();
        SupportPage = new SupportPage();
        SelectedPage = MainPage;

        var canExecuteMainPageCommand = this.WhenAnyValue(vm => vm.SelectedPage,(SelectedPage) => SelectedPage != MainPage);
        var canExecuteHistoryPageCommand = this.WhenAnyValue(vm => vm.SelectedPage,(SelectedPage) => SelectedPage != HistoryPage);
        var canExecutePaymentPageCommand = this.WhenAnyValue(vm => vm.SelectedPage,(SelectedPage) => SelectedPage != PaymentPage);
        var canExecuteSupportPageCommand = this.WhenAnyValue(vm => vm.SelectedPage,(SelectedPage) => SelectedPage != SupportPage);
        
        CommandMainPage = ReactiveCommand.Create(OpenMainPage, canExecuteMainPageCommand);
        CommandHistoryPage = ReactiveCommand.Create(OpenHistoryPage, canExecuteHistoryPageCommand);
        CommandPaymentPage = ReactiveCommand.Create(OpenPaymentPage, canExecutePaymentPageCommand);
        CommandSupportPage = ReactiveCommand.Create(OpenSupportPage, canExecuteSupportPageCommand);
  
        SelectedBitmapMainPage =new Bitmap("D:\\Wallet\\Wallet.Desktop\\Assets\\MainDark.png");
        SelectedBitmapHistoryPage = new Bitmap("D:\\Wallet\\Wallet.Desktop\\Assets\\History.png");
        SelectedBitmapPaymentPage = new Bitmap("D:\\Wallet\\Wallet.Desktop\\Assets\\Payment.png");
        SelectedBitmapSupportPage = new Bitmap("D:\\Wallet\\Wallet.Desktop\\Assets\\Support.png");
    }

    private void OpenSupportPage()
    {
       SelectedPage = SupportPage;
       
    }

    private void OpenPaymentPage()
    {
        SelectedPage = PaymentPage;
        
    }

    private void OpenHistoryPage()
    {
        SelectedPage = HistoryPage;
        
    }

    private void OpenMainPage()
    {
        SelectedPage = MainPage;
        
    }
    
}