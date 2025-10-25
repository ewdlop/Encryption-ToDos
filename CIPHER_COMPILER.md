# Encryption Compiler

## 概述 (Overview)

Encryption Compiler 是一個強大的工具，允許您組合和編譯多個密碼算法，創建複雜的加密鏈。它提供了靈活的 API 來構建自定義加密方案。

The Encryption Compiler is a powerful tool that allows you to compose and compile multiple cipher algorithms, creating complex encryption chains. It provides flexible APIs for building custom encryption schemes.

## 功能特性 (Features)

### 1. 密碼組合 (Cipher Composition)
將多個密碼算法串聯在一起，創建更強的加密方案。

Chain multiple cipher algorithms together to create stronger encryption schemes.

### 2. 流暢 API (Fluent API)
使用直觀的構建器模式來構建加密鏈。

Use an intuitive builder pattern to construct encryption chains.

### 3. 規範解析 (Specification Parsing)
從簡單的文本規範創建加密鏈。

Create encryption chains from simple text specifications.

### 4. 自動反向解密 (Automatic Reverse Decryption)
解密時自動按相反順序應用密碼算法。

Automatically apply ciphers in reverse order during decryption.

## 使用方法 (Usage)

### 基本用法 (Basic Usage)

#### 方法 1: 使用 CipherCompiler.Compile

```csharp
using Encryption;
using Encryption.Classical;

// 從密碼實例編譯
var caesar = new CaesarCipher();
var atbash = new AtbashCipher();
var composed = CipherCompiler.Compile(caesar, atbash);

string plaintext = "HELLO";
string key = "3";

string encrypted = composed.Encrypt(plaintext, key);
string decrypted = composed.Decrypt(encrypted, key);
```

#### 方法 2: 從密碼名稱編譯

```csharp
// 從密碼名稱編譯
var composed = CipherCompiler.Compile("Caesar Cipher", "Atbash Cipher", "ROT13 Cipher");

string encrypted = composed.Encrypt("HELLO", "3");
string decrypted = composed.Decrypt(encrypted, "3");
```

#### 方法 3: 從規範字符串編譯

```csharp
// 使用管道符號分隔密碼名稱
var composed = CipherCompiler.CompileFromSpec("Caesar Cipher | Atbash Cipher | ROT13 Cipher");

string encrypted = composed.Encrypt("HELLO", "3");
string decrypted = composed.Decrypt(encrypted, "3");
```

### 使用構建器 API (Using Builder API)

```csharp
using Encryption;

// 使用流暢 API 構建
var composed = CipherCompiler.CreateBuilder()
    .Add("Caesar Cipher")
    .Add("Atbash Cipher")
    .Add("ROT13 Cipher")
    .WithName("MyCustomEncryption")
    .Build();

string encrypted = composed.Encrypt("HELLO", "3");
string decrypted = composed.Decrypt(encrypted, "3");
```

### 便捷方法 (Convenience Methods)

構建器提供了常用密碼的便捷方法：

The builder provides convenience methods for common ciphers:

```csharp
var composed = CipherCompiler.CreateBuilder()
    .AddCaesar()
    .AddAtbash()
    .AddROT13()
    .AddVigenere()
    .AddPlayfair()
    .AddRailFence()
    .WithName("MyEncryption")
    .Build();

string encrypted = composed.Encrypt("HELLO", "3");
string decrypted = composed.Decrypt(encrypted, "3");
Console.WriteLine($"Decrypted: {decrypted}"); // Output: HELLO
```

### 規範解析 (Specification Parsing)

#### 簡單規範 (Simple Specification)

```csharp
// 使用管道符號分隔密碼
var cipher = CipherSpecification.Parse("Caesar Cipher | Atbash Cipher");
```

#### 命名規範 (Named Specification)

```csharp
// 使用冒號添加自定義名稱
var cipher = CipherSpecification.Parse("MySecureEncryption: Caesar Cipher | Atbash Cipher | ROT13 Cipher");
Console.WriteLine(cipher.Name); // 輸出: "MySecureEncryption"
```

#### 驗證規範 (Validating Specifications)

```csharp
bool isValid = CipherSpecification.Validate(
    "Caesar Cipher | Atbash Cipher", 
    out string? errorMessage
);

if (!isValid)
{
    Console.WriteLine($"Invalid specification: {errorMessage}");
}
```

## 高級用法 (Advanced Usage)

### 創建複雜的加密鏈 (Creating Complex Encryption Chains)

```csharp
// 組合多個密碼以增強安全性
var cipher = CipherCompiler.CreateBuilder()
    .Add("Caesar Cipher")
    .Add("Atbash Cipher")
    .Add("ROT13 Cipher")
    .Add("Transposition Cipher")
    .Add("Rail Fence Cipher")
    .WithName("UltraSecureEncryption")
    .Build();

string plaintext = "ATTACKATDAWN";
string key = "3";

string encrypted = cipher.Encrypt(plaintext, key);
string decrypted = cipher.Decrypt(encrypted, key);

Console.WriteLine($"Plaintext: {plaintext}");
Console.WriteLine($"Encrypted: {encrypted}");
Console.WriteLine($"Decrypted: {decrypted}");
```

### 檢查組合密碼 (Inspecting Composed Ciphers)

```csharp
var composed = CipherCompiler.CreateBuilder()
    .AddCaesar()
    .AddAtbash()
    .Build();

Console.WriteLine($"Cipher Name: {composed.Name}");
Console.WriteLine($"Number of Ciphers: {composed.GetCiphers().Count}");

foreach (var cipher in composed.GetCiphers())
{
    Console.WriteLine($"  - {cipher.Name}");
}
```

