using System.Text.RegularExpressions;
using SecurityDz.Algorithms;
using SecurityDz.Algorithms.Core;
using SecurityDz.Algorithms.Data;
using Spectre.Console;

namespace SecurityDz;

internal partial class Program
{
    [GeneratedRegex("\\P{IsCyrillic}")]
    private static partial Regex MyRegex();

    private IEncryptor _encryptor = null!;

    private void SetEncryptor(string encryptor, SecurityData securityData)
    {
        _encryptor = encryptor switch
        {
            "Виженера" => new VigenerCipher(securityData),
            "Гаммирование по модулю 2" => new ModuleScaling(securityData),
            "Эллиптические кривые" => new Eсс(securityData),
            _ => new VigenerCipher(securityData),
        };
    }

    private void LoadUserSession()
    {
        var algorithm = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Выберите [green]алгоритм шифрования[/]")
                .PageSize(20)
                .MoreChoicesText("[grey](Кнопки вверх или вниз для выбора)[/]")
                .AddChoices("Виженера", "Гаммирование по модулю 2", "Эллиптические кривые", "Все"));

        var securityData = new SecurityData();
        var toEncodeWord = AnsiConsole.Prompt(new TextPrompt<string>("Введите слово для кодировки:").PromptStyle("red")
            .Validate(
                word => MyRegex().IsMatch(word)
                    ? ValidationResult.Error("[red]Можно писать только на кирилице![/]")
                    : ValidationResult.Success())
        );

        if (algorithm.Equals("Все"))
        {
            _encryptor = new VigenerCipher(securityData);
            ShowAlgorithmDataTable("Виженер", toEncodeWord);
            _encryptor = new ModuleScaling(securityData);
            ShowAlgorithmDataTable("Гаммирование по модулю 2", toEncodeWord);
            _encryptor = new Eсс(securityData);
            ShowAlgorithmDataTable("Эллиптические кривые", toEncodeWord);
            return;
        }
        SetEncryptor(algorithm,securityData);
        ShowAlgorithmDataTable(algorithm, toEncodeWord);
    }

    private void ShowAlgorithmDataTable(string algorithm, string toEncodeWord)
    {
        var table = new Table();
        table.AddColumn(new TableColumn("Метод шифрования").Centered());
        table.AddColumn(new TableColumn("До кодировки").Centered());
        table.AddColumn(new TableColumn("После кодировки").Centered());
        table.AddColumn(new TableColumn("После декодирования").Centered());
        table.AddColumn(new TableColumn("Результат сравнения").Centered());
        var encodingResult = _encryptor.Encode(toEncodeWord);

        table.AddRow($"[green]{algorithm}[/]", $"{toEncodeWord}", $"[green]{encodingResult}[/]",
            $"{_encryptor.Decode(encodingResult)}",
            $"[green]{_encryptor.Verify(toEncodeWord, encodingResult)}[/]");


        AnsiConsole.Write(table);
    }

    public static void Main()
    {
        var program = new Program();
        program.LoadUserSession();
        while (AnsiConsole.Confirm("Хотите продолжить?"))
        {
            Console.Clear();
            program.LoadUserSession();
        }
    }
}