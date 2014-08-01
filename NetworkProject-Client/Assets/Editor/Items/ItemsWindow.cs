using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Standard;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.Requirements;
using NetworkProject.Benefits;

namespace EditorExtension
{
    public class ItemsWindow : EditorWindow
    {
        public static List<Language> AllLanguages { get; private set; }
        public static List<ItemWindow> Items { get; private set; }
        public static TypeFinder<IEquipRequirement> RequirementsList { get; private set; }
        public static TypeFinder<IEquipBenefit> BenefitsList { get; set; }

        private static List<ItemWindow> _itemsToDelete;

        [MenuItem("Extension/Items")]
        static void ShowWindow()
        {
            ItemsWindow window = EditorWindow.GetWindow(typeof(ItemsWindow), false, "Items") as ItemsWindow;
            window.Start();
        }

        public void Start()
        {
            _itemsToDelete = new List<ItemWindow>();

            RequirementsList = new TypeFinder<IEquipRequirement>();
            BenefitsList = new TypeFinder<IEquipBenefit>();

            AllLanguages = Languages.LoadAllLanguagesFromResourcesDirectly();

            Items = new List<ItemWindow>();

            foreach(var item in EditorSaveLoad.LoadItems())
            {
                ChooseItemDataTypeAndAdd((VisualItemData)item);
            }
        }

        public static void Delete(ItemWindow itemWindow)
        {
            _itemsToDelete.Add(itemWindow);
        }

        private void OnGUI()
        {
            DeleteUselessItemsWindow();

            foreach (var item in Items)
            {
                item.Draw();
            }

            ShowSave();

            ShowAddItemContextMenu();
        }

        private void DeleteUselessItemsWindow()
        {
            _itemsToDelete.ForEach(x => Items.Remove(x));

            _itemsToDelete.Clear();
        }

        private void ShowSave()
        {
            if (GUILayout.Button("Save"))
            {
                Save();
            }
        }

        private void ShowAddItemContextMenu()
        {
            Event evt = Event.current;

            if (evt.type == EventType.ContextClick)
            {
                var menu = new GenericMenu();

                menu.AddItem(new GUIContent("Add item"), false, AddItem);
                menu.AddItem(new GUIContent("Add weapon"), false, AddWeapon);
                menu.AddItem(new GUIContent("Add armor"), false, AddArmor);
                menu.AddItem(new GUIContent("Add shield"), false, AddShield);
                menu.AddItem(new GUIContent("Add helmet"), false, AddHelmet);
                menu.AddItem(new GUIContent("Add shoes"), false, AddShoes);
                menu.AddItem(new GUIContent("Add addition"), false, AddAddition);

                menu.ShowAsContext();

                evt.Use();
            }
        }

        private void Save()
        {
            var items = new List<VisualItemData>();

            Items.ForEach(x => items.Add((VisualItemData)x.ItemData));

            EditorSaveLoad.SaveItems(items);
        }

        private void ChooseItemDataTypeAndAdd(VisualItemData item)
        {
            if(item is VisualEquipableItemData)
            {
                VisualEquipableItemData equipableItem = (VisualEquipableItemData)item;

                if (equipableItem.EquipableItemData is WeaponData)
                    Items.Add(new ItemWindow(new ItemWindowWeaponData(equipableItem)));
                else if (equipableItem.EquipableItemData is ArmorData)
                    Items.Add(new ItemWindow(new ItemWindowArmorData(equipableItem)));
                else if (equipableItem.EquipableItemData is ShieldData)
                    Items.Add(new ItemWindow(new ItemWindowShieldData(equipableItem)));
                else if (equipableItem.EquipableItemData is HelmetData)
                    Items.Add(new ItemWindow(new ItemWindowHelmetData(equipableItem)));
                else if (equipableItem.EquipableItemData is ShoesData)
                    Items.Add(new ItemWindow(new ItemWindowShoesData(equipableItem)));
                else if (equipableItem.EquipableItemData is AdditionData)
                    Items.Add(new ItemWindow(new ItemWindowAdditionData(equipableItem)));
                else
                    throw new Exception("Nie ma takiego itemu do założenia");
            }
            else
            {
                Items.Add(new ItemWindow(new ItemWindowData(item)));
            }                
        }

        private void AddItem()
        {
            var item = new ItemData();
            var itemWindow = new ItemWindowData(item);
            Items.Add(new ItemWindow(itemWindow));
        }

        private void AddWeapon()
        {
            var item = new WeaponData(0);
            var itemWindow = new ItemWindowWeaponData(item);
            Items.Add(new ItemWindow(itemWindow));
        }

        private void AddArmor()
        {
            var item = new ArmorData(0);
            var itemWindow = new ItemWindowArmorData(item);
            Items.Add(new ItemWindow(itemWindow));
        }

        private void AddShield()
        {
            var item = new ShieldData(0);
            var itemWindow = new ItemWindowShieldData(item);
            Items.Add(new ItemWindow(itemWindow));
        }

        private void AddHelmet()
        {
            var item = new HelmetData(0);
            var itemWindow = new ItemWindowHelmetData(item);
            Items.Add(new ItemWindow(itemWindow));
        }

        private void AddShoes()
        {
            var item = new ShoesData(0);
            var itemWindow = new ItemWindowShoesData(item);
            Items.Add(new ItemWindow(itemWindow));
        }

        private void AddAddition()
        {
            var item = new AdditionData(0);
            var itemWindow = new ItemWindowAdditionData(item);
            Items.Add(new ItemWindow(itemWindow));
        }
    } 
}
