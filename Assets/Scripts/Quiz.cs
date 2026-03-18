using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionsSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;

    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    int correctAnswerIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      GetNextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAwnserSelected(int index)
    {

        Image buttonImage;

        if(index  == question.GetCorrectAwnserIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            correctAnswerIndex = question.GetCorrectAwnserIndex();
            string correctAwser = question.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry The Correct Awnser was: \n" + correctAwser;
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

        SetButtonState(false);
    }

    void DisplayQuestion()
    {
        questionText.text = question.getQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = question.GetAnswer(i);
        }
    }


    void SetButtonState(bool state)
    {
        questionText.text = question.getQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefultButtonSprite();
        DisplayQuestion();
    }

    void SetDefultButtonSprite()
    {
        
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

}
