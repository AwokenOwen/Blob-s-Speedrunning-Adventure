using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearDropScript : MonoBehaviour
{
    [SerializeField]
    List<GhostBlockScript> blocks = new List<GhostBlockScript>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (GhostBlockScript block in blocks)
            {
                block.Activate();
            }
            Destroy(gameObject);
        }
    }

    [SerializeField]
    float amount;
    [SerializeField]
    float speed;
    [SerializeField]
    [Range(0f, Mathf.PI * 2)] float offset;

    Vector3 startingPos;

    float timeOffset;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        timeOffset = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = startingPos + new Vector3(0f, Mathf.Sin(((Time.time - timeOffset) + (offset * (2 * Mathf.PI))) * speed) * amount, 0f);
    }
}
