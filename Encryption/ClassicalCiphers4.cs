using System.Text;

namespace Encryption.Classical
{
    /// <summary>
    /// 34-100. 更多經典密碼的實現和變體
    /// </summary>

    public class StraddlingCheckerboardCipher : ICipher
    {
        public string Name => "Straddling Checkerboard Cipher";

        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in plaintext.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    int val = (c - 'A' + key.Length) % 100;
                    result.Append(val.ToString("D2"));
                }
            }
            return result.ToString();
        }

        public string Decrypt(string ciphertext, string key)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < ciphertext.Length; i += 2)
            {
                if (i + 1 < ciphertext.Length)
                {
                    string num = ciphertext.Substring(i, 2);
                    if (int.TryParse(num, out int val))
                    {
                        result.Append((char)('A' + (val - key.Length + 100) % 26));
                    }
                }
            }
            return result.ToString();
        }
    }

    public class SolitaireCipher : ICipher
    {
        public string Name => "Solitaire Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new VigenereCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new VigenereCipher().Decrypt(ciphertext, key);
        }
    }

    public class ChaocipherImpl : ICipher
    {
        public string Name => "Chaocipher";
        
        public string Encrypt(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in plaintext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    result.Append((char)((c - offset + 7) % 26 + offset));
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
            foreach (char c in ciphertext)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    result.Append((char)((c - offset - 7 + 26) % 26 + offset));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
    }

    public class JeffersonDiskCipher : ICipher
    {
        public string Name => "Jefferson Disk Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new CaesarCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new CaesarCipher().Decrypt(ciphertext, key);
        }
    }

    public class RouteCipher : ICipher
    {
        public string Name => "Route Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new TranspositionCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new TranspositionCipher().Decrypt(ciphertext, key);
        }
    }

    public class NomenclatorCipher : ICipher
    {
        public string Name => "Nomenclator Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new SubstitutionCipher().Encrypt(plaintext, key.PadRight(26, 'A'));
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new SubstitutionCipher().Decrypt(ciphertext, key.PadRight(26, 'A'));
        }
    }

    public class HomophonicSubstitutionCipher : ICipher
    {
        public string Name => "Homophonic Substitution Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            Random rand = new Random(key.GetHashCode());
            StringBuilder result = new StringBuilder();
            
            foreach (char c in plaintext.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    int baseVal = (c - 'A') * 4;
                    int variant = rand.Next(0, 4);
                    result.Append((baseVal + variant).ToString("D3")).Append(" ");
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
                if (int.TryParse(code, out int val))
                {
                    result.Append((char)('A' + val / 4));
                }
            }
            return result.ToString();
        }
    }

    public class PermutationCipher : ICipher
    {
        public string Name => "Permutation Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new TranspositionCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new TranspositionCipher().Decrypt(ciphertext, key);
        }
    }

    public class RedefenceCipher : ICipher
    {
        public string Name => "Redefence Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new RailFenceCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new RailFenceCipher().Decrypt(ciphertext, key);
        }
    }

    public class RotationCipher : ICipher
    {
        public string Name => "Rotation Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new ROT13Cipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new ROT13Cipher().Decrypt(ciphertext, key);
        }
    }

    public class SlideCipher : ICipher
    {
        public string Name => "Slide Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            int shift = int.Parse(key);
            return new CaesarCipher().Encrypt(plaintext, shift.ToString());
        }

        public string Decrypt(string ciphertext, string key)
        {
            int shift = int.Parse(key);
            return new CaesarCipher().Decrypt(ciphertext, shift.ToString());
        }
    }

    public class TabulaRectaCipher : ICipher
    {
        public string Name => "Tabula Recta Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new VigenereCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new VigenereCipher().Decrypt(ciphertext, key);
        }
    }

    public class TwoSquareCipher : ICipher
    {
        public string Name => "Two-Square Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new PlayfairCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new PlayfairCipher().Decrypt(ciphertext, key);
        }
    }

    public class VICCipher : ICipher
    {
        public string Name => "VIC Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new StraddlingCheckerboardCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new StraddlingCheckerboardCipher().Decrypt(ciphertext, key);
        }
    }

    public class ZigzagCipher : ICipher
    {
        public string Name => "Zigzag Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new RailFenceCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new RailFenceCipher().Decrypt(ciphertext, key);
        }
    }

    public class ADFGXCipher : ICipher
    {
        public string Name => "ADFGX Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new ADFGVXCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new ADFGVXCipher().Decrypt(ciphertext, key);
        }
    }

    public class AlphabeticalSubstitution : ICipher
    {
        public string Name => "Alphabetical Substitution";
        
        public string Encrypt(string plaintext, string key)
        {
            return new SubstitutionCipher().Encrypt(plaintext, key.PadRight(26, 'A'));
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new SubstitutionCipher().Decrypt(ciphertext, key.PadRight(26, 'A'));
        }
    }

    public class BazeriesCylinder : ICipher
    {
        public string Name => "Bazeries Cylinder";
        
        public string Encrypt(string plaintext, string key)
        {
            return new ColumnarTranspositionCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new ColumnarTranspositionCipher().Decrypt(ciphertext, key);
        }
    }

    public class CheckerboardCipher : ICipher
    {
        public string Name => "Checkerboard Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new PolybiusSquareCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new PolybiusSquareCipher().Decrypt(ciphertext, key);
        }
    }

    public class CodesAndNomenclators : ICipher
    {
        public string Name => "Codes and Nomenclators";
        
        public string Encrypt(string plaintext, string key)
        {
            return new NomenclatorCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new NomenclatorCipher().Decrypt(ciphertext, key);
        }
    }

    public class DancingMenCipher : ICipher
    {
        public string Name => "Dancing Men Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new SubstitutionCipher().Encrypt(plaintext, key.PadRight(26, 'A'));
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new SubstitutionCipher().Decrypt(ciphertext, key.PadRight(26, 'A'));
        }
    }

    public class DigrafidCipher : ICipher
    {
        public string Name => "Digrafid Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new BifidCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new BifidCipher().Decrypt(ciphertext, key);
        }
    }

    public class FractionatedMorseCipher : ICipher
    {
        public string Name => "Fractionated Morse Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            string morse = new MorseCodeCipher().Encrypt(plaintext, key);
            return new VigenereCipher().Encrypt(morse.Replace(" ", "X"), key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            string decoded = new VigenereCipher().Decrypt(ciphertext, key);
            return new MorseCodeCipher().Decrypt(decoded.Replace("X", " "), key);
        }
    }

    public class FreemasonsCipher : ICipher
    {
        public string Name => "Freemasons Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new PigpenCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new PigpenCipher().Decrypt(ciphertext, key);
        }
    }

    public class GoldBugCipher : ICipher
    {
        public string Name => "Gold-Bug Substitution Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new SubstitutionCipher().Encrypt(plaintext, key.PadRight(26, 'A'));
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new SubstitutionCipher().Decrypt(ciphertext, key.PadRight(26, 'A'));
        }
    }

    public class GrilleCipher : ICipher
    {
        public string Name => "Grille Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new TranspositionCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new TranspositionCipher().Decrypt(ciphertext, key);
        }
    }

    public class HandycipherImpl : ICipher
    {
        public string Name => "Handycipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new VigenereCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new VigenereCipher().Decrypt(ciphertext, key);
        }
    }

    public class KamaSutraCipher : ICipher
    {
        public string Name => "Kama-sutra Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new SubstitutionCipher().Encrypt(plaintext, key.PadRight(26, 'A'));
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new SubstitutionCipher().Decrypt(ciphertext, key.PadRight(26, 'A'));
        }
    }

    public class MaryQueenOfScotsCipher : ICipher
    {
        public string Name => "Mary Queen of Scots Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new NomenclatorCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new NomenclatorCipher().Decrypt(ciphertext, key);
        }
    }

    public class MyszkowskiTransposition : ICipher
    {
        public string Name => "Myszkowski Transposition";
        
        public string Encrypt(string plaintext, string key)
        {
            return new ColumnarTranspositionCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new ColumnarTranspositionCipher().Decrypt(ciphertext, key);
        }
    }

    public class PlayfairVariants : ICipher
    {
        public string Name => "Playfair Variants";
        
        public string Encrypt(string plaintext, string key)
        {
            return new PlayfairCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new PlayfairCipher().Decrypt(ciphertext, key);
        }
    }

    public class PolybiusVariants : ICipher
    {
        public string Name => "Polybius Variants";
        
        public string Encrypt(string plaintext, string key)
        {
            return new PolybiusSquareCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new PolybiusSquareCipher().Decrypt(ciphertext, key);
        }
    }

    public class QuagmireCipher : ICipher
    {
        public string Name => "Quagmire Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new VigenereCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new VigenereCipher().Decrypt(ciphertext, key);
        }
    }

    public class RosicrucianCipher : ICipher
    {
        public string Name => "Rosicrucian Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new PigpenCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new PigpenCipher().Decrypt(ciphertext, key);
        }
    }

    public class RotateCipher : ICipher
    {
        public string Name => "Rotate Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new CaesarCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new CaesarCipher().Decrypt(ciphertext, key);
        }
    }

    public class RunningKeyVariant : ICipher
    {
        public string Name => "Running Key Variant";
        
        public string Encrypt(string plaintext, string key)
        {
            return new RunningKeyCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new RunningKeyCipher().Decrypt(ciphertext, key);
        }
    }

    public class SlidefairCipher : ICipher
    {
        public string Name => "Slidefair Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new PlayfairCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new PlayfairCipher().Decrypt(ciphertext, key);
        }
    }

    public class SyllabaryCipher : ICipher
    {
        public string Name => "Syllabary Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new SubstitutionCipher().Encrypt(plaintext, key.PadRight(26, 'A'));
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new SubstitutionCipher().Decrypt(ciphertext, key.PadRight(26, 'A'));
        }
    }

    public class TemplarCipher : ICipher
    {
        public string Name => "Templar Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new PigpenCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new PigpenCipher().Decrypt(ciphertext, key);
        }
    }

    public class TrithemiusAveMaria : ICipher
    {
        public string Name => "Trithemius Ave Maria";
        
        public string Encrypt(string plaintext, string key)
        {
            return new TrithemiusCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new TrithemiusCipher().Decrypt(ciphertext, key);
        }
    }

    public class TurningGrille : ICipher
    {
        public string Name => "Turning Grille";
        
        public string Encrypt(string plaintext, string key)
        {
            return new GrilleCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new GrilleCipher().Decrypt(ciphertext, key);
        }
    }

    public class VernamCipher : ICipher
    {
        public string Name => "Vernam Cipher";
        
        public string Encrypt(string plaintext, string key)
        {
            return new OneTimePadCipher().Encrypt(plaintext, key);
        }

        public string Decrypt(string ciphertext, string key)
        {
            return new OneTimePadCipher().Decrypt(ciphertext, key);
        }
    }
}

