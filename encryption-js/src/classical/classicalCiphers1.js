import { Cipher } from '../Cipher.js';

/**
 * 1. 凱撒密碼 (Caesar Cipher)
 */
export class CaesarCipher extends Cipher {
  constructor() {
    super('Caesar Cipher');
  }

  encrypt(plaintext, key) {
    const shift = parseInt(key);
    return plaintext.split('').map(char => {
      if (/[a-zA-Z]/.test(char)) {
        const offset = char === char.toUpperCase() ? 65 : 97;
        return String.fromCharCode(((char.charCodeAt(0) - offset + shift) % 26) + offset);
      }
      return char;
    }).join('');
  }

  decrypt(ciphertext, key) {
    const shift = -parseInt(key);
    return this.encrypt(ciphertext, shift.toString());
  }
}

/**
 * 2. Atbash 密碼
 */
export class AtbashCipher extends Cipher {
  constructor() {
    super('Atbash Cipher');
  }

  encrypt(plaintext, key) {
    return plaintext.split('').map(char => {
      if (/[A-Z]/.test(char)) {
        return String.fromCharCode(90 - (char.charCodeAt(0) - 65));
      } else if (/[a-z]/.test(char)) {
        return String.fromCharCode(122 - (char.charCodeAt(0) - 97));
      }
      return char;
    }).join('');
  }

  decrypt(ciphertext, key) {
    return this.encrypt(ciphertext, key); // Atbash is symmetric
  }
}

/**
 * 3. ROT13 密碼
 */
export class ROT13Cipher extends Cipher {
  constructor() {
    super('ROT13 Cipher');
  }

  encrypt(plaintext, key) {
    return plaintext.split('').map(char => {
      if (/[a-zA-Z]/.test(char)) {
        const offset = char === char.toUpperCase() ? 65 : 97;
        return String.fromCharCode(((char.charCodeAt(0) - offset + 13) % 26) + offset);
      }
      return char;
    }).join('');
  }

  decrypt(ciphertext, key) {
    return this.encrypt(ciphertext, key); // ROT13 is symmetric
  }
}

/**
 * 4. 維吉尼亞密碼 (Vigenère Cipher)
 */
export class VigenereCipher extends Cipher {
  constructor() {
    super('Vigenère Cipher');
  }

  encrypt(plaintext, key) {
    key = key.toUpperCase();
    let keyIndex = 0;
    return plaintext.split('').map(char => {
      if (/[a-zA-Z]/.test(char)) {
        const offset = char === char.toUpperCase() ? 65 : 97;
        const shift = key.charCodeAt(keyIndex % key.length) - 65;
        const result = String.fromCharCode(((char.charCodeAt(0) - offset + shift) % 26) + offset);
        keyIndex++;
        return result;
      }
      return char;
    }).join('');
  }

  decrypt(ciphertext, key) {
    key = key.toUpperCase();
    let keyIndex = 0;
    return ciphertext.split('').map(char => {
      if (/[a-zA-Z]/.test(char)) {
        const offset = char === char.toUpperCase() ? 65 : 97;
        const shift = key.charCodeAt(keyIndex % key.length) - 65;
        const result = String.fromCharCode(((char.charCodeAt(0) - offset - shift + 26) % 26) + offset);
        keyIndex++;
        return result;
      }
      return char;
    }).join('');
  }
}

/**
 * 5. Playfair 密碼
 */
export class PlayfairCipher extends Cipher {
  constructor() {
    super('Playfair Cipher');
  }

  buildMatrix(key) {
    key = key.toUpperCase().replace(/J/g, 'I');
    const seen = new Set();
    let matrixString = '';

    for (const char of key) {
      if (/[A-Z]/.test(char) && !seen.has(char)) {
        seen.add(char);
        matrixString += char;
      }
    }

    for (let i = 65; i <= 90; i++) {
      const char = String.fromCharCode(i);
      if (char !== 'J' && !seen.has(char)) {
        matrixString += char;
      }
    }

    const matrix = [];
    for (let i = 0; i < 5; i++) {
      matrix[i] = matrixString.substring(i * 5, i * 5 + 5).split('');
    }
    return matrix;
  }

