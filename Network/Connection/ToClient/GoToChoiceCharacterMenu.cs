using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Connection;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class GoToChoiceCharacterMenu : INetworkRequest
    {
        public List<CharacterInChoiceMenu> Characters { get; set; }

        public void AddCharacter(CharacterInChoiceMenu character)
        {
            Characters.Add(character);
        }
    }
}
