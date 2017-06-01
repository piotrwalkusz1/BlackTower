using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class QuestManagerWindowNext : MonoBehaviour
{
    public QuestManagerWindow _window;

    void OnMouseDown()
    {
        _window.NextQuest();
    }
}
