import {
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

import {
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

import {
  BaconCipher,
  TapCodeCipher,
  ReverseCipher,
  NullCipher,
  createAdditionalCiphers
} from './classical/classicalCiphers3.js';

import {
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

/**
 * 密碼工廠類 - 管理所有200+種密碼算法
 */
class CipherFactory {
  constructor() {
    this.ciphers = new Map();
    this.registerAllCiphers();
  }

  /**
   * 註冊所有密碼算法
   */
  registerAllCiphers() {
    // 註冊經典密碼算法 (1-20)
    this.registerCipher(new CaesarCipher());
    this.registerCipher(new AtbashCipher());
    this.registerCipher(new ROT13Cipher());
    this.registerCipher(new VigenereCipher());
    this.registerCipher(new PlayfairCipher());
    this.registerCipher(new SubstitutionCipher());
    this.registerCipher(new TranspositionCipher());
    this.registerCipher(new RailFenceCipher());
    this.registerCipher(new ScytaleCipher());
    this.registerCipher(new PolybiusSquareCipher());
    this.registerCipher(new AffineCipher());
    this.registerCipher(new BeaufortCipher());
    this.registerCipher(new AutokeyCipher());
    this.registerCipher(new RunningKeyCipher());
    this.registerCipher(new OneTimePadCipher());
    this.registerCipher(new XORCipher());
    this.registerCipher(new MorseCodeCipher());
    this.registerCipher(new ColumnarTranspositionCipher());
    this.registerCipher(new DoubleTranspositionCipher());
    this.registerCipher(new GronsfeldCipher());
    this.registerCipher(new KeywordCipher());
    this.registerCipher(new BaconCipher());
    this.registerCipher(new TapCodeCipher());
    this.registerCipher(new ReverseCipher());
    this.registerCipher(new NullCipher());

    // 註冊經典密碼變體 (25-100)
    const classicalVariants = createClassicalVariantCiphers();
    Object.values(classicalVariants).forEach(CipherClass => {
      this.registerCipher(new CipherClass());
    });

    // 註冊額外的密碼變體
    const additionalCiphers = createAdditionalCiphers();
    Object.values(additionalCiphers).forEach(CipherClass => {
      this.registerCipher(new CipherClass());
    });

    // 註冊現代密碼算法 (101-120)
    this.registerCipher(new AESCipher());
    this.registerCipher(new DESCipher());
    this.registerCipher(new TripleDESCipher());
    this.registerCipher(new RC4Cipher());
    this.registerCipher(new RabbitCipher());
    this.registerCipher(new ChaCha20Cipher());
    this.registerCipher(new RSACipher());
    this.registerCipher(new HMACCipher());
    this.registerCipher(new SHA256Cipher());

    // 註冊現代密碼變體 (121-200+)
    const modernVariants = createModernCipherVariants();
    Object.values(modernVariants).forEach(CipherClass => {
      if (typeof CipherClass === 'function') {
        this.registerCipher(new CipherClass());
      }
    });
  }

  /**
   * 註冊單個密碼算法
   */
  registerCipher(cipher) {
    if (!this.ciphers.has(cipher.name)) {
      this.ciphers.set(cipher.name, cipher);
    }
  }

  /**
   * 根據名稱獲取密碼算法
   */
  getCipher(name) {
    return this.ciphers.get(name) || null;
  }

  /**
   * 獲取所有密碼算法名稱
   */
  getAllCipherNames() {
    return Array.from(this.ciphers.keys());
  }

  /**
   * 獲取所有密碼算法
   */
  getAllCiphers() {
    return Array.from(this.ciphers.values());
  }

  /**
   * 獲取密碼算法總數
   */
  getCipherCount() {
    return this.ciphers.size;
  }

  /**
   * 檢查是否存在指定名稱的密碼算法
   */
  hasCipher(name) {
    return this.ciphers.has(name);
  }
}

// 導出單例實例
export const cipherFactory = new CipherFactory();
export default cipherFactory;

