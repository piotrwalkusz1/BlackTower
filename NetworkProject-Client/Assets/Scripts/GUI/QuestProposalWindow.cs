using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;
using NetworkProject.Quests;
using NetworkProject.Connection.ToServer;

public class QuestProposalWindow : GUIObject, IClosable
{
    public GUIText _questName;
    public GUIText _questDescription;
    public int _maxDescriptionWidth;
    public GUITexture[] _rewards;

    public Quest Quest
    {
        get { return _quest; }
        set
        {
            _quest = value;
            Refresh();          
        }
    }
    public int NetIdQuester { get; set; }

    protected Quest _quest;
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

    public void Accept()
    {
        var request = new TakeQuestToServer(Quest.IdQuest, NetIdQuester);

        Client.SendRequestAsMessage(request);

        GUIController.HideQuestProposalWindow();
    }

    public void Deny()
    {
        GUIController.HideQuestProposalWindow();
    }

    public void Close()
    {
        GUIController.HideQuestProposalWindow();
    }

    protected void Refresh()
    {
        _questName.text = _quest.Name;

        TextUtility.SetMultilineText(_quest.Description, _questDescription, _maxDescriptionWidth);

        var rewards = Quest.GetRewards();

        int i = 0;

        for (; i < rewards.Length; i++)
        {
            _rewards[i].texture = ImageRepository.GetImageByIdImage(rewards[i].GetIdImage());
        }

        for (; i < _rewards.Length; i++)
        {
            _rewards[i].texture = null;
        }
    }
}
