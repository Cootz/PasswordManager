using PasswordManager.Utils;

namespace PasswordManager.Tests.Utils
{
    [TestFixture]
    public class EncryptionHelperTest
    {
        const string expected_string = "1234567890qwertyuiopasdfghjkl;";
        readonly byte[] expected_byte_key = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x71, 0x77, 0x65, 0x72, 0x74, 0x79, 0x75, 0x69, 0x6f, 0x70, 0x61, 0x73, 0x64, 0x66, 0x67, 0x68, 0x6a, 0x6b, 0x6c, 0x3b };

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
            string key_string = string.Empty;

            Assert.DoesNotThrow(() => { key_string = key.ToKeyString(); });

            Assert.That(key_string, Is.EquivalentTo(expected_string));
        }

        [Test]
        public void StringKeyToByteTest()
        {
            string key_string = expected_string;
            byte[] key = null!;

            Assert.DoesNotThrow(() => { key = key_string.ToKey(); });

            Assert.That(key, Is.EquivalentTo(expected_byte_key));
        }
    }
}
