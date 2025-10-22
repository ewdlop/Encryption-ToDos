using System.Text;

namespace Encryption.Classical
{
    /// <summary>
    /// 11. ADFGVX 密碼
    /// </summary>
    public class ADFGVXCipher : ICipher
    {
        public string Name => "ADFGVX Cipher";
        private static readonly char[] headers = { 'A', 'D', 'F', 'G', 'V', 'X' };

        public string Encrypt(string plaintext, string key)
        {
            // Simplified ADFGVX implementation
            char[,] square = BuildSquare(key);
            StringBuilder result = new StringBuilder();
            
            foreach (char c in plaintext.ToUpper())
            {
                if (char.IsLetterOrDigit(c))
                {
                    for (int row = 0; row < 6; row++)
                    {
                        for (int col = 0; col < 6; col++)
                        {
                            if (square[row, col] == c)
                            {
                                result.Append(headers[row]);
                                result.Append(headers[col]);
                            }
                        }
                    }
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            char[,] square = BuildSquare(key);
            StringBuilder result = new StringBuilder();
            
            for (int i = 0; i < ciphertext.Length; i += 2)
            {
                if (i + 1 < ciphertext.Length)
                {
                    int row = Array.IndexOf(headers, ciphertext[i]);
                    int col = Array.IndexOf(headers, ciphertext[i + 1]);
                    if (row >= 0 && col >= 0)
                    {
                        result.Append(square[row, col]);
                    }
                }
            }
            return result.ToString();
        }

        private char[,] BuildSquare(string key)
        {
            char[,] square = new char[6, 6];
            HashSet<char> seen = new HashSet<char>();
            StringBuilder matrixString = new StringBuilder();

            foreach (char c in key.ToUpper())
            {
                if (char.IsLetterOrDigit(c) && !seen.Contains(c))
                {
                    seen.Add(c);
                    matrixString.Append(c);
                }
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (!seen.Contains(c))
                {
                    matrixString.Append(c);
                }
            }

            for (char c = '0'; c <= '9'; c++)
            {
                if (!seen.Contains(c))
                {
                    matrixString.Append(c);
                }
            }

            for (int i = 0; i < Math.Min(36, matrixString.Length); i++)
            {
                square[i / 6, i % 6] = matrixString[i];
            }
            return square;
        }
    }

    /// <summary>
    /// 12. Bifid 密碼
    /// </summary>
    public class BifidCipher : ICipher
    {
        public string Name => "Bifid Cipher";

        public string Encrypt(string plaintext, string key)
        {
            char[,] square = BuildSquare(key);
            plaintext = plaintext.ToUpper().Replace("J", "I");
            List<int> rows = new List<int>();
            List<int> cols = new List<int>();

            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    for (int r = 0; r < 5; r++)
                    {
                        for (int col = 0; col < 5; col++)
                        {
                            if (square[r, col] == c)
                            {
                                rows.Add(r);
                                cols.Add(col);
                            }
                        }
                    }
                }
            }

            List<int> combined = new List<int>();
            combined.AddRange(rows);
            combined.AddRange(cols);

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < combined.Count; i += 2)
            {
                if (i + 1 < combined.Count)
                {
                    result.Append(square[combined[i], combined[i + 1]]);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            char[,] square = BuildSquare(key);
            List<int> positions = new List<int>();

            foreach (char c in ciphertext.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    for (int r = 0; r < 5; r++)
                    {
                        for (int col = 0; col < 5; col++)
                        {
                            if (square[r, col] == c)
                            {
                                positions.Add(r);
                                positions.Add(col);
                            }
                        }
                    }
                }
            }

            int half = positions.Count / 2;
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < half; i++)
            {
                result.Append(square[positions[i], positions[half + i]]);
            }
            return result.ToString();
        }

