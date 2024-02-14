using SecurityDz.Algorithms.Core;
using SecurityDz.Algorithms.Data;

namespace SecurityDz;

public class VigenerCipher : IEncryptor
{
    private readonly string _key;
    private readonly char[] _characters;
    private readonly int _n ;

    public VigenerCipher(SecurityData securityData)
    {
        _key = securityData.GetSecretKey;
        _characters = securityData.GetSecretAlphabet;
        _n = securityData.GetN;
    }

    public string Encode(string input)
    {
        input = input.ToUpper();
        var keyword = _key.ToUpper();
 
        var result = string.Empty;
 
        var keywordIndex = 0;
        
        foreach (var c in input.Select(symbol => (Array.IndexOf(_characters, symbol) +
                                                  Array.IndexOf(_characters, keyword[keywordIndex])) % _n))
        {
            result += _characters[c];
 
            keywordIndex++;
 
            if ((keywordIndex + 1) == keyword.Length)
                keywordIndex = 0;
        }
 
        return result;
    }

    public string Decode(string input)
    {
        input = input.ToUpper();
        var keyword = _key.ToUpper();
 
        var result = string.Empty;
 
        var keywordIndex = 0;
 
        foreach (var p in input.Select(symbol => (Array.IndexOf(_characters, symbol) + _n-
                                                  Array.IndexOf(_characters, keyword[keywordIndex])) % _n))
        {
            result += _characters[p];
 
            keywordIndex++;
 
            if ((keywordIndex + 1) == keyword.Length)
                keywordIndex = 0;
        }
 
        return result;
    }

    public bool Verify(string input, string codedInput)
    {
        return string.Equals(input, Decode(codedInput), StringComparison.OrdinalIgnoreCase);
    }
}