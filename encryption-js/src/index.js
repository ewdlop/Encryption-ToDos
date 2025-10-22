/**
 * Encryption Ciphers Library
 * 實現200+種密碼算法的JavaScript庫
 */

export { Cipher } from './Cipher.js';

// 經典密碼算法
export {
  CaesarCipher,
  AtbashCipher,
  ROT13Cipher,
  VigenereCipher,
  PlayfairCipher,
  SubstitutionCipher,
  TranspositionCipher,
  RailFenceCipher,
  ScytaleCipher,
  PolybiusSquareCipher
} from './classical/classicalCiphers1.js';

export {
  AffineCipher,
  BeaufortCipher,
  AutokeyCipher,
  RunningKeyCipher,
  OneTimePadCipher,
  XORCipher,
  MorseCodeCipher,
  ColumnarTranspositionCipher,
  DoubleTranspositionCipher,
  GronsfeldCipher,
  KeywordCipher,
  createClassicalVariantCiphers
} from './classical/classicalCiphers2.js';

export {
  BaconCipher,
  TapCodeCipher,
  ReverseCipher,
  NullCipher,
  createAdditionalCiphers
} from './classical/classicalCiphers3.js';

// 現代密碼算法
export {
  AESCipher,
  DESCipher,
  TripleDESCipher,
  RC4Cipher,
  RabbitCipher,
  ChaCha20Cipher,
  RSACipher,
  HMACCipher,
  SHA256Cipher,
  createModernCipherVariants
} from './modern/modernCiphers.js';

// 密碼工廠
export { cipherFactory, default as CipherFactory } from './CipherFactory.js';

