﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Monsters;
using NetworkProject.Items;
using UnityEditor;
using UnityEngine;

namespace EditorExtension
{
    public class MonsterWindow
    {
        public MonsterMultiData Monster { get; private set; }

        private bool _isActive;

        public MonsterWindow(MonsterMultiData monster)
        {
            Monster = monster;
        }

        public void Draw()
        {
            if(_isActive = EditorGUILayout.Foldout(_isActive, "Monster " + Monster.IdMonster.ToString()))
            {
                Indentation.BeginIndentation();

                Monster.IdMonster = EditorGUILayout.IntField("Id monster", Monster.IdMonster);
                Monster.IdPrefabOnScene = EditorGUILayout.IntField("Id prefab on scene", Monster.IdPrefabOnScene);
                Monster.MaxHP = EditorGUILayout.IntField("Max HP", Monster.MaxHP);
                Monster.MinDmg = EditorGUILayout.IntField("Min dmg", Monster.MinDmg);
                Monster.MaxDmg = EditorGUILayout.IntField("Max dmg", Monster.MaxDmg);
                Monster.AttackSpeed = EditorGUILayout.IntField("Attack speed", Monster.AttackSpeed);
                Monster.MovementSpeed = EditorGUILayout.FloatField("Movement speed", Monster.MovementSpeed);

                ShowDrop();

                Indentation.EndIndentation();
            }
        }

        private void ShowDrop()
        {
            int length = EditorGUILayout.IntField("Item drop size", Monster.Drop.Length);

            var drop = new ItemDrop[length];

            for (int i = 0; i < length; i++)
            {
                EditorGUILayout.BeginHorizontal();

                int idItem = 0;
                float chances = 0f;

                if (Monster.Drop.Length > i)
                {
                    idItem = Monster.Drop[i].Item.IdItem;
                    chances = Monster.Drop[i].Chances;
                }

                idItem = EditorGUILayout.IntField("Item", idItem);
                chances = EditorGUILayout.FloatField("Chances", chances);

                var item = new Item(idItem);
                drop[i] = new ItemDrop(item, chances);

                EditorGUILayout.EndHorizontal();
            }

            Monster.Drop = drop;
        }
    }
}
