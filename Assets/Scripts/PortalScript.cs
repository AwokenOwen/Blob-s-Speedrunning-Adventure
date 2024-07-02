using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField]
    string SendToLevel;
    private void OnEnable()
    {
        GameManager.OnPhaseEvent += OnPhase;
    }

    private void OnDisable()
    {
        GameManager.OnPhaseEvent -= OnPhase;
    }

    void OnPhase(bool phase)
    {
        if (phase)
        {
            GetComponent<ParticleSystem>().Play();
            GetComponent<Animator>().Play("PhaseIn");
        }
        else
        {
            GetComponent<ParticleSystem>().Clear();
            GetComponent<ParticleSystem>().Pause();
            GetComponent<Animator>().Play("PhaseOut");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.ChangeLevel(SendToLevel);
    }
}
