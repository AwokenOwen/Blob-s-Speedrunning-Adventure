using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeScript : MonoBehaviour
{
    [SerializeField]
    Slider volumeSlider;
    [SerializeField]
    TextMeshProUGUI numbers;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameManager.Instance.GetComponent<AudioSource>();
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", audioSource.volume);
        audioSource.volume = volumeSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        numbers.text = volumeSlider.value.ToString("0.00");
    }
}
