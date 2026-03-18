using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionsSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "Enter New Question";
    [SerializeField] string[] answer = new string[4];
    [SerializeField] int correctAwnserIndex;

    public string getQuestion()
    {
        return question;
    }

    public int GetCorrectAwnserIndex()
    {
        return correctAwnserIndex;
    }

    public string GetAnswer(int index)
    {
        return answer[index];
    }

}
