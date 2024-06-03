namespace NLP;

public class HMMTrainer
{
    public Dictionary<string, double> StartProbabilities { get; private set; }
    public Dictionary<string, Dictionary<string, double>> TransitionProbabilities { get; private set; }
    public Dictionary<string, Dictionary<string, double>> EmissionProbabilities { get; private set; }

    public HMMTrainer(List<(string word, string tag)> corpus)
    {
        StartProbabilities = CalculateStartProbabilities(corpus);
        TransitionProbabilities = CalculateTransitionProbabilities(corpus);
        EmissionProbabilities = CalculateEmissionProbabilities(corpus);
    }

    private Dictionary<string, double> CalculateStartProbabilities(List<(string word, string tag)> corpus)
    {
        var startCounts = new Dictionary<string, int>();
        int totalSentences = 0;
        
        foreach (var (word, tag) in corpus)
        {
            if (totalSentences == 0 || corpus[totalSentences - 1].word == "." || corpus[totalSentences - 1].word == "?" || corpus[totalSentences - 1].word == "!")
            {
                if (!startCounts.ContainsKey(tag))
                {
                    startCounts[tag] = 0;
                }
                startCounts[tag]++;
                totalSentences++;
            }
        }
        
        int totalTags = startCounts.Values.Sum();
        var startProbabilities = startCounts.ToDictionary(pair => pair.Key, pair => (double)pair.Value / totalTags);
        return startProbabilities;
    }

    private Dictionary<string, Dictionary<string, double>> CalculateTransitionProbabilities(List<(string word, string tag)> corpus)
    {
        var transitions = new Dictionary<string, Dictionary<string, int>>();
        
        for (int i = 0; i < corpus.Count - 1; i++)
        {
            var currentTag = corpus[i].tag;
            var nextTag = corpus[i + 1].tag;

            if (!transitions.ContainsKey(currentTag))
            {
                transitions[currentTag] = new Dictionary<string, int>();
            }

            if (!transitions[currentTag].ContainsKey(nextTag))
            {
                transitions[currentTag][nextTag] = 0;
            }
            transitions[currentTag][nextTag]++;
        }
        
        var transitionProbabilities = transitions.ToDictionary(
            pair => pair.Key,
            pair => pair.Value.ToDictionary(subpair => subpair.Key, subpair => (double)subpair.Value / pair.Value.Values.Sum()));
        
        return transitionProbabilities;
    }

    private Dictionary<string, Dictionary<string, double>> CalculateEmissionProbabilities(List<(string word, string tag)> corpus)
    {
        var emissions = new Dictionary<string, Dictionary<string, int>>();
        
        foreach (var (word, tag) in corpus)
        {
            if (!emissions.ContainsKey(tag))
            {
                emissions[tag] = new Dictionary<string, int>();
            }
            
            if (!emissions[tag].ContainsKey(word))
            {
                emissions[tag][word] = 0;
            }
            emissions[tag][word]++;
        }
        
        var emissionProbabilities = emissions.ToDictionary(
            pair => pair.Key,
            pair => pair.Value.ToDictionary(subpair => subpair.Key, subpair => (double)subpair.Value / pair.Value.Values.Sum()));
        
        return emissionProbabilities;
    }
}