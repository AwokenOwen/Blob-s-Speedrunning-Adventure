using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysMovingBlock : MonoBehaviour
{

    [SerializeField]
    float amount;
    [SerializeField]
    float speed;
    [SerializeField]
    [Range(0f,1f)]float offset;

    Vector3 startingPos;

    float timeOffset;

    float oldX;
    [SerializeField]
    bool PlayerOn;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        oldX = startingPos.x;
        timeOffset = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = startingPos + new Vector3(Mathf.Sin(((Time.time - timeOffset) + (offset * (2 * Mathf.PI))) * speed) * amount, 0f, 0f);
        if (PlayerOn)
        {
            PlayerManager.Instance.transform.position += new Vector3(transform.position.x - oldX, 0f, 0f);
        }
        oldX = transform.position.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerOn = false;
        }
    }
}
