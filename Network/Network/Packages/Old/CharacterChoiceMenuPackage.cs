using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class CharacterChoiceMenuPackage : INetworkPackage
    {
        public List<CharacterInChoiceMenuPackage> Characters { get; private set; }

        public CharacterChoiceMenuPackage()
        {
            Characters = new List<CharacterInChoiceMenuPackage>();
        }

        public virtual void Set(IncomingMessage message)
        {
            int length = message.ReadInt();
            CharacterInChoiceMenuPackage[] characters = new CharacterInChoiceMenuPackage[length];
            for(int i = 0; i < length; i ++)
            {
                characters[i] = message.Read<CharacterInChoiceMenuPackage>();
            }

            Characters = new List<CharacterInChoiceMenuPackage>(characters);
        }

        public virtual byte[] ToBytes()
        {
            OutgoingMessage data = new OutgoingMessage();
            data.Write(Characters.Count);
            foreach (CharacterInChoiceMenuPackage character in Characters)
            {
                data.Write(character);
            }
            return data.Data.ToArray();
        }

        public void AddCharacter(CharacterInChoiceMenuPackage character)
        {
            Characters.Add(character);
        }
    }
}
