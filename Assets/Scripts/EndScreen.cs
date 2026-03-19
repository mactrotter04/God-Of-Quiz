using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        scoreKeeper = FindAnyObjectByType <ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "congratulations! \nScore: " + 
            scoreKeeper.CalculateSCore() + "%";
    }
}
