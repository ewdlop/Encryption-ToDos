(** * Cipher - 密碼系統基礎定義 *)

Require Import Coq.Strings.Ascii.
Require Import Coq.Strings.String.
Require Import Coq.Lists.List.
Require Import Coq.Arith.Arith.
Require Import Coq.Arith.EqNat.
Require Import Coq.omega.Omega.
Import ListNotations.

(** ** 基礎類型定義 *)

(** 密碼接口 *)
Record CipherInterface := {
  cipher_name : string;
  encrypt : string -> string -> string;  (* plaintext -> key -> ciphertext *)
  decrypt : string -> string -> string;  (* ciphertext -> key -> plaintext *)
}.

(** ** ASCII 字符輔助函數 *)

(** 判斷是否為大寫字母 *)
Definition is_upper (c : ascii) : bool :=
  (65 <=? nat_of_ascii c) && (nat_of_ascii c <=? 90).

(** 判斷是否為小寫字母 *)
Definition is_lower (c : ascii) : bool :=
  (97 <=? nat_of_ascii c) && (nat_of_ascii c <=? 122).

(** 判斷是否為字母 *)
Definition is_alpha (c : ascii) : bool :=
  is_upper c || is_lower c.

(** 將字符轉換為大寫 *)
Definition to_upper (c : ascii) : ascii :=
  if is_lower c then
    ascii_of_nat (nat_of_ascii c - 32)
  else
    c.

(** 將字符轉換為小寫 *)
Definition to_lower (c : ascii) : ascii :=
  if is_upper c then
    ascii_of_nat (nat_of_ascii c + 32)
  else
    c.

(** 字母表位置 (A/a = 0, B/b = 1, ..., Z/z = 25) *)
Definition alpha_position (c : ascii) : nat :=
  if is_upper c then
    nat_of_ascii c - 65
  else if is_lower c then
    nat_of_ascii c - 97
  else
    0.

(** 從位置獲取字母 (保持大小寫) *)
Definition position_to_alpha (pos : nat) (is_upper_case : bool) : ascii :=
  let normalized := pos mod 26 in
  if is_upper_case then
    ascii_of_nat (normalized + 65)
  else
    ascii_of_nat (normalized + 97).

(** ** 字符串處理函數 *)

(** 映射字符串中的每個字符 *)
Fixpoint map_string (f : ascii -> ascii) (s : string) : string :=
  match s with
  | EmptyString => EmptyString
  | String c s' => String (f c) (map_string f s')
  end.

(** 獲取字符串長度 *)
Fixpoint string_length (s : string) : nat :=
  match s with
  | EmptyString => 0
  | String _ s' => S (string_length s')
  end.

(** 從字符串獲取第 n 個字符 *)
Fixpoint nth_char (s : string) (n : nat) (default : ascii) : ascii :=
  match s, n with
  | EmptyString, _ => default
  | String c _, 0 => c
  | String _ s', S n' => nth_char s' n' default
  end.

(** 過濾字符串中的字母 *)
Fixpoint filter_alpha (s : string) : string :=
  match s with
  | EmptyString => EmptyString
  | String c s' =>
      if is_alpha c then
        String c (filter_alpha s')
      else
        filter_alpha s'
  end.

(** 將字符串轉換為大寫 *)
Definition string_to_upper (s : string) : string :=
  map_string to_upper s.

(** 將字符串轉換為小寫 *)
Definition string_to_lower (s : string) : string :=
  map_string to_lower s.

(** ** 數學輔助函數 *)

(** 模運算 (處理負數) *)
Definition mod_char (n : Z) (m : nat) : nat :=
  Z.to_nat (Z.modulo n (Z.of_nat m)).

(** 將整數轉換為正整數 *)
Definition int_to_nat (n : Z) : nat :=
  if (n <? 0)%Z then 0 else Z.to_nat n.

(** ** 基本證明引理 *)

(** 字母位置總是小於 26 *)
Lemma alpha_position_bound : forall c,
  is_alpha c = true -> alpha_position c < 26.
Proof.
  intros c H.
  unfold alpha_position.
  unfold is_alpha in H.
  destruct (is_upper c) eqn:Hupper.
  - unfold is_upper in Hupper.
    apply Bool.andb_true_iff in Hupper.
    destruct Hupper as [H1 H2].
    apply Nat.leb_le in H1.
    apply Nat.leb_le in H2.
    omega.
  - destruct (is_lower c) eqn:Hlower.
    + unfold is_lower in Hlower.
      apply Bool.andb_true_iff in Hlower.
      destruct Hlower as [H1 H2].
      apply Nat.leb_le in H1.
      apply Nat.leb_le in H2.
      omega.
    + simpl in H. discriminate.
Qed.

(** 映射字符串長度保持不變 *)
Lemma map_string_length : forall f s,
  string_length (map_string f s) = string_length s.
Proof.
  intros f s.
  induction s as [| c s' IH].
  - reflexivity.
  - simpl. rewrite IH. reflexivity.
Qed.


