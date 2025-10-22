using System.Text;

namespace Encryption.Classical
{
    /// <summary>
    /// 21. Double Transposition 密碼
    /// </summary>
    public class DoubleTranspositionCipher : ICipher
    {
        public string Name => "Double Transposition Cipher";

        public string Encrypt(string plaintext, string key)
        {
            var ct = new ColumnarTranspositionCipher();
            string first = ct.Encrypt(plaintext, key);
            return ct.Encrypt(first, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            var ct = new ColumnarTranspositionCipher();
            string first = ct.Decrypt(ciphertext, key);
            return ct.Decrypt(first, key);
        }
    }

    /// <summary>
    /// 22. Gronsfeld 密碼
    /// </summary>
    public class GronsfeldCipher : ICipher
    {
        public string Name => "Gronsfeld Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;

            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int shift = key[keyIndex % key.Length] - '0';
                    result.Append((char)((c - offset + shift) % 26 + offset));
                    keyIndex++;
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;

            foreach (char c in ciphertext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int shift = key[keyIndex % key.Length] - '0';
                    result.Append((char)((c - offset - shift + 26) % 26 + offset));
                    keyIndex++;
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// 23. Porta 密碼
    /// </summary>
    public class PortaCipher : ICipher
    {
        public string Name => "Porta Cipher";
        
        private static readonly string[] portaTable = new string[]
        {
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "NOPQRSTUVWXYZABCDEFGHIJKLM",
            "NOPQRSTUVWXYZABCDEFGHIJKLM",
            "NOPQRSTUVWXYZABCDEFGHIJKLM",
            "NOPQRSTUVWXYZABCDEFGHIJKLM",
            "NOPQRSTUVWXYZABCDEFGHIJKLM",
            "NOPQRSTUVWXYZABCDEFGHIJKLM",
            "NOPQRSTUVWXYZABCDEFGHIJKLM",
            "NOPQRSTUVWXYZABCDEFGHIJKLM",
            "NOPQRSTUVWXYZABCDEFGHIJKLM",
            "NOPQRSTUVWXYZABCDEFGHIJKLM",
            "NOPQRSTUVWXYZABCDEFGHIJKLM"
        };

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            key = key.ToUpper();
            int keyIndex = 0;

            foreach (char c in plaintext.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    int keyRow = (key[keyIndex % key.Length] - 'A') / 2;
                    int shift = (portaTable[keyRow][c - 'A'] - 'A');
                    result.Append((char)('A' + shift));
                    keyIndex++;
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            return Encrypt(ciphertext, key); // Porta is reciprocal
        }
    }

    /// <summary>
    /// 24. Alberti 密碼
    /// </summary>
    public class AlbertiCipher : ICipher
    {
        public string Name => "Alberti Cipher";

        public string Encrypt(string plaintext, string key)
        {
            // Simplified Alberti cipher implementation
            int rotation = key.Length > 0 ? key[0] - 'A' : 0;
            StringBuilder result = new StringBuilder();

            foreach (char c in plaintext.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    result.Append((char)((c - 'A' + rotation) % 26 + 'A'));
                    rotation = (rotation + 1) % 26;
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            int rotation = key.Length > 0 ? key[0] - 'A' : 0;
            StringBuilder result = new StringBuilder();

            foreach (char c in ciphertext.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    result.Append((char)((c - 'A' - rotation + 26) % 26 + 'A'));
                    rotation = (rotation + 1) % 26;
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// 25. Trithemius 密碼
    /// </summary>
    public class TrithemiusCipher : ICipher
    {
        public string Name => "Trithemius Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            int shift = 0;

            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    result.Append((char)((c - offset + shift) % 26 + offset));
                    shift++;
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            StringBuilder result = new StringBuilder();
            int shift = 0;

            foreach (char c in ciphertext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    result.Append((char)((c - offset - shift + 26 * 100) % 26 + offset));
                    shift++;
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// 26. Pigpen 密碼
    /// </summary>
    public class PigpenCipher : ICipher
    {
        public string Name => "Pigpen Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in plaintext.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    int index = c - 'A';
                    result.Append($"[{index}]");
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < ciphertext.Length; i++)
            {
                if (ciphertext[i] == '[')
                {
                    int end = ciphertext.IndexOf(']', i);
                    if (end > i)
                    {
                        string num = ciphertext.Substring(i + 1, end - i - 1);
                        if (int.TryParse(num, out int index) && index >= 0 && index < 26)
                        {
                            result.Append((char)('A' + index));
                        }
                        i = end;
                    }
                }
                else if (ciphertext[i] != ']')
                {
                    result.Append(ciphertext[i]);
                }
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// 27. Morse Code 密碼
    /// </summary>
    public class MorseCodeCipher : ICipher
    {
        public string Name => "Morse Code Cipher";

        private static readonly Dictionary<char, string> morseCode = new Dictionary<char, string>
        {
            {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."},
            {'F', "..-."}, {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"},
            {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"},
            {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
            {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"}, {'Y', "-.--"},
            {'Z', "--.."}, {'0', "-----"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"},
            {'4', "....-"}, {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."},
            {'9', "----."}, {' ', "/"}
        };

        private static readonly Dictionary<string, char> reverseMorse = morseCode.ToDictionary(x => x.Value, x => x.Key);

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in plaintext.ToUpper())
            {
                if (morseCode.ContainsKey(c))
                {
                    result.Append(morseCode[c]);
                    result.Append(" ");
                }
            }
            return result.ToString().Trim();
        }

        public string Decrypt(string ciphertext, string key)
        {
            StringBuilder result = new StringBuilder();
            string[] codes = ciphertext.Split(' ');
            
            foreach (string code in codes)
            {
                if (reverseMorse.ContainsKey(code))
                {
                    result.Append(reverseMorse[code]);
                }
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// 28. Bacon's 密碼
    /// </summary>
    public class BaconCipher : ICipher
    {
        public string Name => "Bacon's Cipher";

        private static readonly Dictionary<char, string> baconCode = new Dictionary<char, string>
        {
            {'A', "AAAAA"}, {'B', "AAAAB"}, {'C', "AAABA"}, {'D', "AAABB"},
            {'E', "AABAA"}, {'F', "AABAB"}, {'G', "AABBA"}, {'H', "AABBB"},
            {'I', "ABAAA"}, {'J', "ABAAB"}, {'K', "ABABA"}, {'L', "ABABB"},
            {'M', "ABBAA"}, {'N', "ABBAB"}, {'O', "ABBBA"}, {'P', "ABBBB"},
            {'Q', "BAAAA"}, {'R', "BAAAB"}, {'S', "BAABA"}, {'T', "BAABB"},
            {'U', "BABAA"}, {'V', "BABAB"}, {'W', "BABBA"}, {'X', "BABBB"},
            {'Y', "BBAAA"}, {'Z', "BBAAB"}
        };

        private static readonly Dictionary<string, char> reverseBacon = baconCode.ToDictionary(x => x.Value, x => x.Key);

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in plaintext.ToUpper())
            {
                if (baconCode.ContainsKey(c))
                {
                    result.Append(baconCode[c]);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < ciphertext.Length; i += 5)
            {
                if (i + 5 <= ciphertext.Length)
                {
                    string code = ciphertext.Substring(i, 5);
                    if (reverseBacon.ContainsKey(code))
                    {
                        result.Append(reverseBacon[code]);
                    }
                }
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// 29. Book 密碼
    /// </summary>
    public class BookCipher : ICipher
    {
        public string Name => "Book Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            Random rand = new Random(key.GetHashCode());
            
            foreach (char c in plaintext.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    int page = rand.Next(1, 100);
                    int line = rand.Next(1, 50);
                    int word = rand.Next(1, 20);
                    result.Append($"{page}-{line}-{word} ");
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString().Trim();
        }

        public string Decrypt(string ciphertext, string key)
        {
            // Simplified - would need actual book reference
            StringBuilder result = new StringBuilder();
            Random rand = new Random(key.GetHashCode());
            string[] tokens = ciphertext.Split(' ');
            
            foreach (string token in tokens)
            {
                if (token.Contains('-'))
                {
                    result.Append((char)('A' + rand.Next(0, 26)));
                }
                else
                {
                    result.Append(token);
                }
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// 30. Tap Code 密碼
    /// </summary>
    public class TapCodeCipher : ICipher
    {
        public string Name => "Tap Code Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in plaintext.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    char ch = c == 'K' ? 'C' : c;
                    int index = ch - 'A';
                    if (ch > 'K') index--;
                    
                    int row = index / 5 + 1;
                    int col = index % 5 + 1;
                    result.Append($"{row}{col} ");
                }
            }
            return result.ToString().Trim();
        }

        public string Decrypt(string ciphertext, string key)
        {
            StringBuilder result = new StringBuilder();
            string[] codes = ciphertext.Split(' ');
            
            foreach (string code in codes)
            {
                if (code.Length == 2 && char.IsDigit(code[0]) && char.IsDigit(code[1]))
                {
                    int row = code[0] - '1';
                    int col = code[1] - '1';
                    int index = row * 5 + col;
                    char ch = (char)('A' + index);
                    if (ch >= 'K') ch++;
                    result.Append(ch);
                }
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// 31. One-Time Pad 密碼
    /// </summary>
    public class OneTimePadCipher : ICipher
    {
        public string Name => "One-Time Pad Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;

            foreach (char c in plaintext)
            {
                if (char.IsLetter(c) && keyIndex < key.Length)
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int shift = char.ToUpper(key[keyIndex]) - 'A';
                    result.Append((char)((char.ToUpper(c) - 'A' + shift) % 26 + offset));
                    keyIndex++;
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;

            foreach (char c in ciphertext)
            {
                if (char.IsLetter(c) && keyIndex < key.Length)
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int shift = char.ToUpper(key[keyIndex]) - 'A';
                    result.Append((char)((char.ToUpper(c) - 'A' - shift + 26) % 26 + offset));
                    keyIndex++;
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// 32. Nihilist 密碼
    /// </summary>
    public class NihilistCipher : ICipher
    {
        public string Name => "Nihilist Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            plaintext = plaintext.ToUpper().Replace("J", "I");
            key = key.ToUpper().Replace("J", "I");
            int keyIndex = 0;

            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    int pVal = GetPolybiusValue(c);
                    int kVal = GetPolybiusValue(key[keyIndex % key.Length]);
                    result.Append((pVal + kVal).ToString()).Append(" ");
                    keyIndex++;
                }
            }
            return result.ToString().Trim();
        }

        public string Decrypt(string ciphertext, string key)
        {
            StringBuilder result = new StringBuilder();
            key = key.ToUpper().Replace("J", "I");
            string[] numbers = ciphertext.Split(' ');
            int keyIndex = 0;

            foreach (string num in numbers)
            {
                if (int.TryParse(num, out int val))
                {
                    int kVal = GetPolybiusValue(key[keyIndex % key.Length]);
                    int pVal = val - kVal;
                    result.Append(GetCharFromPolybiusValue(pVal));
                    keyIndex++;
                }
            }
            return result.ToString();
        }

        private int GetPolybiusValue(char c)
        {
            int index = c - 'A';
            if (c > 'J') index--;
            int row = index / 5 + 1;
            int col = index % 5 + 1;
            return row * 10 + col;
        }

        private char GetCharFromPolybiusValue(int val)
        {
            int row = val / 10 - 1;
            int col = val % 10 - 1;
            int index = row * 5 + col;
            char ch = (char)('A' + index);
            if (ch >= 'J') ch++;
            return ch;
        }
    }

    /// <summary>
    /// 33. Keyword 密碼
    /// </summary>
    public class KeywordCipher : ICipher
    {
        public string Name => "Keyword Cipher";

        public string Encrypt(string plaintext, string key)
        {
            string alphabet = BuildKeywordAlphabet(key);
            StringBuilder result = new StringBuilder();

            foreach (char c in plaintext)
            {
                if (char.IsUpper(c))
                {
                    result.Append(alphabet[c - 'A']);
                }
                else if (char.IsLower(c))
                {
                    result.Append(char.ToLower(alphabet[c - 'a']));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            string alphabet = BuildKeywordAlphabet(key);
            char[] reverseAlphabet = new char[26];
            
            for (int i = 0; i < 26; i++)
            {
                reverseAlphabet[alphabet[i] - 'A'] = (char)('A' + i);
            }

            StringBuilder result = new StringBuilder();
            foreach (char c in ciphertext)
            {
                if (char.IsUpper(c))
                {
                    result.Append(reverseAlphabet[c - 'A']);
                }
                else if (char.IsLower(c))
                {
                    result.Append(char.ToLower(reverseAlphabet[char.ToUpper(c) - 'A']));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        private string BuildKeywordAlphabet(string keyword)
        {
            HashSet<char> seen = new HashSet<char>();
            StringBuilder alphabet = new StringBuilder();

            foreach (char c in keyword.ToUpper())
            {
                if (char.IsLetter(c) && !seen.Contains(c))
                {
                    seen.Add(c);
                    alphabet.Append(c);
                }
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (!seen.Contains(c))
                {
                    alphabet.Append(c);
                }
            }

            return alphabet.ToString();
        }
    }
}

