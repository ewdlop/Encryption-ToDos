namespace Encryption
{
    /// <summary>
    /// 組合密碼類 - 支持多個密碼算法的鏈式組合
    /// Composed cipher that chains multiple encryption algorithms
    /// </summary>
    public class ComposedCipher : ICipher
    {
        private readonly List<ICipher> _ciphers;
        private readonly string _name;

        public ComposedCipher(IEnumerable<ICipher> ciphers, string? name = null)
        {
            _ciphers = new List<ICipher>(ciphers);
            if (_ciphers.Count == 0)
            {
                throw new ArgumentException("At least one cipher is required", nameof(ciphers));
            }

            _name = name ?? $"Composed[{string.Join(" -> ", _ciphers.Select(c => c.Name))}]";
        }

        public string Name => _name;

        /// <summary>
        /// 加密 - 按順序應用所有密碼算法
        /// Encrypt by applying all ciphers in sequence
        /// </summary>
        public string Encrypt(string plaintext, string key)
        {
            string result = plaintext;
            foreach (var cipher in _ciphers)
            {
                result = cipher.Encrypt(result, key);
            }
            return result;
        }

        /// <summary>
        /// 解密 - 按相反順序應用所有密碼算法
        /// Decrypt by applying all ciphers in reverse order
        /// </summary>
        public string Decrypt(string ciphertext, string key)
        {
            string result = ciphertext;
            for (int i = _ciphers.Count - 1; i >= 0; i--)
            {
                result = _ciphers[i].Decrypt(result, key);
            }
            return result;
        }

        /// <summary>
        /// 獲取所有組成的密碼算法
        /// Get all constituent ciphers
        /// </summary>
        public IReadOnlyList<ICipher> GetCiphers() => _ciphers.AsReadOnly();
    }

    /// <summary>
    /// 密碼編譯器 - 提供編譯和組合密碼算法的功能
    /// Cipher Compiler - Provides cipher compilation and composition capabilities
    /// </summary>
    public class CipherCompiler
    {
        /// <summary>
        /// 編譯多個密碼算法為一個組合密碼
        /// Compile multiple ciphers into a composed cipher
        /// </summary>
        public static ComposedCipher Compile(params ICipher[] ciphers)
        {
            return new ComposedCipher(ciphers);
        }

        /// <summary>
        /// 從密碼名稱編譯組合密碼
        /// Compile a composed cipher from cipher names
        /// </summary>
        public static ComposedCipher Compile(params string[] cipherNames)
        {
            var ciphers = new List<ICipher>();
            foreach (var name in cipherNames)
            {
                var cipher = CipherFactory.GetCipher(name);
                if (cipher == null)
                {
                    throw new ArgumentException($"Cipher not found: {name}", nameof(cipherNames));
                }
                ciphers.Add(cipher);
            }
            return new ComposedCipher(ciphers);
        }

        /// <summary>
        /// 從規範字符串編譯組合密碼
        /// 格式: "Caesar Cipher | Atbash Cipher | ROT13 Cipher"
        /// Compile from specification string
        /// Format: "Caesar Cipher | Atbash Cipher | ROT13 Cipher"
        /// </summary>
        public static ComposedCipher CompileFromSpec(string specification)
        {
            if (string.IsNullOrWhiteSpace(specification))
            {
                throw new ArgumentException("Specification cannot be empty", nameof(specification));
            }

            var cipherNames = specification
                .Split('|')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();

            return Compile(cipherNames);
        }

        /// <summary>
        /// 創建密碼構建器
        /// Create a cipher builder
        /// </summary>
        public static CipherBuilder CreateBuilder()
        {
            return new CipherBuilder();
        }
    }

    /// <summary>
    /// 密碼構建器 - 使用流暢API構建組合密碼
    /// Cipher Builder - Build composed ciphers using fluent API
    /// </summary>
    public class CipherBuilder
    {
        private readonly List<ICipher> _ciphers = new List<ICipher>();
        private string? _name;

        /// <summary>
        /// 添加密碼算法
        /// Add a cipher
        /// </summary>
        public CipherBuilder Add(ICipher cipher)
        {
            _ciphers.Add(cipher);
            return this;
        }

        /// <summary>
        /// 通過名稱添加密碼算法
        /// Add a cipher by name
        /// </summary>
        public CipherBuilder Add(string cipherName)
        {
            var cipher = CipherFactory.GetCipher(cipherName);
            if (cipher == null)
            {
                throw new ArgumentException($"Cipher not found: {cipherName}", nameof(cipherName));
            }
            return Add(cipher);
        }

        /// <summary>
        /// 設置組合密碼的名稱
        /// Set the name of the composed cipher
        /// </summary>
        public CipherBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// 添加凱撒密碼
        /// Add Caesar cipher
        /// </summary>
        public CipherBuilder AddCaesar()
        {
            return Add("Caesar Cipher");
        }

        /// <summary>
        /// 添加維吉尼亞密碼
        /// Add Vigenère cipher
        /// </summary>
        public CipherBuilder AddVigenere()
        {
            return Add("Vigenère Cipher");
        }

        /// <summary>
        /// 添加Atbash密碼
        /// Add Atbash cipher
        /// </summary>
        public CipherBuilder AddAtbash()
        {
            return Add("Atbash Cipher");
        }

        /// <summary>
        /// 添加ROT13密碼
        /// Add ROT13 cipher
        /// </summary>
        public CipherBuilder AddROT13()
        {
            return Add("ROT13 Cipher");
        }

        /// <summary>
        /// 添加Playfair密碼
        /// Add Playfair cipher
        /// </summary>
        public CipherBuilder AddPlayfair()
        {
            return Add("Playfair Cipher");
        }

        /// <summary>
        /// 添加Rail Fence密碼
        /// Add Rail Fence cipher
        /// </summary>
        public CipherBuilder AddRailFence()
        {
            return Add("Rail Fence Cipher");
        }

        /// <summary>
        /// 構建組合密碼
        /// Build the composed cipher
        /// </summary>
        public ComposedCipher Build()
        {
            if (_ciphers.Count == 0)
            {
                throw new InvalidOperationException("At least one cipher must be added before building");
            }

            return new ComposedCipher(_ciphers, _name);
        }

        /// <summary>
        /// 獲取當前添加的密碼數量
        /// Get the count of added ciphers
        /// </summary>
        public int Count => _ciphers.Count;

        /// <summary>
        /// 清除所有已添加的密碼
        /// Clear all added ciphers
        /// </summary>
        public CipherBuilder Clear()
        {
            _ciphers.Clear();
            _name = null;
            return this;
        }
    }

    /// <summary>
    /// 密碼規範解析器 - 解析和編譯密碼規範
    /// Cipher Specification Parser - Parse and compile cipher specifications
    /// </summary>
    public class CipherSpecification
    {
        /// <summary>
        /// 解析規範並返回組合密碼
        /// Parse specification and return composed cipher
        /// 
        /// 支持的格式:
        /// - Simple: "Caesar | Vigenère | Atbash"
        /// - Named: "MyCustomCipher: Caesar | Vigenère"
        /// </summary>
        public static ComposedCipher Parse(string specification)
        {
            if (string.IsNullOrWhiteSpace(specification))
            {
                throw new ArgumentException("Specification cannot be empty", nameof(specification));
            }

            // Check for named specification
            string? customName = null;
            string cipherSpec = specification;

            var colonIndex = specification.IndexOf(':');
            if (colonIndex > 0)
            {
                customName = specification.Substring(0, colonIndex).Trim();
                cipherSpec = specification.Substring(colonIndex + 1).Trim();
            }

            var builder = CipherCompiler.CreateBuilder();

            if (!string.IsNullOrEmpty(customName))
            {
                builder.WithName(customName);
            }

            var cipherNames = cipherSpec
                .Split('|')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s));

            foreach (var cipherName in cipherNames)
            {
                builder.Add(cipherName);
            }

            return builder.Build();
        }

        /// <summary>
        /// 驗證規範是否有效
        /// Validate if specification is valid
        /// </summary>
        public static bool Validate(string specification, out string? errorMessage)
        {
            try
            {
                Parse(specification);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
