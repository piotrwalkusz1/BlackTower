using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateNPCToClient : CreateToClient
    {
        public int IdNPC { get; set; }
        public int ModelId { get; set; }

        public CreateNPCToClient(int idNet, Vector3Serializable position, float rotation, int idNPC, int modelId)
            : base(idNet, position, rotation)
        {
            IdNPC = idNPC;
            ModelId = modelId;
        }
    }
}
