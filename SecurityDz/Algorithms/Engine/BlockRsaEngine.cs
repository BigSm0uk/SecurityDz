using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace SecurityDz.Algorithms.Engine;


public class BlockCipherEngine
{

    private readonly Encoding _encoding;
    
    private readonly IBlockCipher _blockCipher;
    private PaddedBufferedBlockCipher _bufferedBlockCipher =null!;

    public BlockCipherEngine(IBlockCipher blockCipher, Encoding encoding)
    {
        _blockCipher = blockCipher;
        _encoding = encoding;
    }

    public string Encrypt(string textToEncrypt, string key)
    {
        byte[] result = BouncyCastleCrypto(true, _encoding.GetBytes(textToEncrypt), key);
        return Convert.ToBase64String(result);
    }
    
    public string Decrypt(string textToDecrypt, string key)
    {
        byte[] result = BouncyCastleCrypto(false, Convert.FromBase64String(textToDecrypt), key);
        return _encoding.GetString(result);
    }
    
    private byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, string key)
    {
        try
        {
            _bufferedBlockCipher = new PaddedBufferedBlockCipher(_blockCipher, new Pkcs7Padding());

            _bufferedBlockCipher.Init(forEncrypt, new KeyParameter(_encoding.GetBytes(key)));

            return _bufferedBlockCipher.DoFinal(input);
        }
        catch (CryptoException ex)
        {
            throw new CryptoException($"Exception during Encryption/Decryption - error: {ex.Message}");
        }
    }
}