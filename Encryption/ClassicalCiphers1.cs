using System.Text;

namespace Encryption.Classical
{
    /// <summary>
    /// 1. 凱撒密碼 (Caesar Cipher)
    /// </summary>
    public class CaesarCipher : ICipher
    {
        public string Name => "Caesar Cipher";

        public string Encrypt(string plaintext, string key)
        {
            int shift = int.Parse(key);
            StringBuilder result = new StringBuilder();
            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    result.Append((char)((c + shift - offset) % 26 + offset));
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
            int shift = -int.Parse(key);
            return Encrypt(ciphertext, shift.ToString());
        }
    }

    /// <summary>
    /// 2. Atbash 密碼
    /// </summary>
    public class AtbashCipher : ICipher
    {
        public string Name => "Atbash Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in plaintext)
            {
                if (char.IsUpper(c))
                {
                    result.Append((char)('Z' - (c - 'A')));
                }
                else if (char.IsLower(c))
                {
                    result.Append((char)('z' - (c - 'a')));
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
            return Encrypt(ciphertext, key); // Atbash is symmetric
        }
    }

    /// <summary>
    /// 3. ROT13 密碼
    /// </summary>
    public class ROT13Cipher : ICipher
    {
        public string Name => "ROT13 Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    result.Append((char)((c - offset + 13) % 26 + offset));
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
            return Encrypt(ciphertext, key); // ROT13 is symmetric
        }
    }

    /// <summary>
    /// 4. 維吉尼亞密碼 (Vigenère Cipher)
    /// </summary>
    public class VigenereCipher : ICipher
    {
        public string Name => "Vigenère Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            key = key.ToUpper();
            int keyIndex = 0;

            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int shift = key[keyIndex % key.Length] - 'A';
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
            key = key.ToUpper();
            int keyIndex = 0;

            foreach (char c in ciphertext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int shift = key[keyIndex % key.Length] - 'A';
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
    /// 5. Playfair 密碼
    /// </summary>
    public class PlayfairCipher : ICipher
    {
        public string Name => "Playfair Cipher";

        private char[,] BuildMatrix(string key)
        {
            key = key.ToUpper().Replace("J", "I");
            HashSet<char> seen = new HashSet<char>();
            StringBuilder matrixString = new StringBuilder();

            foreach (char c in key)
            {
                if (char.IsLetter(c) && !seen.Contains(c))
                {
                    seen.Add(c);
                    matrixString.Append(c);
                }
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (c != 'J' && !seen.Contains(c))
                {
                    matrixString.Append(c);
                }
            }

            char[,] matrix = new char[5, 5];
            for (int i = 0; i < 25; i++)
            {
                matrix[i / 5, i % 5] = matrixString[i];
            }
            return matrix;
        }

        public string Encrypt(string plaintext, string key)
        {
            char[,] matrix = BuildMatrix(key);
            plaintext = plaintext.ToUpper().Replace("J", "I").Replace(" ", "");
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < plaintext.Length; i += 2)
            {
                char a = plaintext[i];
                char b = (i + 1 < plaintext.Length) ? plaintext[i + 1] : 'X';
                if (a == b) b = 'X';

                int r1 = 0, c1 = 0, r2 = 0, c2 = 0;
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (matrix[row, col] == a) { r1 = row; c1 = col; }
                        if (matrix[row, col] == b) { r2 = row; c2 = col; }
                    }
                }

                if (r1 == r2)
                {
                    result.Append(matrix[r1, (c1 + 1) % 5]);
                    result.Append(matrix[r2, (c2 + 1) % 5]);
                }
                else if (c1 == c2)
                {
                    result.Append(matrix[(r1 + 1) % 5, c1]);
                    result.Append(matrix[(r2 + 1) % 5, c2]);
                }
                else
                {
                    result.Append(matrix[r1, c2]);
                    result.Append(matrix[r2, c1]);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            char[,] matrix = BuildMatrix(key);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < ciphertext.Length; i += 2)
            {
                char a = ciphertext[i];
                char b = ciphertext[i + 1];

                int r1 = 0, c1 = 0, r2 = 0, c2 = 0;
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (matrix[row, col] == a) { r1 = row; c1 = col; }
                        if (matrix[row, col] == b) { r2 = row; c2 = col; }
                    }
                }

                if (r1 == r2)
                {
                    result.Append(matrix[r1, (c1 + 4) % 5]);
                    result.Append(matrix[r2, (c2 + 4) % 5]);
                }
                else if (c1 == c2)
                {
                    result.Append(matrix[(r1 + 4) % 5, c1]);
                    result.Append(matrix[(r2 + 4) % 5, c2]);
                }
                else
                {
                    result.Append(matrix[r1, c2]);
                    result.Append(matrix[r2, c1]);
                }
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// 6. 簡單替換密碼 (Simple Substitution Cipher)
    /// </summary>
    public class SubstitutionCipher : ICipher
    {
        public string Name => "Substitution Cipher";

