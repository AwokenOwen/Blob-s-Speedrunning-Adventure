using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI FastestTimeText;

    [SerializeField]
    TextMeshProUGUI PreviousTimeText;

    // Start is called before the first frame update
    void Start()
    {
        float FastestTime = PlayerPrefs.GetFloat("bestTime", -1);
        float PreviousTime = PlayerPrefs.GetFloat("previousTime", -1);

        FastestTimeText.text = "Fastest Clear Time: " + (FastestTime <= 0 ? "Unavailable" : getTime(FastestTime));
        PreviousTimeText.text = "Last Time: " + (PreviousTime <= 0 ? "Unavailable" : getTime(PreviousTime));
    }

    public void StartGame()
    {
        GameManager.Instance.startTime = Time.time;
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public string getTime(float time)
    {
        float milliseconds = time % 1;
        milliseconds *= 100;
        milliseconds = Mathf.Floor(milliseconds);

        float seconds = time % 59;
        float minutes = Mathf.Floor(time / 59) % 59;
        float hours = Mathf.Floor(time / 3481);



        return (hours >= 1 ? (hours.ToString() + ":") : "") + minutes.ToString("00") + ":" + seconds.ToString("00") + "." + milliseconds.ToString();
    }
}
