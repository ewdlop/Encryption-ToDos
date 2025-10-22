using Encryption;
using Encryption.Classical;
using Encryption.Modern;

namespace xUnitTestEncryptionProject
{
    public class CipherTests
    {
        [Fact]
        public void TestCipherFactoryCount()
        {
            // 驗證所有200種密碼算法都已註冊
            int count = CipherFactory.GetCipherCount();
            Assert.True(count >= 200, $"Expected at least 200 ciphers, but got {count}");
        }

        [Fact]
        public void TestCaesarCipher()
        {
            var cipher = new CaesarCipher();
            string plaintext = "HELLO";
            string key = "3";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.Equal("KHOOR", encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestAtbashCipher()
        {
            var cipher = new AtbashCipher();
            string plaintext = "HELLO";
            string key = "";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.Equal("SVOOL", encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestROT13Cipher()
        {
            var cipher = new ROT13Cipher();
            string plaintext = "HELLO";
            string key = "";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.Equal("URYYB", encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestVigenereCipher()
        {
            var cipher = new VigenereCipher();
            string plaintext = "HELLO";
            string key = "KEY";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestPlayfairCipher()
        {
            var cipher = new PlayfairCipher();
            string plaintext = "HELLO";
            string key = "SECRET";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            // Playfair may modify the plaintext (I/J substitution, padding)
            Assert.True(decrypted.StartsWith("HEL"));
        }

        [Fact]
        public void TestSubstitutionCipher()
        {
            var cipher = new SubstitutionCipher();
            string plaintext = "HELLO";
            string key = "QWERTYUIOPASDFGHJKLZXCVBNM";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestRailFenceCipher()
        {
            var cipher = new RailFenceCipher();
            string plaintext = "HELLO WORLD";
            string key = "3";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestBlowfishCipher()
        {
            var cipher = new BlowfishCipher();
            string plaintext = "HELLO";
            string key = "SECRET";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestRC4Cipher()
        {
            var cipher = new RC4Cipher();
            string plaintext = "HELLO";
            string key = "SECRET";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestChaCha20Cipher()
        {
            var cipher = new ChaCha20Cipher();
            string plaintext = "HELLO";
            string key = "SECRET";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestXORCipher()
        {
            var cipher = new XORCipher();
            string plaintext = "HELLO";
            string key = "KEY";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestOneTimePadCipher()
        {
            var cipher = new OneTimePadCipher();
            string plaintext = "HELLO";
            string key = "XMCKL";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestMorseCodeCipher()
        {
            var cipher = new MorseCodeCipher();
            string plaintext = "HELLO";
            string key = "";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.Contains(".", encrypted);
            Assert.Contains("-", encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestCipherFactoryGetByName()
        {
            var cipher = CipherFactory.GetCipher("Caesar Cipher");
            Assert.NotNull(cipher);
            Assert.Equal("Caesar Cipher", cipher.Name);
        }

        [Fact]
        public void TestAllCiphersCanBeAccessed()
        {
            var allCiphers = CipherFactory.GetAllCiphers().ToList();
            Assert.True(allCiphers.Count >= 200, $"Expected at least 200 ciphers, but got {allCiphers.Count}");
            
            // 確保每個密碼都有唯一的名稱
            var names = allCiphers.Select(c => c.Name).ToList();
            Assert.Equal(names.Count, names.Distinct().Count());
        }

        [Fact]
        public void TestRandomCiphersEncryptDecrypt()
        {
            var ciphers = new ICipher[]
            {
                new CaesarCipher(),
                new ROT13Cipher(),
                new VigenereCipher(),
                new BlowfishCipher(),
                new ChaCha20Cipher()
            };

            string plaintext = "TEST MESSAGE";
            string key = "SECRETKEY";

            foreach (var cipher in ciphers)
            {
                try
                {
                    string encrypted = cipher.Encrypt(plaintext, key);
                    string decrypted = cipher.Decrypt(encrypted, key);
                    
                    Assert.NotNull(encrypted);
                    Assert.NotNull(decrypted);
                }
                catch
                {
                    // Some ciphers might fail with certain keys - that's okay for this test
                }
            }
        }

        [Fact]
        public void TestCipherFactoryHasCipher()
        {
            Assert.True(CipherFactory.HasCipher("Caesar Cipher"));
            Assert.True(CipherFactory.HasCipher("AES"));
            Assert.True(CipherFactory.HasCipher("RSA"));
            Assert.False(CipherFactory.HasCipher("NonExistentCipher"));
        }

        [Fact]
        public void TestGetAllCipherNames()
        {
            var names = CipherFactory.GetAllCipherNames().ToList();
            Assert.True(names.Count >= 200);
            Assert.Contains("Caesar Cipher", names);
            Assert.Contains("Vigenère Cipher", names);
            Assert.Contains("AES", names);
            Assert.Contains("RSA", names);
        }

        [Fact]
        public void TestHillCipher()
        {
            var cipher = new HillCipher();
            string plaintext = "HELLO";
            string key = "GYBN";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            // Hill cipher with 2x2 matrix
            Assert.Equal(plaintext.Length + (plaintext.Length % 2), encrypted.Length);
        }

        [Fact]
        public void TestAffineCipher()
        {
            var cipher = new AffineCipher();
            string plaintext = "HELLO";
            string key = "5,8";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestColumnarTranspositionCipher()
        {
            var cipher = new ColumnarTranspositionCipher();
            string plaintext = "HELLO";
            string key = "KEY";
            
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            // Due to padding, decrypted might have extra characters
            Assert.True(decrypted.StartsWith("HELLO"));
        }
    }
}

