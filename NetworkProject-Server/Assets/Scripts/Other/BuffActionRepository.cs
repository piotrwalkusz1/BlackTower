using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Buffs
{
    public static class BuffActionRepository
    {
        private static List<Action<Buff, IBuffable>> _buffsCast = new List<Action<Buff, IBuffable>>();
        private static List<Action<Buff, IBuffable>> _buffsEnd = new List<Action<Buff, IBuffable>>();

        static BuffActionRepository()
        {
            _buffsCast.Add(TransformToFire_Cast); // 0
            _buffsEnd.Add(TransformToFire_End);
        }

        public static Action<Buff, IBuffable> GetOnCast(int idBuff)
        {
            return _buffsCast[idBuff];
        }

        public static Action<Buff, IBuffable> GetOnEnd(int idBuff)
        {
            return _buffsEnd[idBuff];
        }

        public static void TransformToFire_Cast(Buff buff, IBuffable target) // 0
        {
            GameObject fire = SceneBuilder.CreateVisualObject(1, Vector3.zero, 0f);

            fire.GetComponent<NetVisualObject>()._idVisualObject = 1;

            DamageSphere damager = fire.AddComponent<DamageSphere>();
            damager.DamageType = DamageType.Mental;
            damager.Attacker = new Attacker(target.GetGameObject());
            damager.Cooldown = 0.5f;
            damager.MinDmg = 20;
            damager.MaxDmg = 30;
            damager.Radius = 1.5f;
            HealthSystem hp = target.GetGameObject().GetComponent<HealthSystem>();
            if(hp != null)
            {
                damager.Insenitive.Add(hp);
            }

            target.AddChild(fire, Vector3.zero, Quaternion.identity);

            buff.BoundGameObjects.Add(fire);

            target.SetVisibleModel(false);
        }

        public static void TransformToFire_End(Buff buff, IBuffable target)
        {
            target.SetVisibleModel(true);
        }
    }
}
