using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

    [HideInInspector] public bool isComplete;
    int correctAnswerIndex;
    Timer timer;
    bool hasAwnseredEarly = true;
    QuestionsSO currentQuestion;
    ScoreKeeper scoreKeeper;
    int hintsRemaning;

    [Header("hints")]
    [SerializeField] GameObject hintButton;
    [SerializeField] int hintPerQuiz = 3;
    bool hintsUsedPerQuestion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        scoreText.text = "0pts";
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        timer = FindFirstObjectByType<Timer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        hintsRemaning = hintPerQuiz;

    }

    // Update is called once per frame
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }


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
        scoreText.text = scoreKeeper.CalculateSCore() + "pts";
    }

    public void OnHintsSelected()
    {
        if (hintsRemaning <= 0 || hintsUsedPerQuestion) return;

        hintsUsedPerQuestion = true;

        int correctIndex = currentQuestion.GetCorrectAwnserIndex();

        List<int> incorrectIndecies = new List<int>();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i != correctIndex && answerButtons[i].activeSelf)
            {
                incorrectIndecies.Add(i);
            }
        }

        for (int i = incorrectIndecies.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            int temp = incorrectIndecies[i];
            incorrectIndecies[i] = incorrectIndecies[rand];
            incorrectIndecies[rand] = temp;
        }

        int toHide = Mathf.Min(2, incorrectIndecies.Count);
        for (int i = 0; i < toHide; i++)
        {
            answerButtons[incorrectIndecies[i]].SetActive(false);
        }

        hintsRemaning--;

        if (hintsRemaning <= 0)
        {
            hintButton.GetComponent<Button>().interactable = false;
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
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].SetActive(true);
            }
            hintsUsedPerQuestion = false;

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

    void DisplayAwnser(int index)
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
            scoreKeeper.ResetStreak();

        }
    }

    void getRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);

        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }



}
