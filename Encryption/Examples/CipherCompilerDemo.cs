using System;
using Encryption;
using Encryption.Classical;

namespace EncryptionDemo
{
    /// <summary>
    /// Encryption Compiler 示例程序
    /// Demonstrates the Encryption Compiler functionality
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Encryption Compiler Demo ===\n");

            // 示例 1: 基本組合
            Demo1_BasicComposition();

            // 示例 2: 流暢 API
            Demo2_FluentAPI();

            // 示例 3: 從規範編譯
            Demo3_CompileFromSpec();

            // 示例 4: 複雜加密鏈
            Demo4_ComplexChain();

            // 示例 5: 檢查組合密碼
            Demo5_InspectComposedCipher();

            Console.WriteLine("\n=== Demo Complete ===");
        }

        static void Demo1_BasicComposition()
        {
            Console.WriteLine("--- Demo 1: Basic Composition ---");

            // 創建組合密碼: Caesar -> Atbash
            var caesar = new CaesarCipher();
            var atbash = new AtbashCipher();
            var composed = new ComposedCipher(new ICipher[] { caesar, atbash });

            string plaintext = "HELLO";
            string key = "3";

            string encrypted = composed.Encrypt(plaintext, key);
            string decrypted = composed.Decrypt(encrypted, key);

            Console.WriteLine($"Plaintext:  {plaintext}");
            Console.WriteLine($"Encrypted:  {encrypted}");
            Console.WriteLine($"Decrypted:  {decrypted}");
            Console.WriteLine($"Cipher:     {composed.Name}");
            Console.WriteLine();
        }

        static void Demo2_FluentAPI()
        {
            Console.WriteLine("--- Demo 2: Fluent API ---");

            // 使用構建器創建組合密碼
            var cipher = CipherCompiler.CreateBuilder()
                .AddCaesar()
                .AddAtbash()
                .AddROT13()
                .WithName("TripleEncryption")
                .Build();

            string plaintext = "ATTACKATDAWN";
            string key = "5";

            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);

            Console.WriteLine($"Plaintext:  {plaintext}");
            Console.WriteLine($"Encrypted:  {encrypted}");
            Console.WriteLine($"Decrypted:  {decrypted}");
            Console.WriteLine($"Cipher:     {cipher.Name}");
            Console.WriteLine();
        }

        static void Demo3_CompileFromSpec()
        {
            Console.WriteLine("--- Demo 3: Compile from Specification ---");

            // 從規範字符串創建
            var cipher = CipherCompiler.CompileFromSpec("Caesar Cipher | Atbash Cipher | ROT13 Cipher");

            string plaintext = "SECRETMESSAGE";
            string key = "7";

            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);

            Console.WriteLine($"Specification: Caesar Cipher | Atbash Cipher | ROT13 Cipher");
            Console.WriteLine($"Plaintext:     {plaintext}");
            Console.WriteLine($"Encrypted:     {encrypted}");
            Console.WriteLine($"Decrypted:     {decrypted}");
            Console.WriteLine();
        }

        static void Demo4_ComplexChain()
        {
            Console.WriteLine("--- Demo 4: Complex Encryption Chain ---");

            // 創建複雜的加密鏈
            var cipher = CipherSpecification.Parse(
                "UltraSecure: Caesar Cipher | Atbash Cipher | ROT13 Cipher | Transposition Cipher"
            );

            string plaintext = "TOPSECRET";
            string key = "3";

            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);

            Console.WriteLine($"Cipher Name:   {cipher.Name}");
            Console.WriteLine($"Plaintext:     {plaintext}");
            Console.WriteLine($"Encrypted:     {encrypted}");
            Console.WriteLine($"Decrypted:     {decrypted}");
            Console.WriteLine();
        }

        static void Demo5_InspectComposedCipher()
        {
            Console.WriteLine("--- Demo 5: Inspect Composed Cipher ---");

            var cipher = CipherCompiler.CreateBuilder()
                .Add("Caesar Cipher")
                .Add("Atbash Cipher")
                .Add("ROT13 Cipher")
                .Add("Rail Fence Cipher")
                .WithName("QuadrupleEncryption")
                .Build();

            Console.WriteLine($"Cipher Name:          {cipher.Name}");
            Console.WriteLine($"Number of Ciphers:    {cipher.GetCiphers().Count}");
            Console.WriteLine($"Cipher Components:");

            foreach (var component in cipher.GetCiphers())
            {
                Console.WriteLine($"  - {component.Name}");
            }

            // 演示加密
            string plaintext = "DEMO";
            string key = "3";
            string encrypted = cipher.Encrypt(plaintext, key);
            string decrypted = cipher.Decrypt(encrypted, key);

            Console.WriteLine($"\nEncryption Demo:");
            Console.WriteLine($"  Plaintext:  {plaintext}");
            Console.WriteLine($"  Encrypted:  {encrypted}");
            Console.WriteLine($"  Decrypted:  {decrypted}");
            Console.WriteLine();
        }
    }
}
