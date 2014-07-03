using UnityEngine;
using System.Collections;
using InputsSystem;

namespace SaveLoadSystem
{
    static public partial class SaveLoadExtension
    {
        static public InputsToSave CreateInputsToSave(this InputsData inputsData)
        {
            return new InputsToSave(inputsData);
        }
    }
}

