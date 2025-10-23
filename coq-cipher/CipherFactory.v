(** * CipherFactory - 密碼工廠 *)

Require Import Coq.Strings.String.
Require Import Coq.Lists.List.
Require Import Cipher.
Require Import ClassicalCiphers.
Import ListNotations.

(** ** 密碼類型枚舉 *)

Inductive CipherType : Type :=
  | Caesar
  | Atbash
  | ROT13
  | Vigenere
  | Affine.

(** ** 密碼工廠 *)

(** 從類型獲取密碼接口 *)
Definition get_cipher (ctype : CipherType) : CipherInterface :=
  match ctype with
  | Caesar => CaesarCipher
  | Atbash => AtbashCipher
  | ROT13 => ROT13Cipher
  | Vigenere => VigenereCipher
  | Affine => AffineCipher
  end.

(** 從名稱獲取密碼類型 *)
Definition cipher_type_from_name (name : string) : option CipherType :=
  if String.eqb name "caesar" then Some Caesar
  else if String.eqb name "atbash" then Some Atbash
  else if String.eqb name "rot13" then Some ROT13
  else if String.eqb name "vigenere" then Some Vigenere
  else if String.eqb name "affine" then Some Affine
  else None.

(** 從名稱獲取密碼接口 *)
Definition get_cipher_by_name (name : string) : option CipherInterface :=
  match cipher_type_from_name name with
  | Some ctype => Some (get_cipher ctype)
  | None => None
  end.

(** 所有可用的密碼列表 *)
Definition all_ciphers : list CipherInterface :=
  [ CaesarCipher
  ; AtbashCipher
  ; ROT13Cipher
  ; VigenereCipher
  ; AffineCipher
  ].

(** 所有密碼類型 *)
Definition all_cipher_types : list CipherType :=
  [ Caesar; Atbash; ROT13; Vigenere; Affine ].

(** ** 輔助函數 *)

(** 加密函數（帶類型） *)
Definition encrypt_with (ctype : CipherType) (plaintext : string) (key : string) : string :=
  let cipher := get_cipher ctype in
  encrypt cipher plaintext key.

(** 解密函數（帶類型） *)
Definition decrypt_with (ctype : CipherType) (ciphertext : string) (key : string) : string :=
  let cipher := get_cipher ctype in
  decrypt cipher ciphertext key.

(** ** 示例 *)

(** 測試凱撒密碼 *)
Example caesar_example :
  encrypt_with Caesar "Hello"%string "3"%string = "Khoor"%string.
Proof.
  reflexivity.
Qed.

(** 測試 Atbash 密碼 *)
Example atbash_example :
  encrypt_with Atbash "ABC"%string ""%string = "ZYX"%string.
Proof.
  reflexivity.
Qed.


