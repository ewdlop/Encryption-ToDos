# 貢獻指南

感謝您對 CogCipher 項目的興趣！我們歡迎各種形式的貢獻。

## 如何貢獻

### 報告問題

如果您發現 bug 或有改進建議：

1. 檢查是否已有相關 issue
2. 創建新 issue，包含：
   - 清晰的標題
   - 詳細描述
   - 重現步驟（如果是 bug）
   - 預期行為
   - 實際行為
   - Coq 版本信息

### 提交代碼

1. Fork 項目
2. 創建功能分支: `git checkout -b feature/amazing-feature`
3. 提交更改: `git commit -m 'Add amazing feature'`
4. 推送到分支: `git push origin feature/amazing-feature`
5. 開啟 Pull Request

### 代碼規範

#### Coq 代碼風格

```coq
(* 使用清晰的註釋 *)
(** 文檔化的定義使用 (** ... *) *)

(* 縮進: 2 個空格 *)
Definition my_function (x : nat) : nat :=
  match x with
  | 0 => 1
  | S n => n + 1
  end.

(* 命名規範 *)
- 類型: PascalCase (CipherInterface)
- 函數: snake_case (encrypt_with)
- 定理: snake_case_with_verb (preserves_length)
```

#### 證明風格

```coq
(* 簡單證明使用簡潔形式 *)
Theorem simple_fact : 2 + 2 = 4.
Proof. reflexivity. Qed.

(* 複雜證明使用結構化形式 *)
Theorem complex_theorem : forall n m,
  n + m = m + n.
Proof.
  intros n m.
  induction n as [| n' IH].
  - (* Base case *)
    simpl. reflexivity.
  - (* Inductive case *)
    simpl. rewrite IH. reflexivity.
Qed.
```

### 添加新密碼

要添加新的密碼算法：

1. 在 `ClassicalCiphers.v` 或創建新文件
2. 實現加密和解密函數
3. 創建 `CipherInterface` 實例
4. 添加到 `CipherFactory.v`
5. 在 `Examples.v` 中添加測試
6. 在 `CipherProperties.v` 中添加性質證明

示例結構：

```coq
(** 新密碼 *)
Definition new_cipher_encrypt (plaintext : string) (key : string) : string :=
  (* 實現 *)
  plaintext.

Definition new_cipher_decrypt (ciphertext : string) (key : string) : string :=
  (* 實現 *)
  ciphertext.

Definition NewCipher : CipherInterface := {|
  cipher_name := "New Cipher";
  encrypt := new_cipher_encrypt;
  decrypt := new_cipher_decrypt;
|}.

(* 添加測試 *)
Example new_cipher_test :
  encrypt NewCipher "TEST" "key" = "...".
Proof. reflexivity. Qed.

(* 添加性質證明 *)
Theorem new_cipher_correct : forall plaintext key,
  decrypt NewCipher (encrypt NewCipher plaintext key) key = plaintext.
Proof.
  (* 證明 *)
Admitted.
```

### 文檔

- 所有公開定義都應有文檔註釋
- 複雜算法需要解釋原理
- 定理需要說明意義
- 更新 README.md 包含新功能

### 測試

1. 確保所有證明都能通過
2. 添加適當的測試用例
3. 運行 `make` 確保編譯成功
4. 測試邊界情況

```coq
(* 邊界測試 *)
Example empty_string_test :
  encrypt NewCipher "" "key" = "".
Proof. reflexivity. Qed.

Example non_alpha_test :
  encrypt NewCipher "123!@#" "key" = "123!@#".
Proof. reflexivity. Qed.
```

## 項目結構

```
coq-cipher/
├── Cipher.v              - 基礎定義
├── ClassicalCiphers.v    - 經典密碼實現
├── CipherFactory.v       - 工廠模式
├── CipherProperties.v    - 性質和證明
├── Examples.v            - 使用示例
├── README.md             - 項目說明
├── TUTORIAL.md           - 教程
└── _CoqProject           - 項目配置
```

## 開發流程

1. **討論**: 對於重大更改，先開啟 issue 討論
2. **開發**: 在功能分支上開發
3. **測試**: 確保所有測試通過
4. **文檔**: 更新相關文檔
5. **審查**: 提交 PR 等待審查
6. **合併**: 審查通過後合併

## 需要幫助的領域

- 實現更多經典密碼
- 完善現有證明（移除 `Admitted`）
- 添加現代密碼的形式化模型
- 改進文檔和教程
- 添加密碼分析功能
- 性能優化

## 代碼審查標準

Pull Request 將根據以下標準審查：

- ✅ 代碼能夠編譯
- ✅ 遵循代碼風格
- ✅ 包含適當的註釋
- ✅ 有測試用例
- ✅ 文檔已更新
- ✅ 無不必要的 `Admitted`
- ✅ 提交信息清晰

## 問題標籤

- `bug`: 需要修復的問題
- `enhancement`: 新功能或改進
- `documentation`: 文檔相關
- `good first issue`: 適合新手
- `help wanted`: 需要幫助
- `proof needed`: 需要證明

## 行為準則

- 尊重所有貢獻者
- 提供建設性反饋
- 專注於改進項目
- 歡迎不同觀點

## 許可

通過貢獻，您同意您的貢獻將按照項目的 MIT 許可證授權。

## 聯繫

如有問題，請：
- 開啟 GitHub issue
- 或通過項目討論區聯繫

感謝您的貢獻！


