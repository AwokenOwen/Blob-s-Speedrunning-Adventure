using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinningManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMeshProUGUI tagline;

    [SerializeField]
    TextMeshProUGUI timeText;

    void Start()
    {
        float time = GameManager.Instance.currentTime;

        timeText.text = "Run Time: " + GameManager.Instance.getTime(time);

        if (time < PlayerPrefs.GetFloat("bestTime") || PlayerPrefs.GetFloat("bestTime") <= 0f)
        {
            tagline.text = "NEW RECORD!";
            PlayerPrefs.SetFloat("bestTime", time);
        }
        else
        {
            tagline.text = "Better Luck Next Time";
        }

        PlayerPrefs.SetFloat("previousTime", time);
    }
}
