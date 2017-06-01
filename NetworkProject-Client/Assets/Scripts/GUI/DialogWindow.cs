using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;

public class DialogWindow : GUIObject, IClosable
{
    public GUIText _mainText;
    public int _maxWidth;
    public GUIText[] _answers;

    public Interlocutor Interlocutor { get; set; }
    public Conversation Conversation
    {
        get { return _conversation; }
        set
        {
            _conversation = value;
            Refresh();
        }
    }

    private Conversation _conversation;

    private Vector3 _lastMousePosition;

    void Awake()
    {
        _lastMousePosition = Input.mousePosition;
    }

    void LateUpdate()
    {
        _lastMousePosition = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        Vector3 offset = Input.mousePosition - _lastMousePosition;

        transform.position += new Vector3(offset.x / Screen.width, offset.y / Screen.height);

        _lastMousePosition = Input.mousePosition;
    }

    public DialogWindow(Conversation conversation)
    {
        Conversation = conversation;
    }

    public void ChooseAnswer(int id)
    {
        Conversation.ChooseAnswer(id);
    }

    public void Close()
    {
        GUIController.HideDialogWindow();
    }

    public void Refresh()
    {
        TextUtility.SetMultilineText(_conversation.MainDialog, _mainText, _maxWidth);

        var answers = _conversation.GetAnswersTextToShow();

        int i = 0;

        for (i = 0; i < answers.Length; i++)
        {
            _answers[i].text = answers[i];
        }

        for (; i < _answers.Length; i++)
        {
            _answers[i].text = "";
        }
    }
}
