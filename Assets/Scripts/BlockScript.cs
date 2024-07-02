using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
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
            GetComponent<Animator>().Play("PhaseIn");
        }
        else
        {
            GetComponent<Animator>().Play("PhaseOut");
        }
    }
}
