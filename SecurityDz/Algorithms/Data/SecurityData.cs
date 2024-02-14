namespace SecurityDz.Algorithms.Data;

public class SecurityData
{
    private const string KEY = "ИЦТМС";
    private readonly char[] _characters = new[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
        'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 
        'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
        'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
        '8', '9', '0' };

    private int N => _characters.Length;

    public string GetSecretKey => KEY;
    public char[] GetSecretAlphabet => _characters;
    public int GetN => N;
}