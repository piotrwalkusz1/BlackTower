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

        public PhrasesCategory(string name, LanguagePhrases pharses, string matchExpression)
        {
            Name = name;
            RootPharses = pharses;
            MatchExpression = matchExpression;
        }

        public string[] GetPharses()
        {
            var pharses = from pharse in RootPharses.Phrases
                          where IsPharseMatchToCategory(pharse)
                          select pharse;

            return pharses.ToArray();
        }

        public void Change(string oldPharse, string newPharse)
        {
            if (IsPharseMatchToCategory(newPharse))
            {
                for (int i = 0; i < RootPharses.Phrases.Count; i++)
                {
                    if (RootPharses.Phrases[i] == oldPharse)
                    {
                        RootPharses.Phrases[i] = newPharse;
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
