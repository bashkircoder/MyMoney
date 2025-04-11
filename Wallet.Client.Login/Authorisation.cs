namespace Wallet.Client.Login;

public class Authorisation
{
    private readonly string _login;
    private readonly string _password;
    private Context _context;
    
    public Authorisation(string login, string password)
    {
        _login = login;
        _password = password;
        var contextFactory = new ContextFactory();
        _context = contextFactory.CreateDbContext();
    }

    public HttpRequestDto? Login()
    {
        return _context.GetConnection(_login, _password);
    }

}