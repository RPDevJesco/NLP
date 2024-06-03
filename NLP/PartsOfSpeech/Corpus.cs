namespace NLP;

public class Corpus
{
    public static List<(string word, string tag)> GetSampleCorpus()
    {
        return new List<(string word, string tag)>
        {
            ("The", "Determiner"),
            ("quick", "Adjective"),
            ("brown", "Adjective"),
            ("fox", "Noun"),
            ("jumps", "Verb"),
            ("over", "Preposition"),
            ("the", "Determiner"),
            ("lazy", "Adjective"),
            ("dog", "Noun"),
            ("and", "Conjunction"),
            ("runs", "Verb")
        };
    }
}