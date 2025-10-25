# Encryption-ToDos - 多語言密碼學實現

本項目提供經典密碼算法的多語言實現，包括 C#、JavaScript 和 Coq 證明助手。

## 🆕 最新功能: Encryption Compiler

**Encryption Compiler** 允許您組合多個密碼算法，創建複雜的加密鏈！

```csharp
// 使用流暢 API 構建加密鏈
var cipher = CipherCompiler.CreateBuilder()
    .AddCaesar()
    .AddAtbash()
    .AddROT13()
    .WithName("MyCustomEncryption")
    .Build();

string encrypted = cipher.Encrypt("HELLO", "3");
string decrypted = cipher.Decrypt(encrypted, "3");
```

### Reduce the encryption graph for indundant decrypting graph. More doesn't necessarily make it more security.

📖 查看 [完整文檔](CIPHER_COMPILER.md) 了解更多詳情。

## 項目結構

### 1. C# 實現 (`Encryption/`)
完整的 .NET 密碼庫，實現經典和現代密碼算法。

**語言**: C# (.NET 9.0)  
**測試**: xUnit  
**特點**: 
- 220 種密碼算法
- 接口驅動設計
- **Encryption Compiler** ⭐ 新增
- 完整單元測試

### 2. JavaScript 實現 (`encryption-js/`)
Node.js 密碼庫，與 C# 版本功能對等。

**語言**: JavaScript (ES6+)  
**測試**: Jest  
**特點**:
- 經典密碼實現
- 工廠模式
- 完整測試覆蓋

### 3. Coq 形式化驗證 (`coq-cipher/`) ⭐ 新增
使用 Coq 證明助手的形式化密碼系統，提供數學證明。

**語言**: Coq (Gallina)  
**版本**: Coq 8.12+  
**特點**:
- **形式化驗證**: 使用 Coq 提供數學證明
- **類型安全**: 強類型系統確保正確性
- **5 種經典密碼**: Caesar, Atbash, ROT13, Vigenère, Affine
- **定理證明**: 證明密碼的各種性質（對稱性、長度保持等）
- **完整文檔**: README, 教程, 快速開始指南

#### Coq 實現亮點

```coq
(* 密碼接口定義 *)
Record CipherInterface := {
  cipher_name : string;
  encrypt : string -> string -> string;
  decrypt : string -> string -> string;
}.

(* 已證明的定理 *)
Theorem atbash_is_symmetric : symmetric_cipher AtbashCipher.
Theorem caesar_preserves_length : length_preserving CaesarCipher.
Theorem composed_cipher_correctness : forall ci1 ci2 plaintext key, ...
```

#### 快速開始 (Coq)

```bash
cd coq-cipher
make
coqtop
```

```coq
Require Import CogCipher.ClassicalCiphers.
Require Import CogCipher.CipherFactory.

(* 使用凱撒密碼 *)
Compute encrypt_with Caesar "HELLO" "3".
(* 結果: "KHOOR" *)

(* 驗證對稱性 *)
Check atbash_is_symmetric.
```

詳細信息請參考 [`coq-cipher/README.md`](coq-cipher/README.md) 和 [`coq-cipher/QUICKSTART.md`](coq-cipher/QUICKSTART.md)。

## 已實現的經典密碼（所有語言）