### 動態構建 (Dynamic Building)

```csharp
var builder = CipherCompiler.CreateBuilder();

// 根據條件添加密碼
if (requireHighSecurity)
{
    builder.AddCaesar()
           .AddVigenere()
           .AddPlayfair();
}
else
{
    builder.AddCaesar()
           .AddAtbash();
}

var cipher = builder.Build();
```

## 工作原理 (How It Works)

### 加密過程 (Encryption Process)

當您加密數據時，組合密碼按順序應用每個密碼：

When you encrypt data, the composed cipher applies each cipher in sequence:

```
Plaintext → Cipher1 → Cipher2 → Cipher3 → ... → Ciphertext
```

例如 (For example):
```
"HELLO" → Caesar(key="3") → "KHOOR" → Atbash → "PSLLI"
```

### 解密過程 (Decryption Process)

解密時，密碼按相反順序應用：

During decryption, ciphers are applied in reverse order:

```
Ciphertext → CipherN → ... → Cipher2 → Cipher1 → Plaintext
```

例如 (For example):
```
"PSLLI" → Atbash → "KHOOR" → Caesar(key="3") → "HELLO"
```

## API 參考 (API Reference)

### ComposedCipher

組合密碼類，實現 `ICipher` 接口。

Composed cipher class that implements the `ICipher` interface.

```csharp
public class ComposedCipher : ICipher
{
    public string Name { get; }
    public string Encrypt(string plaintext, string key);
    public string Decrypt(string ciphertext, string key);
    public IReadOnlyList<ICipher> GetCiphers();
}
```

### CipherCompiler

靜態編譯器類，提供多種編譯方法。

Static compiler class providing various compilation methods.

```csharp
public static class CipherCompiler
{
    public static ComposedCipher Compile(params ICipher[] ciphers);
    public static ComposedCipher Compile(params string[] cipherNames);
    public static ComposedCipher CompileFromSpec(string specification);
    public static CipherBuilder CreateBuilder();
}
```

### CipherBuilder

流暢 API 構建器。

Fluent API builder.

```csharp
public class CipherBuilder
{
    public CipherBuilder Add(ICipher cipher);
    public CipherBuilder Add(string cipherName);
    public CipherBuilder WithName(string name);
    public CipherBuilder AddCaesar();
    public CipherBuilder AddVigenere();
    public CipherBuilder AddAtbash();
    public CipherBuilder AddROT13();
    public CipherBuilder AddPlayfair();
    public CipherBuilder AddRailFence();
    public ComposedCipher Build();
    public int Count { get; }
    public CipherBuilder Clear();
}
```

### CipherSpecification

規範解析器。

Specification parser.

```csharp
public static class CipherSpecification
{
    public static ComposedCipher Parse(string specification);
    public static bool Validate(string specification, out string? errorMessage);
}
```

## 最佳實踐 (Best Practices)

1. **使用有意義的名稱** - 為您的組合密碼使用描述性名稱
   
   Use meaningful names for your composed ciphers

2. **密鑰兼容性** - 確保所有密碼算法都能使用相同的密鑰格式
   
   Ensure all ciphers can use the same key format

3. **測試您的鏈** - 始終測試加密和解密往返
   
   Always test encryption and decryption roundtrip

4. **文檔化** - 記錄您的自定義加密方案
   
   Document your custom encryption schemes

5. **安全考慮** - 記住這是教育目的，不要用於生產環境
   
   Remember this is for educational purposes, not production use

## 示例場景 (Example Scenarios)

### 場景 1: 增強凱撒密碼 (Enhanced Caesar Cipher)

```csharp
// 通過添加 Atbash 和 ROT13 來增強凱撒密碼
var enhanced = CipherCompiler.CreateBuilder()
    .AddCaesar()
    .AddAtbash()
    .AddROT13()
    .WithName("EnhancedCaesar")
    .Build();
```

### 場景 2: 自定義企業加密 (Custom Enterprise Encryption)

```csharp
// 為企業應用創建自定義方案
var enterpriseEncryption = CipherSpecification.Parse(
    "EnterpriseSecure: Caesar Cipher | Transposition Cipher | Vigenère Cipher"
);
```

### 場景 3: 可配置加密 (Configurable Encryption)

```csharp
// 從配置文件讀取規範
string spec = ConfigurationManager.AppSettings["EncryptionSpec"];
var cipher = CipherSpecification.Parse(spec);

// 使用配置的密碼
string encrypted = cipher.Encrypt(data, key);
```

## 限制 (Limitations)

1. **密鑰共享** - 所有密碼使用相同的密鑰。不同的密碼可能需要不同格式的密鑰。
   
   All ciphers use the same key. Different ciphers may require different key formats.

2. **性能** - 鏈中的密碼越多，加密/解密越慢。
   
   More ciphers in the chain means slower encryption/decryption.

3. **教育用途** - 這些是經典密碼，不應用於實際安全應用。
   
   These are classical ciphers and should not be used for real security applications.

## 貢獻 (Contributing)

歡迎貢獻新的編譯器功能：

- 添加新的構建器便捷方法
- 改進規範解析
- 添加密鑰管理功能
- 提供更多使用示例

## 許可證 (License)

MIT License - 與主項目相同

Same as main project

---

**最後更新 (Last Updated)**: 2025-10-25
**版本 (Version)**: 1.0.0
