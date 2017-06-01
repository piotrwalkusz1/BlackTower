using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Connection;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class GoToChoiceCharacterMenuToClient : INetworkRequestToClient
    {
        public List<CharacterInChoiceMenu> Characters { get; set; }

        public GoToChoiceCharacterMenuToClient()
        {
            Characters = new List<CharacterInChoiceMenu>();
        }

        public void AddCharacter(CharacterInChoiceMenu character)
        {
            Characters.Add(character);
        }
    }
}