1. **Caesar Cipher** (凱撒密碼) - 簡單移位密碼
2. **Atbash Cipher** - 字母表反轉密碼
3. **ROT13** - 固定移位 13 的凱撒密碼
4. **Vigenère Cipher** (維吉尼亞密碼) - 多字母替換密碼
5. **Playfair Cipher** - 雙字母加密 (C#/JS)
6. **Substitution Cipher** (簡單替換密碼) - 字母映射 (C#/JS)
7. **Transposition Cipher** (轉置密碼) - 列轉置 (C#/JS)
8. **Rail Fence Cipher** (柵欄密碼) - 之字形轉置 (C#/JS)
9. **Scytale Cipher** - 圓柱轉置密碼 (C#/JS)
10. **Polybius Square** - 方陣密碼 (C#/JS)
11. **Affine Cipher** (仿射密碼) - 數學函數密碼 (Coq)

## 語言特色對比

| 特性 | C# | JavaScript | Coq |
|------|-------|------------|-----|
| 經典密碼 | ✅ 220 種 | ✅ 10 種 | ✅ 5 種 |
| 現代密碼 | ✅ 部分 | ✅ 部分 | ❌ |
| 密碼編譯器 | ✅ | ❌ | ❌ |
| 單元測試 | ✅ xUnit | ✅ Jest | ✅ 形式化證明 |
| 類型安全 | ✅ 強類型 | ⚠️ 動態類型 | ✅ 依賴類型 |
| 數學證明 | ❌ | ❌ | ✅ Coq 證明 |
| 性能 | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐ |
| 學習曲線 | 中等 | 容易 | 困難 |

## 使用示例

### C# 基本示例
```csharp
using Encryption.Classical;

var caesar = new CaesarCipher();
string encrypted = caesar.Encrypt("HELLO", "3");  // "KHOOR"
string decrypted = caesar.Decrypt(encrypted, "3"); // "HELLO"
```

### C# Encryption Compiler 示例

使用 Encryption Compiler 組合多個密碼：

```csharp
using Encryption;

// 方法 1: 使用流暢 API
var cipher = CipherCompiler.CreateBuilder()
    .AddCaesar()
    .AddAtbash()
    .AddROT13()
    .WithName("TripleEncryption")
    .Build();

string encrypted = cipher.Encrypt("HELLO", "3");
string decrypted = cipher.Decrypt(encrypted, "3");

// 方法 2: 從規範字符串
var cipher2 = CipherCompiler.CompileFromSpec("Caesar Cipher | Atbash Cipher | ROT13 Cipher");

// 方法 3: 命名規範
var cipher3 = CipherSpecification.Parse("SecureEncryption: Caesar Cipher | Vigenère Cipher");
```

查看 [完整 Encryption Compiler 文檔](CIPHER_COMPILER.md) 了解更多示例。

### JavaScript 示例
```javascript
const { CipherFactory } = require('./src/CipherFactory');

const caesar = CipherFactory.createCipher('caesar');
const encrypted = caesar.encrypt('HELLO', '3');  // "KHOOR"
const decrypted = caesar.decrypt(encrypted, '3'); // "HELLO"
```

### Coq 示例
```coq
Require Import CogCipher.ClassicalCiphers.

Compute encrypt_with Caesar "HELLO" "3".  (* "KHOOR" *)
Compute decrypt_with Caesar "KHOOR" "3".  (* "HELLO" *)

(* 證明正確性 *)
Theorem caesar_correct : forall plaintext key,
  decrypt CaesarCipher (encrypt CaesarCipher plaintext key) key = plaintext.
```

## 編譯和測試

### C#
```bash
cd Encryption
dotnet build
dotnet test ../xUnitTestEncryptionProject
```

### JavaScript
```bash
cd encryption-js
npm install
npm test
```

### Coq
```bash
cd coq-cipher
make
make verify  # 運行驗證測試
```

## 項目目標

本項目旨在：
- 📚 **教育**: 展示密碼學基礎概念
- 🔍 **多語言**: 同一算法的不同實現風格
- 🎯 **實踐**: 提供可用的密碼庫
- 🔬 **形式化**: 使用 Coq 進行數學驗證
- ✅ **測試**: 完整的測試覆蓋
- 📖 **文檔**: 詳細的說明和教程

## 形式化驗證的價值

Coq 實現提供了傳統實現無法提供的保證：

1. **數學證明**: 證明密碼算法的正確性
2. **類型安全**: 類型系統防止常見錯誤
3. **不變量保證**: 證明長度保持、對稱性等性質
4. **教育價值**: 深入理解密碼學原理

## 密碼學算法參考

### 經典密碼（100 種）
本項目實現了前 10 種最基礎的經典密碼。完整列表包括：
- Caesar, Atbash, ROT13, Vigenère, Playfair
- Substitution, Transposition, Rail Fence, Scytale, Polybius Square
- ADFGVX, Bifid, Trifid, Four-Square, Hill
- Affine, Beaufort, Running Key, Autokey, Columnar Transposition
- ...以及更多

### 現代密碼（100 種）
包括但不限於：
- 對稱加密: AES, DES, 3DES, Blowfish, Twofish, ChaCha20
- 非對稱加密: RSA, ECC, Diffie-Hellman
- 後量子密碼: Kyber, Dilithium, Falcon
- 哈希和 MAC: HMAC, CMAC, GCM
- ...以及更多

詳細算法列表請查看完整的 README 內容。

## 安全警告 ⚠️

**本項目僅用於教育目的。**

- ❌ 不要在生產環境使用這些經典密碼
- ❌ 經典密碼已被證明不安全
- ✅ 實際應用請使用現代標準加密算法（AES, RSA 等）
- ✅ 使用經過驗證的密碼學庫

## 文檔

### 主要文檔
- [主要實現總結](IMPLEMENTATION_SUMMARY.md)
- [JavaScript 實現總結](encryption-js/IMPLEMENTATION_SUMMARY.md)
- [Coq 實現總結](coq-cipher/IMPLEMENTATION_SUMMARY.md)

### Coq 特定文檔
- [Coq README](coq-cipher/README.md)
- [安裝指南](coq-cipher/INSTALL.md)
- [快速開始](coq-cipher/QUICKSTART.md)
- [詳細教程](coq-cipher/TUTORIAL.md)
- [貢獻指南](coq-cipher/CONTRIBUTING.md)

## 貢獻

歡迎貢獻！可以：
- 添加新的密碼算法
- 改進現有實現
- 添加更多測試
- 完善 Coq 證明
- 改進文檔
- 修復 bug

## 學習資源

### Coq 學習
- [Coq 官方文檔](https://coq.inria.fr/documentation)
- [Software Foundations](https://softwarefoundations.cis.upenn.edu/)
- [Certified Programming with Dependent Types](http://adam.chlipala.net/cpdt/)

### 密碼學學習
- [經典密碼學](https://en.wikipedia.org/wiki/Classical_cipher)
- [現代密碼學](https://en.wikipedia.org/wiki/Cryptography)
- [應用密碼學](https://www.schneier.com/books/applied_cryptography/)

## 許可證

MIT License

---

## 密碼算法完整列表

<details>
<summary>點擊展開 100 種經典密碼列表</summary>

1. Caesar Cipher
2. Atbash Cipher
3. ROT13
4. Vigenère Cipher
5. Playfair Cipher
6. Substitution Cipher
7. Transposition Cipher
8. Rail Fence Cipher
9. Scytale Cipher
10. Polybius Square
11. ADFGVX Cipher
12. Bifid Cipher
13. Trifid Cipher
14. Four-Square Cipher
15. Hill Cipher
16. Affine Cipher
17. Beaufort Cipher
18. Running Key Cipher
19. Autokey Cipher
20. Columnar Transposition Cipher
... (共 100 種)

</details>

<details>
<summary>點擊展開 100 種現代密碼算法列表</summary>

1. AES (Advanced Encryption Standard)
2. DES (Data Encryption Standard)
3. 3DES (Triple DES)
4. RSA (Rivest-Shamir-Adleman)
5. Blowfish
6. Twofish
7. RC4, RC5, RC6
8. ChaCha20
9. Salsa20
10. Diffie-Hellman
... (共 100 種)

</details>

---

**最後更新**: 2025-10-23  
**版本**: 1.0.0  
**語言**: C#, JavaScript, Coq
