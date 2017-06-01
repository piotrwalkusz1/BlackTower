using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;
using NetworkProject.Benefits;
using NetworkProject.Requirements;
using NetworkProject.Spells;

namespace NetworkProject
{
    public static class IoC
    {
        private static List<BodyPartPackage> _bodyParts;

        static IoC()
        {
            InitializeBodyParts();
        }

        public static BodyPartPackage GetBodyPart(int slot)
        {
            try
            {
                return (BodyPartPackage)Activator.CreateInstance(_bodyParts[slot].GetType());
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Nie ma takiego id części ciała.");
            }
        }

        public static BodyPartPackage[] GetBodyParts()
        {
            BodyPartPackage[] bodyParts = new BodyPartPackage[_bodyParts.Count];

            for (int i = 0; i < _bodyParts.Count; i++)
            {
                bodyParts[i] = (BodyPartPackage)Activator.CreateInstance(_bodyParts[i].GetType());
            }

            return bodyParts;
        }

        private static void InitializeBodyParts()
        {
            _bodyParts = new List<BodyPartPackage>();
            _bodyParts.Add(new BodyParts.HeadPackage());
            _bodyParts.Add(new BodyParts.ChestPackage());
            _bodyParts.Add(new BodyParts.FeetPackage());
            _bodyParts.Add(new BodyParts.RightHandPackage());
            _bodyParts.Add(new BodyParts.LeftHandPackage());
            _bodyParts.Add(new BodyParts.OtherPackage());
            _bodyParts.Add(new BodyParts.OtherPackage());
        }
    }
}