        private char[,] BuildSquare(string key)
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
    }

    /// <summary>
    /// 13. Trifid 密碼
    /// </summary>
    public class TrifidCipher : ICipher
    {
        public string Name => "Trifid Cipher";

        public string Encrypt(string plaintext, string key)
        {
            // Simplified Trifid - using 3D cube concept
            plaintext = plaintext.ToUpper();
            StringBuilder result = new StringBuilder();
            
            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    int val = c - 'A';
                    int layer = val / 9;
                    int row = (val % 9) / 3;
                    int col = val % 3;
                    result.Append((char)('A' + ((layer + 1) * 9 + (row + 1) * 3 + (col + 1)) % 26));
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
            ciphertext = ciphertext.ToUpper();
            StringBuilder result = new StringBuilder();
            
            foreach (char c in ciphertext)
            {
                if (char.IsLetter(c))
                {
                    int val = c - 'A';
                    result.Append((char)('A' + ((val + 26 - 13) % 26)));
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
    /// 14. Four-Square 密碼
    /// </summary>
    public class FourSquareCipher : ICipher
    {
        public string Name => "Four-Square Cipher";

        public string Encrypt(string plaintext, string key)
        {
            char[,] sq1 = BuildSquare(key.Length > 0 ? key.Substring(0, Math.Min(key.Length / 2, key.Length)) : "");
            char[,] sq2 = BuildSquare(key.Length > 1 ? key.Substring(key.Length / 2) : "");
            
            plaintext = plaintext.ToUpper().Replace("J", "I").Replace(" ", "");
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < plaintext.Length; i += 2)
            {
                char a = plaintext[i];
                char b = (i + 1 < plaintext.Length) ? plaintext[i + 1] : 'X';

                int r1 = 0, c1 = 0, r2 = 0, c2 = 0;
                FindPosition(sq1, a, ref r1, ref c1);
                FindPosition(sq2, b, ref r2, ref c2);

                result.Append(sq1[r1, c2]);
                result.Append(sq2[r2, c1]);
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            // Four-square decryption is similar to encryption
            return Encrypt(ciphertext, key);
        }

        private char[,] BuildSquare(string key)
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

        private void FindPosition(char[,] square, char ch, ref int row, ref int col)
        {
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    if (square[r, c] == ch)
                    {
                        row = r;
                        col = c;
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 15. Hill 密碼
    /// </summary>
    public class HillCipher : ICipher
    {
        public string Name => "Hill Cipher";

        public string Encrypt(string plaintext, string key)
        {
            // Simplified 2x2 Hill cipher
            int[,] keyMatrix = ParseKey(key);
            plaintext = plaintext.ToUpper().Replace(" ", "");
            if (plaintext.Length % 2 != 0) plaintext += "X";

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < plaintext.Length; i += 2)
            {
                int a = plaintext[i] - 'A';
                int b = plaintext[i + 1] - 'A';

                int c1 = (keyMatrix[0, 0] * a + keyMatrix[0, 1] * b) % 26;
                int c2 = (keyMatrix[1, 0] * a + keyMatrix[1, 1] * b) % 26;

                result.Append((char)('A' + c1));
                result.Append((char)('A' + c2));
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            int[,] keyMatrix = ParseKey(key);
            int[,] invMatrix = InvertMatrix(keyMatrix);
            
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < ciphertext.Length; i += 2)
            {
                int a = ciphertext[i] - 'A';
                int b = ciphertext[i + 1] - 'A';

                int p1 = (invMatrix[0, 0] * a + invMatrix[0, 1] * b) % 26;
                int p2 = (invMatrix[1, 0] * a + invMatrix[1, 1] * b) % 26;
                
                if (p1 < 0) p1 += 26;
                if (p2 < 0) p2 += 26;

                result.Append((char)('A' + p1));
                result.Append((char)('A' + p2));
            }
            return result.ToString();
        }

        private int[,] ParseKey(string key)
        {
            int[,] matrix = new int[2, 2];
            if (key.Length >= 4)
            {
                matrix[0, 0] = key[0] - 'A';
                matrix[0, 1] = key[1] - 'A';
                matrix[1, 0] = key[2] - 'A';
                matrix[1, 1] = key[3] - 'A';
            }
            else
            {
                matrix[0, 0] = 3;
                matrix[0, 1] = 3;
                matrix[1, 0] = 2;
                matrix[1, 1] = 5;
            }
            return matrix;
        }

        private int[,] InvertMatrix(int[,] matrix)
        {
            int det = (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]) % 26;
            if (det < 0) det += 26;
            
            int detInv = ModInverse(det, 26);
            
            int[,] inv = new int[2, 2];
            inv[0, 0] = (matrix[1, 1] * detInv) % 26;
            inv[0, 1] = (-matrix[0, 1] * detInv) % 26;
            inv[1, 0] = (-matrix[1, 0] * detInv) % 26;
            inv[1, 1] = (matrix[0, 0] * detInv) % 26;
            
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    if (inv[i, j] < 0) inv[i, j] += 26;
            
            return inv;
        }

        private int ModInverse(int a, int m)
        {
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return 1;
        }
    }

    /// <summary>
    /// 16. Affine 密碼
    /// </summary>
    public class AffineCipher : ICipher
    {
        public string Name => "Affine Cipher";

        public string Encrypt(string plaintext, string key)
        {
            var (a, b) = ParseKey(key);
            StringBuilder result = new StringBuilder();
            
            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int x = c - offset;
                    int encrypted = (a * x + b) % 26;
                    result.Append((char)(encrypted + offset));
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
            var (a, b) = ParseKey(key);
            int aInv = ModInverse(a, 26);
            StringBuilder result = new StringBuilder();
            
            foreach (char c in ciphertext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int y = c - offset;
                    int decrypted = (aInv * (y - b + 26)) % 26;
                    result.Append((char)(decrypted + offset));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        private (int, int) ParseKey(string key)
        {
            string[] parts = key.Split(',');
            int a = parts.Length > 0 ? int.Parse(parts[0].Trim()) : 5;
            int b = parts.Length > 1 ? int.Parse(parts[1].Trim()) : 8;
            return (a, b);
        }

        private int ModInverse(int a, int m)
        {
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return 1;
        }
    }

    /// <summary>
    /// 17. Beaufort 密碼
    /// </summary>
    public class BeaufortCipher : ICipher
    {
        public string Name => "Beaufort Cipher";

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
                    int keyChar = key[keyIndex % key.Length] - 'A';
                    int plainChar = char.ToUpper(c) - 'A';
                    int encrypted = (keyChar - plainChar + 26) % 26;
                    result.Append((char)(encrypted + offset));
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
            // Beaufort is reciprocal
            return Encrypt(ciphertext, key);
        }
    }

    /// <summary>
    /// 18. Running Key 密碼
    /// </summary>
    public class RunningKeyCipher : ICipher
    {
        public string Name => "Running Key Cipher";

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
            key = key.ToUpper();
            int keyIndex = 0;

            foreach (char c in ciphertext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int shift = key[keyIndex % key.Length] - 'A';
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
    /// 19. Autokey 密碼
    /// </summary>
    public class AutokeyCipher : ICipher
    {
        public string Name => "Autokey Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder fullKey = new StringBuilder(key.ToUpper());
            
            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    fullKey.Append(char.ToUpper(c));
                }
            }

            int keyIndex = 0;
            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int shift = fullKey[keyIndex] - 'A';
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
            StringBuilder fullKey = new StringBuilder(key.ToUpper());

            int keyIndex = 0;
            foreach (char c in ciphertext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int shift = fullKey[keyIndex] - 'A';
                    char decrypted = (char)((char.ToUpper(c) - 'A' - shift + 26) % 26 + 'A');
                    result.Append(char.IsUpper(c) ? decrypted : char.ToLower(decrypted));
                    fullKey.Append(decrypted);
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
    /// 20. Columnar Transposition 密碼
    /// </summary>
    public class ColumnarTranspositionCipher : ICipher
    {
        public string Name => "Columnar Transposition Cipher";

        public string Encrypt(string plaintext, string key)
        {
            plaintext = plaintext.Replace(" ", "");
            int[] order = GetKeyOrder(key);
            int cols = key.Length;
            int rows = (plaintext.Length + cols - 1) / cols;
            
            char[,] grid = new char[rows, cols];
            int index = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = index < plaintext.Length ? plaintext[index++] : 'X';
                }
            }

            StringBuilder result = new StringBuilder();
            foreach (int col in order)
            {
                for (int r = 0; r < rows; r++)
                {
                    result.Append(grid[r, col]);
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            int[] order = GetKeyOrder(key);
            int cols = key.Length;
            int rows = ciphertext.Length / cols;
            
            char[,] grid = new char[rows, cols];
            int index = 0;
            foreach (int col in order)
            {
                for (int r = 0; r < rows; r++)
                {
                    grid[r, col] = ciphertext[index++];
                }
            }

            StringBuilder result = new StringBuilder();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    result.Append(grid[r, c]);
                }
            }
            return result.ToString();
        }

        private int[] GetKeyOrder(string key)
        {
            var sorted = key.Select((c, i) => new { Char = c, Index = i })
                           .OrderBy(x => x.Char)
                           .Select(x => x.Index)
                           .ToArray();
            return sorted;
        }
    }
}

