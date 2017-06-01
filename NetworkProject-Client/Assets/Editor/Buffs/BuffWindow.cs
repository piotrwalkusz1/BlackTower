using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using NetworkProject.Benefits;

namespace EditorExtension
{
    class BenefitToDelete
    {
        public BenefitToDelete(int lvlBuff, IBenefit benefit)
        {
            LvlBuff = lvlBuff;
            Benefit = benefit;
        }

        public int LvlBuff { get; set; }
        public IBenefit Benefit { get; set; }
    }

    public class BuffWindow
    {
        public BuffData Buff;

        private bool _isWindowShow;
        private bool _isBenefitsShow;
        private bool[] _isBenefitsLvlShow;
        private int[] _idBenefitsSelectedToAddByLvl;
        private int _activeBenefitType;
        private List<BenefitToDelete> _benefitsToDelete;

        public BuffWindow()
            : this(new BuffData())
        {

        }

        public BuffWindow(BuffData buff)
        {
            Buff = buff;

            _isBenefitsLvlShow = new bool[Buff.Benefits.Count];
            _idBenefitsSelectedToAddByLvl = new int[Buff.Benefits.Count];
            _benefitsToDelete = new List<BenefitToDelete>();
        }

        public void Draw()
        {
            if (_isWindowShow = EditorGUILayout.Foldout(_isWindowShow, Buff.IdBuff.ToString() + " Buff"))
            {
                Indentation.BeginIndentation();

                Buff.IdBuff = EditorGUILayout.IntField("Id buff", Buff.IdBuff);
                Buff.IdIcon = EditorGUILayout.IntField("Id icon", Buff.IdIcon);

                DrawBenefits();

                Indentation.EndIndentation();
            }

            DeleteSelectedBenefits();
        }

        private void DrawBenefits()
        {
            if(_isBenefitsShow = EditorGUILayout.Foldout(_isBenefitsShow, "Benefits"))
            {
                int oldLvlsNumber = Buff.Benefits.Count;

                int lvlsNumber = EditorGUILayout.IntField("Lvls number", oldLvlsNumber);

                if (lvlsNumber != oldLvlsNumber)
                {
                    if (lvlsNumber > oldLvlsNumber)
                    {
                        int difference = lvlsNumber - oldLvlsNumber;

                        for (int i = 0; i < difference; i++)
                        {
                            Buff.Benefits.Add(new List<IBenefit>());
                        }
                    }
                    else if (lvlsNumber < oldLvlsNumber)
                    {
                        Buff.Benefits.RemoveRange(lvlsNumber, oldLvlsNumber - lvlsNumber);
                    }

                    _isBenefitsLvlShow = new bool[lvlsNumber];
                    _idBenefitsSelectedToAddByLvl = new int[lvlsNumber];
                }

                for (int i = 0; i < lvlsNumber; i++)
                {
                    if (_isBenefitsLvlShow[i] = EditorGUILayout.Foldout(_isBenefitsLvlShow[i], "Lvl " + (i + 1).ToString()))
                    {
                        Indentation.BeginIndentation();

                        foreach (var benefit in Buff.Benefits[i])
                        {
                            try
                            {
                                EditorGUILayout.BeginHorizontal();

                                EditorGUILayout.LabelField(benefit.GetType().Name);
                                benefit.Set(EditorGUILayout.TextField(benefit.GetValueAsString()));

                                if (GUILayout.Button("Delete"))
                                {
                                    _benefitsToDelete.Add(new BenefitToDelete(i, benefit));
                                }

                                EditorGUILayout.EndHorizontal();
                            }
                            catch { }
                        }

                        ShowAddBenefit(i);

                        Indentation.EndIndentation();
                    }
                }
            }
        }

        private void ShowAddBenefit(int lvlBuff)
        {
            EditorGUILayout.BeginHorizontal();

            _idBenefitsSelectedToAddByLvl[lvlBuff] = EditorGUILayout.Popup(_idBenefitsSelectedToAddByLvl[lvlBuff],
                BuffsWindow._benefitsList.GetTypesNames());

            if (GUILayout.Button("Add benefit"))
            {
                AddBenefit(lvlBuff);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void AddBenefit(int lvlBuff)
        {
            IBenefit benefit = BuffsWindow._benefitsList.CreateInstantiateByIndex(_idBenefitsSelectedToAddByLvl[lvlBuff]);

            Buff.Benefits[lvlBuff].Add(benefit);
        }

        private void DeleteSelectedBenefits()
        {
            foreach (var benefitToDelete in _benefitsToDelete)
            {
                Buff.Benefits[benefitToDelete.LvlBuff].Remove(benefitToDelete.Benefit);
            }

            _benefitsToDelete.Clear();
        }
    }
}
