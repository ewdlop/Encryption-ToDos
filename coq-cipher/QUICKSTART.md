# 快速開始指南

## 5 分鐘入門 CogCipher

### 前提條件

確保已安裝 Coq:
```bash
coqc --version
```

如果未安裝，請參考 [INSTALL.md](INSTALL.md)。

### 步驟 1: 編譯項目

```bash
cd coq-cipher
make
```

預期輸出：
```
coq_makefile -f _CoqProject -o Makefile.coq
make -f Makefile.coq
...
Cipher.vo
ClassicalCiphers.vo
...
```

### 步驟 2: 啟動 Coq 交互式環境

```bash
coqtop
```

或使用 IDE：
```bash
coqide Cipher.v
```

### 步驟 3: 加載模塊

在 Coq 中輸入：
```coq
Require Import CogCipher.Cipher.
Require Import CogCipher.ClassicalCiphers.
Require Import CogCipher.CipherFactory.
```

### 步驟 4: 嘗試加密

```coq
(* 凱撒密碼 - 移位 3 *)
Compute encrypt_with Caesar "HELLO" "3".
(* 結果: "KHOOR" *)

(* Atbash 密碼 - 字母表反轉 *)
Compute encrypt_with Atbash "HELLO" "".
(* 結果: "SVOOL" *)

(* ROT13 密碼 *)
Compute encrypt_with ROT13 "HELLO" "".
(* 結果: "URYYB" *)
```

### 步驟 5: 嘗試解密

```coq
(* 解密凱撒密碼 *)
Compute decrypt_with Caesar "KHOOR" "3".
(* 結果: "HELLO" *)

(* Atbash 是對稱的 - 加密就是解密 *)
Compute encrypt_with Atbash "SVOOL" "".
(* 結果: "HELLO" *)
```

### 步驟 6: 查看證明

```coq
(* 加載性質模塊 *)
Require Import CogCipher.CipherProperties.

(* 檢查已證明的定理 *)
Check atbash_is_symmetric.
Check caesar_preserves_length.

(* 查看定理的語句 *)
Print atbash_is_symmetric.
```

### 步驟 7: 運行示例

```coq
(* 加載示例 *)
Require Import CogCipher.Examples.

(* 查看測試用例 *)
Check caesar_encrypt_hello.
Check atbash_symmetric_test.

(* 運行測試 *)
Compute encrypt CaesarCipher "ATTACK AT DAWN" "5".
```

## 常見操作

### 列出所有可用密碼

```coq
Compute all_cipher_types.
(* 結果: [Caesar; Atbash; ROT13; Vigenere; Affine] *)
```

### 使用維吉尼亞密碼

```coq
Compute encrypt_with Vigenere "HELLO" "KEY".
Compute decrypt_with Vigenere (encrypt_with Vigenere "HELLO" "KEY") "KEY".
```

### 組合密碼

```coq
(* 創建組合密碼 *)
Definition my_cipher := compose_ciphers CaesarCipher AtbashCipher.

(* 使用組合密碼 *)
Compute encrypt my_cipher "SECRET" "3".
```

### 字符串處理

```coq
(* 轉換為大寫 *)
Compute string_to_upper "hello".
(* 結果: "HELLO" *)

(* 過濾字母 *)
Compute filter_alpha "H3ll0 W0r1d!".
(* 結果: "HllWrd" *)

(* 獲取字符串長度 *)
Compute string_length "CRYPTOGRAPHY".
(* 結果: 12 *)
```

## 驗證證明

### 檢查證明的正確性

```bash
# 重新編譯以驗證所有證明
make clean
make
```

如果編譯成功，說明所有證明都是正確的。

### 查看證明細節

在 CoqIDE 中：
1. 打開 `CipherProperties.v`
2. 逐步執行證明（Ctrl+Down）
3. 觀察證明目標的變化

## 實用技巧

### 1. 快速測試密碼

創建測試文件 `test.v`:
```coq
Require Import CogCipher.ClassicalCiphers.
Require Import CogCipher.CipherFactory.

Definition my_plaintext := "THE QUICK BROWN FOX".
Definition my_key := "7".

Compute encrypt_with Caesar my_plaintext my_key.
Compute decrypt_with Caesar (encrypt_with Caesar my_plaintext my_key) my_key.
```

運行:
```bash
coqc test.v
```

### 2. 檢查類型

```coq
(* 檢查函數類型 *)
Check encrypt.
(* CipherInterface -> string -> string -> string *)

Check caesar_shift_char.
(* Z -> ascii -> ascii *)
```

### 3. 搜索定理

```coq
(* 搜索關於對稱性的定理 *)
Search symmetric.

(* 搜索關於長度的定理 *)
Search length_preserving.
```

### 4. 獲取幫助

```coq
(* 查看定義 *)
Print CipherInterface.
Print caesar_encrypt.

(* 查看定理內容 *)
Print atbash_is_symmetric.

(* 查看證明腳本 *)
Show Proof.
```

## 交互式開發

### 使用 CoqIDE

1. 啟動: `coqide Cipher.v`
2. 逐步執行: Ctrl+Down
3. 向後撤銷: Ctrl+Up
4. 執行到光標: Ctrl+Right

### 使用 Proof General (Emacs)

1. 打開文件: `emacs Cipher.v`
2. 進入 Proof General 模式
3. 執行命令: C-c C-n
4. 撤銷: C-c C-u

## 調試技巧

### 證明卡住時

```coq
(* 查看當前目標 *)
Show.

(* 查看所有假設 *)
Show Existentials.

(* 嘗試自動策略 *)
auto.
intuition.
```

### 計算結果不符預期

```coq
(* 逐步展開定義 *)
unfold encrypt.
unfold caesar_encrypt.
simpl.

(* 檢查中間結果 *)
Compute caesar_shift_char 3 "A"%char.
```

## 下一步

- 📖 閱讀 [TUTORIAL.md](TUTORIAL.md) 深入學習
- 🔧 查看 [Examples.v](Examples.v) 更多示例
- 🎯 嘗試實現自己的密碼算法
- 📝 閱讀 [CONTRIBUTING.md](CONTRIBUTING.md) 了解如何貢獻

## 常見問題

**Q: 編譯報錯怎麼辦？**
A: 確保 Coq 版本 >= 8.12，並檢查錯誤信息。

**Q: 如何退出 coqtop？**
A: 輸入 `Quit.`

**Q: 計算很慢怎麼辦？**
A: 長字符串計算可能較慢，這是 Coq 的特性。

**Q: 如何保存計算結果？**
A: 使用 `Definition` 保存結果：
```coq
Definition encrypted := encrypt_with Caesar "HELLO" "3".
```

## 資源

- [Coq 官方教程](https://coq.inria.fr/tutorial)
- [Software Foundations](https://softwarefoundations.cis.upenn.edu/)
- [Coq Reference Manual](https://coq.inria.fr/refman/)

祝使用愉快！🎉


