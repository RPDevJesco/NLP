namespace NLP;

/// <summary>
/// Step 5:
/// Hidden Markov Models (HMMs) to Tag parts of Speech
/// </summary>
public class HMMPOSTagger
{
    private readonly Dictionary<string, double> startProbabilities;
    private readonly Dictionary<string, Dictionary<string, double>> transitionProbabilities;
    private readonly Dictionary<string, Dictionary<string, double>> emissionProbabilities;

    public HMMPOSTagger(HMMTrainer trainer)
    {
        startProbabilities = trainer.StartProbabilities;
        transitionProbabilities = trainer.TransitionProbabilities;
        emissionProbabilities = trainer.EmissionProbabilities;
    }

    public List<(string word, string tag)> TagSentence(string sentence)
    {
        List<string> tokens = TextProcessor.Tokenize(sentence);

        var viterbi = new Dictionary<string, double>[tokens.Count];
        var backpointer = new Dictionary<string, string>[tokens.Count];

        for (int i = 0; i < tokens.Count; i++)
        {
            viterbi[i] = new Dictionary<string, double>();
            backpointer[i] = new Dictionary<string, string>();

            foreach (var state in startProbabilities.Keys)
            {
                if (i == 0)
                {
                    viterbi[i][state] = startProbabilities[state] * (emissionProbabilities.ContainsKey(state) && emissionProbabilities[state].ContainsKey(tokens[i]) ? emissionProbabilities[state][tokens[i]] : 0.01);
                }
                else
                {
                    double maxProb = 0;
                    string bestPrevState = null;

                    foreach (var prevState in startProbabilities.Keys)
                    {
                        double prob = viterbi[i - 1][prevState] * (transitionProbabilities.ContainsKey(prevState) && transitionProbabilities[prevState].ContainsKey(state) ? transitionProbabilities[prevState][state] : 0.01) * (emissionProbabilities.ContainsKey(state) && emissionProbabilities[state].ContainsKey(tokens[i]) ? emissionProbabilities[state][tokens[i]] : 0.01);

                        if (prob > maxProb)
                        {
                            maxProb = prob;
                            bestPrevState = prevState;
                        }
                    }

                    viterbi[i][state] = maxProb;
                    backpointer[i][state] = bestPrevState;
                }
            }
        }

        double maxFinalProb = 0;
        string bestFinalState = null;

        foreach (var state in startProbabilities.Keys)
        {
            if (viterbi[tokens.Count - 1][state] > maxFinalProb)
            {
                maxFinalProb = viterbi[tokens.Count - 1][state];
                bestFinalState = state;
            }
        }

        var bestPath = new List<string> { bestFinalState };

        for (int i = tokens.Count - 1; i > 0; i--)
        {
            bestPath.Insert(0, backpointer[i][bestPath[0]]);
        }

        var taggedSentence = new List<(string word, string tag)>();

        for (int i = 0; i < tokens.Count; i++)
        {
            taggedSentence.Add((tokens[i], bestPath[i]));
        }

        return taggedSentence;
    }
}