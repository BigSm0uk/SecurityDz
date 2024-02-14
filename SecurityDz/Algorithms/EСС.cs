using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using SecurityDz.Algorithms.Core;
using SecurityDz.Algorithms.Data;
using SecurityDz.Algorithms.Engine;

namespace SecurityDz.Algorithms;

public class Eсс : IEncryptor
{
    private readonly string _key;
    
    public Eсс(SecurityData securityData)
    {
        for (var i = 0; i < 16; i++)
        {
            _key += securityData.GetSecretKey[i%securityData.GetSecretKey.Length];
        }
    }

    public string Encode(string input)
    {
        var blockCipherEngine = new BlockCipherEngine(new AesEngine(), Encoding.UTF8);

        return blockCipherEngine.Encrypt(input, _key);
    }
    
    public string Decode(string input)
    {
        var blockCipherEngine = new BlockCipherEngine(new AesEngine(), Encoding.UTF8);
        return blockCipherEngine.Decrypt(input, _key);
    }

    public bool Verify(string input, string codedInput)
    {
        return string.Equals(input, Decode(codedInput), StringComparison.OrdinalIgnoreCase);
    }
}