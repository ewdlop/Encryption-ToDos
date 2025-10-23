# 安裝指南

## 系統要求

- **Coq**: 版本 8.12 或更高
- **Make**: GNU Make 工具
- **操作系統**: Linux, macOS, 或 Windows (WSL)

## 安裝 Coq

### Ubuntu/Debian

```bash
sudo apt-get update
sudo apt-get install coq coqide
```

### macOS

使用 Homebrew:
```bash
brew install coq
```

或使用 OPAM:
```bash
brew install opam
opam init
opam install coq
```

### Windows

1. 安裝 WSL (Windows Subsystem for Linux)
2. 在 WSL 中按照 Ubuntu 指南安裝

或者下載 Windows 安裝包:
- 訪問 https://coq.inria.fr/download
- 下載適合 Windows 的安裝程序

### 使用 OPAM (推薦)

```bash
# 安裝 OPAM
sh <(curl -sL https://raw.githubusercontent.com/ocaml/opam/master/shell/install.sh)

# 初始化 OPAM
opam init
eval $(opam env)

# 安裝 Coq
opam install coq
```

## 驗證安裝

```bash
coqc --version
```

應該顯示 Coq 的版本號，例如：
```
The Coq Proof Assistant, version 8.15.0
```

## 編譯項目

1. 克隆或下載項目:
```bash
cd coq-cipher
```

2. 生成 Makefile:
```bash
make
```

這將編譯所有 `.v` 文件並生成 `.vo` 文件。

## IDE 選擇

### CoqIDE (官方)

```bash
coqide Cipher.v
```

### Proof General (Emacs)

```bash
emacs Cipher.v
```

需要安裝 Proof General 插件。

### VSCode

安裝擴展：
- VsCoq
- Coq

### Vim

使用 Coqtail 插件:
```bash
git clone https://github.com/whonore/Coqtail.git ~/.vim/pack/coq/start/coqtail
```

## 常見問題

### Q: 編譯時出現 "omega not found" 錯誤

A: 新版本的 Coq 已經棄用 omega。需要：
```coq
Require Import Lia.
```
然後用 `lia` 替換 `omega`。

### Q: 無法找到模塊

A: 確保在正確的目錄中運行，並且已經運行過 `make`。

### Q: CoqIDE 無法啟動

A: 可能需要安裝 GTK 依賴:
```bash
sudo apt-get install libgtk-3-dev libgtksourceview-3.0-dev
```

## 下一步

安裝完成後，請查看 [README.md](README.md) 了解使用方法。


