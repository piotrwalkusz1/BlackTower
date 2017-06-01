using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class QuestManagerWindowLast : MonoBehaviour
{
    public QuestManagerWindow _window;

    void OnMouseDown()
    {
        _window.LastQuest();
    }
}
