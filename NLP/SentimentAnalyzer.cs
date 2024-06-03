namespace NLP;

/// <summary>
/// Step 7:
/// Analyze if the sentence is positive or negative in context.
/// </summary>
public class SentimentAnalyzer
{
    private static readonly HashSet<string> PositiveWords = new HashSet<string> { "happy", "joy", "love", "excellent", "good" };
    private static readonly HashSet<string> NegativeWords = new HashSet<string> { "sad", "hate", "bad", "terrible", "poor" };

    public string AnalyzeSentiment(List<(string word, string tag)> taggedSentence)
    {
        int score = 0;
        
        foreach (var (word, tag) in taggedSentence)
        {
            if (PositiveWords.Contains(word.ToLower()))
            {
                score++;
            }
            else if (NegativeWords.Contains(word.ToLower()))
            {
                score--;
            }
        }
        
        if (score > 0) return "Positive";
        if (score < 0) return "Negative";
        return "Neutral";
    }
}
