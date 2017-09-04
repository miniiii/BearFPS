using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingTimer : MonoBehaviour {

    UILabel _timerLabel;

    int minute;
    int second;
    float time = 0;
    // Use this for initialization
    void Start () {
        _timerLabel = GetComponent<UILabel>();
        StartCoroutine("UITimer");
    }

    IEnumerator UITimer()
    {
        while (gameObject.activeInHierarchy == true)
        {

            minute = (int)time / 60;
            second = (int)time % 60;
            _timerLabel.text = string.Format("{0:D2}", minute) + ":" + string.Format("{0:D2}", second);

           
            time += 1.0f;

            yield return new WaitForSeconds(1.0f);
        }
        
    }
}
