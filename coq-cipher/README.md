# CogCipher - Coq 密碼系統

這是一個用 Coq 證明助手實現的形式化密碼系統，包含經典密碼算法的實現和形式化證明。

## 特點

- **形式化驗證**: 使用 Coq 提供數學證明，確保密碼算法的正確性
- **經典密碼**: 實現多種經典密碼算法
- **類型安全**: 利用 Coq 的強類型系統確保代碼正確性
- **定理證明**: 證明密碼的各種性質（對稱性、長度保持等）

## 已實現的密碼算法

1. **凱撒密碼 (Caesar Cipher)** - 簡單的移位密碼
2. **Atbash 密碼** - 字母表反轉密碼
3. **ROT13 密碼** - 固定移位 13 的凱撒密碼
4. **維吉尼亞密碼 (Vigenère Cipher)** - 多字母替換密碼
5. **仿射密碼 (Affine Cipher)** - 基於數學函數的密碼

## 文件結構

- `Cipher.v` - 基礎定義和輔助函數
- `ClassicalCiphers.v` - 經典密碼算法實現
- `CipherFactory.v` - 密碼工廠模式
- `CipherProperties.v` - 密碼屬性和定理證明
- `_CoqProject` - Coq 項目配置
- `Makefile` - 構建文件

## 系統要求

- Coq 8.12 或更高版本
- Make 工具

## 安裝和編譯

1. 確保已安裝 Coq:
```bash
# Ubuntu/Debian
sudo apt-get install coq

# macOS (使用 Homebrew)
brew install coq

# 或使用 OPAM
opam install coq
```

2. 編譯項目:
```bash
cd coq-cipher
make
```

## 使用方法

### 在 Coq 中交互式使用

```bash
coqide Cipher.v
# 或
coqtop -l Cipher.v
```

### 基本示例

```coq
Require Import CogCipher.ClassicalCiphers.
Require Import CogCipher.CipherFactory.

(* 凱撒密碼加密 *)
Compute encrypt_with Caesar "HELLO" "3".
(* 結果: "KHOOR" *)

(* 維吉尼亞密碼 *)
Compute encrypt_with Vigenere "HELLO" "KEY".

(* Atbash 密碼 *)
Compute encrypt_with Atbash "ABC" "".
(* 結果: "ZYX" *)
```

## 已證明的性質

### 對稱性
- `atbash_is_symmetric`: Atbash 密碼的加密和解密是相同操作
- `rot13_is_symmetric`: ROT13 密碼是自身的逆運算

### 長度保持
- `caesar_preserves_length`: 凱撒密碼保持文本長度
- `atbash_preserves_length`: Atbash 密碼保持文本長度

### 正確性
- `composed_cipher_correctness`: 組合密碼的正確性證明
- `atbash_involutive`: Atbash 應用兩次返回原文

## 密碼接口

所有密碼都實現了 `CipherInterface` 接口：

```coq
Record CipherInterface := {
  cipher_name : string;
  encrypt : string -> string -> string;
  decrypt : string -> string -> string;
}.
```

## 理論基礎

這個項目展示了如何使用形式化方法來驗證密碼算法。主要概念包括：

1. **函數式編程**: 使用純函數實現密碼算法
2. **依賴類型**: 利用 Coq 的類型系統表達性質
3. **證明驅動開發**: 先定義性質，再實現和證明

## 擴展

可以通過以下方式擴展：

1. 添加更多經典密碼（Playfair, Hill, 等）
2. 實現現代密碼算法的形式化模型
3. 證明更多安全性質
4. 添加密碼分析功能

## 注意事項

- 這些密碼僅用於教育目的
- 經典密碼不安全，不應用於實際加密
- 某些證明使用了 `Admitted`，需要進一步完善

## 參考資料

- [Coq 官方文檔](https://coq.inria.fr/documentation)
- [Software Foundations](https://softwarefoundations.cis.upenn.edu/)
- [經典密碼學](https://en.wikipedia.org/wiki/Classical_cipher)

## 貢獻

歡迎提交 Pull Request 來改進代碼或添加新的密碼算法！

## 許可證

MIT License

## 作者

CogCipher - Coq 實現的密碼系統


