namespace PasswordManager.Model.DB.Schema;

[Obsolete("Not used anymore", true)]
public class EMail
{
    private char SplitChar = '@';

    public int ID { get; set; }

    public string Adress
    {
        get => _adr + SplitChar + _postfix;
        set => Parse(value);
    }

    private string _adr;
    private string _postfix;

    public EMail()
    {
    }

    public EMail(string adress, string postfix)
    {
        _adr = adress;
        _postfix = postfix;
    }

    public EMail(string fullAdress)
    {
        Adress = fullAdress;
    }

    private void Parse(string email)
    {
        var parsed = email.Split(SplitChar);

        _adr = parsed[0];
        _postfix = parsed[1];
    }

    public static bool operator !=(EMail left, EMail right)
    {
        return left.Adress != right.Adress;
    }

    public static bool operator ==(EMail left, EMail right)
    {
        return left.Adress == right.Adress;
    }

    public override bool Equals(object obj)
    {
        try
        {
            return this == (EMail)obj;
        }
        catch
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return Adress;
    }
}