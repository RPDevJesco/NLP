namespace NLP;

public class POSRules
{
    private static Dictionary<string, POSTag> wordTags = new Dictionary<string, POSTag>
    {
        { "dog", POSTag.Noun },
        { "cat", POSTag.Noun },
        { "run", POSTag.Verb },
        { "jumps", POSTag.Verb },
        { "quick", POSTag.Adjective },
        { "brown", POSTag.Adjective },
        { "the", POSTag.Determiner },
        { "over", POSTag.Preposition },
        { "lazy", POSTag.Adjective },
        { "and", POSTag.Conjunction }
    };

    public static POSTag TagWord(string word)
    {
        if (wordTags.ContainsKey(word))
        {
            return wordTags[word];
        }
        return POSTag.Unknown;
    }
}