# 🚀 開始使用 CogCipher

## 歡迎！

歡迎使用 CogCipher - 一個用 Coq 證明助手實現的形式化密碼系統。本指南將帶您在 5 分鐘內開始使用。

## 📋 前置檢查

```bash
# 檢查 Coq 是否已安裝
coqc --version
# 應該顯示: The Coq Proof Assistant, version 8.12.0 (或更高版本)
```

如果沒有安裝，請參考 [INSTALL.md](INSTALL.md)。

## 🎯 三步開始

### 第 1 步: 編譯項目 (30 秒)

```bash
cd coq-cipher
make
```

成功輸出應該類似：
```
COQDEP VFILES
COQC Cipher.v
COQC ClassicalCiphers.v
COQC CipherFactory.v
COQC CipherProperties.v
COQC Examples.v
COQC verify.v
```

### 第 2 步: 啟動 Coq (10 秒)

```bash
coqtop
```

或使用 IDE：
```bash
coqide Cipher.v
```

### 第 3 步: 嘗試加密 (2 分鐘)

在 Coq 提示符下輸入：

```coq
Require Import CogCipher.ClassicalCiphers.
Require Import CogCipher.CipherFactory.
```

然後嘗試：

```coq
(* 凱撒密碼 - 移位 3 *)
Compute encrypt_with Caesar "HELLO" "3".
(* 結果: "KHOOR" *)

(* Atbash 密碼 - 字母表反轉 *)
Compute encrypt_with Atbash "WORLD" "".
(* 結果: "DLIOW" *)

(* ROT13 密碼 *)
Compute encrypt_with ROT13 "COQCIPHER" "".
(* 結果: "PBDPVCURE" *)
```

🎉 **恭喜！您已成功運行 Coq 密碼系統！**

## 📚 下一步

### 選項 A: 快速探索
閱讀 [QUICKSTART.md](QUICKSTART.md) 了解更多操作。

### 選項 B: 深入學習
跟隨 [TUTORIAL.md](TUTORIAL.md) 系統學習。

### 選項 C: 查看示例
探索 [Examples.v](Examples.v) 中的 40+ 個示例。

### 選項 D: 理解證明
查看 [CipherProperties.v](CipherProperties.v) 中的定理。

## 💡 快速提示

### 退出 Coq
```coq
Quit.
```

### 獲取幫助
```coq
(* 查看類型 *)
Check encrypt_with.

(* 查看定義 *)
Print CaesarCipher.

(* 搜索 *)
Search symmetric.
```

### 常見問題

**Q: 編譯失敗怎麼辦？**  
A: 確保 Coq 版本 >= 8.12，檢查是否在正確目錄。

**Q: 如何重新編譯？**  
A: 運行 `make clean` 然後 `make`。

**Q: 計算結果很慢？**  
A: 這是正常的，Coq 的計算比運行時語言慢。

## 🎓 學習路徑

```
1. 開始這裡 ← 您在這裡！
   ↓
2. QUICKSTART.md (更多操作)
   ↓
3. TUTORIAL.md (深入學習)
   ↓  
4. Examples.v (實踐)
   ↓
5. 創建自己的密碼！
```

## 📞 需要幫助？

- 📖 查閱 [README.md](README.md)
- 🔧 參考 [INSTALL.md](INSTALL.md)
- 🤝 閱讀 [CONTRIBUTING.md](CONTRIBUTING.md)
- 🐛 報告問題到 GitHub Issues

## 🌟 特色功能試用

### 證明密碼性質
```coq
Require Import CogCipher.CipherProperties.

(* 檢查已證明的定理 *)
Check atbash_is_symmetric.
Check caesar_preserves_length.
```

### 組合密碼
```coq
(* 創建組合密碼 *)
Definition my_cipher := compose_ciphers CaesarCipher AtbashCipher.

(* 使用它 *)
Compute encrypt my_cipher "SECRET" "5".
```

### 驗證正確性
```coq
(* 加載驗證腳本 *)
Require Import CogCipher.verify.

(* 所有測試都會自動運行！ *)
```

## 🎉 享受探索！

CogCipher 不僅是密碼庫，更是學習形式化驗證的絕佳資源。

祝學習愉快！🚀

---

**提示**: 如果您喜歡這個項目，請給我們一個 ⭐！

**下一步**: [QUICKSTART.md](QUICKSTART.md) 或 [TUTORIAL.md](TUTORIAL.md)


