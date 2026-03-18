using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleateQuestion = 30f;
    [SerializeField] float timeToShowCorrectAwnser = 10f;

    public bool loadNextQuestion;
    float timerValue;

    public bool isAwnseringQuestion = false;
    public float fillFraction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        // timerValue = timerValue - Time.deltaTime; (for a timer)
        if (isAwnseringQuestion)
        {
            if (timerValue > 0)
            {
                // change fraction value
                fillFraction = timerValue / timeToCompleateQuestion;
            }
            else
            {
                timerValue = timeToShowCorrectAwnser;
                isAwnseringQuestion = false;
            }
        }
        else
        {
            if(timerValue > 0)
            {
                // change fraction value
                fillFraction = timerValue / timeToShowCorrectAwnser;
            }

            else
            {
                isAwnseringQuestion = true;
                timerValue = timeToCompleateQuestion;
                loadNextQuestion = true;
            }
                        

        }


        Debug.Log("is awnsering question state:" + isAwnseringQuestion + "\nTimer Value: " + timerValue +"\nFill fraction: " + fillFraction);
    }

    public void CancleTimer()
    {
        timerValue = (0);
    }

}
