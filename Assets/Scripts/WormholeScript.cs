using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormholeScript : MonoBehaviour
{
    public WormholeScript exit;

    public bool active;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            PlayerManager.Instance.transform.position = exit.gameObject.transform.position;
            PlayerManager.Instance.rb.velocity = new Vector2();
            StartCoroutine(cooldown());
        }
    }

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
            GetComponent<Animator>().Play("PhaseIn");
        }
        else
        {
            GetComponent<Animator>().Play("PhaseOut");
        }
    }

    IEnumerator cooldown()
    {
        active = false;
        exit.active = false;
        yield return new WaitForSeconds(.5f);
        active = true;
        exit.active = true;
    }
}
