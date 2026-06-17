using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Bundle", menuName = "OitC/Card Bundle")]
public class OitCCardBundle : ScriptableObject
{
    public List<string> DefenseQuestionLines; //List that houses all possible questions that can be asked to the defense

    public List<string> ProsecutorQuestionLines; //List that houses all possible Prosecutor returnal questions

    public List<string> DefenseCards; //List of cards the defense will have access to

    public List<string> ProsecutorCards; //List of cards the prosecutor will have access to
}
