namespace NLP;

/// <summary>
/// Step 5:
/// Tag the different parts of speech
/// </summary>
public class POSTagger
{
    public static List<(string word, POSTag tag)> TagSentence(string sentence)
    {
        List<string> tokens = TextProcessor.Tokenize(sentence);
        List<(string word, POSTag tag)> taggedSentence = new List<(string word, POSTag tag)>();

        foreach (var token in tokens)
        {
            POSTag tag = POSRules.TagWord(token);
            taggedSentence.Add((token, tag));
        }

        return taggedSentence;
    }
}