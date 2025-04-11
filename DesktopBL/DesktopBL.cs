using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using Avalonia.Media.Imaging;
using Wallet.Client.BL;
using Wallet.Model;

namespace DesktopBL;

public class DesktopBL
{
    private Dictionary<string, Bitmap> _images = new Dictionary<string, Bitmap>();
    
    public static IEnumerable<AccountDto?> AllAccounts = [];
    
    private static Service _service;

    public DesktopBL()
    {
        
    }
    
}