using MongoDB.Bson;
using Realms;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PasswordManager.Model.DB.Schema;

public partial class Profile : RealmObject
{
    private const char FieldSplit = ':';
    private const char ProfileSplit = ';';
    private const string NullMessage = "null";

    [PrimaryKey]
    public ObjectId ID { get; set; }
    public string Service { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public Profile() 
    {
        ID = new ObjectId();
    }

    public override string ToString()
    {
        StringBuilder ret = new StringBuilder();

        ret.Append(Service ?? NullMessage).Append(FieldSplit);
        ret.Append(Username ?? NullMessage).Append(FieldSplit);
        ret.Append(Password ?? NullMessage).Append(ProfileSplit);

        return ret.ToString();
    }

    //public override bool Equals([AllowNull] Profile other)
    //{
    //    bool[] equals = { Service == other?.Service, Password == other?.Password, Username == other?.Username };

    //    return equals[0] & equals[1] & equals[2];
    //}

    public static bool operator !=(Profile left, Profile right)
    {
        return !left.Equals(right);
    }

    public static bool operator ==(Profile left, Profile right)
    {
        return left.Equals(right);
    }

    public override bool Equals([AllowNull]object obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        return Equals(obj as Profile);
    }

    public override int GetHashCode() => base.GetHashCode();

}
