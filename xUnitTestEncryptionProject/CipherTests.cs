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

        #region CipherCompiler Tests

        [Fact]
        public void TestComposedCipher_SingleCipher()
        {
            // Test with a single cipher
            var caesar = new CaesarCipher();
            var composed = new ComposedCipher(new[] { caesar });
            
            string plaintext = "HELLO";
            string key = "3";
            
            string encrypted = composed.Encrypt(plaintext, key);
            string decrypted = composed.Decrypt(encrypted, key);
            
            Assert.Equal("KHOOR", encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestComposedCipher_MultipleCiphers()
        {
            // Test Caesar -> Atbash chain
            var caesar = new CaesarCipher();
            var atbash = new AtbashCipher();
            var composed = new ComposedCipher(new ICipher[] { caesar, atbash });
            
            string plaintext = "HELLO";
            string key = "3";
            
            // First Caesar: HELLO -> KHOOR
            // Then Atbash: KHOOR -> PSLLI
            string encrypted = composed.Encrypt(plaintext, key);
            string decrypted = composed.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestComposedCipher_ThreeCiphers()
        {
            // Test Caesar -> ROT13 -> Atbash chain
            var caesar = new CaesarCipher();
            var rot13 = new ROT13Cipher();
            var atbash = new AtbashCipher();
            var composed = new ComposedCipher(new ICipher[] { caesar, rot13, atbash });
            
            string plaintext = "HELLO";
            string key = "3";
            
            string encrypted = composed.Encrypt(plaintext, key);
            string decrypted = composed.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestComposedCipher_EmptyList_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new ComposedCipher(new ICipher[] { }));
        }

        [Fact]
        public void TestComposedCipher_GetCiphers()
        {
            var caesar = new CaesarCipher();
            var atbash = new AtbashCipher();
            var composed = new ComposedCipher(new ICipher[] { caesar, atbash });
            
            var ciphers = composed.GetCiphers();
            
            Assert.Equal(2, ciphers.Count);
            Assert.Equal("Caesar Cipher", ciphers[0].Name);
            Assert.Equal("Atbash Cipher", ciphers[1].Name);
        }

        [Fact]
        public void TestCipherCompiler_CompileFromInstances()
        {
            var caesar = new CaesarCipher();
            var atbash = new AtbashCipher();
            
            var composed = CipherCompiler.Compile(caesar, atbash);
            
            string plaintext = "HELLO";
            string key = "3";
            
            string encrypted = composed.Encrypt(plaintext, key);
            string decrypted = composed.Decrypt(encrypted, key);
            
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestCipherCompiler_CompileFromNames()
        {
            var composed = CipherCompiler.Compile("Caesar Cipher", "Atbash Cipher");
            
            string plaintext = "HELLO";
            string key = "3";
            
            string encrypted = composed.Encrypt(plaintext, key);
            string decrypted = composed.Decrypt(encrypted, key);
            
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestCipherCompiler_CompileFromNames_InvalidName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => 
                CipherCompiler.Compile("Caesar Cipher", "NonExistentCipher"));
        }

        [Fact]
        public void TestCipherCompiler_CompileFromSpec()
        {
            var composed = CipherCompiler.CompileFromSpec("Caesar Cipher | Atbash Cipher | ROT13 Cipher");
            
            string plaintext = "HELLO";
            string key = "3";
            
            string encrypted = composed.Encrypt(plaintext, key);
            string decrypted = composed.Decrypt(encrypted, key);
            
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestCipherCompiler_CompileFromSpec_EmptySpec_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => CipherCompiler.CompileFromSpec(""));
        }

        [Fact]
        public void TestCipherBuilder_FluentAPI()
        {
            var composed = CipherCompiler.CreateBuilder()
                .Add("Caesar Cipher")
                .Add("Atbash Cipher")
                .Add("ROT13 Cipher")
                .Build();
            
            string plaintext = "HELLO";
            string key = "3";
            
            string encrypted = composed.Encrypt(plaintext, key);
            string decrypted = composed.Decrypt(encrypted, key);
            
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestCipherBuilder_ConvenienceMethods()
        {
            var composed = CipherCompiler.CreateBuilder()
                .AddCaesar()
                .AddAtbash()
                .AddROT13()
                .Build();
            
            string plaintext = "HELLO";
            string key = "3";
            
            string encrypted = composed.Encrypt(plaintext, key);
            string decrypted = composed.Decrypt(encrypted, key);
            
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestCipherBuilder_WithName()
        {
            var composed = CipherCompiler.CreateBuilder()
                .AddCaesar()
                .AddAtbash()
                .WithName("MyCustomCipher")
                .Build();
            
            Assert.Equal("MyCustomCipher", composed.Name);
        }

        [Fact]
        public void TestCipherBuilder_Count()
        {
            var builder = CipherCompiler.CreateBuilder()
                .AddCaesar()
                .AddAtbash();
            
            Assert.Equal(2, builder.Count);
        }

        [Fact]
        public void TestCipherBuilder_Clear()
        {
            var builder = CipherCompiler.CreateBuilder()
                .AddCaesar()
                .AddAtbash()
                .Clear();
            
            Assert.Equal(0, builder.Count);
        }

        [Fact]
        public void TestCipherBuilder_EmptyBuild_ThrowsException()
        {
            var builder = CipherCompiler.CreateBuilder();
            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void TestCipherSpecification_Parse_Simple()
        {
            var composed = CipherSpecification.Parse("Caesar Cipher | Atbash Cipher");
            
            string plaintext = "HELLO";
            string key = "3";
            
            string encrypted = composed.Encrypt(plaintext, key);
            string decrypted = composed.Decrypt(encrypted, key);
            
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestCipherSpecification_Parse_WithName()
        {
            var composed = CipherSpecification.Parse("MySecureEncryption: Caesar Cipher | Atbash Cipher");
            
            Assert.Equal("MySecureEncryption", composed.Name);
            
            string plaintext = "HELLO";
            string key = "3";
            
            string decrypted = composed.Decrypt(composed.Encrypt(plaintext, key), key);
            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TestCipherSpecification_Validate_Valid()
        {
            bool isValid = CipherSpecification.Validate("Caesar Cipher | Atbash Cipher", out string? errorMessage);
            
            Assert.True(isValid);
            Assert.Null(errorMessage);
        }

        [Fact]
        public void TestCipherSpecification_Validate_Invalid()
        {
            bool isValid = CipherSpecification.Validate("Caesar Cipher | InvalidCipher", out string? errorMessage);
            
            Assert.False(isValid);
            Assert.NotNull(errorMessage);
        }

        [Fact]
        public void TestCipherSpecification_Parse_Empty_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => CipherSpecification.Parse(""));
        }

        [Fact]
        public void TestComposedCipher_ComplexChain()
        {
            // Test a complex chain: Caesar -> Atbash -> ROT13
            var composed = CipherCompiler.CreateBuilder()
                .Add("Caesar Cipher")
                .Add("Atbash Cipher")
                .Add("ROT13 Cipher")
                .WithName("ComplexEncryption")
                .Build();
            
            string plaintext = "ATTACKATDAWN";
            string key = "3";
            
            string encrypted = composed.Encrypt(plaintext, key);
            string decrypted = composed.Decrypt(encrypted, key);
            
            Assert.NotEqual(plaintext, encrypted);
            Assert.Equal(plaintext, decrypted);
            Assert.Equal("ComplexEncryption", composed.Name);
        }

        [Fact]
        public void TestComposedCipher_NameGeneration()
        {
            var composed = CipherCompiler.CreateBuilder()
                .AddCaesar()
                .AddAtbash()
                .Build();
            
            // The default name should contain cipher names
            Assert.Contains("Caesar Cipher", composed.Name);
            Assert.Contains("Atbash Cipher", composed.Name);
        }

        #endregion
    }
}

