# CogCipher 教程

本教程將引導您學習如何使用 CogCipher 項目。

## 目錄

1. [Coq 基礎](#coq-基礎)
2. [加載和使用密碼](#加載和使用密碼)
3. [創建新密碼](#創建新密碼)
4. [證明密碼性質](#證明密碼性質)
5. [高級主題](#高級主題)

## Coq 基礎

### 啟動 Coq

```bash
coqtop
```

或使用 IDE:
```bash
coqide Cipher.v
```

### 基本命令

```coq
(* 這是註釋 *)

(* 定義常量 *)
Definition my_number := 42.

(* 計算表達式 *)
Compute 2 + 3.

(* 檢查類型 *)
Check nat.
Check string.
```

## 加載和使用密碼

### 加載模塊

```coq
Require Import CogCipher.Cipher.
Require Import CogCipher.ClassicalCiphers.
Require Import CogCipher.CipherFactory.
```

### 使用凱撒密碼

```coq
(* 加密 *)
Compute encrypt CaesarCipher "HELLO" "3".
(* 結果: "KHOOR" *)

(* 解密 *)
Compute decrypt CaesarCipher "KHOOR" "3".
(* 結果: "HELLO" *)
```

### 使用 Atbash 密碼

```coq
(* Atbash 不需要密鑰 *)
Compute encrypt AtbashCipher "HELLO" "".
(* 結果: "SVOOL" *)

(* 解密（Atbash 是對稱的） *)
Compute decrypt AtbashCipher "SVOOL" "".
(* 結果: "HELLO" *)
```

### 使用維吉尼亞密碼

```coq
(* 使用密鑰 "KEY" *)
Compute encrypt VigenereCipher "HELLO" "KEY".

(* 解密 *)
Compute encrypt VigenereCipher "RIJVS" "KEY".
```

### 使用工廠模式

```coq
(* 更簡潔的語法 *)
Compute encrypt_with Caesar "ATTACK" "13".
Compute encrypt_with Atbash "SECRET" "".
Compute encrypt_with ROT13 "COQPROOF" "".
```

## 創建新密碼

### 步驟 1: 定義加密函數

```coq
Definition my_shift_char (c : ascii) : ascii :=
  if is_alpha c then
    let pos := alpha_position c in
    position_to_alpha ((pos + 7) mod 26) (is_upper c)
  else
    c.

Definition my_encrypt (plaintext : string) (key : string) : string :=
  map_string my_shift_char plaintext.
```

### 步驟 2: 定義解密函數

```coq
Definition my_decrypt_char (c : ascii) : ascii :=
  if is_alpha c then
    let pos := alpha_position c in
    position_to_alpha ((pos + 19) mod 26) (is_upper c)  (* 26 - 7 = 19 *)
  else
    c.

Definition my_decrypt (ciphertext : string) (key : string) : string :=
  map_string my_decrypt_char ciphertext.
```

### 步驟 3: 創建密碼接口

```coq
Definition MyCipher : CipherInterface := {|
  cipher_name := "My Custom Cipher";
  encrypt := my_encrypt;
  decrypt := my_decrypt;
|}.
```

### 步驟 4: 測試

```coq
Compute encrypt MyCipher "HELLO" "".
Compute decrypt MyCipher (encrypt MyCipher "HELLO" "") "".
```

## 證明密碼性質

### 證明長度保持

```coq
Theorem my_cipher_preserves_length : forall plaintext key,
  string_length (encrypt MyCipher plaintext key) = string_length plaintext.
Proof.
  intros plaintext key.
  unfold encrypt, MyCipher. simpl.
  unfold my_encrypt.
  apply map_string_length.
Qed.
```

### 證明正確性

```coq
Theorem my_cipher_correctness : forall plaintext key,
  decrypt MyCipher (encrypt MyCipher plaintext key) key = plaintext.
Proof.
  intros plaintext key.
  unfold encrypt, decrypt, MyCipher. simpl.
  (* 這裡需要更詳細的證明 *)
  admit.
Admitted.
```

### 證明對稱性（如適用）

```coq
Theorem atbash_symmetric_proof :
  forall text key,
    encrypt AtbashCipher text key = decrypt AtbashCipher text key.
Proof.
  intros text key.
  unfold encrypt, decrypt, AtbashCipher. simpl.
  reflexivity.
Qed.
```

## 高級主題

### 組合密碼

```coq
(* 創建組合密碼 *)
Definition double_caesar := compose_ciphers CaesarCipher CaesarCipher.

(* 使用組合密碼 *)
Compute encrypt double_caesar "HELLO" "3".

(* 證明組合密碼的正確性 *)
Theorem double_caesar_correct : forall plaintext key,
  decrypt double_caesar (encrypt double_caesar plaintext key) key = plaintext.
Proof.
  intros plaintext key.
  apply composed_cipher_correctness.
  (* 需要證明 CaesarCipher 的正確性 *)
  admit.
  admit.
Admitted.
```

### 密碼強度分析

```coq
(* 定義可能的密鑰空間 *)
Definition key_space (ci : CipherInterface) : nat :=
  match ci with
  | CaesarCipher => 26  (* 26 種可能的移位 *)
  | AtbashCipher => 1   (* 無密鑰 *)
  | ROT13Cipher => 1    (* 固定移位 *)
  | _ => 0              (* 其他情況 *)
  end.

(* 分析密碼強度 *)
Definition is_strong (ci : CipherInterface) : bool :=
  key_space ci >? 1000.
```

### 頻率分析模擬

```coq
(* 計算字母出現次數 *)
Fixpoint count_char (s : string) (target : ascii) : nat :=
  match s with
  | EmptyString => 0
  | String c s' =>
      if Ascii.eqb c target then
        S (count_char s' target)
      else
        count_char s' target
  end.

(* 分析加密文本 *)
Definition analyze_ciphertext (ciphertext : string) : list (ascii * nat) :=
  (* 返回字母頻率列表 *)
  [].  (* 簡化實現 *)
```

### 暴力破解模擬

```coq
(* 嘗試所有可能的密鑰 *)
Fixpoint brute_force_caesar (ciphertext : string) (max_key : nat) : list string :=
  match max_key with
  | 0 => []
  | S k =>
      let key := string_of_nat k in
      let attempt := decrypt CaesarCipher ciphertext key in
      attempt :: brute_force_caesar ciphertext k
  end.
```

## 練習

### 練習 1: 實現反向密碼

創建一個將文本反轉的密碼。

```coq
(* 提示: 需要實現 reverse_string 函數 *)
Fixpoint reverse_string (s : string) : string :=
  match s with
  | EmptyString => EmptyString
  | String c s' => (* 你的代碼 *)
  end.
```

### 練習 2: 證明 ROT13 的對合性

證明 ROT13 應用兩次返回原文。

```coq
Theorem rot13_involutive : forall s,
  encrypt ROT13Cipher (encrypt ROT13Cipher s "") "" = s.
Proof.
  (* 你的證明 *)
Admitted.
```

### 練習 3: 實現 Hill 密碼

實現基於矩陣運算的 Hill 密碼。

### 練習 4: 密碼組合

證明三個 ROT13 等價於一個 ROT13（因為對合性）。

## 資源

- [Coq 官方文檔](https://coq.inria.fr/documentation)
- [Software Foundations](https://softwarefoundations.cis.upenn.edu/)
- [Certified Programming with Dependent Types](http://adam.chlipala.net/cpdt/)
- [密碼學基礎](https://en.wikipedia.org/wiki/Cryptography)

## 下一步

1. 探索 `Examples.v` 中的更多示例
2. 閱讀 `CipherProperties.v` 學習如何證明密碼性質
3. 嘗試實現自己的密碼算法
4. 為現有密碼添加更多證明

祝學習愉快！


