using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using UnityEngine;

public class TextFromLanguage : MonoBehaviour
{
    public string _phrase;

    void Awake()
    {
        guiText.text = Languages.GetPhrase(_phrase);
    }
}
