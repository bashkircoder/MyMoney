namespace Wallet.Model;


public class TransactionDto : IEquatable<TransactionDto>
{
    
    
    public int? Id { get; set; }
    public DateOnly Date { get; set; }
    
    public string Direction { get; set; }
    
    public string BankName { get; set; }
    public string AccountNumber { get; set; }
    public string Recipient { get; set; }
    public string TransactionType { get; set; }
    public double Amount { get; set; }

    public bool Equals(TransactionDto? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id
               && Date == other.Date
               && Direction == other.Direction
               && BankName == other.BankName
               && AccountNumber == other.AccountNumber
               && Recipient == (other.Recipient)
               && Amount.Equals(other.Amount);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TransactionDto)obj);
    }
    
    public static bool operator ==(TransactionDto? left, TransactionDto? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TransactionDto? left, TransactionDto? right)
    {
        return !Equals(left, right);
    }
}