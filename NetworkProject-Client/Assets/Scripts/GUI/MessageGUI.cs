using UnityEngine;
using System.Collections;


[System.CLSCompliant(false)]
public class MessageGUI
{
    public string Title { get; set; }

    public DoWindowGUIContent Content { get; set; }

    public MessageGUI(string title, DoWindowGUIContent content)
    {
        Title = title;
        Content = content;
    }

    public MessageGUI(string title, string text)
    {
        Title = title;

        Content = delegate(Rect rect)
        {
            GUI.TextArea(new Rect(5, 10, rect.width - 10, rect.height - 15), text);
            return false;
        };
    }
}
