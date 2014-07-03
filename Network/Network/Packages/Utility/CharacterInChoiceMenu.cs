using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.Utility
{
    [Serializable]
    public class CharacterInChoiceMenu
    {
        public string Name { get; set; }

        public CharacterInChoiceMenu(string name)
        {
            Name = name;
        }
    }
}
