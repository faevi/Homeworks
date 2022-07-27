namespace ConsoleApp
{
    internal class TextReader
    {
        private Dictionary<string, int> numberOfWords = new Dictionary<string, int>();

        public TextReader(string filePath)
        {
            string text = File.ReadAllText(filePath);
            string[] subs = text.Split(' ');

            foreach (string word in subs)
            {
                if (!numberOfWords.ContainsKey(word))
                {
                    numberOfWords.Add(word, 1);
                }
                else
                {
                    numberOfWords[word]++;
                }
            }
        }

        public void ShowWords()
        {
            foreach (KeyValuePair<string,int> word in numberOfWords)
            {
                Console.WriteLine($"The word {word.Key} contains in text {word.Value} times");
            }
        }
    }
}
