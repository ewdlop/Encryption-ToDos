import { describe, test, expect } from '@jest/globals';
import {
  CaesarCipher,
  AtbashCipher,
  ROT13Cipher,
  VigenereCipher,
  PlayfairCipher,
  SubstitutionCipher,
  RailFenceCipher,
  XORCipher,
  MorseCodeCipher,
  AffineCipher,
  BeaufortCipher,
  AESCipher,
  DESCipher,
  RC4Cipher,
  ChaCha20Cipher,
  HMACCipher,
  cipherFactory
} from '../src/index.js';

describe('Cipher Factory Tests', () => {
  test('should have at least 200 ciphers registered', () => {
    const count = cipherFactory.getCipherCount();
    expect(count).toBeGreaterThanOrEqual(200);
    console.log(`Total ciphers registered: ${count}`);
  });

  test('should get cipher by name', () => {
    const cipher = cipherFactory.getCipher('Caesar Cipher');
    expect(cipher).not.toBeNull();
    expect(cipher.name).toBe('Caesar Cipher');
  });

  test('should return all cipher names', () => {
    const names = cipherFactory.getAllCipherNames();
    expect(names.length).toBeGreaterThanOrEqual(200);
    expect(names).toContain('Caesar Cipher');
    expect(names).toContain('AES');
  });

  test('should check if cipher exists', () => {
    expect(cipherFactory.hasCipher('Caesar Cipher')).toBe(true);
    expect(cipherFactory.hasCipher('NonExistentCipher')).toBe(false);
  });
});

describe('Classical Cipher Tests', () => {
  test('Caesar Cipher encryption and decryption', () => {
    const cipher = new CaesarCipher();
    const plaintext = 'HELLO';
    const key = '3';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).toBe('KHOOR');
    expect(decrypted).toBe(plaintext);
  });

  test('Atbash Cipher encryption and decryption', () => {
    const cipher = new AtbashCipher();
    const plaintext = 'HELLO';
    
    const encrypted = cipher.encrypt(plaintext, '');
    const decrypted = cipher.decrypt(encrypted, '');
    
    expect(encrypted).toBe('SVOOL');
    expect(decrypted).toBe(plaintext);
  });

  test('ROT13 Cipher encryption and decryption', () => {
    const cipher = new ROT13Cipher();
    const plaintext = 'HELLO';
    
    const encrypted = cipher.encrypt(plaintext, '');
    const decrypted = cipher.decrypt(encrypted, '');
    
    expect(encrypted).toBe('URYYB');
    expect(decrypted).toBe(plaintext);
  });

  test('Vigenère Cipher encryption and decryption', () => {
    const cipher = new VigenereCipher();
    const plaintext = 'HELLO';
    const key = 'KEY';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).not.toBe(plaintext);
    expect(decrypted).toBe(plaintext);
  });

  test('Playfair Cipher encryption and decryption', () => {
    const cipher = new PlayfairCipher();
    const plaintext = 'HELLO';
    const key = 'SECRET';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).not.toBe(plaintext);
    expect(decrypted.startsWith('HEL')).toBe(true);
  });

  test('Rail Fence Cipher encryption and decryption', () => {
    const cipher = new RailFenceCipher();
    const plaintext = 'HELLO WORLD';
    const key = '3';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).not.toBe(plaintext);
    expect(decrypted).toBe(plaintext);
  });

  test('XOR Cipher encryption and decryption', () => {
    const cipher = new XORCipher();
    const plaintext = 'HELLO';
    const key = 'KEY';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).not.toBe(plaintext);
    expect(decrypted).toBe(plaintext);
  });

  test('Morse Code Cipher encryption and decryption', () => {
    const cipher = new MorseCodeCipher();
    const plaintext = 'HELLO';
    
    const encrypted = cipher.encrypt(plaintext, '');
    const decrypted = cipher.decrypt(encrypted, '');
    
    expect(encrypted).toContain('.');
    expect(encrypted).toContain('-');
    expect(decrypted).toBe(plaintext);
  });

  test('Affine Cipher encryption and decryption', () => {
    const cipher = new AffineCipher();
    const plaintext = 'HELLO';
    const key = '5,8';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).not.toBe(plaintext);
    expect(decrypted).toBe(plaintext);
  });

  test('Beaufort Cipher encryption and decryption', () => {
    const cipher = new BeaufortCipher();
    const plaintext = 'HELLO';
    const key = 'KEY';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).not.toBe(plaintext);
    expect(decrypted).toBe(plaintext);
  });
});

