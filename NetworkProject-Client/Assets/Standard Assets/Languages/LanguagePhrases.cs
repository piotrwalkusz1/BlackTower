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
        public PhrasesCategory QuestsNames { get; private set; }
        [XmlIgnore]
        public PhrasesCategory QuestsDescriptions { get; private set; }
        [XmlIgnore]
        public PhrasesCategory Dialogs { get; set; }
        [XmlIgnore]
        public PhrasesCategory MessageText { get; set; }
        [XmlIgnore]
        public PhrasesCategory Other { get; private set; }


        public LanguagePhrases()
        {
            AllPhrases = new List<string>();

            ItemsNames = new PhrasesCategory("Items names", this, @"^" + Languages.ITEM_NAME);

            SpellsNames = new PhrasesCategory("Spells names", this, @"^" + Languages.SPELL_NAME);

            SpellsDescriptions = new PhrasesCategory("Spells descriptions", this, @"^" + Languages.SPELL_DESCRIPTION);

            MonstersNames = new PhrasesCategory("Monsters names", this, @"^" + Languages.MONSTER_NAME);

            QuestsNames = new PhrasesCategory("Quests names", this, @"^" + Languages.QUEST_NAME);

            QuestsDescriptions = new PhrasesCategory("Quests descriptions", this, @"^" + Languages.QUEST_DESCRIPTION);

            Dialogs = new PhrasesCategory("Dialogs", this, @"^" + Languages.DIALOG);

            MessageText = new PhrasesCategory("Message text", this, @"^" + Languages.MESSAGE_TEXT);

            Other = new PhrasesCategory("Other", this, ItemsNames, SpellsNames, SpellsDescriptions, MonstersNames,
                QuestsNames, QuestsDescriptions, Dialogs, MessageText);
        }

        public PhrasesCategory[] GetCategories()
        {
            return new PhrasesCategory[] { ItemsNames, SpellsNames, SpellsDescriptions, MonstersNames, QuestsNames,
                QuestsDescriptions, Dialogs, Other };
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
