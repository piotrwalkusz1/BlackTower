using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;
using NetworkProject.Quests;

public class QuestManagerWindow : GUIObject, IClosable
{
    public GUIText _numberQuest;
    public GUIText _nameQuest;
    public GUIText _descriptionQuest;
    public int _descriptionWidth;
    public GUITexture[] _rewards;

    private int _currentQuest;
    private List<Quest> Quests
    {
        get { return new List<Quest>(QuestRepository.GetCurrentQuests()); }
    }

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

    public void NextQuest()
    {
        if (_currentQuest < Quests.Count - 1)
        {
            _currentQuest++;

            Refresh();
        }
    }

    public void LastQuest()
    {
        if (_currentQuest > 0)
        {
            _currentQuest--;

            Refresh();
        }
    }

    public void Refresh()
    {
        if (_currentQuest >= Quests.Count)
        {
            _currentQuest = 0;
        }

        if (Quests.Count == 0)
        {
            _numberQuest.text = "0/0";
            _nameQuest.text = "";
            _descriptionQuest.text = "";
        }
        else
        {
            _numberQuest.text = (_currentQuest + 1).ToString() + "/" + Quests.Count.ToString();

            _nameQuest.text = Quests[_currentQuest].Name;

            TextUtility.SetMultilineText(GetDescription(), _descriptionQuest, _descriptionWidth);

            var rewards = Quests[_currentQuest].GetRewards();

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

    private string GetDescription()
    {
        var result = Quests[_currentQuest].Description;

        result += "\n\n";

        result += Quests[_currentQuest].GetTargetsDescription();

        return result;
    }

    public void Close()
    {
        GUIController.HideQuestManager();
    }
}
