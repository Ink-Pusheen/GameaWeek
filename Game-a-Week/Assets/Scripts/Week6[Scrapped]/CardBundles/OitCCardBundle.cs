using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestionCard
{
    public string question;

    //[Serializable]
    public enum WeakTo
    {
        Logical,
        Witty,
        Humorous,
        Stupid
    }

    public WeakTo weakness;
}

[Serializable]
public class AnswerCard
{
    public string answer;

    //[Serializable]
    public enum CardType
    {
        Logical,
        Witty,
        Humorous,
        Stupid
    }
    
    public CardType cardType;
}

[CreateAssetMenu(fileName = "Card Bundle", menuName = "OitC/Card Bundle")]
public class OitCCardBundle : ScriptableObject
{
    public List<QuestionCard> DefenseQuestionLines; //List that houses all possible questions that can be asked to the defense

    public List<QuestionCard> ProsecutorQuestionLines; //List that houses all possible Prosecutor returnal questions

    public List<AnswerCard> DefenseCards; //List of cards the defense will have access to

    public List<AnswerCard> ProsecutorCards; //List of cards the prosecutor will have access to
}
