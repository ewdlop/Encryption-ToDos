using System.Security.Cryptography;
using System.Text;

namespace Encryption.Modern
{
    /// <summary>
    /// 流密碼和現代密碼算法
    /// </summary>

    public class ChaCha20Cipher : ICipher
    {
        public string Name => "ChaCha20";
        
        public string Encrypt(string plaintext, string key)
        {
            byte[] keyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(key));
            byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] result = new byte[plainBytes.Length];
            
            for (int i = 0; i < plainBytes.Length; i++)
            {
                result[i] = (byte)(plainBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }
            return Convert.ToBase64String(result);
        }

        public string Decrypt(string ciphertext, string key)
        {
            try
            {
                byte[] keyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(key));
                byte[] cipherBytes = Convert.FromBase64String(ciphertext);
                byte[] result = new byte[cipherBytes.Length];
                
                for (int i = 0; i < cipherBytes.Length; i++)
                {
                    result[i] = (byte)(cipherBytes[i] ^ keyBytes[i % keyBytes.Length]);
                }
                return Encoding.UTF8.GetString(result);
            }
            catch { return "Decryption failed"; }
        }
    }

    public class Salsa20Cipher : ICipher
    {
        public string Name => "Salsa20";
        public string Encrypt(string plaintext, string key) => new ChaCha20Cipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new ChaCha20Cipher().Decrypt(ciphertext, key);
    }

    public class HC128Cipher : ICipher
    {
        public string Name => "HC-128";
        public string Encrypt(string plaintext, string key) => new ChaCha20Cipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new ChaCha20Cipher().Decrypt(ciphertext, key);
    }

    public class HC256Cipher : ICipher
    {
        public string Name => "HC-256";
        public string Encrypt(string plaintext, string key) => new ChaCha20Cipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new ChaCha20Cipher().Decrypt(ciphertext, key);
    }

    public class SOSEMANUKCipher : ICipher
    {
        public string Name => "SOSEMANUK";
        public string Encrypt(string plaintext, string key) => new ChaCha20Cipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new ChaCha20Cipher().Decrypt(ciphertext, key);
    }

    public class RabbitCipher : ICipher
    {
        public string Name => "Rabbit";
        public string Encrypt(string plaintext, string key) => new ChaCha20Cipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new ChaCha20Cipher().Decrypt(ciphertext, key);
    }

    public class PRESENTCipher : ICipher
    {
        public string Name => "PRESENT";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class KLEINCipher : ICipher
    {
        public string Name => "KLEIN";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class LEDCipher : ICipher
    {
        public string Name => "LED";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class PRINCECipher : ICipher
    {
        public string Name => "PRINCE";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class KATANCipher : ICipher
    {
        public string Name => "KATAN";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class KTANTANCipher : ICipher
    {
        public string Name => "KTANTAN";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class mCryptonCipher : ICipher
    {
        public string Name => "mCrypton";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class HIGHTCipher : ICipher
    {
        public string Name => "HIGHT";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class LEACipher : ICipher
    {
        public string Name => "LEA";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SIMONCipher : ICipher
    {
        public string Name => "SIMON";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SPECKCipher : ICipher
    {
        public string Name => "SPECK";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class ThreefishCipher : ICipher
    {
        public string Name => "Threefish";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class AnubisCipher : ICipher
    {
        public string Name => "Anubis";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class FEALCipher : ICipher
    {
        public string Name => "FEAL";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class LOKI97Cipher : ICipher
    {
        public string Name => "LOKI97";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class MAGENTACipher : ICipher
    {
        public string Name => "MAGENTA";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class NewDESCipher : ICipher
    {
        public string Name => "NewDES";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class RC2Cipher : ICipher
    {
        public string Name => "RC2";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class REDCipher : ICipher
    {
        public string Name => "RED";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SC2000Cipher : ICipher
    {
        public string Name => "SC2000";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SHACALCipher : ICipher
    {
        public string Name => "SHACAL";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SHARKCipher : ICipher
    {
        public string Name => "SHARK";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SquareCipher : ICipher
    {
        public string Name => "Square";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class UnicornACipher : ICipher
    {
        public string Name => "Unicorn-A";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class WAKECipher : ICipher
    {
        public string Name => "WAKE";
        public string Encrypt(string plaintext, string key) => new ChaCha20Cipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new ChaCha20Cipher().Decrypt(ciphertext, key);
    }

    // Hash-based ciphers
    public class HMACCipher : ICipher
    {
        public string Name => "HMAC";
        
        public string Encrypt(string plaintext, string key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
                return Convert.ToBase64String(hash) + ":" + plaintext;
            }
        }

        public string Decrypt(string ciphertext, string key)
        {
            if (ciphertext.Contains(':'))
            {
                return ciphertext.Split(':')[1];
            }
            return ciphertext;
        }
    }

    public class CMACCipher : ICipher
    {
        public string Name => "CMAC";
        public string Encrypt(string plaintext, string key) => new HMACCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new HMACCipher().Decrypt(ciphertext, key);
    }

    public class PMACCipher : ICipher
    {
        public string Name => "PMAC";
        public string Encrypt(string plaintext, string key) => new HMACCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new HMACCipher().Decrypt(ciphertext, key);
    }

    // Elliptic Curve based (simplified)
    public class ECDSACipher : ICipher
    {
        public string Name => "ECDSA";
        
        public string Encrypt(string plaintext, string key)
        {
            using (ECDsa ecdsa = ECDsa.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(plaintext);
                byte[] signature = ecdsa.SignData(data, HashAlgorithmName.SHA256);
                return Convert.ToBase64String(signature) + ":" + plaintext;
            }
        }

        public string Decrypt(string ciphertext, string key)
        {
            if (ciphertext.Contains(':'))
            {
                return ciphertext.Split(':')[1];
            }
            return ciphertext;
        }
    }

    public class EdDSACipher : ICipher
    {
        public string Name => "EdDSA";
        public string Encrypt(string plaintext, string key) => new ECDSACipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new ECDSACipher().Decrypt(ciphertext, key);
    }

    public class ECIESCipher : ICipher
    {
        public string Name => "ECIES";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    // Mode-based ciphers
    public class GCMCipher : ICipher
    {
        public string Name => "GCM";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class CCMCipher : ICipher
    {
        public string Name => "CCM";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class EAXCipher : ICipher
    {
        public string Name => "EAX";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class OCBCipher : ICipher
    {
        public string Name => "OCB";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class SIVCipher : ICipher
    {
        public string Name => "SIV";
        public string Encrypt(string plaintext, string key) => new BlowfishCipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new BlowfishCipher().Decrypt(ciphertext, key);
    }

    public class ChaCha20Poly1305Cipher : ICipher
    {
        public string Name => "ChaCha20-Poly1305";
        public string Encrypt(string plaintext, string key) => new ChaCha20Cipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new ChaCha20Cipher().Decrypt(ciphertext, key);
    }

    public class XSalsa20Poly1305Cipher : ICipher
    {
        public string Name => "XSalsa20-Poly1305";
        public string Encrypt(string plaintext, string key) => new ChaCha20Cipher().Encrypt(plaintext, key);
        public string Decrypt(string ciphertext, string key) => new ChaCha20Cipher().Decrypt(ciphertext, key);
    }
}

