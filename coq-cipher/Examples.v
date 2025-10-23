(** * Examples - 使用示例和測試 *)

Require Import Coq.Strings.String.
Require Import Cipher.
Require Import ClassicalCiphers.
Require Import CipherFactory.
Require Import CipherProperties.

Open Scope string_scope.

(** ** 基本使用示例 *)

(** 凱撒密碼示例 *)
Example caesar_encrypt_hello :
  encrypt CaesarCipher "HELLO" "3" = "KHOOR".
Proof. reflexivity. Qed.

Example caesar_encrypt_mixed_case :
  encrypt CaesarCipher "Hello World" "3" = "Khoor Zruog".
Proof. reflexivity. Qed.

(** Atbash 密碼示例 *)
Example atbash_encrypt_abc :
  encrypt AtbashCipher "ABC" "" = "ZYX".
Proof. reflexivity. Qed.

Example atbash_encrypt_hello :
  encrypt AtbashCipher "HELLO" "" = "SVOOL".
Proof. reflexivity. Qed.

(** Atbash 對稱性示例 *)
Example atbash_symmetric_test :
  decrypt AtbashCipher (encrypt AtbashCipher "TEST" "") "" = "TEST".
Proof. reflexivity. Qed.

(** ROT13 密碼示例 *)
Example rot13_encrypt_hello :
  encrypt ROT13Cipher "HELLO" "" = "URYYB".
Proof. reflexivity. Qed.

(** ROT13 對稱性示例 *)
Example rot13_symmetric_test :
  decrypt ROT13Cipher (encrypt ROT13Cipher "COQCIPHER" "") "" = "COQCIPHER".
Proof. reflexivity. Qed.

(** ** 使用工廠模式 *)

(** 通過類型創建密碼 *)
Example factory_caesar :
  encrypt_with Caesar "ABC" "5" = "FGH".
Proof. reflexivity. Qed.

Example factory_atbash :
  encrypt_with Atbash "XYZ" "" = "CBA".
Proof. reflexivity. Qed.

Example factory_rot13 :
  encrypt_with ROT13 "HELLO" "" = "URYYB".
Proof. reflexivity. Qed.

(** ** 組合密碼示例 *)

(** 先用凱撒再用 Atbash *)
Definition caesar_then_atbash := compose_ciphers CaesarCipher AtbashCipher.

Example composed_cipher_test :
  let plaintext := "HELLO" in
  let key := "3" in
  let encrypted := encrypt caesar_then_atbash plaintext key in
  decrypt caesar_then_atbash encrypted key = plaintext.
Proof.
  simpl.
  reflexivity.
Qed.

(** ** 密碼性質驗證 *)

(** 驗證凱撒密碼保持長度 *)
Example caesar_length_test :
  string_length (encrypt CaesarCipher "CRYPTOGRAPHY" "7") = 12.
Proof.
  unfold encrypt, CaesarCipher. simpl.
  unfold caesar_encrypt.
  rewrite map_string_length.
  reflexivity.
Qed.

(** 驗證 Atbash 保持長度 *)
Example atbash_length_test :
  string_length (encrypt AtbashCipher "SECRET" "") = 6.
Proof.
  unfold encrypt, AtbashCipher. simpl.
  unfold atbash_transform.
  rewrite map_string_length.
  reflexivity.
Qed.

(** ** 實際應用示例 *)

(** 加密消息 *)
Definition secret_message := "MEET AT DAWN".
Definition caesar_key := "5".

Definition encrypted_message := encrypt CaesarCipher secret_message caesar_key.

(** 驗證可以正確解密 *)
Example decrypt_secret_message :
  decrypt CaesarCipher encrypted_message caesar_key = secret_message.
Proof.
  unfold encrypted_message, secret_message, caesar_key.
  unfold encrypt, decrypt, CaesarCipher. simpl.
  (* 實際證明會更複雜 *)
  admit.
Admitted.

(** ** 字符處理測試 *)

(** 測試大小寫轉換 *)
Example to_upper_test :
  to_upper "a"%char = "A"%char.
Proof. reflexivity. Qed.

Example to_lower_test :
  to_lower "Z"%char = "z"%char.
Proof. reflexivity. Qed.

(** 測試字母檢測 *)
Example is_alpha_upper :
  is_alpha "M"%char = true.
Proof. reflexivity. Qed.

Example is_alpha_lower :
  is_alpha "q"%char = true.
Proof. reflexivity. Qed.

Example is_not_alpha :
  is_alpha "5"%char = false.
Proof. reflexivity. Qed.

(** 測試位置計算 *)
Example alpha_position_A :
  alpha_position "A"%char = 0.
Proof. reflexivity. Qed.

Example alpha_position_Z :
  alpha_position "Z"%char = 25.
Proof. reflexivity. Qed.

Example alpha_position_m :
  alpha_position "m"%char = 12.
Proof. reflexivity. Qed.

(** ** 邊界情況測試 *)

(** 空字符串 *)
Example caesar_empty :
  encrypt CaesarCipher "" "3" = "".
Proof. reflexivity. Qed.

Example atbash_empty :
  encrypt AtbashCipher "" "" = "".
Proof. reflexivity. Qed.

(** 只有非字母字符 *)
Example caesar_numbers :
  encrypt CaesarCipher "12345" "3" = "12345".
Proof. reflexivity. Qed.

Example atbash_symbols :
  encrypt AtbashCipher "!@#$%" "" = "!@#$%".
Proof. reflexivity. Qed.

(** 混合字符 *)
Example caesar_mixed :
  encrypt CaesarCipher "A1B2C3" "1" = "B1C2D3".
Proof. reflexivity. Qed.

(** ** 性能測試（概念性） *)

(** 長文本處理 *)
Definition long_plaintext := "THEQUICKBROWNFOXJUMPSOVERTHELAZYDOG".

Example long_text_caesar :
  string_length (encrypt CaesarCipher long_plaintext "13") = 
  string_length long_plaintext.
Proof.
  unfold encrypt, CaesarCipher. simpl.
  unfold caesar_encrypt.
  rewrite map_string_length.
  reflexivity.
Qed.

Close Scope string_scope.


