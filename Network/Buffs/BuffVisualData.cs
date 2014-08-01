using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Buffs
{
    [Serializable]
    public class BuffVisualData : BuffData
    {
        public int IdImage { get; set; }
        public bool IsVisibleIcon { get; set; }

        public BuffVisualData(int idBuff, bool isVisibleIcon)
            : this(idBuff, isVisibleIcon, -1)
        {

        }

        public BuffVisualData(int idBuff, bool isVisibleIcon, int idImage)
            : base(idBuff)
        {
            IsVisibleIcon = IsVisibleIcon;
            IdImage = idImage;
        }
    }
}
