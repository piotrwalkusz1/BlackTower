using UnityEngine;
using System.Collections;

namespace Standard
{
    static public class Settings
    {
        public static readonly string pathToInputsSettings = Application.dataPath + "/Settings/inputs.txt";
        public static readonly string pathToDefaultInputsInResources = "defaultInputs";
        public static readonly string pathToDefaultInputs = Application.dataPath + "/Resources/defaultInputs.txt";
        public static readonly string pathToLanguagesInResources = "";
        public static readonly string pathToLanguages = Application.dataPath + "/Languages/";
        public const float jumpAndFallSpeed = 2f;
        public const float playerHalfHeight = 0.9f;
        public const float playerDistanceToGround = 0.1f;
    }
}


