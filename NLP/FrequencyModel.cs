namespace NLP;

/// <summary>
/// Step 4:
/// To start understanding the text, you can build a frequency-based model that counts the occurrences of each word.
/// </summary>
public class FrequencyModel
{
    public static Dictionary<string, int> BuildFrequencyModel(List<string> tokens)
    {
        Dictionary<string, int> frequencyDict = new Dictionary<string, int>();
        foreach (var token in tokens)
        {
            if (frequencyDict.ContainsKey(token))
            {
                frequencyDict[token]++;
            }
            else
            {
                frequencyDict[token] = 1;
            }
        }
        return frequencyDict;
    }
}