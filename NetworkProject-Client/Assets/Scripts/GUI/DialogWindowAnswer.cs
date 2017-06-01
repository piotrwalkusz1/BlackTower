using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DialogWindowAnswer : MonoBehaviour
{
    public DialogWindow _window;
    public int _idAnswer;

    void OnMouseDown()
    {
        _window.ChooseAnswer(_idAnswer);
    }
}
