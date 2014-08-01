using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Buffs;
using NetworkProject.Benefits;

namespace EditorExtension
{
    [Serializable]
    public class BuffMultiData
    {
        public int IdBuff
        {
            get { return ClientVersion.IdBuff; }
            set
            {
                ClientVersion.IdBuff = value;
                ServerVersion.IdBuff = value;
            }
        }

        public bool IsVisibleIcon
        {
            get { return ClientVersion.IsVisibleIcon; }
            set { ClientVersion.IsVisibleIcon = value; }
        }

        public int IdIcon
        {
            get { return ClientVersion.IdImage; }
            set { ClientVersion.IdImage = value; }
        }

        public bool IsTransformBuff
        {
            get { return ServerVersion.IsTransformBuff; }
            set { ServerVersion.IsTransformBuff = value; }
        }

        private BuffVisualData ClientVersion;
        private BuffFullData ServerVersion;

        public BuffMultiData()
        {
            ClientVersion = new BuffVisualData(0, false);

            ServerVersion = new BuffFullData(0);
        }

        public BuffMultiData(BuffVisualData clientVersion, BuffFullData serverVersion)
        {
            ClientVersion = clientVersion;
            ServerVersion = serverVersion;
        }

        public BuffVisualData GetClientVersion()
        {
            return ClientVersion;
        }

        public BuffFullData GetServerVersion()
        {
            return ServerVersion;
        }
    }
}
