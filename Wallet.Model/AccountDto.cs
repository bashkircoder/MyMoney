namespace Wallet.Model;
public class AccountDto : IEquatable<AccountDto>
{
    
    public int Id { get; set; }
    public string BankName { get; set; }
    public string AccountNumber { get; set; }
    public double Balance { get; set; }
    
    public bool Equals(AccountDto? other)
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AccountDto)obj);
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}