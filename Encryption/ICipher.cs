namespace Encryption
{
    /// <summary>
    /// 密碼算法基礎接口
    /// </summary>
    public interface ICipher
    {
        /// <summary>
        /// 密碼算法名稱
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 加密
        /// </summary>
        string Encrypt(string plaintext, string key);

        /// <summary>
        /// 解密
        /// </summary>
        string Decrypt(string ciphertext, string key);
    }

    /// <summary>
    /// 二進制密碼算法接口
    /// </summary>
    public interface IBinaryCipher
    {
        /// <summary>
        /// 密碼算法名稱
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 加密
        /// </summary>
        byte[] Encrypt(byte[] plaintext, byte[] key);

        /// <summary>
        /// 解密
        /// </summary>
        byte[] Decrypt(byte[] ciphertext, byte[] key);
    }
}