        public string Encrypt(string plaintext, string key)
        {
            if (key.Length != 26) throw new ArgumentException("Key must be 26 characters");
            
            StringBuilder result = new StringBuilder();
            foreach (char c in plaintext)
            {
                if (char.IsUpper(c))
                {
                    result.Append(char.ToUpper(key[c - 'A']));
                }
                else if (char.IsLower(c))
                {
                    result.Append(char.ToLower(key[c - 'a']));
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
            if (key.Length != 26) throw new ArgumentException("Key must be 26 characters");
            
            // Build reverse key
            char[] reverseKey = new char[26];
            for (int i = 0; i < 26; i++)
            {
                reverseKey[char.ToUpper(key[i]) - 'A'] = (char)('A' + i);
            }
            
            return Encrypt(ciphertext, new string(reverseKey));
        }
    }

    /// <summary>
    /// 7. 轉置密碼 (Transposition Cipher)
    /// </summary>
    public class TranspositionCipher : ICipher
    {
        public string Name => "Transposition Cipher";

        public string Encrypt(string plaintext, string key)
        {
            int keyLength = int.Parse(key);
            StringBuilder result = new StringBuilder();
            
            for (int i = 0; i < keyLength; i++)
            {
                for (int j = i; j < plaintext.Length; j += keyLength)
                {
                    result.Append(plaintext[j]);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            int keyLength = int.Parse(key);
            int rows = (ciphertext.Length + keyLength - 1) / keyLength;
            char[] result = new char[ciphertext.Length];
            int index = 0;
            
            for (int col = 0; col < keyLength; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    int pos = row * keyLength + col;
                    if (pos < ciphertext.Length && index < ciphertext.Length)
                    {
                        result[pos] = ciphertext[index++];
                    }
                }
            }
            return new string(result);
        }
    }

    /// <summary>
    /// 8. 柵欄密碼 (Rail Fence Cipher)
    /// </summary>
    public class RailFenceCipher : ICipher
    {
        public string Name => "Rail Fence Cipher";

        public string Encrypt(string plaintext, string key)
        {
            int rails = int.Parse(key);
            if (rails <= 1) return plaintext;

            List<StringBuilder> fence = new List<StringBuilder>();
            for (int i = 0; i < rails; i++)
                fence.Add(new StringBuilder());

            int rail = 0;
            bool down = true;

            foreach (char c in plaintext)
            {
                fence[rail].Append(c);
                
                if (rail == 0)
                    down = true;
                else if (rail == rails - 1)
                    down = false;

                rail += down ? 1 : -1;
            }

            StringBuilder result = new StringBuilder();
            foreach (var sb in fence)
                result.Append(sb);

            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            int rails = int.Parse(key);
            if (rails <= 1) return ciphertext;

            int[] railLengths = new int[rails];
            int rail = 0;
            bool down = true;

            foreach (char c in ciphertext)
            {
                railLengths[rail]++;
                
                if (rail == 0)
                    down = true;
                else if (rail == rails - 1)
                    down = false;

                rail += down ? 1 : -1;
            }

            List<StringBuilder> fence = new List<StringBuilder>();
            int index = 0;
            for (int i = 0; i < rails; i++)
            {
                fence.Add(new StringBuilder(ciphertext.Substring(index, railLengths[i])));
                index += railLengths[i];
            }

            StringBuilder result = new StringBuilder();
            rail = 0;
            down = true;
            int[] positions = new int[rails];

            for (int i = 0; i < ciphertext.Length; i++)
            {
                result.Append(fence[rail][positions[rail]++]);
                
                if (rail == 0)
                    down = true;
                else if (rail == rails - 1)
                    down = false;

                rail += down ? 1 : -1;
            }

            return result.ToString();
        }
    }

    /// <summary>
    /// 9. Scytale 密碼
    /// </summary>
    public class ScytaleCipher : ICipher
    {
        public string Name => "Scytale Cipher";

        public string Encrypt(string plaintext, string key)
        {
            int diameter = int.Parse(key);
            int rows = (plaintext.Length + diameter - 1) / diameter;
            
            // Pad if necessary
            while (plaintext.Length < rows * diameter)
                plaintext += "X";

            StringBuilder result = new StringBuilder();
            for (int col = 0; col < diameter; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    result.Append(plaintext[row * diameter + col]);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            int diameter = int.Parse(key);
            int rows = ciphertext.Length / diameter;
            
            StringBuilder result = new StringBuilder();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < diameter; col++)
                {
                    result.Append(ciphertext[col * rows + row]);
                }
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// 10. Polybius Square 密碼
    /// </summary>
    public class PolybiusSquareCipher : ICipher
    {
        public string Name => "Polybius Square Cipher";

        private char[,] BuildSquare()
        {
            char[,] square = new char[5, 5];
            int index = 0;
            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (c == 'J') continue;
                square[index / 5, index % 5] = c;
                index++;
            }
            return square;
        }

        public string Encrypt(string plaintext, string key)
        {
            char[,] square = BuildSquare();
            StringBuilder result = new StringBuilder();
            
            foreach (char c in plaintext.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    char ch = c == 'J' ? 'I' : c;
                    for (int row = 0; row < 5; row++)
                    {
                        for (int col = 0; col < 5; col++)
                        {
                            if (square[row, col] == ch)
                            {
                                result.Append((row + 1).ToString());
                                result.Append((col + 1).ToString());
                                break;
                            }
                        }
                    }
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
            char[,] square = BuildSquare();
            StringBuilder result = new StringBuilder();
            
            for (int i = 0; i < ciphertext.Length; i += 2)
            {
                if (char.IsDigit(ciphertext[i]) && i + 1 < ciphertext.Length && char.IsDigit(ciphertext[i + 1]))
                {
                    int row = ciphertext[i] - '1';
                    int col = ciphertext[i + 1] - '1';
                    if (row >= 0 && row < 5 && col >= 0 && col < 5)
                    {
                        result.Append(square[row, col]);
                    }
                }
            }
            return result.ToString();
        }
    }
}

