using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] List<QuestionsSO> questions = new List<QuestionsSO>();
    [SerializeField] TextMeshProUGUI questionText;

    [Header("Awnsers")]
    [SerializeField] GameObject[] answerButtons;

    [Header("Button Sprites")]
    [Tooltip("Assign Blue Square Hear")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("timer")]
    [SerializeField] Image timerImage;

    [Header("score")]
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("progress bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;
    int correctAnswerIndex;
    Timer timer;
    public bool hasAwnseredEarly;
    QuestionsSO currentQuestion;
    ScoreKeeper scoreKeeper;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "0%";
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        timer = FindFirstObjectByType<Timer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;

        if(timer.loadNextQuestion)
        {
            hasAwnseredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAwnseredEarly && !timer.isAwnseringQuestion)
        {
            DisplayAwnser(-1);
            SetButtonState(false);
        }
    }

    public void OnAwnserSelected(int index)
    {
        hasAwnseredEarly = true;
        DisplayAwnser(index);

        SetButtonState(false);
        timer.CancleTimer();
        scoreText.text = scoreKeeper.CalculateSCore() + "%";

        if(progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.getQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }


    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefultButtonSprite();
            getRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }


    void SetDefultButtonSprite()
    {
        
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void DisplayAwnser (int index)
    {
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAwnserIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAwnsers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAwnserIndex();
            string correctAwser = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry The Correct Awnser was: \n" + correctAwser;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void getRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);

        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }
    

}
