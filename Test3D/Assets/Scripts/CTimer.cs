using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTimer : MonoBehaviour
{



    private float timeToCnt = 180.0f;


    int minute;
    int second;
    public Text timeText;

    Slider slider;

    // Use this for initialization
    void Start()
    {
        timeText = GetComponentInChildren<Text>();
        slider = GetComponent<Slider>();

        slider.maxValue = timeToCnt;
        slider.value = timeToCnt;


        StartCoroutine("Timer", timeToCnt);


    }



    // 타이머
    IEnumerator Timer(float t)
    {
        while (t >= 0)
        {

            minute = (int)t / 60;
            second = (int)t % 60;
            timeText.text = string.Format("{0:D2}", minute) + ":" + string.Format("{0:D2}", second);

            slider.value -= 1.0f;
            t -= 1.0f;

            yield return new WaitForSeconds(1.0f);
        }

        yield return StartCoroutine("ScoreBoardLoad");

    }

    IEnumerator ScoreBoardLoad()
    {
        yield return null;
    }
    //public void PrintDebug()
    //{
    //    Debug.Log(minute + ":" + second);
    //}
}
