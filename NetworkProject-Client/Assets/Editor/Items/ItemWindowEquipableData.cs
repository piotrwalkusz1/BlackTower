using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Requirements;
using NetworkProject.Benefits;
using NetworkProject.Items;
using UnityEditor;
using UnityEngine;

namespace EditorExtension
{
    public class ItemWindowEquipableData : ItemWindowData
    {
        public virtual VisualEquipableItemData VisualEquipableItem { get; set; }
        public override VisualItemData VisualItem
        {
            get { return VisualEquipableItem; }
            protected set { VisualEquipableItem = (VisualEquipableItemData)value; }
        }

        public int IdPrefabOnPlayer
        {
            get { return VisualEquipableItem.IdPrefabOnPlayer; }
            set { VisualEquipableItem.IdPrefabOnPlayer = value; }
        }
        public List<IEquipRequirement> Requirements
        {
            get { return VisualEquipableItem.EquipableItemData._requirements; }
            set { VisualEquipableItem.EquipableItemData._requirements = value; }
        }
        public List<IEquipBenefit> Benefits
        {
            get { return VisualEquipableItem.EquipableItemData._benefits; }
            set { VisualEquipableItem.EquipableItemData._benefits = value; }
        }

        private bool _isActiveRequirements;
        private bool _isActiveBenefits;
        private int _addRequirementSelectedIndex;
        private int _addBenefitSelectedIndex;

        public ItemWindowEquipableData(EquipableItemData item) : this(new VisualEquipableItemData(item))
        {

        }

        public ItemWindowEquipableData(VisualEquipableItemData item) : base(item)
        {

        }

        public override void Draw()
        {
            base.Draw();

            IdPrefabOnPlayer = EditorGUILayout.IntField("Id prefab on player", IdPrefabOnPlayer);

            ShowRequirements();
            ShowBenefits();
        }

        private void ShowRequirements()
        {
            if (_isActiveRequirements = EditorGUILayout.Foldout(_isActiveRequirements, "Requirements"))
            {
                Indentation.BeginIndentation();

                    foreach (var requirement in Requirements)
                    {
                        EditorGUILayout.BeginHorizontal();

                        try
                        {
                            EditorGUILayout.LabelField(requirement.GetType().Name);
                            requirement.Set(EditorGUILayout.TextField(requirement.GetAsString()));
                        }
                        catch { }

                        EditorGUILayout.EndHorizontal();
                    }


                    EditorGUILayout.BeginHorizontal();

                    ShowAddRequirement();

                    _addRequirementSelectedIndex = EditorGUILayout.Popup(_addRequirementSelectedIndex,
                        ItemsWindow.RequirementsList.GetTypesNames());

                    EditorGUILayout.EndHorizontal();

                Indentation.EndIndentation();
            }
        }

        private void ShowAddRequirement()
        {
            if (GUILayout.Button("Add new requirement"))
            {
                AddRequiremnt();
            }
        }

        private void AddRequiremnt()
        {
            Requirements.Add(ItemsWindow.RequirementsList.CreateInstantiateByIndex(_addRequirementSelectedIndex));
        }

        private void ShowBenefits()
        {


            if (_isActiveBenefits = EditorGUILayout.Foldout(_isActiveBenefits, "Benefits"))
            {
                Indentation.BeginIndentation();

                    foreach (var benefit in Benefits)
                    {
                        EditorGUILayout.BeginHorizontal();

                        try
                        {
                            EditorGUILayout.LabelField(benefit.GetType().Name);
                            benefit.Set(EditorGUILayout.TextField(benefit.GetAsString()));
                        }
                        catch { }

                        EditorGUILayout.EndHorizontal();
                    }


                    EditorGUILayout.BeginHorizontal();

                    ShowAddBenefit();

                    _addBenefitSelectedIndex = EditorGUILayout.Popup(_addBenefitSelectedIndex, ItemsWindow.BenefitsList.GetTypesNames());

                    EditorGUILayout.EndHorizontal();

                Indentation.EndIndentation();
            }       
        }

        private void ShowAddBenefit()
        {
            if (GUILayout.Button("Add new benefit"))
            {
                AddBenefit();
            }
        }

        private void AddBenefit()
        {
            Benefits.Add(ItemsWindow.BenefitsList.CreateInstantiateByIndex(_addBenefitSelectedIndex));
        }
    }
}
