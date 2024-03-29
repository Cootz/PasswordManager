﻿using PasswordManager.Utils;

namespace PasswordManager.Tests.Utils;

[TestFixture]
public class EncryptionHelperTest
{
    private const string expected_string = "1234567890qwertyuiopasdfghjkl;";

    // ReSharper disable once InconsistentNaming
    private readonly byte[] expected_byte_key =
    {
        0x31, 0x00, 0x32, 0x00, 0x33, 0x00, 0x34, 0x00, 0x35, 0x00, 0x36, 0x00, 0x37, 0x00, 0x38, 0x00, 0x39, 0x00,
        0x30, 0x00, 0x71, 0x00, 0x77, 0x00, 0x65, 0x00, 0x72, 0x00, 0x74, 0x00, 0x79, 0x00, 0x75, 0x00, 0x69, 0x00,
        0x6f, 0x00, 0x70, 0x00, 0x61, 0x00, 0x73, 0x00, 0x64, 0x00, 0x66, 0x00, 0x67, 0x00, 0x68, 0x00, 0x6a, 0x00,
        0x6b, 0x00, 0x6c, 0x00, 0x3b, 0x00
    };

    [Test]
    public void GenerateKeyTest()
    {
        byte[] key = null!;

        Assert.DoesNotThrow(() => { key = EncryptionHelper.GenerateKey(); });

        Assert.That(key, Is.Not.Null);
        Assert.That(key.Length, Is.EqualTo(64));
    }

    [Test]
    public void ByteKeyToStringTest()
    {
        byte[] key = expected_byte_key;
        string? keyString = string.Empty;

        Assert.DoesNotThrow(() => { keyString = key.ToKeyString(); });

        Assert.That(keyString, Is.EquivalentTo(expected_string));
    }

    [Test]
    public void StringKeyToByteTest()
    {
        byte[] key = null!;

        Assert.DoesNotThrow(() => { key = expected_string.ToKey(); });

        Assert.That(key.Length, Is.EqualTo(60));
        Assert.That(key, Is.EquivalentTo(expected_byte_key));
    }
}