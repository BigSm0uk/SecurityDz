namespace SecurityDz.Algorithms.Core;

public interface IEncryptor
{
    public string Encode(string input);
    public string Decode(string input);
    public bool Verify(string input, string codedInput);
}