using System.Security.Cryptography;
using System.Text;

namespace Encryption.Modern
{
    /// <summary>
    /// 1. AES (Advanced Encryption Standard)
    /// </summary>
    public class AESCipher : IBinaryCipher
    {
        public string Name => "AES";

        public byte[] Encrypt(byte[] plaintext, byte[] key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = AdjustKeySize(key, 32);
                aes.GenerateIV();
                
                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] encrypted = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
                    byte[] result = new byte[aes.IV.Length + encrypted.Length];
                    Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
                    Buffer.BlockCopy(encrypted, 0, result, aes.IV.Length, encrypted.Length);
                    return result;
                }
            }
        }

        public byte[] Decrypt(byte[] ciphertext, byte[] key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = AdjustKeySize(key, 32);
                
                byte[] iv = new byte[16];
                byte[] data = new byte[ciphertext.Length - 16];
                Buffer.BlockCopy(ciphertext, 0, iv, 0, 16);
                Buffer.BlockCopy(ciphertext, 16, data, 0, data.Length);
                aes.IV = iv;
                
                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    return decryptor.TransformFinalBlock(data, 0, data.Length);
                }
            }
        }

        private byte[] AdjustKeySize(byte[] key, int size)
        {
            byte[] adjustedKey = new byte[size];
            Array.Copy(key, adjustedKey, Math.Min(key.Length, size));
            return adjustedKey;
        }
    }

    /// <summary>
    /// 2. DES (Data Encryption Standard)
    /// </summary>
    public class DESCipher : IBinaryCipher
    {
        public string Name => "DES";

        public byte[] Encrypt(byte[] plaintext, byte[] key)
        {
            using (DES des = DES.Create())
            {
                des.Key = AdjustKeySize(key, 8);
                des.GenerateIV();
                
                using (ICryptoTransform encryptor = des.CreateEncryptor(des.Key, des.IV))
                {
                    byte[] encrypted = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
                    byte[] result = new byte[des.IV.Length + encrypted.Length];
                    Buffer.BlockCopy(des.IV, 0, result, 0, des.IV.Length);
                    Buffer.BlockCopy(encrypted, 0, result, des.IV.Length, encrypted.Length);
                    return result;
                }
            }
        }

        public byte[] Decrypt(byte[] ciphertext, byte[] key)
        {
            using (DES des = DES.Create())
            {
                des.Key = AdjustKeySize(key, 8);
                
                byte[] iv = new byte[8];
                byte[] data = new byte[ciphertext.Length - 8];
                Buffer.BlockCopy(ciphertext, 0, iv, 0, 8);
                Buffer.BlockCopy(ciphertext, 8, data, 0, data.Length);
                des.IV = iv;
                
                using (ICryptoTransform decryptor = des.CreateDecryptor(des.Key, des.IV))
                {
                    return decryptor.TransformFinalBlock(data, 0, data.Length);
                }
            }
        }

        private byte[] AdjustKeySize(byte[] key, int size)
        {
            byte[] adjustedKey = new byte[size];
            Array.Copy(key, adjustedKey, Math.Min(key.Length, size));
            return adjustedKey;
        }
    }

    /// <summary>
    /// 3. Triple DES
    /// </summary>
    public class TripleDESCipher : IBinaryCipher
    {
        public string Name => "3DES";

        public byte[] Encrypt(byte[] plaintext, byte[] key)
        {
            using (TripleDES des3 = TripleDES.Create())
            {
                des3.Key = AdjustKeySize(key, 24);
                des3.GenerateIV();
                
                using (ICryptoTransform encryptor = des3.CreateEncryptor(des3.Key, des3.IV))
                {
                    byte[] encrypted = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
                    byte[] result = new byte[des3.IV.Length + encrypted.Length];
                    Buffer.BlockCopy(des3.IV, 0, result, 0, des3.IV.Length);
                    Buffer.BlockCopy(encrypted, 0, result, des3.IV.Length, encrypted.Length);
                    return result;
                }
            }
        }

        public byte[] Decrypt(byte[] ciphertext, byte[] key)
        {
            using (TripleDES des3 = TripleDES.Create())
            {
                des3.Key = AdjustKeySize(key, 24);
                
                byte[] iv = new byte[8];
                byte[] data = new byte[ciphertext.Length - 8];
                Buffer.BlockCopy(ciphertext, 0, iv, 0, 8);
                Buffer.BlockCopy(ciphertext, 8, data, 0, data.Length);
                des3.IV = iv;
                
                using (ICryptoTransform decryptor = des3.CreateDecryptor(des3.Key, des3.IV))
                {
                    return decryptor.TransformFinalBlock(data, 0, data.Length);
                }
            }
        }

        private byte[] AdjustKeySize(byte[] key, int size)
        {
            byte[] adjustedKey = new byte[size];
            Array.Copy(key, adjustedKey, Math.Min(key.Length, size));
            return adjustedKey;
        }
    }

    /// <summary>
    /// 4-50: 基於AES的變體和簡化實現
    /// </summary>
    
    /// <summary>
    /// AES 密碼 (ICipher 接口版本)
    /// </summary>
    public class AESTextCipher : ICipher
    {
        public string Name => "AES";

        public string Encrypt(string plaintext, string key)
        {
            var aesCipher = new AESCipher();
            byte[] data = Encoding.UTF8.GetBytes(plaintext);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] encrypted = aesCipher.Encrypt(data, keyBytes);
            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string ciphertext, string key)
        {
            try
            {
                var aesCipher = new AESCipher();
                byte[] data = Convert.FromBase64String(ciphertext);
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] decrypted = aesCipher.Decrypt(data, keyBytes);
                return Encoding.UTF8.GetString(decrypted);
            }
            catch { return "Decryption failed"; }
        }
    }

    public class RSACipher : ICipher
    {
        public string Name => "RSA";

        public string Encrypt(string plaintext, string key)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.KeySize = 2048;
                byte[] data = Encoding.UTF8.GetBytes(plaintext);
                byte[] encrypted = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
                return Convert.ToBase64String(encrypted);
            }
        }

        public string Decrypt(string ciphertext, string key)
        {
            try
            {
                using (RSA rsa = RSA.Create())
                {
                    rsa.KeySize = 2048;
                    byte[] data = Convert.FromBase64String(ciphertext);
                    byte[] decrypted = rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
                    return Encoding.UTF8.GetString(decrypted);
                }
            }
            catch
            {
                return "Decryption failed - RSA key mismatch";
            }
        }
    }

    public class BlowfishCipher : ICipher
    {
        public string Name => "Blowfish";
        // Simplified implementation using AES as placeholder
        public string Encrypt(string plaintext, string key)
        {
            var aes = new AESCipher();
            byte[] data = Encoding.UTF8.GetBytes(plaintext);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] encrypted = aes.Encrypt(data, keyBytes);
            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string ciphertext, string key)
        {
            try
            {
                var aes = new AESCipher();
                byte[] data = Convert.FromBase64String(ciphertext);
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] decrypted = aes.Decrypt(data, keyBytes);
                return Encoding.UTF8.GetString(decrypted);
            }
            catch { return "Decryption failed"; }
        }
    }

    public class TwofishCipher : ICipher
    {
        public string Name => "Twofish";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class RC4Cipher : ICipher
    {
        public string Name => "RC4";
        public string Encrypt(string plaintext, string key)
        {
            byte[] s = InitializeRC4(Encoding.UTF8.GetBytes(key));
            byte[] data = Encoding.UTF8.GetBytes(plaintext);
            byte[] result = new byte[data.Length];
            
            int i = 0, j = 0;
            for (int k = 0; k < data.Length; k++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;
                Swap(s, i, j);
                result[k] = (byte)(data[k] ^ s[(s[i] + s[j]) % 256]);
            }
            return Convert.ToBase64String(result);
        }

        public string Decrypt(string ciphertext, string key)
        {
            try
            {
                byte[] s = InitializeRC4(Encoding.UTF8.GetBytes(key));
                byte[] data = Convert.FromBase64String(ciphertext);
                byte[] result = new byte[data.Length];
                
                int i = 0, j = 0;
                for (int k = 0; k < data.Length; k++)
                {
                    i = (i + 1) % 256;
                    j = (j + s[i]) % 256;
                    Swap(s, i, j);
                    result[k] = (byte)(data[k] ^ s[(s[i] + s[j]) % 256]);
                }
                return Encoding.UTF8.GetString(result);
            }
            catch
            {
                return "Decryption failed";
            }
        }

        private byte[] InitializeRC4(byte[] key)
        {
            byte[] s = new byte[256];
            for (int i = 0; i < 256; i++) s[i] = (byte)i;
            
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + s[i] + key[i % key.Length]) % 256;
                Swap(s, i, j);
            }
            return s;
        }

        private void Swap(byte[] s, int i, int j)
        {
            byte temp = s[i];
            s[i] = s[j];
            s[j] = temp;
        }
    }

    public class RC5Cipher : ICipher
    {
        public string Name => "RC5";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class RC6Cipher : ICipher
    {
        public string Name => "RC6";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class IDEACipher : ICipher
    {
        public string Name => "IDEA";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SerpentCipher : ICipher
    {
        public string Name => "Serpent";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class CamelliaCipher : ICipher
    {
        public string Name => "Camellia";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class CAST128Cipher : ICipher
    {
        public string Name => "CAST-128";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class CAST256Cipher : ICipher
    {
        public string Name => "CAST-256";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class MARSCipher : ICipher
    {
        public string Name => "MARS";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class GOSTCipher : ICipher
    {
        public string Name => "GOST";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SkipjackCipher : ICipher
    {
        public string Name => "Skipjack";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class TEACipher : ICipher
    {
        public string Name => "TEA";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class XTEACipher : ICipher
    {
        public string Name => "XTEA";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SAFERCipher : ICipher
    {
        public string Name => "SAFER";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class KASUMICipher : ICipher
    {
        public string Name => "KASUMI";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class MISTY1Cipher : ICipher
    {
        public string Name => "MISTY1";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SEEDCipher : ICipher
    {
        public string Name => "SEED";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class ARIACipher : ICipher
    {
        public string Name => "ARIA";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class CLEFIACipher : ICipher
    {
        public string Name => "CLEFIA";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SM4Cipher : ICipher
    {
        public string Name => "SM4";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }
}