  findPosition(matrix, char) {
    for (let row = 0; row < 5; row++) {
      for (let col = 0; col < 5; col++) {
        if (matrix[row][col] === char) {
          return { row, col };
        }
      }
    }
    return { row: 0, col: 0 };
  }

  encrypt(plaintext, key) {
    const matrix = this.buildMatrix(key);
    plaintext = plaintext.toUpperCase().replace(/J/g, 'I').replace(/\s/g, '');
    let result = '';

    for (let i = 0; i < plaintext.length; i += 2) {
      let a = plaintext[i];
      let b = i + 1 < plaintext.length ? plaintext[i + 1] : 'X';
      if (a === b) b = 'X';

      const posA = this.findPosition(matrix, a);
      const posB = this.findPosition(matrix, b);

      if (posA.row === posB.row) {
        result += matrix[posA.row][(posA.col + 1) % 5];
        result += matrix[posB.row][(posB.col + 1) % 5];
      } else if (posA.col === posB.col) {
        result += matrix[(posA.row + 1) % 5][posA.col];
        result += matrix[(posB.row + 1) % 5][posB.col];
      } else {
        result += matrix[posA.row][posB.col];
        result += matrix[posB.row][posA.col];
      }
    }
    return result;
  }

  decrypt(ciphertext, key) {
    const matrix = this.buildMatrix(key);
    let result = '';

    for (let i = 0; i < ciphertext.length; i += 2) {
      const a = ciphertext[i];
      const b = ciphertext[i + 1];

      const posA = this.findPosition(matrix, a);
      const posB = this.findPosition(matrix, b);

      if (posA.row === posB.row) {
        result += matrix[posA.row][(posA.col + 4) % 5];
        result += matrix[posB.row][(posB.col + 4) % 5];
      } else if (posA.col === posB.col) {
        result += matrix[(posA.row + 4) % 5][posA.col];
        result += matrix[(posB.row + 4) % 5][posB.col];
      } else {
        result += matrix[posA.row][posB.col];
        result += matrix[posB.row][posA.col];
      }
    }
    return result;
  }
}

/**
 * 6. 簡單替換密碼 (Simple Substitution Cipher)
 */
export class SubstitutionCipher extends Cipher {
  constructor() {
    super('Substitution Cipher');
  }

  encrypt(plaintext, key) {
    if (key.length !== 26) throw new Error('Key must be 26 characters');
    
    return plaintext.split('').map(char => {
      if (/[A-Z]/.test(char)) {
        return key[char.charCodeAt(0) - 65].toUpperCase();
      } else if (/[a-z]/.test(char)) {
        return key[char.charCodeAt(0) - 97].toLowerCase();
      }
      return char;
    }).join('');
  }

  decrypt(ciphertext, key) {
    if (key.length !== 26) throw new Error('Key must be 26 characters');
    
    const reverseKey = new Array(26);
    for (let i = 0; i < 26; i++) {
      reverseKey[key.toUpperCase().charCodeAt(i) - 65] = String.fromCharCode(65 + i);
    }
    
    return this.encrypt(ciphertext, reverseKey.join(''));
  }
}

/**
 * 7. 轉置密碼 (Transposition Cipher)
 */
export class TranspositionCipher extends Cipher {
  constructor() {
    super('Transposition Cipher');
  }

  encrypt(plaintext, key) {
    const keyLength = parseInt(key);
    let result = '';
    
    for (let i = 0; i < keyLength; i++) {
      for (let j = i; j < plaintext.length; j += keyLength) {
        result += plaintext[j];
      }
    }
    return result;
  }

  decrypt(ciphertext, key) {
    const keyLength = parseInt(key);
    const rows = Math.ceil(ciphertext.length / keyLength);
    const result = new Array(ciphertext.length);
    let index = 0;
    
    for (let col = 0; col < keyLength; col++) {
      for (let row = 0; row < rows; row++) {
        const pos = row * keyLength + col;
        if (pos < ciphertext.length && index < ciphertext.length) {
          result[pos] = ciphertext[index++];
        }
      }
    }
    return result.join('');
  }
}

/**
 * 8. 柵欄密碼 (Rail Fence Cipher)
 */
