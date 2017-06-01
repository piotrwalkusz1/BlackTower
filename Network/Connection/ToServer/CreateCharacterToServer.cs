using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class CreateCharacterToServer : INetworkRequestToServer
    {
        public string Name { get; set; }
        public BreedAndGender BreedAndGender { get; set; }

        public CreateCharacterToServer(string name, BreedAndGender breedAndGender)
        {
            Name = name;
            BreedAndGender = breedAndGender;
        }
    }
}
