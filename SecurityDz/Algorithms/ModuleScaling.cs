using SecurityDz.Algorithms.Core;
using SecurityDz.Algorithms.Data;

namespace SecurityDz.Algorithms;

public class ModuleScaling : IEncryptor
{
    private readonly string _key;
    private readonly char[] _characters;
    private readonly int _n = 0;

    public ModuleScaling(SecurityData securityData)
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

        foreach (var letter in input)
        {
            result += _characters[Array.IndexOf(_characters, letter)^Array.IndexOf(_characters, keyword[keywordIndex])];
            keywordIndex =  ++keywordIndex % keyword.Length;
        }
        return result;
    }

    public string Decode(string input)
    {
        var keyword = _key.ToUpper();
        var result = string.Empty;

        var keywordIndex = 0;
        
        foreach (var letter in input)
        {
            result += _characters[Array.IndexOf(_characters, letter)^Array.IndexOf(_characters, keyword[keywordIndex])];
            keywordIndex =  ++keywordIndex % keyword.Length;
        }
        return result;
    }

    public bool Verify(string input, string codedInput)
    {
        return string.Equals(input, Decode(codedInput), StringComparison.OrdinalIgnoreCase);
    }
}