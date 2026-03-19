using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    int score = 0;
    int currentStreak = 0;
    [SerializeField] int scoreToAdd = 100;
    [SerializeField] int maxStreakBonus = 300;
     
    public int GetCorrectAwnsers()
    {
        return correctAnswers;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementCorrectAwnsers()
    {
        correctAnswers++;
        currentStreak++;
        int points = Mathf.Min(currentStreak * scoreToAdd, maxStreakBonus);
        score += points;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public int CalculateSCore()
    {
        return score;
    }

    public void ResetStreak()
    {
        currentStreak = 0;
    }
}