export class RailFenceCipher extends Cipher {
  constructor() {
    super('Rail Fence Cipher');
  }

  encrypt(plaintext, key) {
    const rails = parseInt(key);
    if (rails <= 1) return plaintext;

    const fence = Array.from({ length: rails }, () => []);
    let rail = 0;
    let down = true;

    for (const char of plaintext) {
      fence[rail].push(char);
      
      if (rail === 0) down = true;
      else if (rail === rails - 1) down = false;
      
      rail += down ? 1 : -1;
    }

    return fence.map(row => row.join('')).join('');
  }

  decrypt(ciphertext, key) {
    const rails = parseInt(key);
    if (rails <= 1) return ciphertext;

    const railLengths = new Array(rails).fill(0);
    let rail = 0;
    let down = true;

    for (let i = 0; i < ciphertext.length; i++) {
      railLengths[rail]++;
      
      if (rail === 0) down = true;
      else if (rail === rails - 1) down = false;
      
      rail += down ? 1 : -1;
    }

    const fence = [];
    let index = 0;
    for (let i = 0; i < rails; i++) {
      fence[i] = ciphertext.substring(index, index + railLengths[i]).split('');
      index += railLengths[i];
    }

    let result = '';
    rail = 0;
    down = true;
    const positions = new Array(rails).fill(0);

    for (let i = 0; i < ciphertext.length; i++) {
      result += fence[rail][positions[rail]++];
      
      if (rail === 0) down = true;
      else if (rail === rails - 1) down = false;
      
      rail += down ? 1 : -1;
    }

    return result;
  }
}

/**
 * 9. Scytale 密碼
 */
export class ScytaleCipher extends Cipher {
  constructor() {
    super('Scytale Cipher');
  }

  encrypt(plaintext, key) {
    const diameter = parseInt(key);
    const rows = Math.ceil(plaintext.length / diameter);
    
    while (plaintext.length < rows * diameter) {
      plaintext += 'X';
    }

    let result = '';
    for (let col = 0; col < diameter; col++) {
      for (let row = 0; row < rows; row++) {
        result += plaintext[row * diameter + col];
      }
    }
    return result;
  }

  decrypt(ciphertext, key) {
    const diameter = parseInt(key);
    const rows = ciphertext.length / diameter;
    
    let result = '';
    for (let row = 0; row < rows; row++) {
      for (let col = 0; col < diameter; col++) {
        result += ciphertext[col * rows + row];
      }
    }
    return result;
  }
}

/**
 * 10. Polybius Square 密碼
 */
export class PolybiusSquareCipher extends Cipher {
  constructor() {
    super('Polybius Square Cipher');
  }

  buildSquare() {
    const square = [];
    let index = 0;
    for (let i = 65; i <= 90; i++) {
      if (i === 74) continue; // Skip J
      const row = Math.floor(index / 5);
      const col = index % 5;
      if (!square[row]) square[row] = [];
      square[row][col] = String.fromCharCode(i);
      index++;
    }
    return square;
  }

  encrypt(plaintext, key) {
    const square = this.buildSquare();
    let result = '';
    
    for (const char of plaintext.toUpperCase()) {
      if (/[A-Z]/.test(char)) {
        const ch = char === 'J' ? 'I' : char;
        for (let row = 0; row < 5; row++) {
          for (let col = 0; col < 5; col++) {
            if (square[row][col] === ch) {
              result += (row + 1).toString() + (col + 1).toString();
              break;
            }
          }
        }
      } else {
        result += char;
      }
    }
    return result;
  }

  decrypt(ciphertext, key) {
    const square = this.buildSquare();
    let result = '';
    
    for (let i = 0; i < ciphertext.length; i += 2) {
      if (/\d/.test(ciphertext[i]) && i + 1 < ciphertext.length && /\d/.test(ciphertext[i + 1])) {
        const row = parseInt(ciphertext[i]) - 1;
        const col = parseInt(ciphertext[i + 1]) - 1;
        if (row >= 0 && row < 5 && col >= 0 && col < 5) {
          result += square[row][col];
        }
      }
    }
    return result;
  }
}

