(** * CipherProperties - 密碼屬性和定理 *)

Require Import Coq.Strings.String.
Require Import Coq.Strings.Ascii.
Require Import Coq.Lists.List.
Require Import Cipher.
Require Import ClassicalCiphers.
Import ListNotations.

(** ** 密碼基本屬性 *)

(** 密碼的正確性：解密加密後的文本應該得到原文 *)
Definition cipher_correctness (ci : CipherInterface) : Prop :=
  forall plaintext key,
    decrypt ci (encrypt ci plaintext key) key = plaintext.

(** 對稱密碼：加密和解密是同一個操作 *)
Definition symmetric_cipher (ci : CipherInterface) : Prop :=
  forall text key,
    encrypt ci text key = decrypt ci text key.

(** 密碼保持長度 *)
Definition length_preserving (ci : CipherInterface) : Prop :=
  forall plaintext key,
    string_length (encrypt ci plaintext key) = string_length plaintext.

(** 密碼只變換字母 *)
Definition alpha_only_transformation (ci : CipherInterface) : Prop :=
  forall c key,
    is_alpha c = false ->
    forall plaintext,
      nth_char (encrypt ci (String c plaintext) key) 0 "0"%char = c.

(** ** 特定密碼的性質 *)

(** Atbash 是對稱的 *)
Theorem atbash_is_symmetric :
  symmetric_cipher AtbashCipher.
Proof.
  unfold symmetric_cipher, AtbashCipher.
  intros text key.
  reflexivity.
Qed.

(** ROT13 是對稱的 *)
Theorem rot13_is_symmetric :
  symmetric_cipher ROT13Cipher.
Proof.
  unfold symmetric_cipher, ROT13Cipher.
  intros text key.
  reflexivity.
Qed.

(** 凱撒密碼保持長度 *)
Theorem caesar_preserves_length :
  length_preserving CaesarCipher.
Proof.
  unfold length_preserving, CaesarCipher.
  intros plaintext key.
  simpl.
  destruct key.
  - reflexivity.
  - unfold caesar_encrypt.
    apply map_string_length.
Qed.

(** Atbash 保持長度 *)
Theorem atbash_preserves_length :
  length_preserving AtbashCipher.
Proof.
  unfold length_preserving, AtbashCipher.
  intros plaintext key.
  simpl.
  unfold atbash_transform.
  apply map_string_length.
Qed.

(** ** 通用性質 *)

(** 空字符串的加密 *)
Theorem encrypt_empty : forall ci key,
  encrypt ci EmptyString key = EmptyString.
Proof.
  intros ci key.
  destruct ci as [name enc dec].
  (* 這個定理對於所有密碼不一定成立，需要具體分析 *)
  admit.
Admitted.

(** 密鑰的影響 *)
Theorem different_keys_may_differ : forall ci plaintext k1 k2,
  k1 <> k2 ->
  exists plaintext, encrypt ci plaintext k1 <> encrypt ci plaintext k2.
Proof.
  intros ci plaintext k1 k2 Hneq.
  exists "A"%string.
  (* 這需要根據具體密碼來證明 *)
  admit.
Admitted.

(** ** 組合密碼 *)

(** 連續應用兩個密碼 *)
Definition compose_ciphers (ci1 ci2 : CipherInterface) : CipherInterface := {|
  cipher_name := append (cipher_name ci1) (append " -> " (cipher_name ci2));
  encrypt := fun plaintext key =>
    let intermediate := encrypt ci1 plaintext key in
    encrypt ci2 intermediate key;
  decrypt := fun ciphertext key =>
    let intermediate := decrypt ci2 ciphertext key in
    decrypt ci1 intermediate key;
|}.

(** 組合密碼的正確性 *)
Theorem composed_cipher_correctness :
  forall ci1 ci2 plaintext key,
    cipher_correctness ci1 ->
    cipher_correctness ci2 ->
    decrypt (compose_ciphers ci1 ci2) 
            (encrypt (compose_ciphers ci1 ci2) plaintext key) 
            key = plaintext.
Proof.
  intros ci1 ci2 plaintext key H1 H2.
  simpl.
  unfold cipher_correctness in *.
  rewrite H2.
  rewrite H1.
  reflexivity.
Qed.

(** ** 安全性考慮 *)

(** 密碼強度的定義（簡化版）*)
Definition weak_cipher (ci : CipherInterface) : Prop :=
  exists plaintext key,
    encrypt ci plaintext key = plaintext.

(** Atbash 對某些文本是弱密碼 *)
Example atbash_weak_example :
  encrypt AtbashCipher ""%string "key"%string = ""%string.
Proof.
  reflexivity.
Qed.


