/**
 * 密碼算法基礎類
 */
export class Cipher {
  constructor(name) {
    this.name = name;
  }

  /**
   * 加密
   * @param {string} plaintext - 明文
   * @param {string} key - 密鑰
   * @returns {string} 密文
   */
  encrypt(plaintext, key) {
    throw new Error('encrypt() must be implemented');
  }

  /**
   * 解密
   * @param {string} ciphertext - 密文
   * @param {string} key - 密鑰
   * @returns {string} 明文
   */
  decrypt(ciphertext, key) {
    throw new Error('decrypt() must be implemented');
  }
}

