using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlockScript : MonoBehaviour
{
    [SerializeField]
    GameObject normalBlock;

    public void Activate()
    {
        normalBlock.SetActive(true);
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        GameManager.OnPhaseEvent += OnPhaseChange;
    }

    private void OnDisable()
    {
        GameManager.OnPhaseEvent -= OnPhaseChange;
    }

    void OnPhaseChange(bool phase)
    {
        if (phase)
        {
            GetComponent<Animator>().Play("Ghost_PhaseIn");
        }
        else
        {
            GetComponent<Animator>().Play("Ghost_PhaseOut");
        }
    }
}
