using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using NetworkProject.Benefits;

namespace EditorExtension
{
    public class BuffWindow
    {
        public BuffMultiData Buff;

        private bool _isWindowShow;
        private bool _isBenefitsShow;
        private int _activeBenefitType;

        public BuffWindow()
        {
            Buff = new BuffMultiData();
        }

        public BuffWindow(BuffMultiData buff)
        {
            Buff = buff;
        }

        public void Draw()
        {
            if (_isWindowShow = EditorGUILayout.Foldout(_isWindowShow, Buff.IdBuff.ToString() + " Buff"))
            {
                Indentation.BeginIndentation();

                Buff.IdBuff = EditorGUILayout.IntField("Id buff", Buff.IdBuff);
                Buff.IsVisibleIcon = EditorGUILayout.Toggle("Is visible icon", Buff.IsVisibleIcon);
                if (Buff.IsVisibleIcon)
                {
                    Buff.IdIcon = EditorGUILayout.IntField("Id icon", Buff.IdIcon);
                }
                Buff.IsTransformBuff = EditorGUILayout.Toggle("Transform buff", Buff.IsTransformBuff);

                Indentation.EndIndentation();
            }
        }
    }
}
