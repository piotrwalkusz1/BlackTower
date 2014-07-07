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
        private static List<BodyPart> _bodyParts;

        static IoC()
        {
            InitializeBodyParts();
        }

        public static BodyPart GetBodyPart(int slot)
        {
            try
            {
                return (BodyPart)Activator.CreateInstance(_bodyParts[slot].GetType());
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Nie ma takiego id części ciała.");
            }
        }

        public static BodyPart[] GetBodyParts()
        {
            BodyPart[] bodyParts = new BodyPart[_bodyParts.Count];

            for (int i = 0; i < _bodyParts.Count; i++)
            {
                bodyParts[i] = (BodyPart)Activator.CreateInstance(_bodyParts[i].GetType());
            }

            return bodyParts;
        }

        public static ISpellRepository GetSpellRepository()
        {
            return new SpellRepositoryImp();
        }

        private static void InitializeBodyParts()
        {
            _bodyParts = new List<BodyPart>();
            _bodyParts.Add(new BodyParts.Head());
            _bodyParts.Add(new BodyParts.Chest());
            _bodyParts.Add(new BodyParts.Feet());
            _bodyParts.Add(new BodyParts.RightHand());
            _bodyParts.Add(new BodyParts.LeftHand());
            _bodyParts.Add(new BodyParts.Other());
            _bodyParts.Add(new BodyParts.Other());
        }
    }
}
