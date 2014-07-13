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
        public List<string> Phrases { get; private set; }

        [XmlIgnore]
        public PhrasesCategory ItemsNames { get; private set; }
        [XmlIgnore]
        public PhrasesCategory SpellsNames { get; private set; }
        [XmlIgnore]
        public PhrasesCategory SpellsDescriptions { get; private set; }

        //xml serializer require 0-argument constructor
        public LanguagePhrases()
        {
            Phrases = new List<string>();

            ItemsNames = new PhrasesCategory("Items names", this, @"^" + Languages.ITEM_NAME);

            SpellsNames = new PhrasesCategory("Spells names", this, @"^" + Languages.SPELL_NAME);

            SpellsDescriptions = new PhrasesCategory("Spells descriptions", this, @"^" + Languages.SPELL_DESCRIPTION);
        }

        public PhrasesCategory[] GetCategories()
        {
            return new PhrasesCategory[] { ItemsNames, SpellsNames, SpellsDescriptions };
        }
    } 
}
