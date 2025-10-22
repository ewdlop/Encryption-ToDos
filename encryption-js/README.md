# Encryption Ciphers - JavaScript Implementation

實現了 **200+ 種密碼算法**的 JavaScript 庫，涵蓋從古典密碼到現代加密技術。

## 特點

- ✅ **200+ 種密碼算法**：包括經典密碼和現代加密算法
- ✅ **統一接口**：所有密碼算法實現相同的接口
- ✅ **工廠模式**：方便地訪問和管理所有密碼算法
- ✅ **完整測試**：使用 Jest 進行全面測試
- ✅ **ES6 模塊**：使用現代 JavaScript 模塊系統
- ✅ **TypeScript 友好**：清晰的類結構

## 安裝

```bash
npm install
```

## 使用方法

### 基本使用

```javascript
import { CaesarCipher, AESCipher, cipherFactory } from './src/index.js';

// 使用特定密碼
const caesar = new CaesarCipher();
const encrypted = caesar.encrypt('HELLO', '3');
const decrypted = caesar.decrypt(encrypted, '3');
console.log(encrypted); // KHOOR
console.log(decrypted); // HELLO

// 使用 AES
const aes = new AESCipher();
const encrypted = aes.encrypt('Secret Message', 'my-key');
const decrypted = aes.decrypt(encrypted, 'my-key');
```

### 使用工廠模式

```javascript
import { cipherFactory } from './src/index.js';

// 獲取密碼總數
console.log(cipherFactory.getCipherCount()); // 200+

// 獲取特定密碼
const cipher = cipherFactory.getCipher('Vigenère Cipher');
const result = cipher.encrypt('ATTACK AT DAWN', 'SECRET');

// 列出所有密碼
const allNames = cipherFactory.getAllCipherNames();
console.log(allNames);
```

## 運行測試

```bash
npm test
```

## 包含的密碼算法

### 經典密碼 (100+)

1. Caesar Cipher（凱撒密碼）
2. Atbash Cipher
3. ROT13
4. Vigenère Cipher（維吉尼亞密碼）
5. Playfair Cipher
6. Substitution Cipher
7. Transposition Cipher
8. Rail Fence Cipher
9. Scytale Cipher
10. Polybius Square
...以及更多

### 現代密碼 (100+)

1. AES（高級加密標準）
2. DES
3. 3DES
4. RC4
5. Rabbit
6. ChaCha20
7. RSA
8. HMAC
9. SHA256
...以及更多

## 項目結構

```
encryption-js/
├── src/
│   ├── Cipher.js                 # 基礎密碼類
│   ├── classical/                # 經典密碼實現
│   │   ├── classicalCiphers1.js
│   │   └── classicalCiphers2.js
│   ├── modern/                   # 現代密碼實現
│   │   └── modernCiphers.js
│   ├── CipherFactory.js          # 密碼工廠
│   └── index.js                  # 主入口
├── tests/
│   └── ciphers.test.js           # 測試文件
├── package.json
├── jest.config.js
└── README.md
```

## API 文檔

### Cipher 類

所有密碼算法都擴展自基礎 `Cipher` 類：

```javascript
class Cipher {
  constructor(name)
  encrypt(plaintext, key)  // 加密
  decrypt(ciphertext, key) // 解密
}
```

### CipherFactory

```javascript
cipherFactory.getCipher(name)         // 獲取指定密碼
cipherFactory.getAllCipherNames()     // 獲取所有密碼名稱
cipherFactory.getAllCiphers()         // 獲取所有密碼對象
cipherFactory.getCipherCount()        // 獲取密碼總數
cipherFactory.hasCipher(name)         // 檢查密碼是否存在
```

## 測試覆蓋

項目包含全面的測試：
- 工廠模式測試
- 經典密碼測試
- 現代密碼測試
- 集成測試
- 性能測試

## 安全警告

⚠️ **重要提示**：

1. 經典密碼僅供教育目的，不應用於實際安全應用
2. 部分現代算法實現經過簡化
3. 生產環境建議使用經過充分測試的專業加密庫

## 許可證

MIT

## 貢獻

歡迎提交 Issue 和 Pull Request！

