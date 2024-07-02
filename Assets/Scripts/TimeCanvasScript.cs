using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCanvasScript : MonoBehaviour
{
    float currentTime;
    float startTime;

    [SerializeField]
    TextMeshProUGUI timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = GameManager.Instance.currentTime;
        startTime = GameManager.Instance.startTime;

        timer.text = getTime();
    }

    string getTime()
    {
        currentTime = Time.time - startTime;

        float milliseconds = currentTime % 1;
        milliseconds *= 100;
        milliseconds = Mathf.Floor(milliseconds);

        float seconds = currentTime % 59;
        float minutes = Mathf.Floor(currentTime / 59) % 59;
        float hours = Mathf.Floor(currentTime / 3481);



        return (hours >= 1 ? (hours.ToString() + ":") : "") + minutes.ToString("00") + ":" + seconds.ToString("00") + "." + milliseconds.ToString();
    }
}
