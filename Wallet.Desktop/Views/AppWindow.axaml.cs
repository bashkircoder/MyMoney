using Avalonia.Controls;
using Avalonia.Interactivity;
using Wallet.Desktop.Views;
using Wallet.Desktop.Views.Pages;

namespace Wallet.Desktop.Views;

public partial class AppWindow : Window
{
    private UserControl MainPage = new MainPage();
    private UserControl HistoryPage = new HistoryPage();
    private UserControl PaymentPage = new PaymentPage();
    private UserControl SupportPage = new SupportPage();
    public AppWindow()
    {
        InitializeComponent();
    }
    
}