using ReactiveUI.Fody.Helpers;

namespace Wallet.Desktop.ViewModels;

public class BasePage
{
    [Reactive] public ViewModelBase? Page { get; set; }
}