using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;

[Serializable]
public class 
    Conversation
{
    public int IdConversation { get; set; }

    public string MainDialog
    {
        get { return Languages.GetDialog(IdMainDialog); }
    }

    public int IdMainDialog { get; set; }

    private List<ConversationAnswer> _answers;

    public Conversation(int idConversation, int idMainDialog, params ConversationAnswer[] answers)
    {
        IdConversation = idConversation;

        IdMainDialog = idMainDialog;

        _answers = new List<ConversationAnswer>(answers);
    }

    public void ChooseAnswer(int id)
    {
        _answers.Where(x => x.IsShowAnswer()).ToArray()[id].GiveAnswer();
    }

    public void AnswerToTop(ConversationAnswer answer)
    {
        int index = _answers.IndexOf(answer);

        if (index != 0)
        {
            var answerToBot = _answers[index - 1];
            _answers[index - 1] = answer;
            _answers[index] = answerToBot;
        }
    }

    public string[] GetAnswersTextToShow()
    {
        List<string> answers = new List<string>();

        foreach (int idAnswer in _answers.Where(x => x.IsShowAnswer()).Select(x => x.IdDialog))
        {
            answers.Add(Languages.GetDialog(idAnswer));
        }

        return answers.ToArray();
    }

    public ConversationAnswer[] GetAnswers()
    {
        return _answers.ToArray();
    }

    public void AddAnswer(ConversationAnswer answer)
    {
        _answers.Add(answer);
    }

    public void RemoveAnswer(ConversationAnswer answer)
    {
        _answers.Remove(answer);
    }
}
