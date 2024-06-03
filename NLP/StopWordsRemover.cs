namespace NLP;

/// <summary>
/// Step 2:
/// Stop words are common words (like "the", "is", "in") that often don't carry significant meaning
/// and can be removed to focus on the important words.
/// </summary>
public class StopWordsRemover
{
    private static HashSet<string> stopWords = new HashSet<string>
    {
        "the", "is", "in", "and", "to", "of", "a", "an"
    };

    public static List<string> RemoveStopWords(List<string> tokens)
    {
        List<string> filteredTokens = new List<string>();
        foreach (var token in tokens)
        {
            if (!stopWords.Contains(token))
            {
                filteredTokens.Add(token);
            }
        }
        return filteredTokens;
    }
}