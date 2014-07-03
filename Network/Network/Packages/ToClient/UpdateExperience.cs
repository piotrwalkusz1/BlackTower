using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class UpdateExperience : INetworkPackage
    {
        public int IdNet { get; set; }
        public int Exp { get; set; }

        public UpdateExperience(int idNet, int exp)
        {
            IdNet = idNet;
            Exp = exp;
        }
    }
}
