using System;
using System.Collections.Generic;

namespace SymSpellAPI
{
    public sealed class SymSpellInterface
    {
        private SymSpell symSpell;

        public SymSpellInterface()
        {
            int initialCapacity = 82765;
            int maxEditDistanceDictionary = 2; //maximum edit distance per dictionary precalculation
            symSpell = new SymSpell(initialCapacity, maxEditDistanceDictionary);

            //load dictionary
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dictionaryPath = baseDirectory + "../../../frequency_dictionary_en_82_765.txt";
            int termIndex = 0; //column of the term in the dictionary text file
            int countIndex = 1; //column of the term frequency in the dictionary text file
            if (!symSpell.LoadDictionary(dictionaryPath, termIndex, countIndex))
            {
                Console.WriteLine("File not found " + dictionaryPath);
                //press any key to exit program

            }

            Console.WriteLine("Initialize");


        }

        private static readonly Lazy<SymSpellInterface> lazy = new Lazy<SymSpellInterface>(() => new SymSpellInterface());
        public static SymSpellInterface Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public List<SymSpell.SuggestItem> getSuggestions(string word,int verbosity,int distance)
        {
            
            var suggestions = symSpell.Lookup(word, (SymSpell.Verbosity)verbosity, distance);
            return suggestions;
        }

        public List<SymSpell.SuggestItem> correctText(string text, int distance)
        {
            var suggestions = symSpell.LookupCompound(text,distance);
            return suggestions;
        }

        public (string segmentedString, string correctedString, int distanceSum, decimal probabilityLogSum) segmentText(string text, int distance)
        {
            var suggestion = symSpell.WordSegmentation(text,distance);
            return suggestion;
        }
    }
}
