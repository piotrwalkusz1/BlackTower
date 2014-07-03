using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SaveLoadSystem;
using Standard;

namespace InputsSystem
{
    static public class Inputs
    {
        static public InputsData Main { get; set; }
        static public Dictionary<string, InputButtonDown> Buttons
        {
            get
            {
                return Main.Buttons;
            }
            set
            {
                Main.Buttons = value;
            }
        }
        static public Dictionary<string, InputAxis> Axes
        {
            get
            {
                return Main.Axes;
            }
            set
            {
                Main.Axes = value;
            }
        }

        static Inputs()
        {
            if (GameObject.FindObjectOfType(typeof(Updating)) == null)
            {
                throw new Exception("Brak Updating na mapie.");
            }

            LoadInputs();
        }

        static public bool DownButton(string name)
        {
            return Main.DownButton(name);
        }

        static public bool UpButton(string name)
        {
            return Main.UpButton(name);
        }

        static public bool IsPressedButton(string name)
        {
            return Main.IsPressedButton(name);
        }

        static public float GetAxis(string name)
        {
            return Main.GetAxis(name);
        }

        static public InputButtonDown GetButton(string name)
        {
            return Main.GetButton(name);
        }

        static public bool AddButton(string name, InputButtonDown inputButton)
        {
            return Main.AddButton(name, inputButton);
        }

        static public bool ChangeButton(string name, InputButtonDown inputButton)
        {
            return Main.ChangeButton(name, inputButton);
        }

        static public void AddOrChangeButton(string name, InputButtonDown inputButton)
        {
            Main.AddOrChangeButton(name, inputButton);
        }

        static public void AddOrChangeButtons(string[] names, InputButtonDown[] inputButtons)
        {
            Main.AddOrChangeButtons(names, inputButtons);
        }

        static public bool AddInputAxis(string name, InputAxis axis)
        {
            return Main.AddInputAxis(name, axis);
        }

        static public bool ChangeInputAxis(string name, InputAxis axis)
        {
            return Main.ChangeInputAxis(name, axis);
        }

        static public void AddOrChangeInputAxis(string name, InputAxis axis)
        {
            Main.AddOrChangeInputAxis(name, axis);
        }

        static public void AddOrChangeInputAxes(string[] names, InputAxis[] axes)
        {
            Main.AddOrChangeInputAxes(names, axes);
        }

        static public void RemoveAll()
        {
            Main.RemoveAll();
        }

        static public void RemoveAllButtons()
        {
            Main.RemoveAllButtons();
        }

        static public void RemoveAllAxes()
        {
            Main.RemoveAllAxes();
        }

        static private void LoadInputs()
        {
            TextAsset inputsAsset = Resources.Load(Settings.pathToDefaultInputsInResources) as TextAsset;
            string inputsText = inputsAsset.text;
            InputsToSave inputsToSave = new InputsToSave();
            inputsToSave.SetByLoadedText(inputsText);
            Main = inputsToSave.InputsData;
        }
    }
}