describe('Modern Cipher Tests', () => {
  test('AES Cipher encryption and decryption', () => {
    const cipher = new AESCipher();
    const plaintext = 'Hello, World!';
    const key = 'secret-key-123';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).not.toBe(plaintext);
    expect(decrypted).toBe(plaintext);
  });

  test('DES Cipher encryption and decryption', () => {
    const cipher = new DESCipher();
    const plaintext = 'Hello, World!';
    const key = 'secret-key-123';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).not.toBe(plaintext);
    expect(decrypted).toBe(plaintext);
  });

  test('RC4 Cipher encryption and decryption', () => {
    const cipher = new RC4Cipher();
    const plaintext = 'Hello, World!';
    const key = 'secret-key';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).not.toBe(plaintext);
    expect(decrypted).toBe(plaintext);
  });

  test('ChaCha20 Cipher encryption and decryption', () => {
    const cipher = new ChaCha20Cipher();
    const plaintext = 'Hello, World!';
    const key = 'secret-key';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).not.toBe(plaintext);
    expect(decrypted).toBe(plaintext);
  });

  test('HMAC Cipher', () => {
    const cipher = new HMACCipher();
    const plaintext = 'Hello, World!';
    const key = 'secret-key';
    
    const encrypted = cipher.encrypt(plaintext, key);
    const decrypted = cipher.decrypt(encrypted, key);
    
    expect(encrypted).toContain(':');
    expect(decrypted).toBe(plaintext);
  });
});

describe('Integration Tests', () => {
  test('should encrypt and decrypt with multiple ciphers', () => {
    const testCases = [
      { cipher: new CaesarCipher(), plaintext: 'TEST', key: '5' },
      { cipher: new VigenereCipher(), plaintext: 'TEST', key: 'KEY' },
      { cipher: new AESCipher(), plaintext: 'TEST', key: 'secret' },
      { cipher: new ChaCha20Cipher(), plaintext: 'TEST', key: 'secret' }
    ];

    testCases.forEach(({ cipher, plaintext, key }) => {
      const encrypted = cipher.encrypt(plaintext, key);
      const decrypted = cipher.decrypt(encrypted, key);
      
      expect(encrypted).not.toBeNull();
      expect(decrypted).not.toBeNull();
    });
  });

  test('should handle empty strings gracefully', () => {
    const cipher = new CaesarCipher();
    const result = cipher.encrypt('', '3');
    expect(result).toBe('');
  });

  test('should handle special characters', () => {
    const cipher = new CaesarCipher();
    const plaintext = 'Hello, World! 123';
    const encrypted = cipher.encrypt(plaintext, '3');
    const decrypted = cipher.decrypt(encrypted, '3');
    
    expect(decrypted).toBe(plaintext);
  });
});

describe('Performance Tests', () => {
  test('should register all ciphers quickly', () => {
    const start = Date.now();
    const count = cipherFactory.getCipherCount();
    const duration = Date.now() - start;
    
    expect(count).toBeGreaterThanOrEqual(200);
    expect(duration).toBeLessThan(100); // Should be fast
  });

  test('should access ciphers quickly', () => {
    const start = Date.now();
    
    for (let i = 0; i < 1000; i++) {
      cipherFactory.getCipher('Caesar Cipher');
    }
    
    const duration = Date.now() - start;
    expect(duration).toBeLessThan(50); // Should be very fast
  });
});


