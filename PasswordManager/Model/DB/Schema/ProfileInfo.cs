﻿using Realms;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PasswordManager.Model.DB.Schema;

public partial class ProfileInfo : RealmObject, IEquatable<ProfileInfo>
{
    private const char FieldSplit = ':';

    private const char ProfileSplit = ';';

    private const string NullMessage = "null";

    [PrimaryKey] public Guid ID { get; set; }

    public ServiceInfo Service { get; init; }

    public string Username { get; init; }

    public string Password { get; init; }

    public ProfileInfo() => ID = Guid.NewGuid();

    public ProfileInfo(Guid id, ServiceInfo service, string username, string password)
    {
        ID = id;
        Service = service;
        Username = username;
        Password = password;
    }

    public override string ToString()
    {
        StringBuilder ret = new();

        ret.Append(Service.Name).Append(FieldSplit);
        ret.Append(Username ?? NullMessage).Append(FieldSplit);
        ret.Append(Password ?? NullMessage).Append(ProfileSplit);

        return ret.ToString();
    }

    public bool Equals([AllowNull] ProfileInfo other)
    {
        if (other is null) return false;

        if (!IsValid || !other.IsValid) return false;

        bool[] equals = { Service == other.Service, Password == other.Password, Username == other.Username };

        return equals[0] & equals[1] & equals[2];
    }

    public static bool operator !=(ProfileInfo left, ProfileInfo right) => !left?.Equals(right) ?? false;

    public static bool operator ==(ProfileInfo left, ProfileInfo right) => left?.Equals(right) ?? false;

    public override bool Equals([AllowNull] object obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        return obj is not null && Equals(obj as ProfileInfo);
    }

    public override int GetHashCode() => base.GetHashCode();
}