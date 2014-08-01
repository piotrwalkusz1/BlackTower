using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using UnityEngine;

namespace Standard
{
    public class LanguagePhrases
    {
        public List<string> AllPhrases { get; private set; }

        [XmlIgnore]
        public PhrasesCategory ItemsNames { get; private set; }
        [XmlIgnore]
        public PhrasesCategory SpellsNames { get; private set; }
        [XmlIgnore]
        public PhrasesCategory SpellsDescriptions { get; private set; }
        [XmlIgnore]
        public PhrasesCategory MonstersNames { get; private set; }
        [XmlIgnore]
        public PhrasesCategory Other { get; private set; }


        public LanguagePhrases()
        {
            AllPhrases = new List<string>();

            ItemsNames = new PhrasesCategory("Items names", this, @"^" + Languages.ITEM_NAME);

            SpellsNames = new PhrasesCategory("Spells names", this, @"^" + Languages.SPELL_NAME);

            SpellsDescriptions = new PhrasesCategory("Spells descriptions", this, @"^" + Languages.SPELL_DESCRIPTION);

            MonstersNames = new PhrasesCategory("Monsters names", this, @"^" + Languages.MONSTER_NAME);

            Other = new PhrasesCategory("Other", this, ItemsNames, SpellsNames, SpellsDescriptions, MonstersNames);
        }

        public PhrasesCategory[] GetCategories()
        {
            return new PhrasesCategory[] { ItemsNames, SpellsNames, SpellsDescriptions, MonstersNames, Other };
        }

        public bool IsContain(string phrase)
        {
            return AllPhrases.Exists(x => x == phrase);
        }

        public void Add(string phrase)
        {
            AllPhrases.Add(phrase);
        }
    } 
}
