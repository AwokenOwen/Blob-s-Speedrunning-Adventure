using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float currentTime;
    public float startTime;
    public static GameManager Instance { get; private set; }
    public bool phaseMode {  get; private set; }
    public delegate void OnPhaseAction(bool phase);
    public static event OnPhaseAction OnPhaseEvent;

    public bool alive = true;

    [SerializeField]
    string currentLevel;

    public AudioClip[] music;
    AudioSource musicSource;
    int clip;

    [SerializeField]
    GameObject PauseMenu, PauseMenuCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(PauseMenuCanvas);
    }

    public void OnPhase()
    {
        phaseMode = !phaseMode;
        OnPhaseEvent(phaseMode);

        PlayerManager.Instance.gameObject.GetComponent<BoxCollider2D>().enabled = phaseMode;

        if (phaseMode && PlayerManager.Instance.checkIfInside())
        {
            Respawn();
            Debug.Log("Inside");
        }
            
    }

    private void Start()
    {
        phaseMode = true;
        startTime = Time.time;

        musicSource = GetComponent<AudioSource>();
        musicSource.clip = music[0];
        clip = 0;
        musicSource.Play();
    }

    private void Update()
    {
        if (PlayerManager.Instance != null && PlayerManager.Instance.gameObject.transform.position.y < -15f && alive)
        {
            Respawn();
        }

        currentTime = Time.time - startTime;

        if (!musicSource.isPlaying)
        {
            if (clip == 3)
            {
                clip = 0;
            }
            else
            {
                clip++;
            }
            musicSource.clip = music[clip];
            musicSource.Play();
        }
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

    public void ChangeLevel(string newLevel)
    {
        currentLevel = newLevel;
        SceneManager.LoadScene(newLevel);
        phaseMode = true;
    }

    public void Respawn()
    {
        alive = false;
        StartCoroutine(PlayRespawn());
    }

    IEnumerator PlayRespawn()
    {
        PlayerManager.Instance.GetComponent<ParticleSystem>().Play();
        PlayerManager.Instance.GetComponent<Rigidbody2D>().gravityScale = 0;
        PlayerManager.Instance.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        PlayerManager.Instance.GetComponent<BoxCollider2D>().enabled = false;
        PlayerManager.Instance.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(currentLevel);
        phaseMode = true;
        alive = true;
    }

    public void PauseMenuActive()
    {
        if (currentLevel != "MainMenu" || currentLevel != "WinningScene")
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
        }
    }

    public void ResetRun()
    {
        SceneManager.LoadScene("Level 1");
        startTime = Time.time;
        PauseMenu.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Destroy(gameObject);
        PauseMenu.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
