using System.Text.Json;

public class Word {
    private static Dictionary<char, string> transliterationTable = new() {
        {'ა', "a"},
        {'ბ', "b"},
        {'გ', "g"},
        {'დ', "d"},
        {'ე', "e"},
        {'ვ', "v"},
        {'ზ', "z"},
        {'თ', "t"},
        {'ი', "i"},
        {'კ', "k'"},
        {'ლ', "l"},
        {'მ', "m"},
        {'ნ', "n"},
        {'ო', "o"},
        {'პ', "p'"},
        {'ჟ', "zh"},
        {'რ', "r"},
        {'ს', "s"},
        {'ტ', "t'"},
        {'უ', "u"},
        {'ფ', "p"},
        {'ქ', "k"},
        {'ღ', "gh"},
        {'ყ', "q'"},
        {'შ', "sh"},
        {'ჩ', "ch"},
        {'ც', "ts"},
        {'ძ', "dz"},
        {'წ', "ts'"},
        {'ჭ', "ch'"},
        {'ხ', "kh"},
        {'ჯ', "j"},
        {'ჰ', "h"}
    };

    public string targetWord {get; set;}
    public string englishWord {get; set;}

    public Word(string targetWord, string englishWord) {
        this.targetWord = targetWord;
        this.englishWord = englishWord;
    }
    public string GetTransliteration() {
        string res = "";
        foreach (char c in targetWord)
            res += transliterationTable[c];
        return res;
    }

    override public string ToString() {
        return $"{targetWord}({englishWord})";
    }
}

class Program {
    const string WORDS_LIST_PATH = "words.json"; 
    static public void Main() {
        Stream wordsStream = File.OpenRead(WORDS_LIST_PATH);
        Word[]? words = JsonSerializer.Deserialize<Word[]>(wordsStream);
        if (words == null) {
            Console.Error.WriteLine("The words file does not match the required format.");
            Environment.Exit(1);    
        }
        Random rng = new();
        while (true) {
            Word word = words[rng.Next() % words.Length];
            Console.WriteLine($"\u001b[1m{word.targetWord}\u001b[0m");
            string? inp = Console.ReadLine();
            if (inp?.Trim() == word.GetTransliteration()) {
                Console.WriteLine($"\u001b[1;92mCorrect!\u001b[0m Translation: \u001b[1m{word.englishWord}\u001b[0m\n");
            } else {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\u001b[1;31mIncorrect!\u001b[0m Translation: \u001b[1m{word.englishWord}\u001b[0m");
                Console.WriteLine($"\u001b[1m{word.GetTransliteration()}\u001b[0m\n");
            }
        }
    }
}
