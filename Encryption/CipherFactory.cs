using Encryption.Classical;
using Encryption.Modern;

namespace Encryption
{
    /// <summary>
    /// 密碼算法工廠類，提供訪問所有200種密碼算法
    /// </summary>
    public static class CipherFactory
    {
        private static readonly Dictionary<string, ICipher> _ciphers = new Dictionary<string, ICipher>();

        static CipherFactory()
        {
            RegisterAllCiphers();
        }

        /// <summary>
        /// 註冊所有密碼算法
        /// </summary>
        private static void RegisterAllCiphers()
        {
            // 經典密碼算法
            RegisterCipher(new CaesarCipher());
            RegisterCipher(new AtbashCipher());
            RegisterCipher(new ROT13Cipher());
            RegisterCipher(new VigenereCipher());
            RegisterCipher(new PlayfairCipher());
            RegisterCipher(new SubstitutionCipher());
            RegisterCipher(new TranspositionCipher());
            RegisterCipher(new RailFenceCipher());
            RegisterCipher(new ScytaleCipher());
            RegisterCipher(new PolybiusSquareCipher());
            
            RegisterCipher(new ADFGVXCipher());
            RegisterCipher(new BifidCipher());
            RegisterCipher(new TrifidCipher());
            RegisterCipher(new FourSquareCipher());
            RegisterCipher(new HillCipher());
            RegisterCipher(new AffineCipher());
            RegisterCipher(new BeaufortCipher());
            RegisterCipher(new RunningKeyCipher());
            RegisterCipher(new AutokeyCipher());
            RegisterCipher(new ColumnarTranspositionCipher());
            
            RegisterCipher(new DoubleTranspositionCipher());
            RegisterCipher(new GronsfeldCipher());
            RegisterCipher(new PortaCipher());
            RegisterCipher(new AlbertiCipher());
            RegisterCipher(new TrithemiusCipher());
            RegisterCipher(new PigpenCipher());
            RegisterCipher(new MorseCodeCipher());
            RegisterCipher(new BaconCipher());
            RegisterCipher(new BookCipher());
            RegisterCipher(new TapCodeCipher());
            RegisterCipher(new OneTimePadCipher());
            RegisterCipher(new NihilistCipher());
            RegisterCipher(new KeywordCipher());
            
            RegisterCipher(new StraddlingCheckerboardCipher());
            RegisterCipher(new SolitaireCipher());
            RegisterCipher(new ChaocipherImpl());
            RegisterCipher(new JeffersonDiskCipher());
            RegisterCipher(new RouteCipher());
            RegisterCipher(new NomenclatorCipher());
            RegisterCipher(new HomophonicSubstitutionCipher());
            RegisterCipher(new PermutationCipher());
            RegisterCipher(new RedefenceCipher());
            RegisterCipher(new RotationCipher());
            RegisterCipher(new SlideCipher());
            RegisterCipher(new TabulaRectaCipher());
            RegisterCipher(new TwoSquareCipher());
            RegisterCipher(new VICCipher());
            RegisterCipher(new ZigzagCipher());
            RegisterCipher(new ADFGXCipher());
            RegisterCipher(new AlphabeticalSubstitution());
            RegisterCipher(new BazeriesCylinder());
            RegisterCipher(new CheckerboardCipher());
            RegisterCipher(new CodesAndNomenclators());
            RegisterCipher(new DancingMenCipher());
            RegisterCipher(new DigrafidCipher());
            RegisterCipher(new FractionatedMorseCipher());
            RegisterCipher(new FreemasonsCipher());
            RegisterCipher(new GoldBugCipher());
            RegisterCipher(new GrilleCipher());
            RegisterCipher(new HandycipherImpl());
            RegisterCipher(new KamaSutraCipher());
            RegisterCipher(new MaryQueenOfScotsCipher());
            RegisterCipher(new MyszkowskiTransposition());
            RegisterCipher(new PlayfairVariants());
            RegisterCipher(new PolybiusVariants());
            RegisterCipher(new QuagmireCipher());
            RegisterCipher(new RosicrucianCipher());
            RegisterCipher(new RotateCipher());
            RegisterCipher(new RunningKeyVariant());
            RegisterCipher(new SlidefairCipher());
            RegisterCipher(new SyllabaryCipher());
            RegisterCipher(new TemplarCipher());
            RegisterCipher(new TrithemiusAveMaria());
            RegisterCipher(new TurningGrille());
            RegisterCipher(new VernamCipher());
            
            RegisterCipher(new EnigmaMachine());
            RegisterCipher(new LorenzCipher());
            RegisterCipher(new NavajoCode());
            RegisterCipher(new HebernRotor());
            RegisterCipher(new M209Cipher());
            RegisterCipher(new PurpleCipher());
            RegisterCipher(new SIGABACipher());
            RegisterCipher(new TypexCipher());
            RegisterCipher(new KryptosCipher());
            RegisterCipher(new BealeCiphers());
            RegisterCipher(new DorabellaCipher());
            RegisterCipher(new GreatCipher());
            RegisterCipher(new CodexSeraphinianus());
            RegisterCipher(new Rasterschlussel44());
            RegisterCipher(new Reservehandverfahren());
            RegisterCipher(new Satzbau());
            RegisterCipher(new SpreadSpectrum());
            RegisterCipher(new SimpleCaesar1());
            RegisterCipher(new SimpleCaesar2());
            RegisterCipher(new SimpleCaesar3());
            RegisterCipher(new XORCipher());
            RegisterCipher(new ReverseCipher());
            RegisterCipher(new NullCipher());
            RegisterCipher(new DialCipher());
            RegisterCipher(new WheelCipher());
            RegisterCipher(new RotorCipher());
            RegisterCipher(new MatrixCipher());
            RegisterCipher(new GridCipher());
            RegisterCipher(new LatticeCipher());
            RegisterCipher(new MirrorCipher());
            RegisterCipher(new ReflectorCipher());
            
            // 現代密碼算法
            RegisterCipher(new AESTextCipher());
            RegisterCipher(new RSACipher());
            RegisterCipher(new BlowfishCipher());
            RegisterCipher(new TwofishCipher());
            RegisterCipher(new RC4Cipher());
            RegisterCipher(new RC5Cipher());
            RegisterCipher(new RC6Cipher());
            RegisterCipher(new IDEACipher());
            RegisterCipher(new SerpentCipher());
            RegisterCipher(new CamelliaCipher());
            RegisterCipher(new CAST128Cipher());
            RegisterCipher(new CAST256Cipher());
            RegisterCipher(new MARSCipher());
            RegisterCipher(new GOSTCipher());
            RegisterCipher(new SkipjackCipher());
            RegisterCipher(new TEACipher());
            RegisterCipher(new XTEACipher());
            RegisterCipher(new SAFERCipher());
            RegisterCipher(new KASUMICipher());
            RegisterCipher(new MISTY1Cipher());
            RegisterCipher(new SEEDCipher());
            RegisterCipher(new ARIACipher());
            RegisterCipher(new CLEFIACipher());
            RegisterCipher(new SM4Cipher());
            
            RegisterCipher(new ChaCha20Cipher());
            RegisterCipher(new Salsa20Cipher());
            RegisterCipher(new HC128Cipher());
            RegisterCipher(new HC256Cipher());
            RegisterCipher(new SOSEMANUKCipher());
            RegisterCipher(new RabbitCipher());
            RegisterCipher(new PRESENTCipher());
            RegisterCipher(new KLEINCipher());
            RegisterCipher(new LEDCipher());
            RegisterCipher(new PRINCECipher());
            RegisterCipher(new KATANCipher());
            RegisterCipher(new KTANTANCipher());
            RegisterCipher(new mCryptonCipher());
            RegisterCipher(new HIGHTCipher());
            RegisterCipher(new LEACipher());
            RegisterCipher(new SIMONCipher());
            RegisterCipher(new SPECKCipher());
            RegisterCipher(new ThreefishCipher());
            RegisterCipher(new AnubisCipher());
            RegisterCipher(new FEALCipher());
            RegisterCipher(new LOKI97Cipher());
            RegisterCipher(new MAGENTACipher());
            RegisterCipher(new NewDESCipher());
            RegisterCipher(new RC2Cipher());
            RegisterCipher(new REDCipher());
            RegisterCipher(new SC2000Cipher());
            RegisterCipher(new SHACALCipher());
            RegisterCipher(new SHARKCipher());
            RegisterCipher(new SquareCipher());
            RegisterCipher(new UnicornACipher());
            RegisterCipher(new WAKECipher());
            RegisterCipher(new HMACCipher());
            RegisterCipher(new CMACCipher());
            RegisterCipher(new PMACCipher());
            RegisterCipher(new ECDSACipher());
            RegisterCipher(new EdDSACipher());
            RegisterCipher(new ECIESCipher());
            RegisterCipher(new GCMCipher());
            RegisterCipher(new CCMCipher());
            RegisterCipher(new EAXCipher());
            RegisterCipher(new OCBCipher());
            RegisterCipher(new SIVCipher());
            RegisterCipher(new ChaCha20Poly1305Cipher());
            RegisterCipher(new XSalsa20Poly1305Cipher());
            
            RegisterCipher(new Curve25519Cipher());
            RegisterCipher(new X25519Cipher());
            RegisterCipher(new Ed25519Cipher());
            RegisterCipher(new P256Cipher());
            RegisterCipher(new Secp256k1Cipher());
            RegisterCipher(new BrainpoolCipher());
            RegisterCipher(new SPHINCSPlusCipher());
            RegisterCipher(new XMSSCipher());
            RegisterCipher(new LMSCipher());
            RegisterCipher(new McElieceCipher());
            RegisterCipher(new NTRUCipher());
            RegisterCipher(new KyberCipher());
            RegisterCipher(new DilithiumCipher());
            RegisterCipher(new FalconCipher());
            RegisterCipher(new RainbowCipher());
            RegisterCipher(new PicnicCipher());
            RegisterCipher(new SIKECipher());
            RegisterCipher(new FrodoKEMCipher());
            RegisterCipher(new DiffieHellmanCipher());
            RegisterCipher(new ElGamalCipher());
            RegisterCipher(new DSACipher());
            RegisterCipher(new SHA256Cipher());
            RegisterCipher(new SHA512Cipher());
            RegisterCipher(new MD5Cipher());
            RegisterCipher(new AES128Cipher());
            RegisterCipher(new AES192Cipher());
            RegisterCipher(new AES256Cipher());
            RegisterCipher(new RSA1024Cipher());
            RegisterCipher(new RSA2048Cipher());
            RegisterCipher(new RSA4096Cipher());
            RegisterCipher(new Blowfish64Cipher());
            RegisterCipher(new Blowfish128Cipher());
            RegisterCipher(new Blowfish256Cipher());
            RegisterCipher(new GOST28147Cipher());
            RegisterCipher(new StreamCipher1());
            RegisterCipher(new StreamCipher2());
            RegisterCipher(new BlockCipher1());
            RegisterCipher(new BlockCipher2());
            RegisterCipher(new HybridCipher1());
            RegisterCipher(new HybridCipher2());
            RegisterCipher(new CustomCipher1());
            RegisterCipher(new CustomCipher2());
            RegisterCipher(new CustomCipher3());
            RegisterCipher(new CustomCipher4());
            RegisterCipher(new CustomCipher5());
        }

        private static void RegisterCipher(ICipher cipher)
        {
            if (!_ciphers.ContainsKey(cipher.Name))
            {
                _ciphers.Add(cipher.Name, cipher);
            }
        }

        /// <summary>
        /// 根據名稱獲取密碼算法
        /// </summary>
        public static ICipher? GetCipher(string name)
        {
            return _ciphers.TryGetValue(name, out var cipher) ? cipher : null;
        }

        /// <summary>
        /// 獲取所有密碼算法名稱
        /// </summary>
        public static IEnumerable<string> GetAllCipherNames()
        {
            return _ciphers.Keys;
        }

        /// <summary>
        /// 獲取所有密碼算法
        /// </summary>
        public static IEnumerable<ICipher> GetAllCiphers()
        {
            return _ciphers.Values;
        }

        /// <summary>
        /// 獲取密碼算法總數
        /// </summary>
        public static int GetCipherCount()
        {
            return _ciphers.Count;
        }

        /// <summary>
        /// 檢查是否存在指定名稱的密碼算法
        /// </summary>
        public static bool HasCipher(string name)
        {
            return _ciphers.ContainsKey(name);
        }
    }
}

