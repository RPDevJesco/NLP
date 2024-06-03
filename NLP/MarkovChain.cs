namespace NLP;

/// <summary>
/// Step 8:
/// Text Generation
/// </summary>
public class MarkovChain
{
    private readonly Dictionary<string, List<string>> wordDict = new Dictionary<string, List<string>>();
    private readonly Random random = new Random();
    private readonly List<(string word, string tag)> trainingData;

    public MarkovChain(string filePath)
    {
        trainingData = TrainingDataLoader.LoadTrainingData(filePath);
        if (trainingData.Count >= 5) // Ensure there is enough data
        {
            BuildChain();
        }
    }

    private void BuildChain()
    {
        for (int i = 0; i < trainingData.Count - 4; i++)
        {
            var currentWords = $"{trainingData[i].word} {trainingData[i + 1].word} {trainingData[i + 2].word} {trainingData[i + 3].word}";
            var nextWord = trainingData[i + 4].word;

            if (!wordDict.ContainsKey(currentWords))
            {
                wordDict[currentWords] = new List<string>();
            }
            wordDict[currentWords].Add(nextWord);
        }
    }

    public string GenerateSentence(int minLength = 8, int maxLength = 15)
    {
        if (wordDict.Count == 0)
        {
            return "Insufficient training data.";
        }

        var startWords = GetStartingWords();
        if (startWords.Count == 0)
        {
            Console.WriteLine("No suitable starting words found.");
            startWords = wordDict.Keys.ToList(); // Fallback to any sequence if no suitable start found
        }

        if (startWords.Count == 0)
        {
            return "Unable to generate a starting word sequence.";
        }

        var currentWords = startWords[random.Next(startWords.Count)];
        var sentence = new List<string>(currentWords.Split(' '));
        var currentTags = new List<string>(currentWords.Split(' ').Select(word => GetTag(word)));

        int maxAttempts = 1000; // Safeguard to prevent infinite loops
        int attempts = 0;
        int targetLength = random.Next(minLength, maxLength + 1);

        for (int i = 4; i < targetLength; i++)
        {
            attempts++;
            if (attempts > maxAttempts)
            {
                Console.WriteLine("Max attempts reached, stopping generation.");
                break;
            }

            if (wordDict.ContainsKey(currentWords))
            {
                var nextWords = wordDict[currentWords];
                var nextWord = nextWords[random.Next(nextWords.Count)];
                sentence.Add(nextWord);

                currentWords = $"{sentence[sentence.Count - 4]} {sentence[sentence.Count - 3]} {sentence[sentence.Count - 2]} {sentence[sentence.Count - 1]}";
                currentTags.Add(GetTag(nextWord));

                if (!IsValidSequence(currentTags))
                {
                    sentence.RemoveAt(sentence.Count - 1);
                    currentTags.RemoveAt(currentTags.Count - 1);
                    currentWords = $"{sentence[sentence.Count - 4]} {sentence[sentence.Count - 3]} {sentence[sentence.Count - 2]} {sentence[sentence.Count - 1]}";
                    i--;
                }
            }
            else
            {
                currentWords = startWords[random.Next(startWords.Count)];
                sentence.AddRange(currentWords.Split(' '));
            }
        }

        return PostProcessSentence(string.Join(" ", sentence));
    }

    private List<string> GetStartingWords()
    {
        var startingWords = new List<string>();

        for (int i = 0; i < trainingData.Count - 3; i++)
        {
            if (trainingData[i].tag == "Determiner" || trainingData[i].tag == "Noun" || trainingData[i].tag == "Pronoun" ||
                (trainingData[i].tag == "Verb" && (trainingData[i].word.Equals("is", StringComparison.OrdinalIgnoreCase) || trainingData[i].word.Equals("are", StringComparison.OrdinalIgnoreCase))))
            {
                startingWords.Add($"{trainingData[i].word} {trainingData[i + 1].word} {trainingData[i + 2].word} {trainingData[i + 3].word}");
            }
        }

        Console.WriteLine($"Found {startingWords.Count} starting sequences.");
        return startingWords;
    }

    private string GetTag(string word)
    {
        var entry = trainingData.FirstOrDefault(pair => pair.word == word);
        return entry.Equals(default((string word, string tag))) ? "Unknown" : entry.tag;
    }

    private bool IsValidSequence(List<string> tags)
    {
        if (tags.Count < 2) return true;

        var lastTwoTags = tags.Skip(tags.Count - 2).ToList();

        if (lastTwoTags.SequenceEqual(new List<string> { "Determiner", "Determiner" }) ||
            lastTwoTags.SequenceEqual(new List<string> { "Preposition", "Preposition" }) ||
            lastTwoTags.SequenceEqual(new List<string> { "Conjunction", "Conjunction" }) ||
            lastTwoTags.SequenceEqual(new List<string> { "Verb", "Verb" }) ||
            lastTwoTags.SequenceEqual(new List<string> { "Noun", "Noun" }))
        {
            return false;
        }

        return true;
    }

    private string PostProcessSentence(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
        {
            return sentence;
        }

        sentence = char.ToUpper(sentence[0]) + sentence.Substring(1);

        if (!sentence.EndsWith("."))
        {
            sentence += ".";
        }

        return sentence;
    }
}