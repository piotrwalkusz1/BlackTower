using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Standard
{
    public class PhrasesCategory
    {
        public string Name { get; private set; }
        public LanguagePhrases RootPharses { get; private set; }
        public string MatchExpression { get; private set; }

        private List<PhrasesCategory> _exludeCategories;

        public PhrasesCategory(string name, LanguagePhrases pharses, string matchExpression)
        {
            Name = name;
            RootPharses = pharses;
            MatchExpression = matchExpression;
            _exludeCategories = new List<PhrasesCategory>();
        }

        public PhrasesCategory(string name, LanguagePhrases phrases, params PhrasesCategory[] exludeCategories)
        {
            MatchExpression = "";
            Name = name;
            RootPharses = phrases;
            _exludeCategories = new List<PhrasesCategory>(exludeCategories);
        }

        public string[] GetPharses()
        {
            var phrases = from pharse in RootPharses.AllPhrases
                          where IsPharseMatchToCategory(pharse)
                          select pharse;

            var copyPhrases = phrases.ToList();

            foreach (var phrase in phrases)
            {
                foreach (var exludeCategory in _exludeCategories)
                {
                    if (exludeCategory.ContainPhrase(phrase))
                    {
                        copyPhrases.Remove(phrase);
                    }
                }
            }

            return copyPhrases.ToArray();
        }

        public bool ContainPhrase(string phrase)
        {
            return GetPharses().Contains(phrase);
        }

        public void Change(string oldPharse, string newPharse)
        {
            if (IsPharseMatchToCategory(newPharse))
            {
                for (int i = 0; i < RootPharses.AllPhrases.Count; i++)
                {
                    if (RootPharses.AllPhrases[i] == oldPharse)
                    {
                        RootPharses.AllPhrases[i] = newPharse;
                        break;
                    }
                }
            } 
        }

        private bool IsPharseMatchToCategory(string pharse)
        {
            return Regex.IsMatch(pharse, MatchExpression);
        }
    }
}
